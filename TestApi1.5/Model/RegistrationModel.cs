using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class RegistrationModel
    {
        public RegistrationModel()
        {

        }

        public RegistrationModel(string email, string password, string name, string surname, string patronimic,
            string phone, string companyInn, string companyName,
            string companyAdress)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Phone = phone;
            CompanyInn = companyInn;
            CompanyName = companyName;
            CompanyAddress = companyAdress;
        }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CompanyInn { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
    }
}
