using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    [Serializable]
    public class SupplierProductModel
    {
        public SupplierProductModel()
        {

        }

        public SupplierProductModel(string productId, string count, string price)
        {
            ProductId = productId;
            Count = count;
            Price = price;
        }

        [Required]
        public string ProductId { get; set; }
        [Required]
        public string Count { get; set; }
        [Required]
        public string Price { get; set; }
    }
}
