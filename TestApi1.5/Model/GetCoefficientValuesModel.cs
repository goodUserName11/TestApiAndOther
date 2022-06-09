using System.ComponentModel.DataAnnotations;
using TestApi.Entity;

namespace TestApi.Model
{
    public class GetCoefficientValuesModel
    {
        public GetCoefficientValuesModel()
        {

        }

        public GetCoefficientValuesModel(CoefficientValue coefficientValue)
        {
            Id = coefficientValue.Id.ToString();
            Value = coefficientValue.Value;
            IsActive = coefficientValue.IsActive;
            Name = coefficientValue.Coefficient.Name;
        }

        public GetCoefficientValuesModel(double value, bool isActive, string id, string name)
        {
            Value = value;
            IsActive = isActive;
            Id = id;
            Name = name;
        }

        [Required]
        public string Id { get; set; }
        [Required]
        public double Value { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
