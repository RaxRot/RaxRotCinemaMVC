using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Repo.IRepository;

namespace RaxRotCinema.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProducerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var allProducers = _unitOfWork.Producer.GetAll().ToList();
            return View(allProducers);
        }
    }
}
