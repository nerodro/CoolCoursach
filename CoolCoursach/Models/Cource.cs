using System.Collections.Generic;

namespace CoolCoursach.Models
{
    public class Cource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Cource()
        {
            Users = new List<User>();
        }
    }
}
