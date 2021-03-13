using System;
using System.ComponentModel.DataAnnotations;

namespace TheUgly.Models
{
    public class MovieModel {
		public string Title { get; set; }

		[UIHint("GenreEditor"), DataType("Genre")]
		public int GenreId { get; set; }
	}
}
