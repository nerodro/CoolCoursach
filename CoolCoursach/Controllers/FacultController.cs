using CoolCoursach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCoursach.Controllers
{
    public class FacultController : Controller
    {
        GeneralContext _context;
        public FacultController(GeneralContext context)
        {
            _context = context;
        }
        [HttpGet]
        public void FindId(int? Id)
        {
            if (Id == null)
            {
                HttpNotFound();
            }
        }

        public IActionResult ListFacult()
        {
            return View(_context.Facults.ToList());
        }

        [HttpGet]
        public IActionResult AddFacultAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFacult(Facult facult)
        {
            _context.Facults.Add(facult);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListFacult", "Facult");
        }
        [HttpGet]
        public IActionResult EditFacult(int? Id)
        {
            FindId(Id: Id);
            Facult facult = _context.Facults.Find(Id);
            if (facult != null)
            {
                return View(facult);
            }
            return RedirectToAction("ListFacult", "Facult");
        }
        [HttpPost]
        public IActionResult EditFacult(Facult facult)
        {
            _context.Entry(facult).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ListFacult", "Facult");
        }

        [HttpGet]
        public IActionResult DeleteFacult(int? Id)
        {
            Facult facult = _context.Facults.Find(Id);
            FindId(Id: Id);
            return View(facult);
        }
        [HttpPost, ActionName("DeleteFacult")]
        public IActionResult DeleteConfirm(Facult facult)
        {
            _context.Facults.Remove(facult);
            _context.SaveChanges();
            return RedirectToAction("ListFacult", "Facult");
        }
        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }

    }
}
