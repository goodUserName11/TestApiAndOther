using System.ComponentModel.DataAnnotations;

namespace TestApi.Model
{
    public class CrititionsModel
    {
        public CrititionsModel()
        {

        }

        public CrititionsModel(decimal maxPrice, decimal minPrice, string region, 
            int count, string wayOfDestribution, string okpd2)
        {
            MaxPrice = maxPrice;
            MinPrice = minPrice;
            Region = region;
            Count = count;
            WayOfDestribution = wayOfDestribution;
            Okpd2 = okpd2;
        }

        [Required]
        public decimal MaxPrice { get; set; }
        [Required]
        public decimal MinPrice { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string WayOfDestribution { get; set; }
        [Required]
        public string Okpd2 { get; set; }
    }
}
