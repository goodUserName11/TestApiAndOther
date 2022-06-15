using System.ComponentModel.DataAnnotations;
using TestApi.Entity;

namespace TestApi.Model
{
    public class SetCoefficientValueModel
    {
        public SetCoefficientValueModel()
        {

        }

        public SetCoefficientValueModel(CoefficientValue coefficientValue)
        {
            Id = coefficientValue.Id.ToString();
            Value = coefficientValue.Value;
            IsActive = coefficientValue.IsActive;
        }

        public SetCoefficientValueModel(double value, bool isActive, string id, string name)
        {
            Value = value;
            IsActive = isActive;
            Id = id;
        }

        [Required]
        public string Id { get; set; }
        [Required]
        public double Value { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
