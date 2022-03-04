﻿using System.Collections.Generic;

namespace CoolCoursach.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Password { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public string GroupName { get; set; }
        public Facult Facult { get; set; }
       // public int FacultId { get; set; }
        public string FacultName { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public int? CourceId { get; set; }
        public Cource Cource { get; set; }
        public Status Status { get; set; }
        public string StatusName { get; set; }
        public byte[] Photo { get; set; }
        public string Passport { get; set; }

    }
}
