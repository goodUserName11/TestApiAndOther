using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    [Serializable]
    public class SupplierProductModel
    {
        [NonSerialized]
        public Guid PruductDbId;

        public SupplierProductModel()
        {

        }

        public SupplierProductModel(string productId, int count, string price)
        {
            ProductId = productId;
            Count = count;
            Price = price;
        }

        public SupplierProductModel(string productId, string name, int count, string price)
        {
            ProductId = productId;
            Name = name;
            Count = count;
            Price = price;
        }

        [Required]
        public string ProductId { get; set; }
        public string Name { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string Price { get; set; }
    }
}
