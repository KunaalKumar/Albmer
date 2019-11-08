using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Albmer.Models
{
    public class MusicBrainzResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public List<Artist> artists { get; set; }
    }

    public class Artist
    {
        public string id;
        public string type;
        public int score;
        [JsonProperty("type-id")]
        public string type_id;
        public string name;
    }
}
