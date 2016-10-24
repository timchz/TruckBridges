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
        public async Task<List<LocationAutoCompleteResult>> GetLocations(GeoLocation location, string searchTerm)
        {
            if (location == null)
                return null;

            string locationText = location.LatLongText();

            WebRequest request = WebRequest.CreateHttp(
                String.Format("{0}?key={1}&location={2}&keyword={3}&rankby=distance"
                    , LocationApp.AutoCompleteEndpoint
                    , LocationApp.ApiKey
                    , locationText
                    , searchTerm
//                    , WebUtility.HtmlEncode(searchTerm)
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
            var sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationAutoCompleteResult>>(responseValue);

            return sresponse;
/*
            if (sresponse != null)
            {
                return sresponse;
            }
            else
            {
                return null;
            }
*/
        }
    }
}
