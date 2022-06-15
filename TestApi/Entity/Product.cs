using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Suppler_s_Products")]
    public class Product
    {
        public Product(Guid id, string okpd2, string supplierId, decimal price, int count, string name)
        {
            Id = id;
            Okpd2 = okpd2;
            SupplierId = supplierId;
            Price = price;
            Count = count;
            Name = name;
        }

        [Key]
        public Guid Id { get; set; }
        public string Okpd2 { get; set; }
        [Column("Supplier_Id")]
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }

        public Supplier Supplier { get; set; }
    }
}
