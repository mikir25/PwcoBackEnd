using Microsoft.EntityFrameworkCore;
using Projekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class MovieSeeder
    {
        private readonly MovieDbContext _dbContext;

        public MovieSeeder(MovieDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();

                if(pendingMigrations !=null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }


                if (!_dbContext.Movie.Any())
                {
                    var movies = GetMovie();
                    _dbContext.Movie.AddRange(movies);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Movie> GetMovie()
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Title = "Title1",
                    Type = "Type1",
                    Director = "Director1",
                    Duration = 1,
                    Rating = 1,
                    Description = "Description1",
                    Actors = "Actors1",
                    DateAdded ="DateAdded1"
                },
                new Movie()
                {
                    Title = "Title2",
                    Type = "Type2",
                    Director = "Director2",
                    Duration = 2,
                    Rating = 2,
                    Description = "Description2",
                    Actors = "Actors2",
                    DateAdded ="DateAdded2"
                },
            };
            return movies;
        }
    }
}
