using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class ProductGetFromApi
    {
        public ProductGetFromApi()
        {

        }

        public ProductGetFromApi(string id, string okpd2, string name)
        {
            Id = id;
            Okpd2 = okpd2;
            Name = name;
        }

        [Required]
        public string Id { get; set; }
        [Required]
        public string Okpd2 { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
