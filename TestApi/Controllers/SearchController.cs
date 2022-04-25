
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
        public async Task<ActionResult<List<Supplier>>> Get([FromQuery]string? searchQuerry = null)
        {
            //OtcParcerAdapter otc = new OtcParcerAdapter();

            //var list = await otc.Find(searchQuerry);



            return BadRequest();
        }
    }
}
