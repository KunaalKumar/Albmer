using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Albmer.Controllers
{
    public class ScraperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}