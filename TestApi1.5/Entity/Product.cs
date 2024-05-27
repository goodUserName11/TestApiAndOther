using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApi1._5.Entity;

namespace TestApi.Entity
{
    [Table("Suppler_s_Products")]
    public class Product
    {
        public Product(){ }

        public Product(Guid id, string okpd2, string supplierId, decimal price, int count, string name, string priceBy)
        {
            Id = id;
            Okpd2 = okpd2;
            SupplierId = supplierId;
            Price = price;
            Count = count;
            Name = name;
            PriceBy = priceBy;
        }

        [Key]
        [Column("Id")]
        public Guid Id { get; set; }
        public string Okpd2 { get; set; }
        [Column("Supplier_Id")]
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string PriceBy {  get; set; }
        public double Reasonableness { get; set; }

        public Supplier Supplier { get; set; }

        [ForeignKey("ProductId")]
        public List<Prices> Prices { get; set; }
    }
}