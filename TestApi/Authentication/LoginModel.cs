using System.ComponentModel.DataAnnotations;

namespace TestApi.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Нужена почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Нужен пароль")]
        public string Password { get; set; }
    }
}
