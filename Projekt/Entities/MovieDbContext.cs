using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Entities
{
    public class MovieDbContext : DbContext
    {       
        public DbSet<Movie> Movie { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(50);
        }

    }
}
