using AngleSharp.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.ComponentModel.DataAnnotations;
using TestApi.Adapter;
using TestApi.Authentication;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;
using TestApi1._5.Model;

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

        [HttpGet("getAllWithProd")]
        public async Task<ActionResult<List<SupplierWithProdModel>>> GetAllWithProd(
            [FromQuery] string userId,
            [FromQuery] string? supplierNamePattern = "",
            [FromQuery] string? productNamePattern = "")
        {
            List<SupplierWithProdModel> resSuppliers = new();

            List<Supplier> dbSuppliers;

            using (var context = new SearchAndRangeContext())
            {
                var uid = Guid.Parse(userId);

                User? dbUser = await context.Users.FindAsync(uid);

                if (dbUser is null)
                {
                    Log.Logger.Information($"Пользователь '{userId}' не найден");

                    return BadRequest(new { ErrorMessage = "Пользователь не найден" });
                }

                if (dbUser.Role != UserRoles.Admin.Id
                    && dbUser.Role != UserRoles.Moderator.Id)
                {
                    Log.Logger.Information($"У пользователя {dbUser.Email} не достаточно прав");

                    return Unauthorized(new { ErrorMessage = "У текущего пользователя не достаточно прав" });
                }

                Log.Logger.Information($"Поиск поставщиков пользователем {dbUser.Email}");

                dbSuppliers = context
                    .Suppliers
                    .Where(
                        s => s
                        .Name
                        .ToLower()
                        .Contains(supplierNamePattern.ToLower())
                    ).Include(s => s.Products)
                    .ToList();
            }

            foreach (var dbSupplier in dbSuppliers)
            {
                List<ProdForSuppModel> resProd = new();
                foreach (var dbProd in dbSupplier.Products)
                {
                    if (dbProd
                        .Name
                        .ToLower()
                        .Contains(productNamePattern.ToLower())
                    )
                    {
                        resProd.Add(new ProdForSuppModel()
                        {
                            ProductId = dbProd.Id,
                            ProductName = dbProd.Name,
                            Price = dbProd.Price,
                        });
                    }
                }

                if (resProd.Count > 0)
                {
                    resSuppliers.Add(new SupplierWithProdModel()
                    {
                        Inn = dbSupplier.Inn,
                        Name = dbSupplier.Name,
                        Products = resProd,
                        //Rank = 10 * Random.Shared.NextDouble(),
                    });
                }
            }

            return Ok(resSuppliers);
        }

        [HttpGet("Update")]
        public async Task<ActionResult> Update()
        {
            Log.Logger.Information($"Обновление информации о поставщиках...");

            using (var dbContext = new SearchAndRangeContext())
            {
                await AdapterContainer.SupplierSearchAdapters[1].UpdateAll();
            }

            Log.Logger.Information($"Информация о поставщиках обновлена");

            return Ok();
        }
    }
}
