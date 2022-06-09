using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class ChangeUserProfileModel
    {
        public ChangeUserProfileModel()
        {

        }

        public ChangeUserProfileModel(string email, string name, string surname, string patronimic,
            string phone, string id)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Phone = phone;
            Id = id;
        }

        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
