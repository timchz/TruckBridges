// Created by Tim Heinz - n8683981

using Newtonsoft.Json;

namespace TruckBridges.Core.Models
{
    public class BridgeDetails
    {
        [JsonProperty("ASSET_ID")]
        public string AssetID { get; set; }

        [JsonProperty("Angle 1")]
        public double Angle1 { get; set; }

        [JsonProperty("Angle 2")]
        public double Angle2 { get; set; }

        [JsonProperty("COUNTER")]
        public string Counter { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Direction")]
        public string Direction { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Signed_Clearance")]
        public double SignedClearance { get; set; }

        [JsonProperty("SIGNED_CLEARANCE_MAX")]
        public double SignedClearanceMax { get; set; }

        [JsonProperty("Street_Name")]
        public string StreetName { get; set; }

        [JsonProperty("Structure_ID")]
        public string StructureID { get; set; }

        [JsonProperty("Suburb")]
        public string Suburb { get; set; }
    }
}
