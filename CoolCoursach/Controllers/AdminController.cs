﻿using CoolCoursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(User user)
        {
            Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("PersonsList", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> EditPersonAsync(int? Id)
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
                Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                return View(user);
            }
            return RedirectToAction("PersonsList", "Admin");
        }
        [HttpPost]
        public IActionResult EditPerson(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
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
