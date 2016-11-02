// Created by Tim Heinz - n8683981

namespace TruckBridges.Core.Models
{
    public class GeoArea
    {
        public GeoLocation TopLeft { get; set; }
        public GeoLocation BottomRight { get; set; }
        public GeoLocation Center { get; set; }
        public string Locality { get; set; }

        public GeoArea(GeoLocation topLeft, GeoLocation bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;

            Center = new GeoLocation(TopLeft.Latitude + ((BottomRight.Latitude - TopLeft.Latitude) / 2),
                                     TopLeft.Longitude + ((BottomRight.Longitude - TopLeft.Longitude) / 2));
            Center.Locality = topLeft.Locality;

            Locality = topLeft.Locality;
        }

        public string LatLongText()
        {
            return (TopLeft.Latitude.ToString() + "," + TopLeft.Longitude.ToString() + ";" +
                    BottomRight.Latitude.ToString() + "," + BottomRight.Longitude.ToString());
        }

    }
}
