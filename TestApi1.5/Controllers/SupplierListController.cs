using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierListController : ControllerBase
    {
        // GET: api/<SupplierListController>
        [HttpGet]
        public async Task<ActionResult<List<SupplierInList>>> GetAll([FromQuery] string userId)
        {
            var supLists = new List<SuppliersList>();

            using (var dbContext = new SearchAndRangeContext())
            {
                supLists = dbContext.SuppliersLists.Where(s => s.UserId == Guid.Parse(userId)).ToList();

                dbContext.Dispose();
            }

                return Ok(supLists);
        }

        // POST api/<SupplierListController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SupplierListAddModel supplierListModel)
        {
            using (var dbContext = new SearchAndRangeContext())
            {
                List<SupplierInList> suppliers = new List<SupplierInList>();

                

                SuppliersList suppliersList =
                    dbContext
                    .SuppliersLists
                    .FirstOrDefault
                    (
                        s =>
                        s.UserId == Guid.Parse(supplierListModel.UserId)
                        && s.Name == supplierListModel.ListName
                    );

                if (suppliersList == null)
                {
                    suppliersList = 
                        new SuppliersList() 
                        { 
                            Id = Guid.NewGuid(),
                            Date = DateTime.Now,
                            Name = supplierListModel.ListName,
                            UserId = Guid.Parse(supplierListModel.UserId)
                        };

                    dbContext.SuppliersLists.Add(suppliersList);

                    dbContext.SaveChanges();
                }

                foreach (var id in supplierListModel.SupliersId)
                {
                    dbContext.SupplierInLists.Find(Guid.Parse(id)).SupplierListId = suppliersList.Id;
                }
                dbContext.SaveChanges(true);
            }

            return Ok();
        }

        // PUT api/<SupplierListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SupplierListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
