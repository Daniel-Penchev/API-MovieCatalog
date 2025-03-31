using MovieCatalogAPI.DTO.Models.Movie;
using MovieCatalogAPI.DTO.Models.MovieDetail;

namespace MovieCatalogAPI.Domain.Abstract
{
    public interface IMovieRepository
    {
        MovieModel GetMovieModel();

        MovieDetailModel GetMovieDetailModel(int id);

        MovieResult Save(MovieItem item);

        MovieResult DeleteMovie(MovieItem item);
        MovieResult DeleteMovieImage(MovieItem item);

        MovieItem UpdateImage(int id, string fileName);
    }
}
