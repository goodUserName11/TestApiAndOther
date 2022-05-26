using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class LoginModel
    {
        public LoginModel()
        {

        }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "Нужена почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Нужен пароль")]
        public string Password { get; set; }
    }
}
