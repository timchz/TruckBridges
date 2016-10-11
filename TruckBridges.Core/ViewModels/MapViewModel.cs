// Created by Tim Heinz - n8683981

using System;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;
using TruckBridges.Core.Services;

namespace TruckBridges.Core.ViewModels
{
    public class MapViewModel : MvxViewModel
    {
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



        // NEW
        private GeoLocation myLocation;
        public GeoLocation MyLocation
        {
            get { return myLocation; }
            set { myLocation = value; }
        }

        // NEW
        public void OnMyLocationChanged(GeoLocation location)
        {
            MyLocation = location;
        }

        public void MapTapped(GeoLocation location)
        {
            //GetWeatherInfo(location);
        }

/*
        private async Task GetWeatherInfo(GeoLocation location)
        {
            var weatherService = new WeatherService();
            var city = await geocoder.GetCityFromLocation(location);
            var locationKey = await weatherService.GetLocations(city);
            var bestLocation = locationKey.FirstOrDefault();
            var forecast = await weatherService.GetForecast(bestLocation.Key);
            location.Locality = city;
            weatherPinFound(location, forecast);
        }
*/
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

        // NEW
        private Action<GeoLocation, float> moveToLocation;
        private Action<GeoLocation> stopPinFound;
        public void OnMapSetup(Action<GeoLocation, float> MoveToLocation,
                               Action<GeoLocation> StopPinFound)
        {
            moveToLocation = MoveToLocation;
            stopPinFound = StopPinFound;

            // add selected destination pin
            AddStopPin(City);
        }

        // NEW
        private IGeoCoder geocoder;
        public System.Windows.Input.ICommand ButtonFinish { get; private set; }
        public System.Windows.Input.ICommand ButtonMenu { get; private set; }

        public MapViewModel(IGeoCoder geocoder)
        {
            this.geocoder = geocoder;

            ButtonFinish = new MvxCommand(() =>
            {
                ShowViewModel<ScanViewModel>();
            });

            ButtonMenu = new MvxCommand(() =>
            {
            });
        }

    }
}
