using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Supplier_List")]
    public class SuppliersList
    {
        [Key]
        public Guid Id { get; set; }
        [Column("User_Id")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
        [ForeignKey("SupplierListId")]
        public List<SupplierInList> SupliersInList { get; set; }
    }
}
