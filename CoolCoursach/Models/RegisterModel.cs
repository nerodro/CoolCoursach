﻿using System.ComponentModel.DataAnnotations;

namespace CoolCoursach.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}
