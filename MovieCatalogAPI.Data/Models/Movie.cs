using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalogAPI.Data.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(450)]
        public string Description { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }  // Дата на излизане

        [MaxLength(100)]
        public string Director { get; set; }  // Режисьор

        public decimal Rating { get; set; } // Оценка от 0 до 5

        [MaxLength(50)]
        public string Genre { get; set; } // Жанр (Action, Comedy, Horror и т.н.)

        public string ImageUrl { get; set; } // Снимка на филма

        public bool IsDeleted { get; set; } = false; // Логическо изтриване
    }
}
