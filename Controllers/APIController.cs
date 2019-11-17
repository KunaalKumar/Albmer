using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Albmer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Albmer.Controllers
{
    public class APIController : Controller
    {

        private static string userAgent = "Albmer/1.0.0a (https://www.utah.edu/)";

        [HttpGet]
        public JsonResult searchArtist(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist?query=" + name + "&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzArtistSearchResult result = JsonConvert.DeserializeObject<MusicBrainzArtistSearchResult>(responseBody);
                if (result.artists.Count > 0)
                {
                    var test = result.artists.Select(artist => new
                    {
                        artist.id,
                        artist.name,
                        artist.score,
                        artist.country,
                        artist.tags,
                        artist.begin_area,
                        artist.life_span
                    });
                    return Json(new { success = true, result = test });
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
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                HttpResponseMessage response = client.GetAsync("http://musicbrainz.org/ws/2/release-group/?query=" + name + "%20AND%20type:album&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzAlbumSearchResult result = JsonConvert.DeserializeObject<MusicBrainzAlbumSearchResult>(responseBody);
                if (result.release_groups.Count > 0)
                {
                    var test = result.release_groups.Select(album => new
                    {
                        album.id,
                        album.title,
                        album.score,
                        album.artist_credit,
                        album.tags
                    });
                    return Json(new { success = true, result = test });
                }
                else
                    return Json(new { success = false, result = "No result found matching query" });
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false, result = "Error: " + e });
            }
        }
    }
}