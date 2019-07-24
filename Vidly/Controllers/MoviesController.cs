using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }

        //    if (string.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }

        //    return Content($"pageIndex: {pageIndex} sortBy: {sortBy}");
        //}
        ////[Route("movies/random/:regex(\\d{1,3})")]

        //public ActionResult Index()
        //{
        //    var movie = new Movie() {Name = "Sherk"};


        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie
        //    };
        //    return View(movie);
        //}

        private ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var movie = _context.Movies.Include(e=>e.Genre).ToList();
            return View(movie);
        }


        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
           

            return View("New",viewModel);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (ModelState.IsValid)
            {


                if (movie.Id == 0)
                {
                    _context.Movies.Add(movie);
                }
                else
                {
                    var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);
                    movieInDB.Name = movie.Name;
                    movieInDB.ReleaseDate = movie.ReleaseDate;
                    movieInDB.Genre = movie.Genre;
                    movieInDB.NumberInStock = movie.NumberInStock;
                }

                _context.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            else
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("New", viewModel);
            }
        }

        public ActionResult Edit(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDB == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movieInDB,
                Genres = _context.Genres.ToList()
            };

            return View("New", viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).FirstOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        } 
             

        //public IEnumerable<Movie> Movies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie() {Id=1, Name = "Ramy Atef"},
        //        new Movie() {Id=2, Name = "Mena Hany"}
        //    };
        //}

    }
}