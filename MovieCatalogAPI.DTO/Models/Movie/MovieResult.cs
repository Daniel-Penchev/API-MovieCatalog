using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogAPI.DTO.Models.Movie
{
    public class MovieResult
    {
        public int Id { get; set; }

        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public string FullImageUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
