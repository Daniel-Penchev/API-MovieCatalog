using MovieCatalogAPI.Data;
using MovieCatalogAPI.Data.Models;
using MovieCatalogAPI.Domain.Abstract;
using MovieCatalogAPI.DTO.Models.Movie;

namespace MovieCatalogAPI.Domain.Concrete
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDataContext context;

        public MovieRepository(MovieDataContext context)
        {
            this.context = context;
        }

        public MovieModel GetMovieModel()
        {
            MovieModel model = new MovieModel();

            List<Movie> movies = context.Movies.Where(x=>x.IsDeleted == false).ToList();

            foreach (Movie movie in movies)
            {
                MovieItem movieItem  = new MovieItem();

                movieItem.Id = movie.Id;

                movieItem.Name = movie.Name;

                movieItem.Description = movie.Description;

                model.MovieItems.Add(movieItem);
            }

            return model;
        }
    }
}
