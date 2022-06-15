using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Company_Сoefficient_Values")]
    public class CoefficientValue
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Coefficient_Id")]
        [ForeignKey("Coefficient")]
        public Guid CoefficientId { get; set; }
        [Column("Company_Inn")]
        public string CompanyInn { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }

        public Coefficient Coefficient { get; set; }
    }
}
