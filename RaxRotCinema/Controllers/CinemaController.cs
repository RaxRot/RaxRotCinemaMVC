using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CinemaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var allCinemas = _unitOfWork.Cinema.GetAll().ToList();
            return View(allCinemas);
        }
    }
}
