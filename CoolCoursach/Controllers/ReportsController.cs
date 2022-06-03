using ClosedXML.Excel;
using CoolCoursach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoolCoursach.Controllers
{
    public class ReportsController : Controller
    {
        GeneralContext _context;
        public ReportsController(GeneralContext context)
        {
            _context = context;
        }
        public IActionResult AllReports()
        {
            return View();
        }
        public IActionResult ExportAll()
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
                        for (int i = 2; i <= currentRow; i++)
                        {
                            if (i % 2 == 0)
                            {
                                worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.25);
                            }
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
                        FileDownloadName = $"All_students_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        public IActionResult ExportActive()
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
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1);
                worksheet.Row(1).Style.Font.FontColor = XLColor.White;

                //нумерация строк/ столбцов начинается с индекса 1(не 0)
                foreach (var users in _context.Users)
                {
                    if (users.RoleId == 3)
                    {
                        if (users.StatusName == "Учится")
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
                            for (int i = 2; i <= currentRow; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                                }
                            }
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
                        FileDownloadName = $"Studing_students_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        public IActionResult ExportInactive()
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
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1);
                worksheet.Row(1).Style.Font.FontColor = XLColor.White;

                //нумерация строк/ столбцов начинается с индекса 1(не 0)
                foreach (var users in _context.Users)
                {
                    if (users.RoleId == 3)
                    {
                        if (users.StatusName == "Выпустился")
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
                            for (int i = 2; i <= currentRow; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                                }
                            }
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
                        FileDownloadName = $"Inactive_students_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        public IActionResult ExportHalt()
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
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1);
                worksheet.Row(1).Style.Font.FontColor = XLColor.White;

                //нумерация строк/ столбцов начинается с индекса 1(не 0)
                foreach (var users in _context.Users)
                {
                    if (users.RoleId == 3)
                    {
                        if (users.StatusName == "Отчислен")
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
                            for (int i = 2; i <= currentRow; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                                }
                            }
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
                        FileDownloadName = $"Halt_students_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
        [HttpGet]
        public IActionResult FindExport(string group, string facult, string Email, string Surname, string Patronymic/*,string facult*/, string Passport, int RoleId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult FindExport() { 
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
                string Email = Request.Form["Email"];
                string Surname = Request.Form["Surname"];
                string Patronymic = Request.Form["Patronymic"];
                string GroupName = Request.Form["GroupName"];
                string FacultName = Request.Form["FacultName"];
                foreach (var userss in _context.Users)
                {
                    //users.RoleId = 3;
                    if (userss.RoleId == 3)
                    {
                        if ((userss.Email == Email) || (userss.Surname == Surname) || (userss.Patronymic == Patronymic) || (userss.GroupName == GroupName)
                            || (userss.FacultName == FacultName))
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = userss.Email;
                            worksheet.Cell(currentRow, 2).Value = userss.Surname;
                            worksheet.Cell(currentRow, 3).Value = userss.Patronymic;
                            worksheet.Cell(currentRow, 4).Value = userss.Passport;
                            worksheet.Cell(currentRow, 5).Value = userss.PhoneNumber;
                            worksheet.Cell(currentRow, 6).Value = userss.Adress;
                            worksheet.Cell(currentRow, 7).Value = userss.MotherName;
                            worksheet.Cell(currentRow, 8).Value = userss.MotherSurname;
                            worksheet.Cell(currentRow, 9).Value = userss.MotherPhone;
                            worksheet.Cell(currentRow, 10).Value = userss.MotherWork;
                            worksheet.Cell(currentRow, 11).Value = userss.MotherPosition;
                            worksheet.Cell(currentRow, 12).Value = userss.FatherName;
                            worksheet.Cell(currentRow, 13).Value = userss.FatherSurname;
                            worksheet.Cell(currentRow, 14).Value = userss.FatherPhone;
                            worksheet.Cell(currentRow, 15).Value = userss.FatherrWork;
                            worksheet.Cell(currentRow, 16).Value = userss.FatherPosition;
                            worksheet.Cell(currentRow, 17).Value = userss.ReletedName;
                            worksheet.Cell(currentRow, 18).Value = userss.ReletedSurname;
                            worksheet.Cell(currentRow, 19).Value = userss.ReletedPhone;
                            worksheet.Cell(currentRow, 20).Value = userss.ReleatedWork;
                            worksheet.Cell(currentRow, 21).Value = userss.ReleatedPosition;
                            worksheet.Cell(currentRow, 22).Value = userss.BirthDay;
                            worksheet.Cell(currentRow, 23).Value = userss.StartDate;
                            worksheet.Cell(currentRow, 24).Value = userss.EndDate;
                            worksheet.Cell(currentRow, 25).Value = userss.GroupName;
                            worksheet.Cell(currentRow, 26).Value = userss.FacultName;
                            worksheet.Cell(currentRow, 27).Value = userss.StatusName;
                            for (int i = 2; i <= currentRow; i++)
                            {
                                if (i % 2 == 0)
                                {
                                    worksheet.Row(i).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.25);
                                }
                            }
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
                        FileDownloadName = $"students_{DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}.xlsx"
                    };
                }
            }
        }
    }
}
