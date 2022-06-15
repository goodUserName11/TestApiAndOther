namespace TestApi.Model
{
    public class SupplierListAddModel
    {
        public SupplierListAddModel()
        {

        }

        public SupplierListAddModel(List<string> supliersId, string listName, string userId)
        {
            SupliersId = supliersId;
            ListName = listName;
            UserId = userId;
        }

        public List<string> SupliersId { get; set; }
        public string ListName { get; set; }
        public string UserId { get; set; }
    }
}
