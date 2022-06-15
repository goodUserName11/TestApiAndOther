using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    [Serializable]
    public class SupplierGetFromApi
    {
        public SupplierGetFromApi()
        {

        }

        public SupplierGetFromApi(string id, string inn, string name, string region, string kpp,
            string ogrn, int succededContracts, int overallContracts, DateTime workSince,
            DateTime? dishonesty, bool bankruptcyOrLiquidation, bool wayOfDistribution,
            bool smallBusinessEntity, int minimumDeliveryDays, bool isManufacturer,
            string phone, string email, List<SupplierProductModel> products,
            SupplierDirectorModel director, string conflict)
        {
            Id = id;
            Inn = inn;
            Name = name;
            Region = region;
            Kpp = kpp;
            Ogrn = ogrn;
            SuccededContracts = succededContracts;
            OverallContracts = overallContracts;
            WorkSince = workSince;
            Dishonesty = dishonesty;
            BankruptcyOrLiquidation = bankruptcyOrLiquidation;
            WayOfDistribution = wayOfDistribution;
            SmallBusinessEntity = smallBusinessEntity;
            MinimumDeliveryDays = minimumDeliveryDays;
            IsManufacturer = isManufacturer;
            Phone = phone;
            Email = email;
            Products = products;
            Director = director;
            Conflict = conflict;
        }

        [Required]
        public string Id { get; set; }
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
        public int SuccededContracts { get; set; }
        [Required]
        public int OverallContracts { get; set; }
        [Required]
        public DateTime WorkSince { get; set; }
        [Required]
        public DateTime? Dishonesty { get; set; }
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
        public string Conflict { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public List<SupplierProductModel> Products { get; set; }
        [Required]
        public SupplierDirectorModel Director { get; set; }

    }
}
