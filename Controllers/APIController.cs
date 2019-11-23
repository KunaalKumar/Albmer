using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Albmer.Data;
using Albmer.Models;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Albmer.Controllers
{
    public class APIController : Controller
    {

        private static string userAgent = "Albmer/1.0.0a (https://www.utah.edu/)";
        private HttpClient client = new HttpClient();
        private readonly CacheContext _context;
        public APIController(CacheContext context) 
        {
            _context = context;
            client.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        [HttpGet]
        public JsonResult searchArtist(string name)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist?query=" + name + "&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzArtistSearchResult result = JsonConvert.DeserializeObject<MusicBrainzArtistSearchResult>(responseBody);
                if (result.artists.Count > 0)
                {
                    var data = result.artists.Select(artist => new
                    {
                        artist.id,
                        artist.name,
                        artist.score,
                        artist.country,
                        artist.tags,
                        artist.begin_area,
                        artist.life_span
                    });
                    return Json(new { success = true, result = data });
                }
                else
                    return Json(new { success = false, result = "No result found matching query" });
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false, result = "Error: " + e});
            }
        }

        [HttpGet]
        public JsonResult searchAlbum(string name)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("http://musicbrainz.org/ws/2/release-group/?query=" + name + "%20AND%20type:album&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzAlbumSearchResult result = JsonConvert.DeserializeObject<MusicBrainzAlbumSearchResult>(responseBody);
                if (result.release_groups.Count > 0)
                {
                    var data = result.release_groups.Select(album => new
                    {
                        album.id,
                        album.title,
                        album.score,
                        album.artist_credit,
                        album.tags
                    });
                    return Json(new { success = true, result = data });
                }
                else
                    return Json(new { success = false, result = "No result found matching query" });
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false, result = "Error: " + e });
            }
        }

        [HttpGet]
        public JsonResult artistDetails(string id)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist/" + id + "?fmt=json&inc=url-rels+release-groups+artist-rels").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                ArtistDetails result = JsonConvert.DeserializeObject<ArtistDetails>(responseBody);
                if (result.error == null)
                {
                    var image = result.relations.Where(relation => relation.type.Equals("image"))
                        .Select(relation => relation.url.resource)
                        .FirstOrDefault();

                    var band_memebers = result.relations.Where(relation => relation.type.Equals("member of band"))
                        .Select(relation => new 
                        { 
                            relation.artist.id,
                            relation.artist.name,
                            start_year = relation.begin,
                            end_year = relation.end
                        });

                    var official_website = result.relations.Where(relation => relation.type.Equals("official homepage"))
                        .Select(relation => relation.url.resource)
                        .FirstOrDefault();

                    var allmusic = result.relations.Where(relation => relation.type.Equals("allmusic"))
                        .Select(relation => relation.url.resource)
                        .FirstOrDefault();

                    var discogs = result.relations.Where(relation => relation.type.Equals("discogs"))
                        .Select(relation => relation.url.resource)
                        .FirstOrDefault();

                    var rate_your_music = result.relations.Where(relation => relation.type.Equals("other databases") && relation.url.resource.Contains("rateyourmusic.com"))
                        .Select(relation => relation.url.resource)
                        .FirstOrDefault();

                    var data = new
                    {
                        image,
                        result.name,
                        result.life_span,
                        band_memebers,
                        result.albums,
                        official_website,
                        allmusic,
                        discogs,
                        rate_your_music
                    };
                    return Json(new { success = true, result = data });
                }
                else
                    return Json(new { success = false, result = "No result found matching query" });
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false, result = "Error: " + e });
            }
        }

        [HttpGet]
        public JsonResult ScrapeAlbumChart()
        {
            string topAlbumsUrl = "https://www.billboard.com/charts/current-albums";
            HttpClient httpClient = new HttpClient();
           
            HttpResponseMessage request = client.GetAsync(topAlbumsUrl).Result;

            Stream response = request.Content.ReadAsStreamAsync().Result;

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
           
            return Json(new { success = true });
        }

        
    
    }
}