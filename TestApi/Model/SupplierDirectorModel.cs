using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace TestApi.Model
{
    [Serializable]
    public class SupplierDirectorModel
    {
        public SupplierDirectorModel()
        {
                
        }

        public SupplierDirectorModel(string name, string surname, string phone1, string phone2)
        {
            Name = name;
            Surname = surname;
            Phone1 = phone1;
            Phone2 = phone2;
        }

        public SupplierDirectorModel(string name, string surname, string? patronymic, string phone1, string phone2)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone1 = phone1;
            Phone2 = phone2;
        }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        [Required]
        public string Phone1 { get; set; }
        [Required]
        public string Phone2 { get; set; }
    }
}
