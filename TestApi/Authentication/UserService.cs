namespace TestApi.Authentication
{
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using TestApi.Data;
    using TestApi.Entity;
    using TestApi.Model;

    public interface IUserService
    {
        ApplicationUser Authenticate(LoginModel model);
        public User GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        IConfiguration _configuration;
        IHttpContextAccessor _contextAccessor;

        public UserService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public ApplicationUser Authenticate(LoginModel model)
        {
            User? dbUser;

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.FirstOrDefault(user => user.Email == model.Email));

                context.Dispose();
            }

            if (dbUser == null)
                return null;

            var hashAlgorithm = MD5.Create();
            var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(model.Password));

            if (passwordHash.Union(dbUser.Password).Count() != dbUser.Password.Length)
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(dbUser);

            var user = new ApplicationUser(dbUser.Id, "role", token);

            _contextAccessor.HttpContext.Items.Add("User", user);

            return user;
        }

        public User GetById(Guid id)
        {
            User? user = null;

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == id);

                context.Dispose();
            }

            return user;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
