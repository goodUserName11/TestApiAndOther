using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class UserListModel
    {
        public UserListModel()
        {

        }

        public UserListModel(int id, string email, string name, string surname, string? patronimic, Guid role, string companyInn, string companyName)
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
        public int Id { get; set; }
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
