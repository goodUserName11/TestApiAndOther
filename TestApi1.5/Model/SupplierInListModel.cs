using TestApi.Entity;

namespace TestApi.Model
{
    public class SupplierInListModel
    {
        public SupplierInListModel()
        {

        }

        public SupplierInListModel(SupplierInList supplier)
        {
            SupplierId = supplier.Id;
            Name = supplier.Supplier.Name;
            Rank = supplier.Rank;
            Price = supplier.Product.Price;
            ProductName = supplier.Product.Name;
        }

        public Guid SupplierId { get; set; }
        public string Name { get; set; }

        public double Rank { get; set; }

        public decimal Price { get; set; }
        public string ProductName { get; set; }
    }
}
