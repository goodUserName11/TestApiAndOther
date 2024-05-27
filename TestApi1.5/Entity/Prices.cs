namespace TestApi1._5.Entity
{
    public class Prices
    {
        public Prices() { }

        public Prices(Guid id, Guid productId, DateTime date, decimal price)
        {
            Id = id;
            ProductId = productId;
            Date = date;
            Price = price;
        }

        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public DateTime Date {  get; set; }
        public decimal Price { get; set; }
    }
}
