using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;

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
            using (var db = new SearchAndRangeContext())
            {
                //users = db.Users
                //    .ToList();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetOne(Guid id)
        {
            User? user = null;

            using (var db = new SearchAndRangeContext())
            {
                //user = db.Users
                //    .FirstOrDefault(u => u.Id == id);
            }

            if (user == null) NotFound();
            return Ok(user);
        }
    }
}
