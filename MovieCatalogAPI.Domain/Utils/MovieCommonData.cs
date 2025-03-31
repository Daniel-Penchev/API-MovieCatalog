using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCatalogAPI.DTO.Models.Movie;
using MovieCatalogAPI.DTO.Utility;

namespace MovieCatalogAPI.Domain.Utils
{
    public class MovieCommonData
    {
        public static string GetFullImageUrl(MovieItem movie)
        {
            string fullImageUrl = string.Empty;

            string imageFoldePath = string.Format("{0}/{1}/{2}", Params.Domain, Params.UploadFolder, Params.ProductImagesPath, movie.Id);

            fullImageUrl = !string.IsNullOrEmpty(movie.ImageUrl) ?
                string.Format(@"{0}/{1}/{2}", imageFoldePath, movie.Id, movie.ImageUrl) :
                Params.NoImageFolderPath;

            return fullImageUrl;
        }
    }
}
