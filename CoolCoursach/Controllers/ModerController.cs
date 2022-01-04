using CoolCoursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCoursach.Controllers
{
    public class ModerController : Controller
    {
        GeneralContext _context;
        public ModerController(GeneralContext context)
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

        [Authorize(Roles = "admin, moder")]
        public IActionResult ModerPage()
        {
            return View(_context.Users.FromSqlRaw("SELECT * FROM Users WHERE RoleId = 3").ToList());
        }
        [HttpGet]
        public IActionResult AddStudentAsync()
        {
            //SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            //ViewBag.Roles = roles;
            SelectList groups = new SelectList(_context.Groups, "Name", "Name");
            ViewBag.Groups = groups;    
            SelectList facults = new SelectList(_context.Facults, "Name", "Name");
            ViewBag.Facults = facults;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(User user)
        {
            Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("ModerPage", "Moder");
        }
        [HttpGet]
        public async Task<IActionResult> EditStudentAsync(int? Id)
        {
            FindId(Id: Id);
            User user = _context.Users.Find(Id);
            if (user != null)
            {
                //SelectList roles = new SelectList(_context.Roles, "Id", "Name");
                //ViewBag.Roles = roles;
                SelectList groups = new SelectList(_context.Groups, "Name", "Name");
                ViewBag.Groups = groups;
                SelectList facults = new SelectList(_context.Facults, "Name", "Name");
                ViewBag.Facults = facults;
                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                return View(user);
            }
            return RedirectToAction("ModerPage", "Moder");
        }
        [HttpPost]
        public IActionResult EditStudent(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ModerPage", "Moder");
        }

        [HttpGet]
        public IActionResult DeleteStudent(int? Id)
        {
            User user = _context.Users.Find(Id);
            FindId(Id: Id);
            return View(user);
        }
        [HttpPost, ActionName("DeleteStudent")]
        public IActionResult DeleteConfirm(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("ModerPage", "Moder");
        }

        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
