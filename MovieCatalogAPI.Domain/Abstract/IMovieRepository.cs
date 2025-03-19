using MovieCatalogAPI.DTO.Models.Movie;

namespace MovieCatalogAPI.Domain.Abstract
{
    public interface IMovieRepository
    {
        MovieModel GetMovieModel();
    }
}
