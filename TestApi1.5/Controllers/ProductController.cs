using Microsoft.AspNetCore.Mvc;
using TestApi.Entity;
using DataGetter.Common.Models;
using TestApi.Data;
using System.Xml.Linq;
using TestApi1._5.Entity;
using System.Linq;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TestApi1._5.Model;
using DataGetter.Common;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi1._5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// TODO:
        /// </summary>
        /// <returns></returns>
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<ProductPriceArrayResponseModel>> GetAll()
        {
            List<ProductPriceArrayResponseModel> response = new();

            using var context = new SearchAndRangeContext();

            var pricesList1 = context.Prices;

            var products = context.Products.ToList();

            foreach (var product in products)
            {
                var pricesList = context.Prices
                    .Where(x => x.ProductId == product.Id)
                    .ToList()
                    .Select(
                    (x,y) => new PricesModel()
                    {
                        Date = x.Date,
                        Price = x.Price,
                    })
                    .ToList();

                response.Add(new ProductPriceArrayResponseModel
                {
                    Okpd = product.Okpd2,
                    Price = decimal.ToDouble(product.Price),
                    Count = product.Count,
                    PriceBy = product.PriceBy,
                    ProductName = product.Name,
                    Prices = pricesList
                });
            }

            return Ok(response);
        }

        /// <summary>
        /// TODO:
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ProductController>
        [HttpGet("GetOne")]
        public async Task<ActionResult<ProductProfileModel>> GetOne([FromQuery] string id)
        {
            ProductProfileModel resProd;
            List<SimilarProdModel> similarProds;
            List<ProdPriceModel> ProdPrices;

            Product? dbProd;
            List<Product> dbSimilarProducts;

            using (SearchAndRangeContext context = new()) 
            {
                var prodId = Guid.Parse(id);

                dbProd = context
                    .Products
                    .Include(p => p.Prices)
                    .Include(p => p.Supplier)
                    .ThenInclude(s => s.Contact)
                    .FirstOrDefault(p => p.Id.Equals(prodId));

                if (dbProd is null)
                {
                    Log.Logger.Information($"Товар '{id}' не найден");

                    return NotFound(new { ErrorMessage = $"Товар '{id}' не найден" });
                }

                dbProd.Prices = dbProd
                    .Prices
                    .OrderBy(pr => pr.Date)
                    .ToList();


                dbSimilarProducts = context
                    .Products
                    .Where(sp => sp
                        .Okpd2
                        .Equals(dbProd.Okpd2)
                    ).Include(p => p.Supplier)
                    .ToList();
            }

            similarProds = dbSimilarProducts
                .Select(
                    sp => new SimilarProdModel()
                    {
                        Id = sp.Id,
                        Price = sp.Price,
                        ProductName = sp.Name,

                        Inn = sp.Supplier.Inn,
                        SupplierName = sp.Supplier.Name,
                    }
                ).ToList();

            ProdPrices = dbProd
                .Prices
                .Select(
                    p => new ProdPriceModel()
                    {
                        Date = p.Date,
                        Price = p.Price,
                    }
                )
                .ToList();

            resProd = new ProductProfileModel()
            {
                Id = dbProd.Id,
                Okpd2 = dbProd.Okpd2,
                Price = dbProd.Price,
                Count = dbProd.Count,
                ProductName = dbProd.Name,

                Reasonableness = dbProd.Reasonableness,

                //SupplierId = dbProd.SupplierId,
                Inn = dbProd.Supplier.Inn,
                SupplierName = dbProd.Supplier.Name,

                Email = dbProd.Supplier.Email,
                Phone = dbProd.Supplier.Phone,
                Region = dbProd.Supplier.Region,
                Kpp = dbProd.Supplier.Kpp,
                Ogrn = dbProd.Supplier.Ogrn,
                Reputation = dbProd.Supplier.Reputation,
                Age = DateTime.Now - dbProd.Supplier.WorkSince,
                Dishonesty = dbProd.Supplier.Dishonesty,
                BankruptcyOrLiquidation = dbProd.Supplier.BankruptcyOrLiquidation,
                WayOfDistribution = dbProd.Supplier.WayOfDistribution,
                SmallBusinessEntity = dbProd.Supplier.SmallBusinessEntity,
                IsManufacturer = dbProd.Supplier.IsManufacturer,
                MinimumDeliveryDays = dbProd.Supplier.MinimumDeliveryDays,
                OverallContracts = dbProd.Supplier.OverallContracts,
                SuccededContracts = dbProd.Supplier.SuccededContracts,

                GoodReviewsCount = dbProd.Supplier.GoodReviewsCount,
                BadReviewsCount = dbProd.Supplier.BadReviewsCount,

                Rank = Random.Shared.Next(9) + Random.Shared.NextDouble(),
                Conflict = false,

                FirstName = dbProd.Supplier.Contact.Name,
                Surname = dbProd.Supplier.Contact.Surname,
                Patronimic = dbProd.Supplier.Contact.Patronimic,
                Phone1 = dbProd.Supplier.Contact.Phone1,
                Phone2 = dbProd.Supplier.Contact.Phone2,

                Prices = ProdPrices,

                SimilarProds = similarProds,
            };

            Log.Logger.Information($"Получен товар '{id}'");

            return Ok(resProd);
        }

        [HttpGet("GetForUptadeMlDatas")]
        public async Task<ActionResult<List<ForMLData>>> GetForUptadeMlData([FromQuery] int? count)
        {
            List<ForMLData> resProds = new();
            List<Product> dbProducts;

            using (SearchAndRangeContext context = new())
            {
                if (count is null)
                {
                    dbProducts = context
                        .Products
                        .Where(p => p.Name.Equals("Клеи прочие"))
                        .Include(p => p.Prices)
                        .Include(p => p.Supplier)
                        .ToList();
                }
                else
                {
                    dbProducts = context
                        .Products
                        .Where(p => p.Name.Equals("Клеи прочие"))
                        .Include(p => p.Prices)
                        .Include(p => p.Supplier)
                        .Take(count.Value)
                        .ToList();
                }
            }

            foreach (var dbProduct in dbProducts ) 
            {
                double medianPrice = Convert
                    .ToDouble(
                        dbProduct
                        .Prices
                        .Median(pr => pr.Price));

                resProds.Add(
                    new() 
                    {
                        ProductId = dbProduct.Id.ToString(),
                        MedianPrice = medianPrice,
                        BadReviewsCount = dbProduct.Supplier.BadReviewsCount,
                        GoodReviewsCount = dbProduct.Supplier.GoodReviewsCount, 
                    });
            }

            return resProds;
        }

        [HttpGet("GetOneForUptadeMl")]
        public async Task<ActionResult<ForMLData>> GetOneForUptadeMl([FromQuery] string productName, string inn)
        {
            ForMLData forMLData;
            Product? dbProduct;

            using (SearchAndRangeContext context = new())
            {
                dbProduct = context
                        .Products
                        .Include(p => p.Prices)
                        .Include(p => p.Supplier)
                        .FirstOrDefault(p => 
                            p.Name.Equals(productName) 
                            && p.SupplierId.Equals(inn)
                        );
            }

            if(dbProduct is null)
            {
                Log.Logger.Information($"Товар '{productName}' не найден");

                return NotFound(new { ErrorMessage = $"Товар '{productName}' не найден" });
            }

            double medianPrice = Convert
                    .ToDouble(
                        dbProduct
                        .Prices
                        .Median(pr => pr.Price)
                     );

            forMLData = new()
            {
                ProductId = dbProduct.Id.ToString(),
                MedianPrice = medianPrice,
                BadReviewsCount = dbProduct.Supplier.BadReviewsCount,
                GoodReviewsCount = dbProduct.Supplier.GoodReviewsCount,
            };

            return Ok(forMLData);
        }

            [HttpGet("GetProductsToUpdate")]
        public async Task<ActionResult<List<ProductToUpdate>>> GetProductsToUpdate([FromQuery] int? count)
        {
            List<ProductToUpdate> resProds = new();
            List<Product> dbProducts;

            using (SearchAndRangeContext context = new())
            {
                if(count is null)
                {
                    dbProducts = context
                        .Products
                        .Where(p => p.Name.Equals("Клеи прочие"))
                        .ToList();
                }
                else
                {
                    dbProducts = context
                        .Products
                        .Where(p => p.Name.Equals("Клеи прочие"))
                        .Take(count.Value)
                        .ToList();
                }
            }

            foreach (var dbProduct in dbProducts)
            {
                resProds.Add(
                    new()
                    {
                        ProductId = dbProduct.Id.ToString(),
                        ProductName = dbProduct.Name,
                        ProductOkpd = dbProduct.Okpd2,
                        SupplierInn = dbProduct.SupplierId,
                    });
            }

            return resProds;
        }


        /// <summary>
        /// TODO:
        /// <list type="bullet">
        /// <item>Протестировать!!</item>
        /// <item>?(Вроде починил)Конфликт инструкции INSERT с ограничением FOREIGN KEY "FK_Prices_Suppler_s_Products". Конфликт произошел в базе данных "ZakupkiLocal", таблица "dbo.Suppler_s_Products", column 'Id'</item>
        /// <item>?(Вроде починил)Проблема со сравнением в точке останова</item>
        /// </list>
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateOne")]
        public async Task<ActionResult> AddOrUpdateOne([FromBody] ProductToDbModel prod)
        {
            using SearchAndRangeContext context = new();

            var comp = prod.Manufacturer;

            var dbProduct = context
                .Products
                .Include(p => p.Supplier)
                .ToList()
                .FirstOrDefault(x => 
                    x.SupplierId.StartsWith(prod.Manufacturer.Inn)
                    && x.Name.StartsWith(prod.ProductName)
                );

            if (dbProduct is null)
            {
                var dbComp = await context.Suppliers.FindAsync(comp.Inn);

                if (dbComp is null)
                {
                    var contact = new Contact(Guid.NewGuid(), null, null, null, null, null);

                    context.Contacts.Add(contact);

                    var supplier = new Supplier()
                    {
                        Inn = comp.Inn,
                        Ogrn = comp.Ogrn,
                        BankruptcyOrLiquidation = comp.BankruptcyOrLiquidation,
                        WorkSince = comp.WorkSince.Value,
                        Name = comp.Name,
                        Region = comp.Region,
                        SmallBusinessEntity = comp.SmallBusinessEntity.Value,
                        GoodReviewsCount = comp.GoodReviewsCount.Value,
                        BadReviewsCount = comp.BadReviewsCount.Value,

                        Kpp = null,
                        Email = null,
                        Phone = null,

                        Conflict = null,
                        Dishonesty = false,
                        IsManufacturer = false,
                        WayOfDistribution = false,

                        MinimumDeliveryDays = comp.MinimumDeliveryDays.Value,
                        OverallContracts = comp.OverallContracts.Value,
                        SuccededContracts = comp.SuccededContracts.Value,
                        Reputation = comp.Reputation.Value,

                        ContactId = contact.Id,
                    };

                    context.Suppliers.Add(supplier);

                    context.SaveChanges();
                }
                else
                {
                    dbComp.BankruptcyOrLiquidation = comp.BankruptcyOrLiquidation;
                    dbComp.Name = comp.Name;
                    dbComp.Region = comp.Region;
                    dbComp.SmallBusinessEntity = comp.SmallBusinessEntity.Value;
                    //dbComp.GoodReviewsCount = comp.GoodReviewsCount;
                    //dbComp.BadReviewsCount = comp.BadReviewsCount;
                    //dbComp.MinimumDeliveryDays = comp.MinimumDeliveryDays;
                    //dbComp.OverallContracts = comp.OverallContracts;
                    //dbComp.SuccededContracts = comp.SuccededContracts;
                    //dbComp.Reputation = comp.Reputation;
                }

                context.SaveChanges();

                dbProduct = new(Guid.NewGuid(), prod.Okpd, comp.Inn, (decimal)prod.Price, prod.Count, prod.ProductName, prod.PriceBy);

                context.Products.Add(dbProduct);

                context.SaveChanges();
            }
            else
            {
                dbProduct.Price = Convert.ToDecimal(prod.Price);
                dbProduct.PriceBy = prod.PriceBy;
                dbProduct.Count = prod.Count;

                if(!prod.Manufacturer.Name.Equals("No"))
                    dbProduct.Supplier.BankruptcyOrLiquidation = prod.Manufacturer.BankruptcyOrLiquidation;

                context.SaveChanges();
            }

            context.Prices.Add(new Entity.Prices(Guid.NewGuid(), dbProduct.Id, DateTime.Now, dbProduct.Price));

            await context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// TODO:
        /// Доделать
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateRange")]
        public async Task<ActionResult> AddOrUpdateRange([FromBody]List<ProductToDbModel> prods)
        {
            string supplierId = "";

            using SearchAndRangeContext context = new();

            prods.OrderBy(x => x.Manufacturer.Inn);

            for (int i = 0; i < prods.Count; i++)
            {
                var dbProduct = context
                    .Products
                    .Include(p => p.Supplier)
                    .FirstOrDefault(x =>
                        x.SupplierId.Equals(prods[i].Manufacturer.Inn)
                        && x.Name.Equals(prods[i].ProductName)
                    );

                if (dbProduct is null) 
                {

                    var manufacturer = prods[i].Manufacturer;

                    if (supplierId != manufacturer.Inn)
                    {
                        supplierId = manufacturer.Inn;

                        var dbComp = await context.Suppliers.FindAsync(manufacturer.Inn);

                        if(dbComp is null)
                        {
                            var contact = new Contact(Guid.NewGuid(), null, null, null, null, null);

                            context.Contacts.Add(contact);

                            var supplier = new Supplier()
                            {
                                Inn = manufacturer.Inn,
                                Ogrn = manufacturer.Ogrn,
                                BankruptcyOrLiquidation = manufacturer.BankruptcyOrLiquidation,
                                WorkSince = manufacturer.WorkSince.Value,
                                Name = manufacturer.Name,
                                Region = manufacturer.Region,
                                SmallBusinessEntity = manufacturer.SmallBusinessEntity.Value,
                                GoodReviewsCount = manufacturer.GoodReviewsCount.Value,
                                BadReviewsCount = manufacturer.BadReviewsCount.Value,

                                Kpp = null,
                                Email = null,
                                Phone = null,

                                Conflict = null,
                                Dishonesty = false,
                                IsManufacturer = false,
                                WayOfDistribution = false,

                                MinimumDeliveryDays = manufacturer.MinimumDeliveryDays.Value,
                                OverallContracts = manufacturer.OverallContracts.Value,
                                SuccededContracts = manufacturer.SuccededContracts.Value,
                                Reputation = manufacturer.Reputation.Value,

                                ContactId = contact.Id,
                            };

                            context.Suppliers.Add(supplier);
                        }
                        else
                        {
                            dbComp.BankruptcyOrLiquidation = manufacturer.BankruptcyOrLiquidation;
                            dbComp.Name = manufacturer.Name;
                            dbComp.Region = manufacturer.Region;
                            dbComp.SmallBusinessEntity = manufacturer.SmallBusinessEntity.Value;
                            //dbComp.GoodReviewsCount = manufacturer.GoodReviewsCount;
                            //dbComp.BadReviewsCount = manufacturer.BadReviewsCount;
                            //dbComp.MinimumDeliveryDays = manufacturer.MinimumDeliveryDays;
                            //dbComp.OverallContracts = manufacturer.OverallContracts;
                            //dbComp.SuccededContracts = manufacturer.SuccededContracts;
                            //dbComp.Reputation = manufacturer.Reputation;
                        }
                        context.SaveChanges();

                    }

                    dbProduct = new(Guid.NewGuid(), prods[i].Okpd, manufacturer.Inn, (decimal)prods[i].Price, prods[i].Count, prods[i].ProductName, prods[i].PriceBy);

                    context.Products.Add(dbProduct);
                }
                else
                {
                    dbProduct.Price = Convert.ToDecimal(prods[i].Price);
                    dbProduct.PriceBy = prods[i].PriceBy;
                    dbProduct.Count = prods[i].Count;

                    if (!prods[i].Manufacturer.Name.Equals("No"))
                        dbProduct.Supplier.BankruptcyOrLiquidation = prods[i].Manufacturer.BankruptcyOrLiquidation;

                }
                context.SaveChanges();

                context.Prices.Add(new Entity.Prices(Guid.NewGuid(), dbProduct.Id, DateTime.Now, dbProduct.Price));
            }

            context.SaveChanges();

            return Ok();
        }

        [HttpPost("UpdatePrice")]
        public async Task<ActionResult> UpdatePrice([FromBody] List<PriceUpdateFileInputModel> prods)
        {
            using (SearchAndRangeContext context = new())
            {
                foreach (var prod in prods)
                {
                    string prodName = prod
                        .ProductName
                        .Replace("\"", "")
                        .Replace("'", "");

                    string companyInn = prod.CompanyInn.Trim();

                    Product? dbProd = context
                        .Products
                        .FirstOrDefault(
                            p => p.Name.Equals(prodName)
                            && p.SupplierId.Trim().Equals(companyInn)
                        );

                    if(dbProd is null)
                    {
                        Log.Logger.Information($"Товар {prod.ProductName} не найден");

                        return NotFound(new { ErrorMessage = $"Товар {prod.ProductName} не найден" });
                    }

                    decimal newPrice = Convert.ToDecimal(prod.NewPrice);

                    context.Prices.Add(new Prices(Guid.NewGuid(), dbProd.Id, prod.ChangeDate, newPrice));


                    dbProd.Price = context
                        .Prices
                        .Where(pr => pr.ProductId.Equals(dbProd.Id))
                        .OrderBy(pr => pr.Date)
                        .Last()
                        .Price;

                }
                context.SaveChanges();
            }

            Log.Logger.Information($"Цены на товары обновлены пользователем");

            return Ok();
        }

        [HttpPost("UpdateReasonableness")]
        public async Task<ActionResult> UpdateReasonableness([FromBody] PriceReasonablenessOutputModel prod)
        {
            Guid prodId = Guid.Parse(prod.ProductId);
            using (var context = new SearchAndRangeContext())
            {
                var dbProd = context
                    .Products
                    .Include(p => p.Supplier)
                    .FirstOrDefault(p => p.Id.Equals(prodId));

                if(dbProd is null)
                {
                    Log.Logger.Information($"Товар с id '{prod.ProductId}' не найден");
                    return NotFound(new { ErrorMessage = $"Товар с id '{prod.ProductId}' не найден" });
                }

                dbProd.Reasonableness = prod.Reasonableness;
                dbProd.Supplier.GoodReviewsCount = prod.GoodReviewsCount;
                dbProd.Supplier.BadReviewsCount = prod.BadReviewsCount;

                await context.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpGet("Test")]
        public async Task<ActionResult<double>> Test([FromQuery] string productId)
        {
            //string[] strings = new string[] { "22.22.19.190", "22.22", "22.22.19" };

            //strings.OrderBy(s => s);

            Guid prodId = Guid.Parse(productId);

            List<Prices> dbPrices;
            double medianPrice;
            using (var context = new SearchAndRangeContext())
            {
                dbPrices = context
                    .Prices
                    .Where(pr => pr.ProductId.Equals(prodId))
                    .ToList();

                medianPrice = Convert
                    .ToDouble(
                        dbPrices
                        .Median(pr => pr.Price));
            }

            return Ok(medianPrice);
        }
    }
}
