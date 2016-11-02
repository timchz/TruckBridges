// Created by Tim Heinz - n8683981

using System.Collections.Generic;

namespace TruckBridges.Core.Models
{
    public class GooglePlacesWebAPIResult
    {
        public List<string> HTMLAttributions { get; set; }
        public List<LocationAutoCompleteResult> Results { get; set; }
        public string Status { get; set; }
    }
}
