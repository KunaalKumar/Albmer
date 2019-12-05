using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;

namespace Albmer.Controllers
{
    public class ScraperController : Controller
    {
        private HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }

        private JsonResult FailRetuenJson()
        {
            return Json(new
            {
                success = false,
                response = "Given id is invalid"
            });
        }

        private JsonResult FailRetuenJson(string test)
        {
            return Json(new
            {
                success = false,
                response = "Given id is invalid",
                info = test
            });
        }

        [HttpGet]
        public JsonResult DiscogsRatings(string id)
        {

            /* base URL */
            string discogsURL = "https://www.discogs.com/master/";
            HttpResponseMessage response = client.GetAsync(discogsURL + id).Result;

            if (response.StatusCode == HttpStatusCode.OK) /* if return status is 200 */
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

                /* Refer: https://stackoverflow.com/questions/7824138/how-to-grab-elements-by-class-or-id-in-html-source-in-c */
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(responseContent);
                HtmlNode rateValueNode = doc.DocumentNode.SelectSingleNode("//*[@class=\"rating_value\"]");
                HtmlNode rateCountNode = doc.DocumentNode.SelectSingleNode("//*[@class=\"rating_count\"]");
                string rateValueString = (rateValueNode == null) ? "Error, rating_value not found" : rateValueNode.InnerHtml; //TODO: how to deal with this situation?
                string rateCountString = (rateCountNode == null) ? "Error, rating_count not found" : rateCountNode.InnerHtml; //TODO: how to deal with this situation?

                /* check if the number is valid */
                if (!float.TryParse(rateValueString, out float rateValue))
                {
                    return FailRetuenJson();
                }
                if (!int.TryParse(rateCountString, out int rateCount))
                {
                    return FailRetuenJson();
                }

                /* return result follow the guide: https://github.com/KunaalKumar/Albmer/blob/master/docs/discogsRating.md */
                return Json(new
                {
                    success = true,
                    rating = rateValue,
                    max_rating = 5,
                    number_of_ratings = rateCount
                });

            } else { /* if can't get result */
                return FailRetuenJson();
            }

        }

        [HttpGet]
        public JsonResult RateYourMusicRatings(string partial)
        {
            string rymURL = "https://rateyourmusic.com/release/album/";
            string url = rymURL + partial;

            /* base URL */
            /* example: https://rateyourmusic.com/release/album/green-day/39_smooth/ */
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK) /* if return status is 200 */
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

                /* Refer: https://stackoverflow.com/questions/7824138/how-to-grab-elements-by-class-or-id-in-html-source-in-c */
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(responseContent);
                HtmlNode rateValueNode = doc.DocumentNode.SelectSingleNode("//*[@class=\"avg_rating\"]");
                HtmlNode rateMaxNode = doc.DocumentNode.SelectSingleNode("//*[@class=\"max_rating\"]");
                HtmlNode rateCountNode = doc.DocumentNode.SelectSingleNode("//*[@class=\"num_ratings\"]");
                string rateValueString = (rateValueNode == null) ? "Error, rating_value not found" : rateValueNode.InnerHtml;
                string rateMaxString = (rateMaxNode == null) ? "Error, rating_value not found" : rateMaxNode.InnerHtml;
                string rateCountString = (rateCountNode == null) ? "Error, rating_count not found" : rateCountNode.InnerHtml;

                /* check if the number is valid */
                rateValueString = Regex.Replace(rateValueString, @"\s+", "");
                if (!float.TryParse(rateValueString, out float rateValue))
                {
                    return FailRetuenJson("1");
                }
                rateCountString = Regex.Replace(rateCountString, @"\s+", "");
                if (!int.TryParse(rateCountString, out int rateCount))
                {
                    return FailRetuenJson("2");
                }
                rateMaxString = Regex.Replace(rateMaxString, @"\s+", "");
                if (!int.TryParse(rateMaxString, out int rateMax))
                {
                    return FailRetuenJson("3");
                }

                /* return result follow the guide: https://github.com/KunaalKumar/Albmer/blob/master/docs/discogsRating.md */
                return Json(new
                {
                    success = true,
                    rating = rateValue,
                    max_rating = rateMax,
                    number_of_ratings = rateCount
                });
            } else
            {
                return FailRetuenJson("4");
            }
        }


        [HttpGet]
        public JsonResult AllMusicRatings(string id)
        {
            string rymURL = "https://www.allmusic.com/album/";
            string url = rymURL + id;

            /* base URL */
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.StatusCode == HttpStatusCode.OK) /* if return status is 200 */
            {
                string responseContent = response.Content.ReadAsStringAsync().Result;

                /* Refer: https://stackoverflow.com/questions/7824138/how-to-grab-elements-by-class-or-id-in-html-source-in-c */
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(responseContent);

                HttpResponseMessage request = client.GetAsync(url).Result;

                Stream responses = request.Content.ReadAsStreamAsync().Result;

                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.ParseDocument(responses);
                AngleSharp.Dom.IElement allMusicRateElement = document.GetElementsByClassName("allmusic-rating")[0];
                AngleSharp.Dom.IElement userRateElement = document.GetElementsByClassName("average-user-rating")[0];

                string siteRateString = allMusicRateElement.TextContent.Trim();
                return FailRetuenJson(userRateElement.ClassList.ToString());

                string userRateString = userRateElement.ClassList[1].Trim();
                userRateString = userRateString.Substring(userRateString.Length - 1);

                /* check if the number is valid */
                if (!float.TryParse(siteRateString, out float siteRate))
                {
                    return FailRetuenJson(siteRateString);
                }
                if (!int.TryParse(userRateString, out int userRate))
                {
                    return FailRetuenJson(userRateString);
                }

                return Json(new
                {
                    success = true,
                    site_rating = siteRate,
                    user_rating = userRate,
                    max_rating = 8
                });
            }
            else
            {
                return FailRetuenJson();
            }
        }



   /*
         
        [HttpGet]
        public JsonResult AllMusicRatings(string musicBrainzId, string albumName)
        {
            string baseUrl = "https://www.allmusic.com/album/";

            // encode special characters, spaces should be '-' for allmusic
            albumName.Replace(' ', '-');
            string albumUrl = Uri.EscapeDataString(albumName.ToLower() +"-" + musicBrainzId);

            string Url = baseUrl + albumUrl;

            HttpResponseMessage request = client.GetAsync(Url).Result;

            Stream response = request.Content.ReadAsStreamAsync().Result;

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            AngleSharp.Dom.IElement ratingElement = document.GetElementsByClassName("allmusic-rating")[0];

            string rating= ratingElement.TextContent.Trim();
            rating += "/10";


            return Json(new { 
                success = true, 
                allMusicRating = rating 
            });
        }

    */
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

            for (int i = 0; i < albums.Length; i++)
            {
                string title = albums[i].GetElementsByClassName("chart-list-item__title-text")[0].TextContent.Trim();
                string artist = "";

                // Some albums have a link tag, some don't.
                if (albums[i].GetElementsByClassName("chart-list-item__artist")[0].ChildElementCount > 0) //sometimes there is an <a> tag
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