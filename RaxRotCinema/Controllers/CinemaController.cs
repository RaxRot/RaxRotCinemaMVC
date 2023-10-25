using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Helper;
using RaxRotCinema.Models;
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

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var allCinemas = _unitOfWork.Cinema.GetAll().ToList();
            return View(allCinemas);
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
        public IActionResult Create([Bind("Logo,Name,ShortDescription,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(cinema);
            }

            TempData[TagManager.ToastrSuccess] = "Created successfully";
            _unitOfWork.Cinema.Add(cinema);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Cinema cinema = _unitOfWork.Cinema.Get(x => x.Id == id);
            if (cinema == null)
            {
                return View(nameof(NotFound));
            }

            return View(cinema);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Logo,Name,ShortDescription,Description,Id")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(cinema);
            }

            TempData[TagManager.ToastrSuccess] = "Updated successfully";
            _unitOfWork.Cinema.Update(cinema);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Cinema cinema = _unitOfWork.Cinema.Get(x => x.Id == id);
            if (cinema == null)
            {
                return View(nameof(NotFound));
            }

            return View(cinema);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {

            Cinema cinemaToDelete = _unitOfWork.Cinema.Get(x => x.Id == id);

            if (cinemaToDelete == null)
            {
                TempData[TagManager.ToastrError] = "Error";
                return NotFound();
            }


            TempData[TagManager.ToastrSuccess] = "Deleted successfully";

            _unitOfWork.Cinema.Remove(cinemaToDelete);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var cinema = _unitOfWork.Cinema.Get(x => x.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }
    }
}
