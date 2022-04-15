using CoolCoursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCoursach.Controllers
{
    public class AdminController : Controller
    {
        GeneralContext _context;
        public AdminController(GeneralContext context)
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
        [Authorize(Roles = "admin")]
        public IActionResult AdminPage()
        {
            return View();
        }
        public IActionResult PersonsList()
        {
            return View(_context.Users.ToList());
        }

        [HttpGet]
        public IActionResult AddPerson()
        {
            SelectList roles = new SelectList(_context.Roles, "Id", "Name");
            ViewBag.Roles = roles;
            SelectList groups = new SelectList(_context.Groups, "Name", "Name");
            ViewBag.Groups = groups;
            SelectList facults = new SelectList(_context.Facults, "Name", "Name");
            ViewBag.Facults = facults;
            SelectList status = new SelectList(_context.Statuss, "Name", "Name");
            ViewBag.Status = status;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(UserViewModel user)
        {
            User users = new User
            {
                Email = user.Email,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Password = user.Password,
                GroupName = user.GroupName,
                FacultName = user.FacultName,
                RoleId = user.RoleId,
                StatusName = user.StatusName,
                FatherSurname = user.FatherSurname,
                FatherName = user.FatherName,
                MotherName = user.MotherName,
                MotherSurname = user.MotherSurname,
                ReletedName = user.ReletedName,
                ReletedSurname = user.ReletedSurname
            };
            if (user.Photo != null)
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
            return RedirectToAction("PersonsList", "Admin");
        }
        [HttpGet]
        public IActionResult EditPersonAsync(int? Id)
        {
            FindId(Id: Id);
            User user = _context.Users.Find(Id);
            if (user != null)
            {
                SelectList roles = new SelectList(_context.Roles, "Id", "Name");
                ViewBag.Roles = roles;
                SelectList groups = new SelectList(_context.Groups, "Name", "Name");
                ViewBag.Groups = groups;
                SelectList facults = new SelectList(_context.Facults, "Name", "Name");
                ViewBag.Facults = facults;
                SelectList status = new SelectList(_context.Statuss, "Name", "Name");
                ViewBag.Facults = status;
                //Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                return View(user);
            }
            return RedirectToAction("PersonsList", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> EditPerson(User user, UserViewModel userss)
        {
            if (userss != null)
            {
                byte[] imageData = user.Photo;
                using (var binaryReader = new BinaryReader(userss.Photo.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)userss.Photo.Length);
                }
                user.Photo = imageData;
            }
            else
            {
                user.Photo = user.Photo;
            }
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction("PersonsList", "Admin");
        }

        [HttpGet]
        public IActionResult DeletePerson(int? Id)
        {
            User user = _context.Users.Find(Id);
            FindId(Id: Id);
            return View(user);
        }
        [HttpPost, ActionName("DeletePerson")]
        public IActionResult DeleteConfirm(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("PersonsList", "Admin");
        }
        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
