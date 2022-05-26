﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;
using System.Security.Cryptography;
using System.Text;
using TestApi.Authentication;
using TestApi.Model;

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
        public async Task<ActionResult<RegistrationModel>> GetOne(string id)
        {
            User? user = null;
            UserCompany? company;

            using (var context = new SearchAndRangeContext())
            {
                user = await context.Users.FindAsync(Guid.Parse(id));

                company = await context.Companies.FindAsync(user.CompanyInn);

                await context.DisposeAsync();
            }

            if (user == null) 
                return NotFound(new { ErrorMessage = "Пользователь не найден" });

            if (company == null)
                return BadRequest(new { ErrorMessage = "Компания пользователя не найдена" });

            return Ok(
                new UserProfileModel(user.Email, user.Name, user.Surname, user.Patronimic, 
                user.Phone, user.CompanyInn, company.CompanyName, company.Address)
                );
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody]RegistrationModel requestUser)
        {
            Guid role;

            using (SearchAndRangeContext context = new())
            {
                if ((context.Users.FirstOrDefault(user => user.Email == requestUser.Email)) != null)
                    return BadRequest(new { errorText = "Такой пользователь уже зарегистрирован" });

                if ((await context.Companies.FindAsync(requestUser.CompanyInn)) is null)
                {
                    context.Companies.Add(
                        new UserCompany(requestUser.CompanyInn, requestUser.CompanyName, requestUser.CompanyAddress));
                    context.SaveChanges();

                    role = UserRoles.Moderator.Id;
                }
                else
                    role = UserRoles.User.Id;

                var hashAlgorithm = MD5.Create();
                var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(requestUser.Password));

                var newUser = new User(passwordHash, requestUser.Name, 
                    requestUser.Surname, requestUser.Patronimic, requestUser.Email, 
                    requestUser.Phone, role, requestUser.CompanyInn);

                context.Users.Add(newUser);

                context.SaveChanges();
                await context.DisposeAsync();
            }

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<ActionResult<ApplicationUser>> SignIn([FromBody] LoginModel RequestUser)
        {
            User dbUser;
            Role role;

            using (SearchAndRangeContext context = new())
            {
                dbUser = (context?.Users?.FirstOrDefault(user => user.Email == RequestUser.Email));

                role = await context.Roles.FindAsync(dbUser.Role);

                await context.DisposeAsync();
            }

            if (dbUser == null)
                return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

            var hashAlgorithm = MD5.Create();
            var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(RequestUser.Password));

            if (!passwordHash.SequenceEqual(dbUser.Password))
                return BadRequest(new { ErrorMessage = "Не верный логин или пароль" });

            ApplicationUser appUser = new(dbUser.Id, role.Name, "token");

            return Ok(appUser);
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            return Ok();
        }
    }
}
