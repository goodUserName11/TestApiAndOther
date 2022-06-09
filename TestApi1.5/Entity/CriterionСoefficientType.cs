using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Criterion_Сoefficient_Type")]
    [DisplayColumn("Type_Name")]
    public class CriterionСoefficientType
    {
        public CriterionСoefficientType()
        {

        }

        [Key]
        public Guid Id { get; set; }
        [Column("Type_Name")]
        public string TypeName { get; set; }

    }
}
