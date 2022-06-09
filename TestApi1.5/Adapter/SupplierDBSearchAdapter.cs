using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Adapter
{
    public class SupplierDBSearchAdapter : AbstractSearchAdapter
    {
        public override async Task<List<SupplierFoundModel>> Find(string okpd2)
        {
            List<SupplierFoundModel> suppliers = new List<SupplierFoundModel>();
            List<Supplier> dbSuppliers;

            using (SearchAndRangeContext dbContext = new())
            {
                dbSuppliers = dbContext.Suppliers
                    .Include(s => s.Products)
                    .Include(s => s.Contact)
                    .Where(
                    s => s.Products
                    .Where(p => p.Okpd2 == okpd2)
                    .ToList().Count > 0)
                    .ToList();

                await dbContext.DisposeAsync();
            }

            dbSuppliers.ForEach(s =>
                suppliers.Add(new SupplierFoundModel(s))
            );

            return suppliers;
        }
    }
}
