using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class SupplierSearchResultModel
    {
        public SupplierSearchResultModel()
        {

        }

        public SupplierSearchResultModel(string inn, string name, string region, string kpp,
            string ogrn, double reputation, DateTime workSince,
            bool dishonesty, bool bankruptcyOrLiquidation, bool wayOfDistribution,
            bool smallBusinessEntity, int minimumDeliveryDays, bool isManufacturer, string phone,
            string email, double rank, SupplierDirectorModel director, bool conflict)
        {
            Id = Guid.NewGuid();
            Inn = inn;
            Name = name;
            Region = region;
            Kpp = kpp;
            Ogrn = ogrn;
            Reputation = reputation;
            WorkSince = workSince;
            Dishonesty = dishonesty;
            BankruptcyOrLiquidation = bankruptcyOrLiquidation;
            WayOfDistribution = wayOfDistribution;
            SmallBusinessEntity = smallBusinessEntity;
            MinimumDeliveryDays = minimumDeliveryDays;
            IsManufacturer = isManufacturer;
            Phone = phone;
            Email = email;
            Rank = rank;
            Director = director;
            Conflict = conflict;
        }

        public SupplierSearchResultModel(SupplierFoundModel model, double rank, bool conflict)
        {
            Id = Guid.NewGuid();
            Inn = model.Inn;
            Name = model.Name;
            Region = model.Region;
            Kpp = model.Kpp;
            Ogrn = model.Ogrn;
            Reputation = model.Reputation;
            WorkSince = model.WorkSince;
            Dishonesty = model.Dishonesty;
            BankruptcyOrLiquidation = model.BankruptcyOrLiquidation;
            WayOfDistribution = model.WayOfDistribution;
            SmallBusinessEntity = model.SmallBusinessEntity;
            MinimumDeliveryDays = model.MinimumDeliveryDays;
            IsManufacturer = model.IsManufacturer;
            Conflict = conflict;

            Phone = model.Phone;
            Email = model.Email;
            Rank = rank;
            Director = model.Director;
            Product = model.Product;
        }

        public Guid Id { get; set; }
        [Required]
        public string Inn { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Kpp { get; set; }
        [Required]
        public string Ogrn { get; set; }
        [Required]
        public double Reputation { get; set; }
        [Required]
        public int OverallContracts { get; set; }
        [Required]
        public DateTime WorkSince { get; set; }
        [Required]
        public bool Dishonesty { get; set; }
        [Required]
        public bool BankruptcyOrLiquidation { get; set; }
        [Required]
        public bool WayOfDistribution { get; set; }
        [Required]
        public bool SmallBusinessEntity { get; set; }
        [Required]
        public int MinimumDeliveryDays { get; set; }
        [Required]
        public bool IsManufacturer { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public double Rank { get; set; }
        public bool Conflict { get; set; }
        [Required]
        public SupplierDirectorModel Director { get; set; }
        [Required]
        public SupplierProductModel Product { get; set; }
    }
}
