using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Suppliers_In_Supplier_List")]
    public class SupplierInList
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Supplier_Id")]
        [ForeignKey("Supplier")]
        public string SupplierId { get; set; }
        [Column("Supplier_List_Id")]
        [ForeignKey("SuppliersList")]
        public Guid? SupplierListId { get; set; }
        public double Rank { get; set; }
        public string Okpd2 { get; set; }
        public bool Conflict { get; set; }

        public Supplier Supplier { get; set; }
        public SuppliersList SuppliersList { get; set; }
    }
}
