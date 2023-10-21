using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            var allMovies = _unitOfWork.Movie.GetAll().ToList();
            return View(allMovies);
        }
    }
}
