using System.ComponentModel.DataAnnotations;
using TestApi.Entity;

namespace TestApi.Model
{
    public class UserListModel
    {
        public UserListModel()
        {

        }

        public UserListModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            Patronimic = user.Patronimic;
            Role = user.RoleRole.Name;
            Phone = user.Phone;
            CompanyName = user.Company.CompanyName;
        }

        public UserListModel(Guid id, string email, string name, string surname, string? patronimic, 
            string role, string phone, string companyName)
        {
            Id = id;
            Email = email;
            Phone = phone;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Role = role;
            CompanyName = companyName;
        }

        [Required]
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        [Required]
        public string Role { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}
