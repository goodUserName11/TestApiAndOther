using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;
using System.Security.Cryptography;
using System.Text;
using TestApi.Authentication;
using TestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<List<User>>> Get()
        //{
        //    List<User> users = new List<User>();
        //    using (var context = new SearchAndRangeContext())
        //    {
        //        users = context.Users.ToList();

        //        await context.DisposeAsync();
        //    }

        //    return Ok(users);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetOne(string id)
        {
            User? user = null;

            using (var context = new SearchAndRangeContext())
            {
                user = context.Users.Include(u => u.Company).FirstOrDefault(u => u.Id == Guid.Parse(id));

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
                Guid defaultRole = UserRoles.User.Id;

                List<Coefficient> coefficients;

                if ((context.Users.FirstOrDefault(user => user.Email == requestUser.Email)) != null)
                    return BadRequest(new { ErrorMessage = "Такой пользователь уже существует" });

                if ((await context.Companies.FindAsync(requestUser.CompanyInn)) is null)
                {
                    context.Companies.Add(
                        new UserCompany(requestUser.CompanyInn, requestUser.CompanyName, requestUser.CompanyAddress));
                    context.SaveChanges();

                    coefficients =
                    context.Coefficients
                    .Include(c => c.Type)
                    .Where(c => c.Name.Contains(""))
                    .ToList();

                    foreach (var coefficient in coefficients)
                    {
                        context.CoefficientValues.Add(
                            new CoefficientValue() 
                            { 
                                CoefficientId = coefficient.Id,
                                CompanyInn = requestUser.CompanyInn
                            }
                        );
                    }

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

            ApplicationUser appUser = new(dbUser.Id, role.Name, "Token");

            return Ok(appUser);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            
            return Ok();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel RequestUser)
        {
            User dbUser;

            if (string.IsNullOrEmpty(RequestUser.Id))
                return BadRequest(new { errorMessage = "ошибка сервера?" });

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.Find(Guid.Parse(RequestUser.Id)));

                if (dbUser == null)
                    return BadRequest(new { ErrorMessage = "Не авторизован" });

                var hashAlgorithm = MD5.Create();
                var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(RequestUser.Password));

                if (!passwordHash.SequenceEqual(dbUser.Password))
                    return BadRequest(new { ErrorMessage = "Не верный пароль" });

                var newPasswordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(RequestUser.NewPassword));

                dbUser.Password = newPasswordHash;

                await context.SaveChangesAsync();

                await context.DisposeAsync();
            }

            return Ok();
        }

        [HttpPost("changeUserProfile")]
        public async Task<IActionResult> ChangeUserProfile([FromBody] ChangeUserProfileModel RequestUser)
        {
            User dbUser;

            if (string.IsNullOrEmpty(RequestUser.Id))
                return BadRequest(new { errorMessage = "ошибка сервера?" });

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.Find(Guid.Parse(RequestUser.Id)));

                if (dbUser == null)
                    return BadRequest(new { ErrorMessage = "Не авторизован" });

                dbUser.Surname = RequestUser.Surname;
                dbUser.Name = RequestUser.Name;
                dbUser.Patronimic = RequestUser.Patronimic;
                dbUser.Email = RequestUser.Email;
                dbUser.Phone = RequestUser.Phone;


                await context.SaveChangesAsync();

                await context.DisposeAsync();
            }

            return Ok();
        }

        [HttpGet("GetUserList")]
        public async Task<ActionResult<List<UserListModel>>> GetUserList([FromQuery]string? userId)
        {
            List<User> users = new List<User>();
            List<UserListModel> resUsers = new List<UserListModel>();
            User dbUser;

            if (string.IsNullOrEmpty(userId))
                return BadRequest(new { errorMessage = "ошибка сервера?" });

            using (var context = new SearchAndRangeContext())
            {
                dbUser = context?.Users?.Find(Guid.Parse(userId));
                

                if(dbUser == null)
                    return BadRequest(new { errorMessage = "Не авторизаван" });

                if(dbUser.Role != UserRoles.Moderator.Id && dbUser.Role != UserRoles.Admin.Id)
                    return Unauthorized(new { errorMessage = "Не авторизаван" });

                if(dbUser.Role == UserRoles.Moderator.Id)
                    users = context.Users
                        .Include(u => u.Company)
                        .Include(u => u.RoleRole)
                        .Where(u => u.CompanyInn == dbUser.CompanyInn)
                        .Where(u => u.Id != dbUser.Id)
                        .ToList();

                else if (dbUser.Role == UserRoles.Admin.Id)
                    users = context.Users
                        .Include(u => u.Company)
                        .Include(u => u.RoleRole)
                        .Where(u => u.Id != dbUser.Id)
                        .ToList(); ;

                await context.DisposeAsync();
            }

            foreach (var user in users)
            {
                resUsers.Add(new UserListModel(user));
            }

            return Ok(resUsers);
        }

        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeRoleModel requestUser)
        {
            User dbUser;
            User changingUser;

            if (string.IsNullOrEmpty(requestUser.UserId) || string.IsNullOrEmpty(requestUser.ChangingId))
                return BadRequest(new { errorMessage = "ошибка сервера?" });

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.Find(Guid.Parse(requestUser.UserId)));

                if (dbUser == null)
                    return BadRequest(new { ErrorMessage = "Не авторизован" });

                if (dbUser.Role != UserRoles.Moderator.Id && dbUser.Role != UserRoles.Admin.Id)
                    return Unauthorized(new { errorMessage = "Не авторизаван" });

                changingUser = (context?.Users?.Find(Guid.Parse(requestUser.ChangingId)));

                if(changingUser == null)
                    return BadRequest(new { ErrorMessage = "Пользователя не существует" });

                if (changingUser.Role == UserRoles.Moderator.Id)
                    changingUser.Role = UserRoles.User.Id;
                else changingUser.Role = UserRoles.Moderator.Id;


                await context.SaveChangesAsync();

                await context.DisposeAsync();
            }

            return Ok();
        }
    }
}
