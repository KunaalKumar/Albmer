using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [HttpGet]
        public JsonResult DiscogsRatings(string id)
        {
            /* fail status result */
            var failReturn = Json(new
            {
                success = false,
                response = "Given id is invalid"
            });

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
                    return failReturn;
                }
                if (!int.TryParse(rateCountString, out int rateCount))
                {
                    return failReturn;
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
                return failReturn;
            }

        }

        [HttpGet]
        public JsonResult RateYourMuiscRatings(string partial)
        {
            /* fail status result */
            var failReturn = Json(new
            {
                success = false,
                response = "Given id is invalid"
            });

            /* base URL */
            string rymURL = "https://rateyourmusic.com/release/album/";
            HttpResponseMessage response = client.GetAsync(rymURL + partial).Result;

            if (response.StatusCode == HttpStatusCode.OK) /* if return status is 200 */
            {

            } else
            {
                return failReturn;
            }

            return Json(new { success = true });
        }
    }
}