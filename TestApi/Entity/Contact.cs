using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("Contact")]
    public class Contact
    {
        public Contact()
        {

        }

        public Contact(Guid id, string name, string surname, string? patronimic, string phone1, string? phone2)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronimic = patronimic;
            Phone1 = phone1;
            Phone2 = phone2;
        }

        [Key]
        public Guid Id { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronimic { get; set; }
        public string Phone1 { get; set; }
        public string? Phone2 { get; set; }
    }
}
