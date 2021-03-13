using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheUgly.Models;

namespace TheUgly.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = Data.GetMovie();
            return View(model);
        }
    }
}
