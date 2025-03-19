using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Domain.Abstract;
using MovieCatalogAPI.DTO.Models.Movie;

namespace MovieCatalogAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("CorePolicy")]
    public class MovieController : ControllerBase
    {
         private readonly IMovieRepository movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        public MovieModel GetMovies()
        {
            MovieModel model = movieRepository.GetMovieModel();

            return model;
        }
    }
}
