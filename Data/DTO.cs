using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Albmer.Data
{
    /**
     * The following classes are to serialize and deserialize MusicBrainz API responses
     */
    public class MusicBrainzArtistSearchResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        public List<MBArtist> artists { get; set; }
    }
    public class MusicBrainzAlbumSearchResult
    {
        public DateTime created { get; set; }
        public int count { get; set; }
        public int offset { get; set; }
        [JsonProperty("release-groups")]
        public List<MBAlbum> release_groups { get; set; }
    }

    public class MBArtist
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

    public class MBAlbum
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

    public class ArtistDetails
    {
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("life-span")]
        public LifeSpan life_span { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("relations")]
        public List<Relation> relations { get; set; }
        [JsonProperty("release-groups")]
        public List<ReleaseGroup> albums { get; set; }
    }

    public class Relation
    {
        [JsonProperty("target-type")]
        public string target_type { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("url")]
        public BrainzURL url { get; set; }
        // The following only apply to band members
        [JsonProperty("artist")]
        public ArtistSub1 artist { get; set; }
        [JsonProperty("begin")]
        public string begin { get; set; }
        [JsonProperty("end")]
        public string end { get; set; }
    }

    public class BrainzURL
    {
        [JsonProperty("resource")]
        public string resource { get; set; }
    }

    public class ReleaseGroup
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("title")]
        public string title { get; set; }
        [JsonProperty("first-release-date")]
        public string release_date { get; set; }
    }
}
