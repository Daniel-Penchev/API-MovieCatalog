using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Data.Identity;
using MovieCatalogAPI.Data.Models;
using MovieCatalogAPI.Domain.Abstract;

namespace MovieCatalogAPI.Domain.Concrete
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory scopeFactory;

        private MovieDataContext context;

        public DbInitializer(MovieDataContext context, IServiceScopeFactory scopeFactory)
        {
            this.context = context;

            this.scopeFactory = scopeFactory;
        }

        public async Task Initialize()
        {
            try
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    //if (!context.Movies.Any())
                    //{
                    //    List<Movie> movies = new List<Movie>
                    //    {
                    //        new Movie{ Name ="Rambo 1", Description="Rambo 1 description"},
                    //        new Movie{ Name ="Rambo 2", Description="Rambo 2 description"},
                    //        new Movie{ Name ="Rambo 3", Description="Rambo 3 description"}
                    //    };

                    //    await context.AddRangeAsync(movies);

                    //    await context.SaveChangesAsync();
                    //}

                    if (!context.Users.Any())
                    {
                        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                        var user = new ApplicationUser()
                        {
                            Email = "krasen_kostadinov@abv.bg",
                            UserName = "krasen_kostadinov@abv.bg"
                        };

                        var result = await userManager.CreateAsync(user, "Dragon1128");

                        if (result.Succeeded)
                        {
                            user.EmailConfirmed = true;

                            await userManager.UpdateAsync(user);
                        }
                    }



                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
