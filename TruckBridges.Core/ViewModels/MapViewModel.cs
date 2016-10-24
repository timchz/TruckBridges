// Created by Tim Heinz - n8683981

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;
using TruckBridges.Core.Services;

namespace TruckBridges.Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                SetProperty(ref searchTerm, value);
            }
        }

        private ObservableCollection<LocationAutoCompleteResult> locations;
        public ObservableCollection<LocationAutoCompleteResult> Locations
        {
            get { return locations; }
            set { SetProperty(ref locations, value); }
        }

        public async void SearchLocations(GeoLocation centerLocation, string searchTerm)
        {
            LocationService locationService = new LocationService();
            Locations.Clear();
            var locationResults = await locationService.GetLocations(centerLocation, searchTerm);
            //var bestLocationResults = locationResults.Where(location => location.Rank > 80);
            //foreach (var item in bestLocationResults)
            foreach (var item in locationResults)
            {
                Locations.Add(item);
            }
        }


/*
        LocationAutoCompleteResult selectedLocation;
        public void Init(LocationAutoCompleteResult parameters)
        {
            selectedLocation = parameters;

        }


        private string city;
        public string City
        {
            get { return city; }
            set { SetProperty(ref city, value); }
        }
        public override void Start()
        {
            base.Start();
            City = selectedLocation.LocalizedName;
        }
*/


        private GeoLocation myLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }

        public void MapTapped(GeoLocation location)
        {
            //GetWeatherInfo(location);
        }

        private async void MoveToAddress(string address)
        {
            if (address == "")
                return;

            var location = await geocoder.GetLocationFromAddress(address);
            location.Locality = address;
            moveToLocation(location, 18);
        }

        private async void AddStopPin(string address)
        {
            if (address == "")
                return;

            var location = await geocoder.GetLocationFromAddress(address);
            location.Locality = address;
            stopPinFound(location);
        }

        private Action<GeoLocation, float> moveToLocation;
        private Action<GeoLocation> stopPinFound;
        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation,
                               Action<GeoLocation> StopPinFound)
        {
            moveToLocation = MoveToLocation;
            stopPinFound = StopPinFound;

            // add selected destination pin
            //AddStopPin(City);
        }


        private IGeoCoder geocoder;
        public ICommand SelectLocationCommand { get; private set; }
        public ICommand ButtonSearch { get; private set; }
        public ICommand ButtonMenu { get; private set; }

        public MapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;
            locations = new ObservableCollection<LocationAutoCompleteResult>();

            SelectLocationCommand = new
                MvxCommand<LocationAutoCompleteResult>(selectedLocation =>
            {
                AddStopPin(selectedLocation.LocalizedName);
            });

            ButtonSearch = new MvxCommand(() =>
            {
                if (SearchTerm.Length > 3 && MyLocation != null)
                {
                    SearchLocations(MyLocation, SearchTerm);
                }
            });

            ButtonMenu = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });
        }

    }
}
