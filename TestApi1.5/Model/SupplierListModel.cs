using TestApi.Entity;

namespace TestApi.Model
{
    public class SupplierListModel
    {
        public SupplierListModel()
        {
            SupplierProfileModels = new List<SupplierInListModel>();
        }

        public SupplierListModel(SuppliersList suppliersList, List<SupplierInListModel> suppliersInList)
        {
            Name = suppliersList.Name;
            Date = suppliersList.Date;
            this.SupplierProfileModels = suppliersInList;
        }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<SupplierInListModel> SupplierProfileModels { get; set; }
    }
}
