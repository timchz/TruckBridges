// Created by Tim Heinz - n8683981

namespace TruckBridges.Core.Services
{
    public static class LocationApp
    {
        // AccuWeather
        /*
        public static string ApiKey = "Aooa3DdZXWk4Lnlm0L3IBMGKEfJZz7Cp";
        public static string AutoCompleteEndpoint =
            "http://dataservice.accuweather.com/locations/v1/cities/autocomplete";
        */


        // Google Places

        public static string ApiKey = "AIzaSyB6CwV9qBJ5iNdjJA4l6sWEInL3BOAXOLY";
        public static string AutoCompleteEndpoint =
            "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
    }
}