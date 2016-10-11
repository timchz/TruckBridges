using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Locations;
using TruckBridges.Core.Interfaces;
using TruckBridges.Core.Models;

namespace TruckBridges.Droid.Services
{
    public class GeoCoderService : IGeoCoder
    {
        public async Task<string> GetCityFromLocation(GeoLocation location)
        {
            using (var geocoder = new Geocoder(Application.Context))
            {
                var foundLocation = await geocoder.GetFromLocationAsync(location.Latitude, location.Longitude, 1);
                return foundLocation.FirstOrDefault().Locality;

            }
        }

        public async Task<GeoLocation> GetLocationFromAddress(string address)
        {
            using (var geocoder = new Geocoder(Application.Context))
            {
                var foundlocation = await geocoder.GetFromLocationNameAsync(address, 1);
                var bestLocation = foundlocation.FirstOrDefault();
                return new GeoLocation(bestLocation.Latitude, bestLocation.Longitude);
            }
        }
    }
}
