using System.Threading.Tasks;
using TruckBridges.Core.Models;

namespace TruckBridges.Core.Interfaces
{
    public interface IGeoCoder
    {
        Task<string> GetCityFromLocation(GeoLocation location);
        Task<GeoLocation> GetLocationFromAddress(string address);
    }
}
