using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Helper;
using RaxRotCinema.Models;
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var allProducers = _unitOfWork.Producer.GetAll().ToList();
            return View(allProducers);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProfilePictureURL,FullName,ShortBio,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(producer);
            }

            TempData[TagManager.ToastrSuccess] = "Created successfully";
            _unitOfWork.Producer.Add(producer);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Producer producer = _unitOfWork.Producer.Get(x => x.Id == id);
            if (producer == null)
            {
                return View(nameof(NotFound));
            }

            return View(producer);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProfilePictureURL,FullName,ShortBio,Bio,Id")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(producer);
            }

            TempData[TagManager.ToastrSuccess] = "Updated successfully";
            _unitOfWork.Producer.Update(producer);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Producer producer = _unitOfWork.Producer.Get(x => x.Id == id);
            if (producer == null)
            {
                return View(nameof(NotFound));
            }

            return View(producer);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {

            Producer producerToDelete = _unitOfWork.Producer.Get(x => x.Id == id);

            if (producerToDelete == null)
            {
                TempData[TagManager.ToastrError] = "Error";
                return NotFound();
            }


            TempData[TagManager.ToastrSuccess] = "Deleted successfully";

            _unitOfWork.Producer.Remove(producerToDelete);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var producer = _unitOfWork.Producer.Get(x=>x.Id==id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }
    }
}
