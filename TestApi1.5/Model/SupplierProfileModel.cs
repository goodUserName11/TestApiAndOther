using TestApi.Entity;

namespace TestApi.Model
{
    public class SupplierProfileModel
    {
        public SupplierProfileModel()
        {

        }

        public SupplierProfileModel(SupplierInList supplier)
        {
            Inn = supplier.Supplier.Inn;
            Name = supplier.Supplier.Name;
            Email = supplier.Supplier.Email;
            Phone = supplier.Supplier.Phone;
            Region = supplier.Supplier.Region;
            Kpp = supplier.Supplier.Kpp;
            Ogrn = supplier.Supplier.Ogrn;
            Reputation = supplier.Supplier.Reputation;
            Age = DateTime.Now - supplier.Supplier.WorkSince;
            Dishonesty = supplier.Supplier.Dishonesty;
            BankruptcyOrLiquidation = supplier.Supplier.BankruptcyOrLiquidation;
            WayOfDistribution = supplier.Supplier.WayOfDistribution;
            SmallBusinessEntity = supplier.Supplier.SmallBusinessEntity;
            IsManufacturer = supplier.Supplier.IsManufacturer;
            MinimumDeliveryDays = supplier.Supplier.MinimumDeliveryDays;
            OverallContracts = supplier.Supplier.OverallContracts;
            SuccededContracts = supplier.Supplier.SuccededContracts;

            Rank = supplier.Rank;
            Conflict = supplier.Conflict;

            FirstName = supplier.Supplier.Contact.Name;
            Surname = supplier.Supplier.Contact.Surname;
            Patronimic = supplier.Supplier.Contact.Patronimic;
            Phone1 = supplier.Supplier.Contact.Phone1;
            Phone2 = supplier.Supplier.Contact.Phone2;

            Okpd2 = supplier.Product.Okpd2;
            Price = supplier.Product.Price;
            Count = supplier.Product.Count;
            ProductName = supplier.Product.Name;
        }

        public string Inn { get; set; }
        public string Name { get; set; }
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

        public double Rank { get; set; }
        public bool Conflict { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }

        public string Okpd2 { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
    }
}
