using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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
        //public IFormFile Photos { get; set; }
        public string Passport { get; set; }
        public string MotherName { get; set; }
        public string MotherSurname { get; set; }
        public string FatherName { get; set; }
        public string FatherSurname { get; set; }
        public string BirthDay { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ReletedName { get; set; }
        public string ReletedSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string MotherPhone { get; set; }
        public string FatherPhone { get; set; }
        public string ReletedPhone { get; set; }
        public string MotherWork { get; set; }
        public string MotherPosition { get; set; }
        public string FatherrWork { get; set; }
        public string FatherPosition { get; set; }
        public string ReleatedWork { get; set; }
        public string ReleatedPosition { get; set; }
    }
}
