using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class SupplierProductModel
    {
        public SupplierProductModel()
        {

        }

        public SupplierProductModel(int productId, int count, decimal price)
        {
            ProductId = productId;
            Count = count;
            Price = price;
        }

        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
