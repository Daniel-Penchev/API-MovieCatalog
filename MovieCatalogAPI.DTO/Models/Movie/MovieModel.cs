namespace MovieCatalogAPI.DTO.Models.Movie
{
    public class MovieModel
    {
        public string Search { get; set; }

        public MovieItem MovieItem { get; set; } = new MovieItem();

        public List<MovieItem> MovieItems { get; set; } = new List<MovieItem>();
    }
}
