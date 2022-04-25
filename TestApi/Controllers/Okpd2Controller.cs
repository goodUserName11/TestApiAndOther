using Microsoft.AspNetCore.Mvc;
using TestApi.Data;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Okpd2Controller : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Okpd2>>> Get([FromQuery]int? top)
        {
            List<Okpd2> okpd2s;
            using (var dbContext = new SearchAndRangeContext())
            {
                if(top == null)
                    okpd2s = dbContext.Okpd2s.ToList();
                else
                    okpd2s = dbContext.Okpd2s.Take(top.Value).ToList();
            }
            return Ok(okpd2s);
        }
    }
}
