using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using TestApi.Adapter;
using TestApi.Parser;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> Get([FromQuery]string? okpd2 = null)
        {
            List<Supplier> list = new();

            if(okpd2 is null)
                return NotFound();

            var ad = new SupplierSearchAdapter();

            await ad.Find(okpd2);

            return BadRequest();
        }
    }
}
