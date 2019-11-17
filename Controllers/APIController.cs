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
        [HttpGet]
        public JsonResult searchArtist(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Albmer/1.0.0a (https://www.utah.edu/)");
                HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist?query=" + name + "&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzResult result = JsonConvert.DeserializeObject<MusicBrainzResult>(responseBody);
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
                    return Json(new { success = false });
            }
            catch (HttpRequestException e)
            {
                return Json(new { success = false });
            }
        }
    }
}