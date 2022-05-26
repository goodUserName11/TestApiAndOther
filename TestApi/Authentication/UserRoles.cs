using TestApi.Data;
using TestApi.Entity;

namespace TestApi.Authentication
{
    public class UserRoles
    {
        public  static readonly Role Admin;
        public static readonly Role Moderator;
        public static readonly Role User;

        static UserRoles()
        {
            using (SearchAndRangeContext context = new())
            {
                Admin = context.Roles.FirstOrDefault(role => role.Name == "admin");
                Moderator = context.Roles.FirstOrDefault(role => role.Name == "moderator");
                User = context.Roles.FirstOrDefault(role => role.Name == "user");
            }
        }
    }
}
