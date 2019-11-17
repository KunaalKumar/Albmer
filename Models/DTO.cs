using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Albmer.Models
{
    /**
     * The following classes are to serialize and deserialize MusicBrainz API responses
     */
    public class MusicBrainzArtistSearchResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public List<Artist> artists { get; set; }
    }
    public class MusicBrainzAlbumSearchResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        [JsonProperty("release-groups")]
        public List<Album> release_groups { get; set; }
    }

    public class Artist
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("score")]
        public int score { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("name")]
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

    public class Album
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("count")]
        public int count { get; set; }
        [JsonProperty("score")]

        public int score { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("tags")]
        public List<SubName> tags { get; set; }
        [JsonProperty("artist-credit")]
        public List<ArtistCredit> artist_credit { get; set; }
    }

    public class ArtistCredit
    {
        [JsonProperty("artist")]
        public ArtistSub1 artist { get; set; }
    }

    public class ArtistSub1
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}
