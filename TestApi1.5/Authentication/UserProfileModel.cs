using System.ComponentModel.DataAnnotations;

namespace TestApi.Authentication
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {

        }

        public UserProfileModel(string email, string name, string surname, string patronimic,
            string phone, string companyInn, string companyName,
            string companyAdress)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Phone = phone;
            CompanyInn = companyInn;
            CompanyName = companyName;
            CompanyAddress = companyAdress;
        }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Patronimic { get; set; }
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
