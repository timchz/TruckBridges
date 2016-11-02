// Created by Tim Heinz - n8683981

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TruckBridges.Core.Models;

namespace TruckBridges.Core.Services
{
    public class LocationService
    {
        public List<BridgeDetails> GetBridgeLocations(string jsonContent)
        {
            var details = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BridgeDetails>>(jsonContent);
            return details;
        }

        public async Task<List<Route>> GetRoutes(GeoLocation startPoint, GeoLocation endPoint, VehicleDetails vehicleDetails)
        {
            if (startPoint == null || endPoint == null)
                return null;

            string requestString = String.Format(
                "{0}?app_id={1}&app_code={2}&waypoint0=geo!{3}&waypoint1=geo!{4}&mode=fastest;truck&height={5}&limitedWeight={6}&trailersCount={7}"
                , LocationApp.HereEndpoint
                , LocationApp.HereAppID
                , LocationApp.HereAppCode
                , startPoint.LatLongTextNoSpaces()
                , endPoint.LatLongTextNoSpaces()
                , vehicleDetails.Clearance
                , vehicleDetails.Weight
                , vehicleDetails.Trailers
            );

            if (vehicleDetails.HazardousMaterialClass != "")
            {
                string hazardousString = String.Format(
                    "&shippedHazardousGoods={0}"
                    , vehicleDetails.HazardousMaterialClass
                );
                requestString += hazardousString;
            }

            WebRequest request = WebRequest.CreateHttp(requestString);

            string responseValue = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            var sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<HereAPIResult>(responseValue);

            return sresponse.response.route;
        }

        public async Task<List<SnappedPoints>> SnapToRoads(List<Maneuver> positions)
        {
            if (positions == null)
                return null;

            string points = "";

            foreach (var position in positions)
            {
                var location = new GeoLocation(position.position.latitude, position.position.longitude);
                points += location.LatLongTextNoSpaces() + "|";
            }
            points = points.TrimEnd('|');

            WebRequest request = WebRequest.CreateHttp(
                String.Format("{0}?key={1}&path={2}&interpolate=true"
                    , LocationApp.GoogleRoadsEndpoint
                    , LocationApp.GoogleMapsApiKey
                    , points
                )
            );

            string responseValue = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            var sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleRoadsAPIResult>(responseValue);

            return sresponse.snappedPoints;
        }

        public async Task<List<LocationAutoCompleteResult>> GetLocations(GeoLocation location, string searchTerm)
        {
            if (location == null)
                return null;

            string locationText = location.LatLongText();

            WebRequest request = WebRequest.CreateHttp(
                String.Format("{0}?key={1}&location={2}&keyword={3}&rankby=distance"
                    , LocationApp.GooglePlacesEndpoint
                    , LocationApp.GoogleMapsApiKey
                    , locationText
                    , searchTerm
                )
            );

            string responseValue = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            var sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GooglePlacesWebAPIResult>(responseValue);

            return sresponse.Results;
        }
    }
}
