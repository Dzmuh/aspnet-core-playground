using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using DynamicDropDownList.Models;

namespace DynamicDropDownList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new ArticleEditViewModel());
        }

        public string GetProductsForCategory(string id)
        {
            // get the products from the repository 

            var products = new List<SelectListItem>();

            if (id == "1")
            {
                products.Add(new SelectListItem() { Text = "Introduction to Python", Value = "1" });
                products.Add(new SelectListItem() { Text = "Python Unit Testing", Value = "2" });
            }
            else if (id == "2")
            {
                products.Add(new SelectListItem() { Text = "JavaScript testing", Value = "1" });
                products.Add(new SelectListItem() { Text = "JavaScript Ninja", Value = "2" });
            }

            //return new JavaScriptSerializer().Serialize(products);
            //return JsonConvert.SerializeObject(products, Formatting.Indented);
            return System.Text.Json.JsonSerializer.Serialize(products, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
