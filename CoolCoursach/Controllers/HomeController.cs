using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CoolCoursach.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoolCoursach.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        GeneralContext _context;
        public HomeController(GeneralContext context)
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

        [Authorize(Roles = "admin, moder,user")]
        public IActionResult Index(string group,string facult, string Email, string Surname, string Patronymic/*,string facult*/, string Passport)
        {
            IQueryable<User> users = _context.Users.Include(p => p.Group);
            //if (String.IsNullOrEmpty(group))
            //{
            //    users = users.Where(p => p.GroupName == group);
            //}
            //if (String.IsNullOrEmpty(facult))
            //{
            //    users = users.Where(p => p.FacultName == facult);
            //}
            if (!String.IsNullOrEmpty(Email))
            {
                users = users.Where(p => p.Email.Contains(Email));
            }
            if (!String.IsNullOrEmpty(Surname))
            {
                users = users.Where(p => p.Surname.Contains(Surname));
            }
            if (!String.IsNullOrEmpty(Patronymic))
            {
                users = users.Where(p => p.Patronymic.Contains(Patronymic));
            }
            if (!String.IsNullOrEmpty(Passport))
            {
                users = users.Where(p => p.Passport.Contains(Passport));
            }

            List<Group> groups = _context.Groups.ToList();
            List<Facult> facults = _context.Facults.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех

            groups.Insert(0, new Group { Name = "Все", Id = 0 });
            UserListViewModel viewModel = new UserListViewModel
            {
                Users = users.ToList(),
                //Groups = new SelectList(groups, "Name", "Name"),
                Email = Email,
                Surname = Surname,
                Patronymic = Patronymic,
                Passport = Passport,
                //Facults = new SelectList(facults, "Name", "Name")

            };
            return View(viewModel);
        }
 
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PersonalCabinet(int? Id)
        {
            User user = _context.Users.Find(Id);
            FindId(Id: Id);
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
