using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoolCoursach.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public SelectList Groups { get; set; }
        public SelectList Facults { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
    }
}
