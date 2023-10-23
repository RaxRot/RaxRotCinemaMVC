using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RaxRotCinema.Helper;
using RaxRotCinema.Models.ViewModels;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allMovies = _unitOfWork.Movie.GetAll(includeProperties:"Cinema").ToList();
            return View(allMovies);
        }

        [HttpPost]
        public IActionResult Filter(string searchString)
        {
            var allMovies = _unitOfWork.Movie.GetAll(includeProperties: "Cinema").ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterResult = allMovies.Where(x => x.Name.Contains(searchString) || x.Description.Contains(searchString)).ToList();
                if (filterResult.Count > 0)
                {
                    return View("Index", filterResult);
                }
            }
            return View("Index", allMovies);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var movieDetail=_unitOfWork.Movie.GetMovieById(id);

            return View(movieDetail);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var movieDropDownsData=_unitOfWork.Movie.GetMovieDropDownVMValues();

            ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "FullName");


            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieVM movie)
        {
            if(!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";

                var movieDropDownsData = _unitOfWork.Movie.GetMovieDropDownVMValues();

                ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            TempData[TagManager.ToastrSuccess] = "Created";

            _unitOfWork.Movie.AddNewMovie(movie);
            

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movieDetails=_unitOfWork.Movie.GetMovieById(id);
            if(movieDetails == null)
            {
                return View("NotFound");
            }

            var response = new MovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                ShortDescription = movieDetails.ShortDescription,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(x => x.ActorId).ToList()
            };

            var movieDropDownsData = _unitOfWork.Movie.GetMovieDropDownVMValues();

            ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "FullName");


            return View(response);
        }

        [HttpPost]
        public IActionResult Edit(int id,MovieVM movie)
        {
            if (id != movie.Id)
            {
                return View("NotFound");
            }

            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";

                var movieDropDownsData = _unitOfWork.Movie.GetMovieDropDownVMValues();

                ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            TempData[TagManager.ToastrSuccess] = "Created";

            _unitOfWork.Movie.UpdateNewMovie(movie);


            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movieDetails = _unitOfWork.Movie.GetMovieById(id);
            if (movieDetails == null)
            {
                return View("NotFound");
            }

            var response = new MovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                ShortDescription = movieDetails.ShortDescription,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ImageUrl = movieDetails.ImageUrl,
                ActorIds = movieDetails.Actors_Movies.Select(x => x.ActorId).ToList()
            };

            var movieDropDownsData = _unitOfWork.Movie.GetMovieDropDownVMValues();

            ViewBag.Cinemas = new SelectList(movieDropDownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropDownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownsData.Actors, "Id", "FullName");


            return View(response);
        }

        [HttpPost]
        public IActionResult Delete(int id, MovieVM movie)
        {
            if (id != movie.Id)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View("NotFound");
            }

            TempData[TagManager.ToastrSuccess] = "Deleted";

            _unitOfWork.Movie.DeleteMovie(movie);

            return RedirectToAction("Index");
        }

    }
}
