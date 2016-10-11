
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
        public async Task<List<LocationAutoCompleteResult>> GetLocations(string searchTerm)
        {
            WebRequest request = WebRequest.CreateHttp(String.Format("{0}?apikey={1}&q={2}"
                , LocationApp.AutoCompleteEndpoint
                , LocationApp.ApiKey
                , WebUtility.HtmlEncode(searchTerm)));

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

            if (sresponse != null)
            {
                return sresponse;
            }
            else
            {
                return null;
            }
        }
    }
}
