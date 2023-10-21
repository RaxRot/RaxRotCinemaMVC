using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Controllers
{
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var allActors = _unitOfWork.Actor.GetAll().ToList();
            return View(allActors);
        }
    }
}
