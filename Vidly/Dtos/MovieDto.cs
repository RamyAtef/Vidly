using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        public int NumberInStock { get; set; }

        public byte GenreId { get; set; }

    }
}