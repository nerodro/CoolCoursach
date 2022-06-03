using ClosedXML.Excel;
using CoolCoursach.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace CoolCoursach.Controllers
{
    public class HomeController : Controller
    {

        GeneralContext _context;
        public HomeController(GeneralContext context)
        {
            _context = context;
        }
        [HttpGet]
        private void FindId(int? Id)
        {
            if (Id == null)
            {
                HttpNotFound();
            }
        }

        [Authorize(Roles = "admin, moder,user")]
        public IActionResult Index(string group, string facult, string Email, string Surname, string Patronymic/*,string facult*/, string Passport, 
            string PhoneNumber, string Birthday)
        {
            IQueryable<User> users = _context.Users.Include(p => p.Role);
            //if (!String.IsNullOrEmpty(group))
            //{
            //    users = users.Where(p => p.GroupName == group);
            //}
            //if (!String.IsNullOrEmpty(facult))
            //{
            //    users = users.Where(p => p.FacultName == facult);
            //}
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
            if (!String.IsNullOrEmpty(Passport))
            {
                users = users.Where(p => p.Passport.Contains(Passport));
            }
            if (!String.IsNullOrEmpty(PhoneNumber))
            {
                users = users.Where(p => p.PhoneNumber.Contains(PhoneNumber));
            }
            if (!String.IsNullOrEmpty(Birthday))
            {
                users = users.Where(p => p.BirthDay.Contains(Birthday));
            }

            List<Group> groups = _context.Groups.ToList();
            List<Facult> facults = _context.Facults.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех

            //groups.Insert(0, new Group { Name = "Все", Id = 0 });
            //facults.Insert(0, new Facult { Name = "Все", Id = 0 });

            UserListViewModel viewModel = new UserListViewModel
            {
                Users = users.ToList(),
                //Groups = new SelectList(groups, "Name", "Name"),
                Email = Email,
                //Groups = group,
                Surname = Surname,
                Patronymic = Patronymic,
                Passport = Passport,
                PhoneNumber = PhoneNumber,
                BirthDay = Birthday,
                //Facults = new SelectList(facults, "Name", "Name")

            };
            return View(viewModel);
        }

        [Authorize(Roles = "admin, moder,user")]
        public IActionResult Export(int? Id)
        {
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Student");
                var currentRow = 1;

                worksheet.Cell("A1").Value = "Имя";
                worksheet.Cell("B1").Value = "Фамилия";
                worksheet.Cell("C1").Value = "Отчество";
                worksheet.Cell("D1").Value = "Паспортные данные";
                worksheet.Cell("E1").Value = "Номер телефона";
                worksheet.Cell("F1").Value = "Адрес проживания";
                worksheet.Cell("G1").Value = "Имя матери";
                worksheet.Cell("H1").Value = "Фамилия матери";
                worksheet.Cell("I1").Value = "Номер телефона матери";
                worksheet.Cell("J1").Value = "Место работы матери";
                worksheet.Cell("K1").Value = "Должность матери";
                worksheet.Cell("L1").Value = "Имя отца";
                worksheet.Cell("M1").Value = "Фамилия отца";
                worksheet.Cell("N1").Value = "Номер телефона отца";
                worksheet.Cell("O1").Value = "Место работы отца";
                worksheet.Cell("P1").Value = "Должность отца";
                worksheet.Cell("Q1").Value = "Имя законного представителя";
                worksheet.Cell("R1").Value = "Фамилия законного представителя";
                worksheet.Cell("S1").Value = "Номер телефона законного представителя";
                worksheet.Cell("T1").Value = "Место работы законного представителя";
                worksheet.Cell("U1").Value = "Должность законного представителя";
                worksheet.Cell("V1").Value = "Дата рождения";
                worksheet.Cell("W1").Value = "Дата поступления";
                worksheet.Cell("X1").Value = "Дата выпуска/отчисления";
                worksheet.Cell("Y1").Value = "Группа ";
                worksheet.Cell("Z1").Value = "Специальность";
                worksheet.Cell("AA1").Value = "Статус студента";
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.FromName("MediumBlue");
                worksheet.Row(1).Style.Font.FontColor = XLColor.White;

                //нумерация строк/ столбцов начинается с индекса 1(не 0)
                foreach (var users in _context.Users)
                {
                    if (users.RoleId == 3)
                    {
                        if (users.Id == Id)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = users.Email;
                            worksheet.Cell(currentRow, 2).Value = users.Surname;
                            worksheet.Cell(currentRow, 3).Value = users.Patronymic;
                            worksheet.Cell(currentRow, 4).Value = users.Passport;
                            worksheet.Cell(currentRow, 5).Value = users.PhoneNumber;
                            worksheet.Cell(currentRow, 6).Value = users.Adress;
                            worksheet.Cell(currentRow, 7).Value = users.MotherName;
                            worksheet.Cell(currentRow, 8).Value = users.MotherSurname;
                            worksheet.Cell(currentRow, 9).Value = users.MotherPhone;
                            worksheet.Cell(currentRow, 10).Value = users.MotherWork;
                            worksheet.Cell(currentRow, 11).Value = users.MotherPosition;
                            worksheet.Cell(currentRow, 12).Value = users.FatherName;
                            worksheet.Cell(currentRow, 13).Value = users.FatherSurname;
                            worksheet.Cell(currentRow, 14).Value = users.FatherPhone;
                            worksheet.Cell(currentRow, 15).Value = users.FatherrWork;
                            worksheet.Cell(currentRow, 16).Value = users.FatherPosition;
                            worksheet.Cell(currentRow, 17).Value = users.ReletedName;
                            worksheet.Cell(currentRow, 18).Value = users.ReletedSurname;
                            worksheet.Cell(currentRow, 19).Value = users.ReletedPhone;
                            worksheet.Cell(currentRow, 20).Value = users.ReleatedWork;
                            worksheet.Cell(currentRow, 21).Value = users.ReleatedPosition;
                            worksheet.Cell(currentRow, 22).Value = users.BirthDay;
                            worksheet.Cell(currentRow, 23).Value = users.StartDate;
                            worksheet.Cell(currentRow, 24).Value = users.EndDate;
                            worksheet.Cell(currentRow, 25).Value = users.GroupName;
                            worksheet.Cell(currentRow, 26).Value = users.FacultName;
                            worksheet.Cell(currentRow, 27).Value = users.StatusName;
                        }
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"student_{DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}.xlsx"
                    };
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult PersonalCabinet(int? Id)
        {
            User user = _context.Users.Find(Id);
            FindId(Id: Id);
            Export(Id: Id);
            return View(user);
        }

        private void HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}