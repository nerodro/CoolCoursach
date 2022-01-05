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
        [Authorize(Roles = "admin, moder,user")]
        public IActionResult Index(int? group, string Email, string Surname, string Patronymic)
        {
            IQueryable<User> users = _context.Users.Include(p => p.Group);
            if (group != null && group != 0)
            {
                users = users.Where(p => p.GroupId == group);
            }
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

            List<Group> groups = _context.Groups.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            groups.Insert(0, new Group { Name = "Все", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Users = users.ToList(),
                Groups = new SelectList(groups, "Name", "Name"),
                Email = Email,
                Surname = Surname,
                Patronymic = Patronymic

            };
            return View(viewModel);
            //return View(_context.Users.ToList());
        }
 
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ListUsers()
        {
            return View(_context.Users.FromSqlRaw("SELECT * FROM Users WHERE RoleId = 3").ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
