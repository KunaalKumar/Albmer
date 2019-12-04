using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
    public class Billboard_Album
    {
       public string Title { get; set; }
       public string Artist { get; set; }
    }

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
            List<Object> data = new List<Object>();
            var cachedResult = _context.Artists.Where(artist => artist.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();
            if (cachedResult != null) // Exists in cache
            {
                data.Add(cachedResult);
                return Json(new 
                { 
                    success = true, 
                    result = data
                });
            } 
            else
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist?query=" + name + "&fmt=json").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    MusicBrainzArtistSearchResult result = JsonConvert.DeserializeObject<MusicBrainzArtistSearchResult>(responseBody);
                    if (result.artists.Count > 0)
                    {
                        foreach (MBArtist artist in result.artists) 
                        {
                            var dbArtist = new Artist { ID = artist.id, 
                                Name = artist.name, Type = artist.type, 
                                BeginYear = artist.life_span.begin, 
                                EndYear = artist.life_span.ended,
                                Origin = artist.begin_area != null ? artist.begin_area.name : null,
                                Genre = artist.tags != null ? tagsListToString(artist.tags) : null
                            };

                            _context.Artists.AddAsync(dbArtist); // Cache results to db
                            
                            data.Add(mbArtistToAnon(artist));
                        }
                        _context.SaveChangesAsync();
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
        }

        private string tagsListToString(List<SubName> tags) 
        {
            StringBuilder sb = new StringBuilder();
            foreach (SubName tag in tags) 
            {
                sb.Append(tag.name).Append(", ");
            }
 
            return sb.ToString()[0..^2];
        }

        // Used to parse MBArtist to pass off as json
        private Object mbArtistToAnon(MBArtist artist) 
        {
            return (new 
            { 
                artist.id,
                artist.name,
                origin = artist.begin_area != null ? artist.begin_area.name : null, 
                genre = artist.tags != null ? tagsListToString(artist.tags) : null, 
                begin_year = artist.life_span.begin, 
                end_year = artist.life_span.ended 
            });
        }

        // Parse Artist to formatted object for json
        private Object cachedArtistToAnon(Artist artist)
        {
            return (new
            { 
                id = artist.ID,
                name = artist.Name,
                origin = artist.Origin,
                begin_year = artist.BeginYear,
                end_year = artist.EndYear,
                genre = artist.Genre
            });
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
            Billboard_Album[] topAlbums = new Billboard_Album[100];
           
            HttpResponseMessage request = client.GetAsync(topAlbumsUrl).Result;

            Stream response = request.Content.ReadAsStreamAsync().Result;

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> albums = document.GetElementsByClassName("chart-list-item__first-row chart-list-item__cursor-pointer");

            for(int i = 0; i < albums.Length; i++)
            {
                string title = albums[i].GetElementsByClassName("chart-list-item__title-text")[0].TextContent.Trim();
                string artist = "";

                // Some albums have a link tag, some don't.
                if(albums[i].GetElementsByClassName("chart-list-item__artist")[0].ChildElementCount > 0) //sometimes there is an <a> tag
                    artist = albums[i].GetElementsByClassName("chart-list-item__artist")[0].FirstElementChild.TextContent.Trim();
                else
                    artist = albums[i].GetElementsByClassName("chart-list-item__artist")[0].TextContent.Trim();

                var ab = new Billboard_Album
                {
                    Title = title,
                    Artist = artist

                };
                topAlbums[i] = ab;
                
            }

            return Json(new
            {
                success = true,
                albums = topAlbums
            });
        }

        
    
    }
}