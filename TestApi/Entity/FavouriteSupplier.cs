using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("User_Favourite_Suppliers")]
    public class FavouriteSupplier
    {
        [Key]
        public Guid Id { get; set; }
        [Column("User_Id")]
        public Guid UserId { get; set; }
        [Column("Supplier_Id")]
        public Guid SupplierId { get; set; }
    }
}
