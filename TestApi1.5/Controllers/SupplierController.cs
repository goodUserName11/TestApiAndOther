using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestApi.Data;
using TestApi.Entity;

namespace TestApi1._5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<SupplierInList>>> GetSuppliersInList([FromQuery]string ListId)
        {
            List<SupplierInList> suppliersInList = new List<SupplierInList>();

            using (var dbContext = new SearchAndRangeContext())
            {
                suppliersInList = dbContext.SupplierInLists
                                            .Where(s => s.SupplierListId == Guid.Parse(ListId))
                                            .ToList();
            }

                return Ok(suppliersInList);
        }

        [HttpGet("{supplierId}")]
        public async Task<ActionResult<SupplierInList>> GetSupplierInList(string supplierId)
        {

            SupplierInList supplier;

            using (var dbContext = new SearchAndRangeContext())
            {
                var id = Guid.Parse(supplierId);

                supplier = await dbContext.SupplierInLists.Include(s => s.Supplier).FirstOrDefaultAsync(s => s.Id == id);
            }

            return Ok(supplier);
        }
    }
}
