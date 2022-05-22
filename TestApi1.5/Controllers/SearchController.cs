
using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using TestApi.Adapter;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> Get([FromQuery]string? okpd2 = null)
        {
            List<Supplier> list;

            //list = await AdapterContainer.OtcAdapter.Find(okpd2);

            return BadRequest();
        }
    }
}
