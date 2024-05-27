namespace TestApi1._5.Entity
{
    public class ProductPricesRow
    {
        public Guid PriceId { get; set; }
        public Guid ProductId { get; set; }
        public string Okpd2 { get; set; }
        public string Supplier_Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public string PriceBy {  get; set; }
        public int Count { get; set; }
        public DateTime Date {  get; set; }
        public decimal Price { get; set; }
    }
}
