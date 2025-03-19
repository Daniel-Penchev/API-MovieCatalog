using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Data.Identity;
using MovieCatalogAPI.Data.Models;

namespace MovieCatalogAPI.Data
{
    public class MovieDataContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDataContext() { }

        public MovieDataContext(DbContextOptions<MovieDataContext> options)
        : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
