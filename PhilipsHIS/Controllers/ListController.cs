using Microsoft.AspNetCore.Mvc;
using PhilipsHIS.Data;
using PhilipsHIS.Models;

namespace PhilipsHIS.Controllers
{
    public class ListController : Controller
    {
        public readonly ApplicationDbContext _db;

        public ListController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult ListIndex()
        {
            IEnumerable<List> objListList = _db.Lists;
            return View(objListList);
        }

        //GET
        public IActionResult Admit()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Admit(List obj)
        {
            if (ModelState.IsValid)
            {
                _db.Lists.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Admit new patient successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(string? hn)
        {
            if (hn == null)
            {
                return NotFound();
            }
            var listFromDb = _db.Lists.Find(hn);
            if (listFromDb == null)
            {
                return NotFound();
            }
            return View(listFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List obj)
        {
            if (ModelState.IsValid)
            {
                _db.Lists.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Update patient information successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(string? hn)
        {
            if (hn == null)
            {
                return NotFound();
            }
            var listFromDb = _db.Lists.Find(hn);
            if (listFromDb == null)
            {
                return NotFound();
            }
            return View(listFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? hn)
        {
            var obj = _db.Lists.Find(hn);
            if (hn == null)
            {
                return NotFound();
            }
                _db.Lists.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Delete patient successfully";
                return RedirectToAction("Index");
        }

        public IActionResult ViewDetail()
        {
            return View();
        }

    }

}
