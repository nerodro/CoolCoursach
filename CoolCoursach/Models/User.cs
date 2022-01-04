using System.Collections.Generic;

namespace CoolCoursach.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Group Group { get; set; }
        public string GroupName { get; set; }
        public Facult Facult { get; set; }
        public string FacultName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
    }
}
