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
        public string MotherName { get; set; }
        public string MotherSurname { get; set; }
        public string FatherName { get; set; }
        public string FatherSurname { get; set; }
        public int RoleId { get; set; }
        public string BirthDay { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
