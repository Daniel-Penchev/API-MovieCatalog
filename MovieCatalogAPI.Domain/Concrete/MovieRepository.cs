using MovieCatalogAPI.Data;
using MovieCatalogAPI.Data.Models;
using MovieCatalogAPI.Domain.Abstract;
using MovieCatalogAPI.DTO.Models.Movie;
using MovieCatalogAPI.DTO.Models.MovieDetail;
using MovieCatalogAPI.DTO.Utility;

namespace MovieCatalogAPI.Domain.Concrete
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDataContext context;

        public MovieRepository(MovieDataContext context)
        {
            this.context = context;
        }

        public MovieResult DeleteMovie(MovieItem Item)
        {
            MovieResult result = new MovieResult();

            Movie? movie = context.Movies.Where(x => x.Id == Item.Id).FirstOrDefault();

            if (movie != null)
            {
                result.SuccessMessage = "You have successfully deleted this movie";
                movie.IsDeleted = true;
            }
            else
            {
                result.ErrorMessage = "Not found in the database";
                return result;
            }

            context.SaveChanges();

            return result;
        }

        public MovieResult DeleteMovieImage(MovieItem Item)
        {
            MovieResult result = new MovieResult();

            Movie? movie = context.Movies.Where(x => x.Id == Item.Id).FirstOrDefault();

            if (movie != null && !string.IsNullOrEmpty(movie.ImageUrl)) // Уверяваме се, че има записан файл
            {
                string relativePath = movie.ImageUrl.Replace("https://localhost:7068/", ""); // премахваме домейна
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // Изтриваме файла
                }

                // Изчистваме изображението от базата
                movie.ImageUrl = Params.NoImageFolderPath;

                result.SuccessMessage = "You have successfully deleted this movie image";
            }
            else
            {
                result.ErrorMessage = "Not found in the database";
                return result;
            }

            context.SaveChanges();

            return result;
        }

        public MovieDetailModel GetMovieDetailModel(int id)
        {
            MovieDetailModel model = new MovieDetailModel();

            Movie? movie = context.Movies.Where(x => x.Id == id).FirstOrDefault();

            if (movie != null)
            {
                MovieItem movieItem = new MovieItem();

                movieItem.Id = id;

                movieItem.Name = movie.Name;

                movieItem.Description = movie.Description;

                movieItem.ReleaseDate = movie.ReleaseDate;

                movieItem.Director = movie.Director;

                movieItem.Rating = movie.Rating;

                movieItem.Genre = movie.Genre;

                movieItem.ImageUrl = movie.ImageUrl;

                model.MovieItem = movieItem;
            }

            return model;
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

                movieItem.ReleaseDate = movie.ReleaseDate;

                movieItem.Director = movie.Director;

                movieItem.Rating = movie.Rating;

                movieItem.Genre = movie.Genre;

                movieItem.ImageUrl = movie.ImageUrl;

                model.MovieItems.Add(movieItem);
            }

            return model;
        }

        public MovieResult Save(MovieItem item)
        {
            MovieResult result = new MovieResult();

            Movie? movie = context.Movies.Where(x=>x.Id == item.Id).FirstOrDefault();
            try
            {
                if (movie != null)
                {
                    movie.Id = item.Id;

                    movie.Name = item.Name;

                    movie.Description = item.Description;

                    movie.ReleaseDate = item.ReleaseDate;

                    movie.Director = item.Director;

                    movie.Rating = item.Rating;

                    movie.Genre = item.Genre;

                    movie.ImageUrl = item.ImageUrl;

                    context.SaveChanges();

                    result.SuccessMessage = "The movie was updated successfully";
                }
                else
                {
                    movie = new Movie();

                    movie.Name = item.Name;

                    movie.Description = item.Description;

                    movie.ReleaseDate = item.ReleaseDate;

                    movie.Director = item.Director;

                    movie.Rating = item.Rating;

                    movie.Genre = item.Genre;

                    movie.ImageUrl = item.ImageUrl;

                    context.Add(movie);

                    context.SaveChanges();

                    result.SuccessMessage = "The movie was inserted successfully";
                }

                result.Id = movie.Id;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An error occurred while saving the movie: " + ex.Message;
            }
            return result;
        }

        public MovieItem UpdateImage(int id, string fileName)
        {
            MovieItem movieItem = new MovieItem();

            Movie? movie = context.Movies.Where(x => x.Id == id).FirstOrDefault();

            if (movie != null)
            {
                movie.ImageUrl = fileName;

                movieItem.Id = id;

                context.SaveChanges();

                //movieItem.Id = movie.Id;

                movieItem.Name = movie.Name;

                movieItem.Description = movie.Description;

                movieItem.ReleaseDate = movie.ReleaseDate;

                movieItem.Director = movie.Director;

                movieItem.Rating = movie.Rating;

                movieItem.Genre = movie.Genre;

                movieItem.ImageUrl = movie.ImageUrl;

            }

            return movieItem;
        }
    }
}
