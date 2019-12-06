using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Albmer.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Artist(string id)
        {
            ViewData["id"] = id;
            return View();
        }

        public IActionResult Album(string id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}