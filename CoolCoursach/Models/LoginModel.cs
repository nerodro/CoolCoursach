using System.ComponentModel.DataAnnotations;

namespace CoolCoursach.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Не указано Имя")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
