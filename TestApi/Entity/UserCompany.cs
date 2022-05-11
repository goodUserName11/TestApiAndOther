using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Table("UserCompany")]
    public class UserCompany
    {
        public UserCompany()
        {

        }

        public UserCompany(string inn, string companyName, string address)
        {
            Inn = inn;
            CompanyName = companyName;
            Address = address;
        }

        public string Inn { get; set; }
        [Column("Company_Name")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
    }
}
