using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Projekt.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public interface IMovieSevice
    {
        int Create(Movie movie);
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
        public bool Delete(int id);
        public bool Update(int id, Movie newMovie);
    }

    public class MovieSevice : IMovieSevice
    {
        private readonly MovieDbContext dbContext;
        private readonly IMapper mapper;

        public MovieSevice(MovieDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Movie GetById(int id)
        {
            var movie = dbContext.Movie.FirstOrDefault(m => m.Id == id);

            if (movie is null)
            {
                return null;
            }

            return movie;
        }

        public IEnumerable<Movie> GetAll()
        {
            var movies = dbContext.Movie.ToList();

            return movies;
        }

        public int Create(Movie movie)
        {
            dbContext.Movie.Add(movie);
            dbContext.SaveChanges();

            return movie.Id;
        }

        public bool Delete(int id)
        {
            var movie = dbContext.Movie.FirstOrDefault(m => m.Id == id);

            if (movie is null)
            {
                return true;
            }

            dbContext.Movie.Remove(movie);
            dbContext.SaveChanges();
            return false;
        }

        public bool Update(int id, Movie newMovie)
        {
            var movie = dbContext.Movie.FirstOrDefault(m => m.Id == id);

            if (movie is null)
            {
                return false;
            }
            
            movie.Title = newMovie.Title;
            movie.Type = newMovie.Type;
            movie.Director = newMovie.Director;
            movie.Duration = newMovie.Duration;
            movie.Rating = newMovie.Rating;
            movie.Rating = newMovie.Rating;
            movie.Description = newMovie.Description;
            movie.Actors = newMovie.Actors;
            movie.DateAdded = newMovie.DateAdded;

            dbContext.SaveChanges();

            return true;
        }
    }
}
