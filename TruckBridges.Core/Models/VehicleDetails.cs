// Created by Tim Heinz - n8683981

using System.Collections.Generic;
using Newtonsoft.Json;

namespace TruckBridges.Core.Models
{
    public class VehicleDetails
    {
        [JsonProperty("Registration")]
        public string Registration { get; set; }

        [JsonProperty("Height")]
        public double Height { get; set; }

        [JsonProperty("Weight")]
        public double Weight { get; set; }

        [JsonProperty("Length")]
        public double Length { get; set; }

        [JsonProperty("SpeedLimit")]
        public int SpeedLimit { get; set; }

        [JsonProperty("Trailers")]
        public int Trailers { get; set; }

        public double ExtraHeight { get; set; }

        public double Clearance { get; set; }
        public string HazardousMaterialClass { get; set; }


        public void Calculate()
        {
            Clearance = Height + ExtraHeight;
        }

        public void ParseQRCode(string QRCodeText)
        {
            // return if input string is null
            if (QRCodeText == null)
                return;

            var contents = QRCodeText;
            string[] tokens = contents.Split('\n');

            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].StartsWith("RG:"))
                    Registration = tokens[i].Substring(3);

                if (tokens[i].StartsWith("HT:"))
                    Height = double.Parse(tokens[i].Substring(3));

                if (tokens[i].StartsWith("WT:"))
                    Weight = double.Parse(tokens[i].Substring(3));

                if (tokens[i].StartsWith("LT:"))
                    Length = double.Parse(tokens[i].Substring(3));

                if (tokens[i].StartsWith("SL:"))
                    SpeedLimit = int.Parse(tokens[i].Substring(3));

                if (tokens[i].StartsWith("TR:"))
                    Trailers = int.Parse(tokens[i].Substring(3));
            }
        }

    }
}
