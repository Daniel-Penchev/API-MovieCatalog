using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalogAPI.Data.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(450)]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}
