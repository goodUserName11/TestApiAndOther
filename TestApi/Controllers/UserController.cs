using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;
using System.Security.Cryptography;
using System.Text;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
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
        public async Task<ActionResult> Add([FromBody]UserForRequest requestUser)
        {
            using (SearchAndRangeContext context = new())
            {
                Guid defaultRole = Guid.Parse("dc6f5947-6e2b-4704-857c-929d20f7b953");
                Guid? roleId = null;

                if ((context.Users.FirstOrDefault(user => user.Email == requestUser.Email)) != null)
                    return BadRequest(new { ErrorMessage = "Такой пользователь уже существует" });

                if ((await context.Companies.FindAsync(requestUser.CompanyInn)) is null)
                {
                    context.Companies.Add(
                        new UserCompany(requestUser.CompanyInn, requestUser.CompanyName, requestUser.CompanyAddress));
                    context.SaveChanges();
                }

                if (requestUser.Role != null)
                {
                    roleId = (await context.Roles.FindAsync(requestUser.Role))?.Id;

                    if (roleId == null)
                        return BadRequest(new { ErrorMessage = "Не правильные данные" });
                }
                else roleId = defaultRole;

                var hashAlgorithm = MD5.Create();
                var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(requestUser.Password));

                var newUser = new User(passwordHash, requestUser.Name, 
                    requestUser.Surname, requestUser.Patronimic, requestUser.Email, 
                    requestUser.Phone, roleId.Value, requestUser.RegistratedBy, 
                    requestUser.CompanyInn);

                context.Users.Add(newUser);

                context.SaveChanges();
                await context.DisposeAsync();
            }

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult> SignIn([FromBody]UserForRequest RequestUser)
        {
            User? dbUser;

            using (SearchAndRangeContext context = new()) 
            {
                dbUser = (context?.Users?.FirstOrDefault(user => user.Email == RequestUser.Email));

                await context.DisposeAsync();
            }

            if (dbUser == null)
                return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

            var hashAlgorithm = MD5.Create();
            var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(RequestUser.Password));

            if (passwordHash.Union(dbUser.Password).Count() != dbUser.Password.Length)
                return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

            return Ok();
        }
    }
}
