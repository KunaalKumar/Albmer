using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Albmer.Data;
using Albmer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            if (String.IsNullOrEmpty(name))
            {
                return Json(new { success = false, result = "name parameter not provided" });
            }
            // Remove whitespace from start and end; Replace successive white spaces with a single white space
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            name = regex.Replace(name.Trim(), " ");

            List<Object> data = new List<Object>();
            List<Artist> cachedResult = _context.Artists.Where(artist => artist.Name.ToLower().Contains(name.ToLower())).ToList();
            if (cachedResult.Count > 0) // Exists in cache
            {
                data.AddRange(cachedResult);
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
                            // Check to see if already exists in dataabse
                            bool isArtistInDatabase = true;
                            var dbArtist = _context.Artists.Where(al => al.ID.Equals(artist.id)).FirstOrDefault();
                            if (dbArtist == null)
                            {
                                isArtistInDatabase = false;
                                dbArtist = new Artist
                                {
                                    ID = artist.id,
                                    Name = artist.name,
                                    Type = artist.type,
                                    BeginYear = artist.life_span.begin,
                                    EndYear = artist.life_span.ended,
                                    Origin = artist.begin_area != null ? artist.begin_area.name : null,
                                    Genre = artist.tags != null ? tagsListToString(artist.tags) : null
                                };

                            }

                            if (isArtistInDatabase)
                            {
                                _context.Artists.Update(dbArtist);
                            }
                            else
                                _context.Artists.Add(dbArtist);

                            data.Add(mbArtistToAnon(artist));
                        }
                        _context.SaveChanges();
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
            if (tags == null) 
            {
                return null;
            }

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
            if (String.IsNullOrEmpty(name))
            {
                return Json(new { success = false, result = "name parameter not provided" });
            }

            // Remove whitespace from start and end; Replace successive white spaces with a single white space
            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            name = regex.Replace(name.Trim(), " ");

            List<Object> data = new List<Object>();
            var cachedResult = _context.Albums.Where(album => album.Title.ToLower().Contains(name.ToLower()))
                .Select(album => new 
                {
                    id = album.ID,
                    track_count = album.TrackCount,
                    title = album.Title,
                    tags = album.Genre,
                    artist_credit = album.ArtistAlbum
                    .Select(m => new {
                        name = m.Artist.Name,
                        id = m.ArtistId
                    })
                })
                .ToList();
            if (cachedResult.Count > 0) // Exists in cache
            {
                data.AddRange(cachedResult);
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
                    var url = "http://musicbrainz.org/ws/2/release-group/?query=%22" + name.Replace(" ", "%20") +"%22%20AND%20type:album&fmt=json";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    MusicBrainzAlbumSearchResult result = JsonConvert.DeserializeObject<MusicBrainzAlbumSearchResult>(responseBody);
                    if (result.release_groups.Count > 0)
                    {
                        foreach (MBAlbum album in result.release_groups)
                        {
                            // Check to see if album exists in database
                            Album dbAlbum = _context.Albums.Where(al => al.ID.Equals(album.id)).FirstOrDefault();
                            if (dbAlbum == null)
                            {
                                dbAlbum = new Album
                                {
                                    ID = album.id,
                                    Title = album.title,
                                    TrackCount = album.count,
                                    Genre = tagsListToString(album.tags)
                                };
                                _context.Albums.Add(dbAlbum);
                                _context.SaveChanges();
                            }

                            foreach (ArtistCredit credit in album.artist_credit)
                            {
                                // Add artist to cache if not in cache
                                Artist artist = _context.Artists.Where(artist => artist.ID.Equals(credit.artist.id)).FirstOrDefault();
                                if (artist == null) 
                                {
                                    artist = new Artist { ID = credit.artist.id, Name = credit.artist.name };
                                    _context.Artists.Add(artist);
                                    _context.SaveChanges();
                                }

                                // Add relations to artist and album entities
                                ArtistAlbum rel = new ArtistAlbum 
                                { 
                                    ArtistId = artist.ID,
                                    AlbumId = dbAlbum.ID
                                };
                                dbAlbum.ArtistAlbum.Add(rel);
                            }

                            _context.Albums.Update(dbAlbum);
                            try
                            {
                                _context.SaveChanges();
                            }
                            catch (Exception e)
                            { 
                                // Ignore already exists
                            }
                            data.Add(mbAlbumToAnon(album));
                        }
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

        private Object mbAlbumToAnon(MBAlbum album)
        {
            return (new
            { 
                album.id,
                album.title,
                track_count = album.count,
                artist_credit = album.artist_credit.Select(credit => new { name = credit.artist.name, id = credit.artist.id}),
                tags = tagsListToString(album.tags)
            });
        }

        [HttpGet]
        public JsonResult artistDetails(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return Json(new { success = false, result = "id parameter not provided" });
            }

            var artist = _context.Artists.Where(artist => artist.ID.Equals(id))
                .Include(artist => artist.ArtistAlbum)
                .ThenInclude(aa => aa.Album)
                .FirstOrDefault();
            if (artist != null && isDetailedArtist(artist)) // Exists in cache
            {
                return Json(new
                {
                    success = true,
                    result = detailedArtistToAnon(artist)
                });
            }
            else
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/artist/" + id + "?fmt=json&inc=url-rels+release-groups+artist-rels").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    ArtistDetails result = JsonConvert.DeserializeObject<ArtistDetails>(responseBody);
                    if (result.error == null)
                    {
                        if (artist == null) 
                        {
                            artist = new Artist { ID = id, Name = result.name, Type = result.type};
                            _context.Artists.Add(artist);
                            _context.SaveChanges();
                        }

                        artist.Image = result.relations.Where(relation => relation.type.Equals("image"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        artist.OfficialWebsite = result.relations.Where(relation => relation.type.Equals("official homepage"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        artist.AllMusic = result.relations.Where(relation => relation.type.Equals("allmusic"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        artist.Discogs = result.relations.Where(relation => relation.type.Equals("discogs"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        artist.RateYourMusic = result.relations.Where(relation => relation.type.Equals("other databases") && relation.url.resource.Contains("rateyourmusic.com"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        // Add album relations
                        foreach (ReleaseGroup release in result.albums)
                        {
                            bool test = false;
                            var album = _context.Albums.Where(album => album.ID.Equals(release.id)).FirstOrDefault();
                            if (album == null)
                            {
                                test = true;
                                album = new Album { ID = release.id, Title = release.title, Date = release.release_date };
                                _context.Albums.Add(album);
                                _context.SaveChanges();

                                ArtistAlbum rel = new ArtistAlbum { ArtistId = artist.ID, AlbumId = album.ID };
                                artist.ArtistAlbum.Add(rel);
                                _context.SaveChanges();
                            }
                        }

                        _context.Artists.Update(artist);
                        _context.SaveChanges();

                        return Json(new { success = true, result = detailedArtistToAnon(artist) });
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

        [HttpGet]
        public JsonResult albumDetails(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return Json(new { success = false, result = "id parameter not provided" });
            }
            var album = _context.Albums.Where(album => album.ID.Equals(id))
                .Include(album => album.ArtistAlbum)
                .ThenInclude(aa => aa.Artist)
                .FirstOrDefault();
            if (album != null && isDetailedAlbum(album)) // Exists in cache
            {
                return Json(new
                {
                    success = true,
                    result = detailedAlbumToAnon(album)
                });
            }
            else
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync("https://musicbrainz.org/ws/2/release-group/" + id + "?fmt=json&inc=url-rels%20releases").Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    ReleaseGroupDetails result = JsonConvert.DeserializeObject<ReleaseGroupDetails>(responseBody);
                    if (result.error == null)
                    {
                        if (album == null)
                        {
                            album = new Album { ID = id, Title = result.title};
                            _context.Albums.Add(album);
                            _context.SaveChanges();
                        }

                        album.AllMusic = result.relations.Where(relation => relation.type.Equals("allmusic"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        album.Discogs = result.relations.Where(relation => relation.type.Equals("discogs"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        album.RateYourMusic = result.relations.Where(relation => relation.type.Equals("other databases") && relation.url.resource.Contains("rateyourmusic.com"))
                            .Select(relation => relation.url.resource)
                            .FirstOrDefault();

                        // Get release id for next calls
                        string releaseId = result.releases[0].id; // Should never crash since there is always at least 1 release
                        for (int i = 0; i < result.releases.Count; i++)
                        {
                            if (result.releases[i].date.Equals(result.releaseDate))
                            {
                                releaseId = result.releases[i].id;
                                break;
                            }
                        }

                        try
                        {
                            response = client.GetAsync("http://coverartarchive.org/release/" + releaseId).Result;
                            response.EnsureSuccessStatusCode();
                            responseBody = response.Content.ReadAsStringAsync().Result;
                            CoverArtResult artResult = JsonConvert.DeserializeObject<CoverArtResult>(responseBody);
                            foreach (CoverImages image in artResult.images)
                            {
                                if (image.isFront)
                                    album.Image = image.image;
                            }
                        }
                        catch (Exception e)
                        { 
                            // Ignore - Cover art not found                        
                        }

                        // Call to get artist relations and songs
                        try
                        {
                            response = client.GetAsync("https://musicbrainz.org/ws/2/release/" + releaseId + "?fmt=json&inc=release-groups%20recordings%20artists").Result;
                            response.EnsureSuccessStatusCode();
                            responseBody = response.Content.ReadAsStringAsync().Result;
                            ReleaseDetails releaseResult = JsonConvert.DeserializeObject<ReleaseDetails>(responseBody);
                            if (releaseResult.error == null)
                            {
                                foreach (ArtistSub2 rel in releaseResult.artist_credits)
                                {
                                    var artist = _context.Artists.Where(artist => artist.ID.Equals(rel.artist.id)).FirstOrDefault();
                                    if (artist == null)
                                    {
                                        artist = new Artist { ID = rel.artist.id, Name = rel.artist.name };
                                        _context.Artists.Add(artist);
                                        _context.SaveChanges();
                                    }
                                    ArtistAlbum newRelation = new ArtistAlbum { Artist = artist, ArtistId = artist.ID, Album = album, AlbumId = album.ID };

                                    album.ArtistAlbum.Add(newRelation);

                                    _context.Artists.Update(artist);

                                    try
                                    {
                                        _context.SaveChanges();
                                    }
                                    catch (Exception e)
                                    {
                                        var t = 1;
                                        // Ignore - already exists
                                    }
                                }

                                _context.SaveChanges();

                                return Json(new { success = true, result = detailedAlbumToAnon(album) });
                            }
                            else
                                return Json(new { success = false, result = "Unexpected error getting artist details" });
                        }
                        catch(Exception e) {
                            // Ignore - / Ignore - Failed to get artist/song lists
                            return Json(new { success = true, result = detailedAlbumToAnon(album) });
                        }

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

        private bool isDetailedArtist(Artist artist)
        {
            if(artist.OfficialWebsite != null ||
                artist.RateYourMusic != null ||
                artist.Discogs != null ||
                artist.AllMusic != null ||
                artist.Image != null)
            {
                return true;
            }
            return false;
        }

        private bool isDetailedAlbum(Album album)
        {
            if (album.RateYourMusic != null ||
                album.Discogs != null ||
                album.AllMusic != null ||
                album.Image != null)
            {
                return true;
            }
            return false;
        }

        private Object detailedArtistToAnon(Artist artist) 
        {
            return (new
            { 
                name = artist.Name,
                image = artist.Image,
                begin_year = artist.BeginYear,
                end_year = artist.EndYear,
                origin = artist.Origin,
                type = artist.Type,
                albums = artist.ArtistAlbum.Select(m => new { m.AlbumId, m.Album.Title}),
                official_website = artist.OfficialWebsite,
                allmusic = artist.AllMusic,
                discogs = artist.Discogs,
                rate_your_music = artist.RateYourMusic
            });
        }

        private Object detailedAlbumToAnon(Album album)
        {
            return (new
            { 
                title = album.Title,
                image = album.Image,
                id = album.ID,
                artists = album.ArtistAlbum.Select(m => new { id = m.ArtistId, m.Artist.Name}),
                track_count = album.TrackCount,
                allmusic = album.AllMusic,
                discogs = album.Discogs,
                rate_your_music = album.RateYourMusic
            });
        }

        [HttpGet]
        public JsonResult matchAlbum(string artistName, string albumName)
        {
            if (String.IsNullOrEmpty(artistName) || String.IsNullOrEmpty(albumName))
            {
                return Json(new { success = false, result = "Supply both artistName and albumName" });
            }
            dynamic result = searchAlbum(albumName).Value;
            if (!result.success)
            {
                return Json(new { success = false, result = "Unexpected error" });
            }
            foreach (dynamic album in result.result)
            {
                if (album.title.Equals(albumName)) // Album name matches
                {
                    foreach (dynamic rel in album.artist_credit)
                    {
                        if (rel.name.ToLower().Equals(artistName.ToLower())) // Artist matches
                        {
                            dynamic detailedAlbum = albumDetails(album.id).Value;
                            if (!detailedAlbum.success)
                            {
                                return Json(new { success = false, result = "Unexpected error" });
                            }
                            return Json(new { success = true, detailedAlbum.result.title, artist_name = rel.name, 
                                detailedAlbum.result.allmusic, detailedAlbum.result.discogs, rate_your_music = detailedAlbum.result.rate_your_music });
                        }
                    }
                }
            }
            return Json(new { success = false, result = "Failed to find a match" });
        }
    }
}