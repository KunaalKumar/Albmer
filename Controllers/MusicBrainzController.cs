using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Albmer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Albmer.Controllers
{
    public class MusicBrainzController : ControllerBase
    {
        // MusicBrainz/FindArtist?name={artist_name}
        [HttpGet]
        public JsonResult FindArtist(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Albmer/1.0.0a (https://www.utah.edu/)");
                HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist?query="+name+"&fmt=json").Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                MusicBrainzResult result = JsonConvert.DeserializeObject<MusicBrainzResult>(responseBody);
                return new JsonResult(new { result = result.artists[0].name });
            }
            catch (HttpRequestException e)
            {
                return new JsonResult(new { result = "error"});
            }
        }
    }
}