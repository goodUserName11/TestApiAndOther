using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    //public enum CoefficientType
    //{
    //    Default = 0,
    //    Definite,
    //    Positive,
    //    Negative
    //}

    [Table("Criterion_Сoefficient")]
    public class Coefficient
    {
        public Coefficient()
        {

        }

        public Coefficient(string name, Guid id, Guid coefficientType)
        {
            Name = name;
            Id = id;
            CoefficientType = coefficientType;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Column("Coefficient_Type")]
        [ForeignKey("Type")]
        public Guid CoefficientType { get; set; }

        
        public CriterionСoefficientType Type { get; set; }
    }
}
