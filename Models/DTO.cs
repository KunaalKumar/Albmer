using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Albmer.Models
{
    /**
     * The following classes are to serialize and deserialize MusicBrainz API responses
     */
    public class MusicBrainzResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public List<Artist> artists { get; set; }
    }

    public class Artist
    {
        public string id { get; set; }
        public string type { get; set; }
        public int score { get; set; }
        public string country { get; set; }
        public string name { get; set; }
        [JsonProperty("tags")]
        public List<SubName> tags { get; set; }
        [JsonProperty("begin-area")]
        public SubName begin_area { get; set; }
        [JsonProperty("life-span")]
        public LifeSpan life_span { get; set; }
    }

    // Any sub property where all that is needed is the "name"
    public class SubName
    {
        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class LifeSpan
    {
        [JsonProperty("begin")]
        public string begin { get; set; }
        [JsonProperty("ended")]
        public string ended { get; set; }
    }
}
