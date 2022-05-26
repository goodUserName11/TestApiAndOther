using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    [Serializable]
    public class SupplierModel
    {
        public SupplierModel()
        {

        }

        public SupplierModel(string id, string inn, string name, string region, string kpp,
            string ogrn, int succededContracts, int overallContracts, DateTime workSince,
            DateTime? dishonesty/*, bool bankruptcyOrLiquidation, string wayOfDistribution,
            bool smallBusinessEntity, string minimumDeliveryDays, bool isManufacturer,
            string phone, string email, List<SupplierProductModel> products,
            SupplierDirectorModel director*/)
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
            //BankruptcyOrLiquidation = bankruptcyOrLiquidation;
            //WayOfDistribution = wayOfDistribution;
            //SmallBusinessEntity = smallBusinessEntity;
            //MinimumDeliveryDays = minimumDeliveryDays;
            //IsManufacturer = isManufacturer;
            //Phone = phone;
            //Email = email;
            //Products = products;
            //Director = director;
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
        //[Required]
        //public bool BankruptcyOrLiquidation { get; set; }
        //[Required]
        //public string WayOfDistribution { get; set; }
        //[Required]
        //public bool SmallBusinessEntity { get; set; }
        //[Required]
        //public string MinimumDeliveryDays { get; set; }
        //[Required]
        //public bool IsManufacturer { get; set; }
        //[Required]
        //public string Phone { get; set; }
        //[Required]
        //public string Email { get; set; }
        //[Required]
        //public List<SupplierProductModel> Products { get; set; }
        //[Required]
        //public SupplierDirectorModel Director { get; set; }
    }
}
