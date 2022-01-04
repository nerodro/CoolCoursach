using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CoolCoursach.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
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
