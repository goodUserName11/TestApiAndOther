using System.ComponentModel.DataAnnotations;
using TestApi.Entity;

namespace TestApi.Model
{
    public class UserProfileModel
    {
        public UserProfileModel()
        {

        }

        public UserProfileModel(User dbUser)
        {
            Email = dbUser.Email;
            Name = dbUser.Name;
            Surname = dbUser.Surname;
            Patronimic = dbUser.Patronimic;
            Phone = dbUser.Phone;
            CompanyInn = dbUser.CompanyInn;
            CompanyName = dbUser.Company.CompanyName;
            CompanyAddress = dbUser.Company.Address;
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
