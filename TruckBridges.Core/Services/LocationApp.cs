// Created by Tim Heinz - n8683981

namespace TruckBridges.Core.Services
{
    public static class LocationApp
    {
        // Google Maps

        public static string GoogleMapsApiKey = "AIzaSyB6CwV9qBJ5iNdjJA4l6sWEInL3BOAXOLY";


        // Google Places

        public static string GooglePlacesEndpoint =
            "https://maps.googleapis.com/maps/api/place/nearbysearch/json";


        // Google Roads

        public static string GoogleRoadsEndpoint =
            "https://roads.googleapis.com/v1/snapToRoads";


        // HERE API

        public static string HereAppID = "lvQ02cV1jdlpdZm9bnKY";
        public static string HereAppCode = "rLsMcAzJlZujKRp-f5VNMg";
        public static string HereEndpoint =
            "http://route.cit.api.here.com/routing/7.2/calculateroute.json";
    }
}