using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestApi.Adapter;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<SupplierListModel>> GetSuppliersInList([FromQuery]string ListId)
        {
            List<SupplierInList> suppliersInList = new List<SupplierInList>();
            SupplierListModel supplierListModel = new();

            using (var dbContext = new SearchAndRangeContext())
            {
                var supplierList = dbContext.SuppliersLists.Find(Guid.Parse(ListId));

                suppliersInList = dbContext.SupplierInLists
                                            .Include(s => s.Product)
                                            .Include(s => s.Supplier)
                                            .ThenInclude(s => s.Contact)
                                            .Where(s => s.SupplierListId == supplierList.Id)
                                            .ToList();

                suppliersInList.ForEach(
                    s => supplierListModel
                        .SupplierProfileModels
                        .Add(new SupplierInListModel(s)));

                supplierListModel.Name = supplierList.Name;
                supplierListModel.Date = supplierList.Date;
            }

                return Ok(supplierListModel);
        }

        [HttpGet("{supplierId}")]
        public async Task<ActionResult<SupplierProfileModel>> GetSupplierInList(string supplierId)
        {

            SupplierInList supplier;
            SupplierProfileModel suppliermodel;

            using (var dbContext = new SearchAndRangeContext())
            {
                var id = Guid.Parse(supplierId);

                supplier = await dbContext
                    .SupplierInLists
                    .Include(s => s.Product)
                    .Include(s => s.Supplier)
                    .ThenInclude(s => s.Contact)
                    .FirstOrDefaultAsync(s => s.Id == id);

                dbContext.Dispose();
            }

            suppliermodel = new SupplierProfileModel(supplier);

            return Ok(suppliermodel);
        }

        [HttpGet("Update")]
        public async Task<ActionResult> Update()
        {
            using (var dbContext = new SearchAndRangeContext())
            {
                await AdapterContainer.SupplierSearchAdapters[1].UpdateAll();
            }

            return Ok();
        }
    }
}
