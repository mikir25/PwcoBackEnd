using Microsoft.AspNetCore.Mvc;
using Projekt.Entities;
using Projekt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Controllers
{
    [Route("api/movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieSevice movieSevice;

        public MovieController(IMovieSevice movieSevice)
        {
            this.movieSevice = movieSevice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            var movies = movieSevice.GetAll();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> Get([FromRoute]int id)
        {
            var movie = movieSevice.GetById(id);

            if (movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult CreateMovie([FromBody] Movie movie)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = movieSevice.Create(movie);
            return Created($"/api/movie/{id}",null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isNull = movieSevice.Delete(id);
            if (isNull) 
                return NotFound();

            return NoContent();

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Movie movie, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = movieSevice.Update(id, movie);

            if (!isUpdated)
                return NotFound();

            return Ok();

        }
    }
}
