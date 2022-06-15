using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class ChangePasswordModel
    {
        public ChangePasswordModel()
        {

        }

        public ChangePasswordModel(string id, string password, string newPassword)
        {
            Id = id;
            Password = password;
            NewPassword = newPassword;
        }

        [Required]
        public string Id { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
