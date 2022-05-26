using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;
using System.Security.Cryptography;
using System.Text;
using TestApi.Authentication;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TestApi.Model;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> _logger;
        IConfiguration _configuration;
        IUserService _userService;

        public UserController(ILogger<UserController> logger, IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            List<User> users = new List<User>();
            using (var context = new SearchAndRangeContext())
            {
                users = context.Users.ToList();

                await context.DisposeAsync();
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetOne(Guid id)
        {
            User? user = null;

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.FirstOrDefault(u => u.Id == id);

                await context.DisposeAsync();
            }

            if (user == null) 
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]RegistrationModel requestUser)
        {
            using (SearchAndRangeContext context = new())
            {
                Guid defaultRole = Guid.Parse("62aee459-6fd9-44ef-bb8d-696ead00b01a");

                if ((context.Users.FirstOrDefault(user => user.Email == requestUser.Email)) != null)
                    return BadRequest(new { ErrorMessage = "Такой пользователь уже существует" });

                if ((await context.Companies.FindAsync(requestUser.CompanyInn)) is null)
                {
                    context.Companies.Add(
                        new UserCompany(requestUser.CompanyInn, requestUser.CompanyName, requestUser.CompanyAddress));
                    context.SaveChanges();
                }

                var hashAlgorithm = MD5.Create();
                var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(requestUser.Password));

                var newUser = new User(passwordHash, requestUser.Name, 
                    requestUser.Surname, requestUser.Patronimic, requestUser.Email, 
                    requestUser.Phone, defaultRole, requestUser.CompanyInn);

                context.Users.Add(newUser);

                context.SaveChanges();
                await context.DisposeAsync();
            }

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<ActionResult<ApplicationUser>> SignIn([FromBody]LoginModel RequestUser)
        {
            var iser = _userService.Authenticate(RequestUser);
            User dbUser;
            Role role;

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.FirstOrDefault(user => user.Email == RequestUser.Email));

                if (dbUser == null)
                    return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

                role = await context.Roles.FindAsync(dbUser.Role);

                await context.DisposeAsync();
            }

            

            var hashAlgorithm = MD5.Create();
            var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(RequestUser.Password));

            if (!passwordHash.SequenceEqual(dbUser.Password))
                return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

            CreateToken(dbUser);

            ApplicationUser appUser = new(dbUser.Id, role.Name, iser.Token);

            return Ok(appUser);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            
            return Ok();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Secret").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
