using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(AutoMapper.Mapper.Map<Movie, MovieDto>));
        }

        public IHttpActionResult GetMovieById(int id)
        {
            var Movie = _context.Movies.SingleOrDefault(e => e.Id == id);
            if (Movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(Movie));
        }
        [HttpPost]
        public IHttpActionResult CreateCustomer(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var Movie = Mapper.Map<MovieDto, Movie>(movieDto);
                _context.Movies.Add(Movie);
                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri + "/" + Movie.Id), Movie);
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer([FromUri]int id,[FromBody] MovieDto movieDto)
        {
            var movieInDB = _context.Movies.SingleOrDefault(e => e.Id == id);
            if (movieInDB == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDB);

            //Or

            //customerInDB.Name = MovieDto.Name;
            //customerInDB.Birthdate = MovieDto.Birthdate;
            //customerInDB.IsSubscribedToNewsLetter = MovieDto.IsSubscribedToNewsLetter;
            //MovieDto.MembershipTypeId = customerDto.MembershipTypeId;
            _context.SaveChanges();
            return Ok(movieInDB);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var cust = _context.Movies.SingleOrDefault(e => e.Id == id);
            if (cust == null)
                return BadRequest();

            _context.Movies.Remove(cust);
            _context.SaveChanges();
            return Ok(cust);
        }
    }
}
