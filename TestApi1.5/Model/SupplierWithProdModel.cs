namespace TestApi1._5.Model
{
    public class SupplierWithProdModel
    {
        public string Inn { get; set; }
        public string Name { get; set; }

        //public double Rank { get; set; }

        public List<ProdForSuppModel> Products { get; set; }
    }
}
