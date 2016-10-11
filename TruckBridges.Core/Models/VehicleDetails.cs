
using Newtonsoft.Json;

namespace TruckBridges.Core.Models
{
    public class VehicleDetails
    {
        [JsonProperty("Registration")]
        public string Registration { get; set; }

        [JsonProperty("Clearance")]
        public double Clearance { get; set; }

        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("Length")]
        public double Length { get; set; }

        [JsonProperty("SpeedLimit")]
        public int SpeedLimit { get; set; }

        [JsonProperty("Trailers")]
        public int Trailers { get; set; }

        [JsonProperty("HazardousMaterialClass")]
        public int HazardousMaterialClass { get; set; }
    }
}
