using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCatalogAPI.DTO.Models.Movie;

namespace MovieCatalogAPI.DTO.Models.MovieDetail
{
    public class MovieDetailModel
    {
        public string SuccessMessage { get; set; }

        public string ErrorMessage { get; set; }

        public MovieItem MovieItem { get; set; } = new MovieItem();

    }
}
