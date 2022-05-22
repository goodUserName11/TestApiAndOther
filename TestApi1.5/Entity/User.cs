using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("User")]
    public class User
    {
        public User()
        {

        }

        public User(byte[] password, string? name, string? surname, string? patronimic, 
            string email, string phone, Guid role, string companyInn)
        {
            Password = password;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Email = email;
            Phone = phone;
            Role = role;
            CompanyInn = companyInn;
        }

        [Key]
        public Guid Id { get; }
        public byte[] Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronimic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid Role { get; set; }
        public string CompanyInn { get; set; }
    }
}
