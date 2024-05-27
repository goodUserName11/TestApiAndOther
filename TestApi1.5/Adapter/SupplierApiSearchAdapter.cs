using System.Net;
using TestApi.Entity;
using TestApi.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using TestApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Adapter
{
    public class SupplierApiSearchAdapter : AbstractSearchAdapter
    {
  
        public override async Task<List<SupplierFoundModel>> Find(string okpd2)
        {
            List<SupplierGetFromApi> suppliers = new List<SupplierGetFromApi>();
            List<ProductGetFromApi> products = new List<ProductGetFromApi>();
            List<SupplierFoundModel> resSup = new List<SupplierFoundModel>();

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://6273cc4f3d2b510074221878.mockapi.io/api/source/Suppliers");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.IncludeFields = true;
                options.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;

                suppliers = JsonSerializer.Deserialize<List<SupplierGetFromApi>>(responseBody, options);

                response = await client.GetAsync("https://6273cc4f3d2b510074221878.mockapi.io/api/source/Products");

                response.EnsureSuccessStatusCode();

                responseBody = await response.Content.ReadAsStringAsync();

                products = JsonSerializer.Deserialize<List<ProductGetFromApi>>(responseBody, options);

                products = products.Where(x => x.Okpd2 == okpd2).ToList();

                foreach (var product in products)
                {
                    suppliers = suppliers.Where(x =>
                    {
                        x.Products = x.Products.Where(prod => prod.ProductId == product.Id).ToList();
                        if (x.Products.Count == 1)
                            return true;
                        
                        return false;
                    }).ToList();

                    foreach (var supplier in suppliers)
                    {
                        supplier.Products[0].Name = product.Name;

                        using (SearchAndRangeContext dbContext = new())
                        {

                            var dbSupplierProduct = dbContext.Products
                                .Include(t => t.Supplier)
                                .Where(t => t.SupplierId == supplier.Inn)
                                .Where(t => t.Okpd2 == product.Okpd2)
                                .ToList();



                            if (dbSupplierProduct == null || dbSupplierProduct.Count == 0)
                            {
                                var dbSupplier = dbContext.Suppliers.Find(supplier.Inn);

                                if (dbSupplier == null) 
                                {
                                    double reputation = (double)supplier.SuccededContracts / supplier.OverallContracts;
                                    bool dishonesty = 
                                        supplier.Dishonesty != null
                                        && supplier.Dishonesty.Value.AddYears(3) >= DateTime.Now;

                                    resSup.Add(new SupplierFoundModel(supplier, dishonesty, reputation));

                                    var newContact = 
                                        new Contact(Guid.NewGuid(), supplier.Director.Name, supplier.Director.Surname,
                                        supplier.Director.Patronymic, supplier.Director.Phone1, supplier.Director.Phone2);
                                    dbContext.Contacts.Add(newContact);

                                    dbContext.SaveChanges();

                                    Random rand = new Random();

                                    dbContext.Suppliers.Add(
                                        new Supplier(supplier.Inn, supplier.Name, supplier.Email, supplier.Phone, newContact.Id, 
                                        supplier.Region, supplier.Kpp, supplier.Ogrn, reputation, supplier.WorkSince, dishonesty, 
                                        supplier.BankruptcyOrLiquidation, supplier.WayOfDistribution, supplier.SmallBusinessEntity, 
                                        supplier.IsManufacturer, supplier.MinimumDeliveryDays, supplier.Conflict,
                                        supplier.OverallContracts, supplier.SuccededContracts, rand.Next(50), rand.Next(20)));

                                    dbContext.SaveChanges();
                                }

                                var style = NumberStyles.AllowDecimalPoint;
                                var provider = new CultureInfo("en-GB");

                                var decimalPrice = decimal.Parse(supplier.Products[0].Price, style, provider);

                                var newProduct = new Product(Guid.NewGuid(), product.Okpd2, supplier.Inn, decimalPrice,
                                    supplier.Products[0].Count, supplier.Products[0].Name, "шт.");

                                dbContext.Products.Add(newProduct);

                                dbContext.Prices.Add(new TestApi1._5.Entity.Prices(Guid.NewGuid(), newProduct.Id, DateTime.Now, newProduct.Price));

                                await dbContext.SaveChangesAsync();

                                resSup[^1].Product.PruductDbId = newProduct.Id;
                            }

                            await dbContext.DisposeAsync();
                        }
                    }
                }


                return resSup;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }

        }

        public override async Task UpdateAll()
        {
            List<SupplierGetFromApi> suppliers = new List<SupplierGetFromApi>();

            var style = NumberStyles.AllowDecimalPoint;
            var provider = new CultureInfo("en-GB");

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://6273cc4f3d2b510074221878.mockapi.io/api/source/Suppliers");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions();
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.IncludeFields = true;
                options.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;

                suppliers = JsonSerializer.Deserialize<List<SupplierGetFromApi>>(responseBody, options);
                using (SearchAndRangeContext dbContext = new())
                {
                    var dbsuppliers =
                        dbContext
                        .Suppliers
                        .Include(s => s.Products)
                        .Include(s => s.Contact);

                    foreach (var dbsupplier in dbsuppliers)
                    {
                        var supplier = suppliers.Find(s => s.Inn == dbsupplier.Inn.TrimEnd());
                        if (supplier != null)
                        {
                            dbsupplier.Kpp = supplier.Kpp;
                            dbsupplier.SmallBusinessEntity = supplier.SmallBusinessEntity;
                            dbsupplier.Email = supplier.Email;
                            dbsupplier.IsManufacturer = supplier.IsManufacturer;
                            dbsupplier.BankruptcyOrLiquidation = supplier.BankruptcyOrLiquidation;
                            dbsupplier.Conflict = supplier.Conflict;
                            dbsupplier.Dishonesty = 
                                supplier.Dishonesty != null 
                                && supplier.Dishonesty.Value.AddYears(3) >= DateTime.Now;
                            dbsupplier.MinimumDeliveryDays = supplier.MinimumDeliveryDays;
                            dbsupplier.Name = supplier.Name;
                            dbsupplier.Ogrn = supplier.Ogrn;
                            dbsupplier.Reputation = (double)supplier.SuccededContracts / supplier.OverallContracts;
                            dbsupplier.Phone = supplier.Phone;
                            dbsupplier.Region = supplier.Region;
                            dbsupplier.WorkSince = supplier.WorkSince;
                            dbsupplier.WayOfDistribution = supplier.WayOfDistribution;
                            dbsupplier.SuccededContracts = supplier.SuccededContracts;
                            dbsupplier.OverallContracts = supplier.OverallContracts;

                            dbsupplier.Contact.Phone1 = supplier.Director.Phone1;
                            dbsupplier.Contact.Name = supplier.Director.Name;
                            dbsupplier.Contact.Patronimic = supplier.Director.Patronymic;
                            dbsupplier.Contact.Surname = supplier.Director.Surname;
                            dbsupplier.Contact.Phone2 = supplier.Director.Phone2;

                            foreach (var dbproduct in dbsupplier.Products)
                            {
                                var product = supplier.Products.Find(p => p.Name == dbproduct.Name);

                                if (product != null)
                                {
                                    dbproduct.Price = decimal.Parse(product.Price, style, provider);
                                    dbproduct.Name = product.Name;
                                    dbproduct.Count = product.Count;

                                    dbContext.Prices.Add(new TestApi1._5.Entity.Prices(Guid.NewGuid(), dbproduct.Id, DateTime.Now, dbproduct.Price));
                                }

                            }
                        }
                    }

                    dbContext.SaveChanges(true);

                    dbContext.Dispose();
                }
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
