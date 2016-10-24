// Created by Tim Heinz - n8683981

namespace TruckBridges.Core.Models
{
    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        public bool HasAltitude { get; set; }
        public string Locality { get; set; }

        public GeoLocation() { }
        public GeoLocation(double latitude, double longitude, double? altitude = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            HasAltitude = (altitude == null ? false : true);
            Altitude = altitude;
        }

        public string LatLongText()
        {
            return (Latitude.ToString() + ", " + Longitude.ToString());
        }
    }
}
