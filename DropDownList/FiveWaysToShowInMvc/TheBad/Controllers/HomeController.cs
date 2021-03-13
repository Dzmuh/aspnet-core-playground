using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheBad.Models;

namespace TheBad.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = Data.GetMovie();
            ViewData["AllGenres"] = from genre in Data.GetGenres() select new SelectListItem { Text = genre.Name, Value = genre.Id.ToString() };
            return View(model);
        }
    }
}
