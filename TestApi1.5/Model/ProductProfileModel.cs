using static TestApi1._5.Controllers.ProductController;

namespace TestApi1._5.Model
{
    public class ProductProfileModel
    {
        public Guid Id { get; set; }
        public string Okpd2 { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }

        public double Reasonableness { get; set; }

        //public string SupplierId { get; set; }
        public string Inn { get; set; }
        /// <summary>
        /// SupplierName (Name - old)
        /// </summary>
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public double Reputation { get; set; }
        public TimeSpan Age { get; set; }
        public bool Dishonesty { get; set; }
        public bool BankruptcyOrLiquidation { get; set; }
        public bool WayOfDistribution { get; set; }
        public bool SmallBusinessEntity { get; set; }
        public bool IsManufacturer { get; set; }
        public int MinimumDeliveryDays { get; set; }
        public int OverallContracts { get; set; }
        public int SuccededContracts { get; set; }

        public int GoodReviewsCount { get; set; }
        public int BadReviewsCount { get; set; }

        public double Rank { get; set; }
        public bool Conflict { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }

        public List<ProdPriceModel> Prices { get; set; }

        public List<SimilarProdModel> SimilarProds { get; set; }
    }
}
