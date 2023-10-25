using Microsoft.AspNetCore.Mvc;
using RaxRotCinema.Models;
using RaxRotCinema.Repo.IRepository;
using RaxRotCinema.Helper;
using Microsoft.AspNetCore.Authorization;

namespace RaxRotCinema.Controllers
{
   
    public class ActorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var allActors = _unitOfWork.Actor.GetAll().ToList();
            return View(allActors);
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
        public IActionResult Create([Bind("ProfilePictureURL,FullName,ShortBio,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(actor);
            }

            TempData[TagManager.ToastrSuccess] = "Created successfully";
            _unitOfWork.Actor.Add(actor);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Actor actor = _unitOfWork.Actor.Get(x=>x.Id== id);
            if (actor == null)
            {
                return View(nameof(NotFound));
            }

            return View(actor);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProfilePictureURL,FullName,ShortBio,Bio,Id")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                TempData[TagManager.ToastrError] = "Error";
                return View(actor);
            }

            TempData[TagManager.ToastrSuccess] = "Updated successfully";
            _unitOfWork.Actor.Update(actor);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Actor actor = _unitOfWork.Actor.Get(x => x.Id == id);
            if (actor == null)
            {
                return View(nameof(NotFound));
            }

            return View(actor);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
           
            Actor actorToDelete = _unitOfWork.Actor.Get(x => x.Id == id);

            if (actorToDelete == null)
            {
                TempData[TagManager.ToastrError] = "Error";
                return NotFound();
            }


            TempData[TagManager.ToastrSuccess] = "Deleted successfully";

            _unitOfWork.Actor.Remove(actorToDelete);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            var actor = _unitOfWork.Actor.Get(x => x.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }
    }
}
