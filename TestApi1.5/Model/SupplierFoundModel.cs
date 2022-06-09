using TestApi.Entity;

namespace TestApi.Model
{
    public class SupplierFoundModel
    {
        public SupplierFoundModel()
        {

        }

        public SupplierFoundModel(Supplier entity)
        {
            Inn = entity.Inn;
            Name = entity.Name;
            Region = entity.Region;
            Kpp = entity.Kpp;
            Ogrn = entity.Ogrn;
            Reputation = entity.Reputation;
            WorkSince = entity.WorkSince;
            Dishonesty = entity.Dishonesty;
            BankruptcyOrLiquidation = entity.BankruptcyOrLiquidation;
            WayOfDistribution = entity.WayOfDistribution;
            SmallBusinessEntity = entity.SmallBusinessEntity;
            MinimumDeliveryDays = entity.MinimumDeliveryDays;
            IsManufacturer = entity.IsManufacturer;
            Phone = entity.Phone;
            Email = entity.Email;
            Product = 
                new SupplierProductModel(
                    entity.Products[0].Id.ToString(), 
                    entity.Products[0].Name,
                    entity.Products[0].Count, 
                    entity.Products[0].Price.ToString());

            Director = 
                new SupplierDirectorModel(
                    entity.Contact.Name, 
                    entity.Contact.Surname,
                    entity.Contact.Patronimic,
                    entity.Contact.Phone1,
                    entity.Contact.Phone2);

            Conflict = entity.Conflict;
        }

        public SupplierFoundModel(SupplierGetFromApi model, bool dishonesty, double reputation)
        {
            Inn = model.Inn;
            Name = model.Name;
            Region = model.Region;
            Kpp = model.Kpp;
            Ogrn = model.Ogrn;
            Reputation = reputation;
            WorkSince = model.WorkSince;
            Dishonesty = dishonesty;
            BankruptcyOrLiquidation = model.BankruptcyOrLiquidation;
            WayOfDistribution = model.WayOfDistribution;
            SmallBusinessEntity = model.SmallBusinessEntity;
            MinimumDeliveryDays = model.MinimumDeliveryDays;
            IsManufacturer = model.IsManufacturer;
            Phone = model.Phone;
            Email = model.Email;
            Product = model.Products[0];
            Director = model.Director;
            Conflict = model.Conflict;
        }

        public SupplierFoundModel(string inn, string name, string region, string kpp,
            string ogrn, double reputation, DateTime workSince,
            bool dishonesty, bool bankruptcyOrLiquidation, bool wayOfDistribution,
            bool smallBusinessEntity, int minimumDeliveryDays, bool isManufacturer,
            string phone, string email, SupplierProductModel product,
            SupplierDirectorModel director, string conflict)
        {
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
            Product = product;
            Director = director;
            Conflict = conflict;
        }

        public string Inn { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Kpp { get; set; }
        public string Ogrn { get; set; }
        public double Reputation { get; set; }
        public DateTime WorkSince { get; set; }
        public bool Dishonesty { get; set; }
        public bool BankruptcyOrLiquidation { get; set; }
        public bool WayOfDistribution { get; set; }
        public bool SmallBusinessEntity { get; set; }
        public int MinimumDeliveryDays { get; set; }
        public bool IsManufacturer { get; set; }
        public string Conflict { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public SupplierProductModel Product { get; set; }
        public SupplierDirectorModel Director { get; set; }
    }
}
