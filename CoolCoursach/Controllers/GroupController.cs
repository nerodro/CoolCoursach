using CoolCoursach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoolCoursach.Controllers
{
    public class GroupController : Controller
    {
        GeneralContext _context;
        public GroupController(GeneralContext context)
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

        public IActionResult ListGroup()
        {
            return View(_context.Groups.ToList());
        }
        [HttpGet]
        public IActionResult AddGroupAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListGroup", "Group");
        }
        [HttpGet]
        public IActionResult EditGroup(int? Id)
        {
            FindId(Id: Id);
            Group group = _context.Groups.Find(Id);
            if (group != null)
            {
                return View(group);
            }
            return RedirectToAction("ListGroup", "Group");
        }
        [HttpPost]
        public IActionResult EditGroup(Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ListGroup", "Group");
        }

        [HttpGet]
        public IActionResult DeleteGroup(int? Id)
        {
            Group group = _context.Groups.Find(Id);
            FindId(Id: Id);
            return View(group);
        }
        [HttpPost, ActionName("DeleteGroup")]
        public IActionResult DeleteConfirm(Group group)
        {
            _context.Groups.Remove(group);
            _context.SaveChanges();
            return RedirectToAction("ListGroup", "Group");
        }

        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
