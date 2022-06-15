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
            Role = user.Role;
            CompanyInn = user.CompanyInn;
            CompanyName = user.Company.CompanyName;
        }

        public UserListModel(Guid id, string email, string name, string surname, string? patronimic, Guid role, string companyInn, string companyName)
        {
            Id = id;
            Email = email;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Role = role;
            CompanyInn = companyInn;
            CompanyName = companyName;
        }

        [Required]
        public Guid Id { get; set; }
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        [Required]
        public Guid Role { get; set; }

        [Required]
        public string CompanyInn { get; set; }
        [Required]
        public string CompanyName { get; set; }
    }
}
