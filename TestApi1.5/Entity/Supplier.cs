using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Supplier")]
    public class Supplier
    {
        public Supplier()
        {

        }

        public Supplier(string inn, string name, string email, string phone, Guid contactId,
            string region, string kpp, string ogrn, double reputation,
            DateTime workSince, bool dishonesty, bool bankruptcyOrLiquidation, bool wayOfDistribution,
            bool smallBusinessEntity, bool isManufacturer, int minimumDeliveryDays, string conflict, 
            int overallContracts, int succededContracts)
        {
            Inn = inn;
            Name = name;
            Email = email;
            Phone = phone;
            ContactId = contactId;
            Region = region;
            Kpp = kpp;
            Ogrn = ogrn;
            Reputation = reputation;
            WorkSince = workSince;
            Dishonesty = dishonesty;
            BankruptcyOrLiquidation = bankruptcyOrLiquidation;
            WayOfDistribution = wayOfDistribution;
            SmallBusinessEntity = smallBusinessEntity;
            IsManufacturer = isManufacturer;
            MinimumDeliveryDays = minimumDeliveryDays;
            Conflict = conflict;
            OverallContracts = overallContracts;
            SuccededContracts = succededContracts;
        }

        [Key]
        public string Inn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Column("Contact_Id")]
        [ForeignKey("Contact")]
        public Guid ContactId { get; set; }
        public string Region { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        [Column("Reputation")]
        public double Reputation { get; set; }
        [Column("Work_Since")]
        public DateTime WorkSince { get; set; }
        public bool Dishonesty { get; set; }
        [Column("Bankruptcy_Or_Liquidation")]
        public bool BankruptcyOrLiquidation { get; set; }
        [Column("Way_Of_Distribution")]
        public bool WayOfDistribution { get; set; }
        [Column("Small_Business_Entity")]
        public bool SmallBusinessEntity { get; set; }
        [Column("Is_Manufacturer")]
        public bool IsManufacturer { get; set; }
        [Column("Minimum_Delivery_Days")]
        public int MinimumDeliveryDays { get; set; }
        public string Conflict { get; set; }
        public int OverallContracts { get; set; }
        public int SuccededContracts { get; set; }

        public Contact Contact { get; set; }
        [ForeignKey("SupplierId")]
        public List<Product> Products { get; set; }

    }
}
