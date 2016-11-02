// Created by Tim Heinz - n8683981

using System.Collections.Generic;

namespace TruckBridges.Core.Models
{
    public class GoogleRoadsAPIResult
    {
        public List<SnappedPoints> snappedPoints { get; set; }
    }

    public class SnappedPoints
    {
        public Location location { get; set; }
        public int originalIndex { get; set; }
        public string placeId { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
