using Projekt.Entities;
using Projekt.Controllers;
using Projekt.Services;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace TestBackEnd
{
    public class UnitTest
    {
        public IEnumerable<Movie> generateMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = 1,
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
                    Id = 2,
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
        public Movie generateMovie()
        {
            var movie = new Movie()
            {
                Id = 3,
                Title = "Title3",
                Type = "Type3",
                Director = "Director3",
                Duration = 3,
                Rating = 3,
                Description = "Description3",
                Actors = "Actors3",
                DateAdded = "DateAdded3"
            };

            return movie;
        }

        [Fact]
        public void TestGetAllController()
        {
            var list_movie = generateMovies();
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.GetAll()).Returns(list_movie);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.GetAll();

            var result = (OkObjectResult)actionResult.Result;            
            var result_Value = result.Value;

            Assert.Equal(list_movie, result_Value);
        }
        [Fact]
        public void TestGetController_Ok()
        {
            var movie = generateMovie();
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.GetById(3)).Returns(movie);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Get(3);

            var result = (OkObjectResult)actionResult.Result;
            var result_Value = result.Value;

            Assert.Equal(movie, result_Value);
        }
        [Fact]
        public void TestGetController_NotFound()
        {
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.GetById(4)).Returns((Movie)null);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Get(4);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        [Fact]
        public void TestPostController_Created()
        {
            var movie = generateMovie();
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Create(movie)).Returns(movie.Id);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.CreateMovie(movie);

            Assert.IsType<CreatedResult>(actionResult);

            var result = (CreatedResult)actionResult;
            var result_Value = result.Location;

            Assert.Equal("/api/movie/3", result_Value);
        }
        [Fact]
        public void TestPostController_BadRequest()
        {

            var movie = new Movie()
            {
                Director = "Director3",
                Duration = 3,
                Rating = 3,
                Description = "Description3",
                Actors = "Actors3",
                DateAdded = "DateAdded3"
            };

            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Create(movie)).Returns(movie.Id);

            var MovieController = new MovieController(MovieServiceMock.Object);
            MovieController.ModelState.AddModelError("Type", "The Type field is required.");
            MovieController.ModelState.AddModelError("Title", "The Title field is required.");

            var actionResult = MovieController.CreateMovie(movie);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        [Fact]
        public void TestDeleteController_NoContent()
        {
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Delete(3)).Returns(false);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Delete(3);

            Assert.IsType<NoContentResult>(actionResult);
        }
        [Fact]
        public void TestDeleteController_NotFound()
        {
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Delete(3)).Returns(true);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Delete(3);

            Assert.IsType<NotFoundResult>(actionResult);
        }
        [Fact]
        public void TestUpdateController_Ok()
        {
            var movie = generateMovie();
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Update(3,movie)).Returns(true);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Update(movie, 3);

            Assert.IsType<OkResult>(actionResult);
        }
        [Fact]
        public void TestUpdateController_NotFound()
        {
            var movie = generateMovie();
            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Update(3, movie)).Returns(false);

            var MovieController = new MovieController(MovieServiceMock.Object);

            var actionResult = MovieController.Update(movie, 3);

            Assert.IsType<NotFoundResult>(actionResult);
        }
        [Fact]
        public void TestUpdateController_BadRequest()
        {
            var movie = new Movie()
            {
                Director = "Director3",
                Duration = 3,
                Rating = 3,
                Description = "Description3",
                Actors = "Actors3",
                DateAdded = "DateAdded3"
            };

            var MovieServiceMock = new Mock<IMovieSevice>();
            MovieServiceMock.Setup(repo => repo.Update(3, movie)).Returns(true);

            var MovieController = new MovieController(MovieServiceMock.Object);
            MovieController.ModelState.AddModelError("Type", "The Type field is required.");
            MovieController.ModelState.AddModelError("Title", "The Title field is required.");

            var actionResult = MovieController.Update(movie, 3);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);

            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}