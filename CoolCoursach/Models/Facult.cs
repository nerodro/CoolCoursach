using System.Collections.Generic;

namespace CoolCoursach.Models
{
    public class Facult
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public List<User> Users { get; set; }
        public Facult()
        {
            Users = new List<User>();
        }
    }
}
