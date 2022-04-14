using CoolCoursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCoursach.Controllers
{
    public class ModerController : Controller
    {
        private readonly GeneralContext _context;
        public ModerController(GeneralContext context/*, IRepository repo*/)
        {
            _context = context;
            //_repository = repo;
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
            SelectList status = new SelectList(_context.Statuss, "Name", "Name");
            ViewBag.Status = status;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(UserViewModel user)
        {
            User users = new User{Email = user.Email,Surname = user.Surname,Patronymic = user.Patronymic,
            Password = user.Password, GroupName = user.GroupName, FacultName = user.FacultName, RoleId = 3, StatusName = user.StatusName, Passport = user.Passport,
            FatherSurname = user.FatherSurname, FatherName = user.FatherName, MotherName = user.MotherName, MotherSurname = user.MotherSurname, BirthDay = user.BirthDay,
            StartDate = user.StartDate, EndDate = user.EndDate};
            if(user.Photo != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(user.Photo.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)user.Photo.Length);
                }
                users.Photo = imageData;
            }
            _context.Users.Add(users);
            await _context.SaveChangesAsync();
            return RedirectToAction("ModerPage", "Moder");
        }
        [HttpGet]
        public IActionResult EditStudent(int? Id)
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
                SelectList status = new SelectList(_context.Statuss, "Name", "Name");
                ViewBag.Status = status;
                return View(user);
            }
            return RedirectToAction("ModerPage", "Moder");
        }
        [HttpPost]
        public IActionResult EditStudent(User user, UserViewModel userss)
        {
            //Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
            //if (userRole != null)
            //    user.Role = userRole;
            user.Photo = user.Photo;
            user.RoleId = 3;
            if (userss.Photo == null)
            {
                user.Photo = user.Photo;
            }
            else
            {
                byte[] imageData = user.Photo;
                using (var binaryReader = new BinaryReader(userss.Photo.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)userss.Photo.Length);
                }
                user.Photo = imageData;
            }
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
