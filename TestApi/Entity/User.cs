using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("User")]
    public class User
    {
        //public User(string login, string password, bool isAdmin)
        //{
        //    Login = login;
        //    Password = password;
        //    IsAdmin = isAdmin;
        //}

        [Key]
        public Guid Id { get; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronimic { get; set; }
        public bool IsAdmin { get; set; }
    }
}
