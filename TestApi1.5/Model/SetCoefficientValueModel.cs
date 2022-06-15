using System.ComponentModel.DataAnnotations;
using TestApi.Entity;

namespace TestApi.Model
{
    public class SetCoefficientValueModel
    {
        public SetCoefficientValueModel()
        {
            Id = new List<string>();
            Value = new List<double>();
            IsActive = new List<bool> ();
        }

        //public SetCoefficientValueModel(CoefficientValue coefficientValue)
        //{
        //    Id = coefficientValue.Id.ToString();
        //    Value = coefficientValue.Value;
        //    IsActive = coefficientValue.IsActive;
        //}

        //public SetCoefficientValueModel(double value, bool isActive, string id, string name)
        //{
        //    Value = value;
        //    IsActive = isActive;
        //    Id = id;
        //}

        [Required]
        public List<string> Id { get; set; }
        [Required]
        public List<double> Value { get; set; }

        [Required]
        public List<bool> IsActive { get; set; }
    }
}
