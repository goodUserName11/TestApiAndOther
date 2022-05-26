using Microsoft.AspNetCore.Identity;

namespace TestApi.Model
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {

        }

        public ApplicationUser(Guid id, string role, string token)
        {
            Id = id;
            Role = role;
            Token = token;
        }

        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
