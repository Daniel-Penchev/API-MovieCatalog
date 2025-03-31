namespace MovieCatalogAPI.DTO.Models.Movie
{
    public class MovieItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
