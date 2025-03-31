using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Domain.Abstract;
using MovieCatalogAPI.Domain.Utils;
using MovieCatalogAPI.DTO.Models.Movie;
using MovieCatalogAPI.DTO.Models.MovieDetail;

namespace MovieCatalogAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CorePolicy")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository movieRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MovieController(IMovieRepository movieRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.movieRepository = movieRepository;

            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public MovieModel GetMovies()
        {
            MovieModel model = movieRepository.GetMovieModel();

            return model;
        }

        [HttpGet("{id}")]
        public MovieDetailModel GetMovieDetail(int id)
        {
            MovieDetailModel model = movieRepository.GetMovieDetailModel(id);

            return model;
        }

        [HttpPost]
        public MovieResult Save(MovieItem Item)
        {
            MovieResult result = movieRepository.Save(Item);

            return result;
        }

        [HttpPost]
        public MovieResult DeleteMovie(MovieItem Item)
        {
            MovieResult result = movieRepository.DeleteMovie(Item);

            return result;
        }

        [HttpPost]
        public MovieResult DeleteMovieImage(MovieItem Item)
        {
            MovieResult result = movieRepository.DeleteMovieImage(Item);

            return result;
        }

        [HttpPost("{id}")]
        public MovieResult UploadImage(int id)
        {
            MovieResult movieResult = new MovieResult();

            try
            {
                var file = Request.Form.Files[0];

                string uploadFolderNamePath = "Uploads";

                string imagesPath = "MovieImages";

                string webRootPath = _webHostEnvironment.WebRootPath;

                string newPath = Path.Combine(webRootPath, uploadFolderNamePath);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                newPath = string.Format(@"{0}\{1}", newPath, imagesPath);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                newPath = string.Format(@"{0}\{1}", newPath, id);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(newPath);

                    foreach (FileInfo image in directoryInfo.GetFiles())
                    {
                        image.Delete();
                    }

                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    string fullPath = Path.Combine(newPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    MovieItem movieItem = movieRepository.UpdateImage(id, fileName);

                    if (movieItem != null)
                    {
                        movieResult.FullImageUrl = MovieCommonData.GetFullImageUrl(movieItem);

                        movieResult.ImageUrl = movieItem.ImageUrl;
                    }
                }
            }
            catch (Exception ex)
            {
                //return new BadRequestObjectResult(ex.Message);
            }

            return movieResult;
        }
    }
}
