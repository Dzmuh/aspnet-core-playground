using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheUgly.Models;

namespace TheUgly.Controllers
{
	public static class Data
    {
		public static MovieModel GetMovie()
        {
			return new MovieModel { Title = "Inception", GenreId = 1 };
		}

		public static IEnumerable<GenreModel> GetGenres()
        {
			yield return new GenreModel { Name = "Horror", Id = 1 };
			yield return new GenreModel { Name = "Comedy", Id = 2 };
		}
	}
}
