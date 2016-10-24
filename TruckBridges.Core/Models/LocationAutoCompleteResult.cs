﻿// Created by Tim Heinz - n8683981

using Newtonsoft.Json;

namespace TruckBridges.Core.Models
{
    public class LocationAutoCompleteResult
    {
        [JsonProperty("geometry.location.lat")]
        public double Latitude { get; set; }

        [JsonProperty("geometry.location.lng")]
        public double Longitude { get; set; }

        [JsonProperty("icon")]
        public string IconURL { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string LocalizedName { get; set; }

        [JsonProperty("place_id")]
        public string PlaceID { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }

/*
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        [JsonProperty("Country.ID")]
        public string CountryId { get; set; }
        [JsonProperty("Country.LocalizedName")]
        public string CountryLocalizedName { get; set; }
        [JsonProperty("AdministrativeArea.ID")]
        public string AdministrativeAreaId { get; set; }
        [JsonProperty("AdministrativeArea.LocalizedName")]
        public string AdministrativeAreaLocalizedName { get; set; }
*/
    }
}
