using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheGood.Models
{
	public class ViewModel
    {
		public MovieModel Movie { get; set; }
		public IEnumerable<SelectListItem> Genres { get; set; }
	}
}
