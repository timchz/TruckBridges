// Created by Tim Heinz - n8683981

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;
using TruckBridges.Core.Services;

namespace TruckBridges.Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        //--------------------------------------------------------------------
        // GLOBAL VARIABLES
        //--------------------------------------------------------------------

        LocationService locationService = new LocationService();


        //--------------------------------------------------------------------
        // BRIDGE DATA
        //--------------------------------------------------------------------

        private List<GeoArea> bridgeLocations = new List<GeoArea>();
        public List<GeoArea> BridgeLocations
        {
            get { return bridgeLocations; }
            set { SetProperty(ref bridgeLocations, value); }
        }


        //--------------------------------------------------------------------
        // SEARCH BOX
        //--------------------------------------------------------------------

        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                SetProperty(ref searchTerm, value);
            }
        }

        private ObservableCollection<LocationAutoCompleteResult> locations
            = new ObservableCollection<LocationAutoCompleteResult>();
        public ObservableCollection<LocationAutoCompleteResult> Locations
        {
            get { return locations; }
            set { SetProperty(ref locations, value); }
        }

        public async void SearchLocations(GeoLocation centerLocation, string searchTerm)
        {
            if (centerLocation == null)
                return;

            Locations.Clear();
            var locationResults = await locationService.GetLocations(centerLocation, searchTerm);
            foreach (var item in locationResults)
                Locations.Add(item);
        }


        //--------------------------------------------------------------------
        // MAP FRAGMENT
        //--------------------------------------------------------------------

        private GeoLocation myLocation = null;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }

        private async void MoveToAddress(string address)
        {
            if (address == "")
                return;

            var location = await geocoder.GetLocationFromAddress(address);
            location.Locality = address;
            moveToLocation(location, 18);
        }


        private GeoLocation destinationLocation = null;
        public GeoLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }

        private async void SetNewDestinationAddress(string address)
        {
            if (address == "")
                return;

            var location = await geocoder.GetLocationFromAddress(address);
            location.Locality = address;

            DestinationLocation = location;

            Locations.Clear();

            ShowRoute();
        }

        private async void ShowRoute()
        {
            List<Route> routes = await locationService.GetRoutes(MyLocation, DestinationLocation, vehicleDetails);

            if (routes.Count == 0)
                return;

            CombinedRoute route = new CombinedRoute(routes.First());

            foreach (var leg in route.leg)
            {
                List<Maneuver> maneuvers = route.InterpolateManeuvers(leg.maneuver);

                List<SnappedPoints> points = await locationService.SnapToRoads(maneuvers);

                if (points != null)
                    route.snappedSpan.Add(new SnappedSpan(points));
            }
            
            setNewRoute(route);
        }


        public void ReceiveBridgeData(string bridgeJSON)
        {
            List<BridgeDetails> bridgeDetailsList = locationService.GetBridgeLocations(bridgeJSON);

            if (bridgeDetailsList != null && vehicleDetails != null)
            {
                foreach (var bridgeDetails in bridgeDetailsList)
                {
                    if (bridgeDetails.SignedClearance <= vehicleDetails.Clearance)
                    {
                        var area = new GeoArea(new GeoLocation(bridgeDetails.Latitude, bridgeDetails.Longitude),
                                               new GeoLocation(bridgeDetails.Latitude, bridgeDetails.Longitude));
                        area.Locality = bridgeDetails.Description;
                        BridgeLocations.Add(area);
                    }
                }
            }

            // set bridge locations on map
            setBridgeLocations(BridgeLocations);
        }


        private Action<GeoLocation, float> moveToLocation;
        private Action<List<GeoArea>> setBridgeLocations;
        private Action<CombinedRoute> setNewRoute;
        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation,
                               Action<List<GeoArea>> SetBridgeLocations,
                               Action<CombinedRoute> SetNewRoute)
        {
            moveToLocation = MoveToLocation;
            setBridgeLocations = SetBridgeLocations;
            setNewRoute = SetNewRoute;
        }


        public VehicleDetails vehicleDetails { get; set; }
        public void Init(VehicleDetails details)
        {
            vehicleDetails = details;
        }


        private IGeoCoder geocoder;
        public ICommand SelectLocationCommand { get; private set; }
        public ICommand ButtonSearch { get; private set; }
        public ICommand ButtonMenu { get; private set; }

        public MapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;

            SelectLocationCommand = new MvxCommand<LocationAutoCompleteResult>(selectedLocation =>
            {
                SetNewDestinationAddress(selectedLocation.LocalizedName);
            });

            ButtonSearch = new MvxCommand(() =>
            {
                if (SearchTerm.Length > 3 && MyLocation != null)
                    SearchLocations(MyLocation, SearchTerm);
            });

            ButtonMenu = new MvxCommand(() =>
            {
                ShowViewModel<VehicleDetailsViewModel>(vehicleDetails);
            });
        }

    }
}
