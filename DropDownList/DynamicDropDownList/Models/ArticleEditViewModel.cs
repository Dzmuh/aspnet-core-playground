using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DynamicDropDownList.Models
{
    public class ArticleEditViewModel
    {
        private List<SelectListItem> _categories = new List<SelectListItem>();
        private List<SelectListItem> _products = new List<SelectListItem>();

        [Required(ErrorMessage = "Please select a category")]
        public string SelectedCategory { get; set; }

        [Required(ErrorMessage = "Please select a product")]
        public string SelectedProduct { get; set; }

        public List<SelectListItem> Products
        {
            get { return _products; }
        }

        public List<SelectListItem> Categories
        {
            get
            {
                _categories.Add(new SelectListItem() { Text = "Python", Value = "1" });
                _categories.Add(new SelectListItem() { Text = "JavaScript", Value = "2" });

                return _categories;
            }
        }
    }
}
