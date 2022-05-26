using System.Net;
using TestApi.Entity;
using TestApi.Model;

namespace TestApi.Adapter
{
    public class SupplierSearchAdapter : AbstractSearchAdapter
    {
        public override async Task<List<Supplier>> Find(string okpd2, List<CretitionModel> cretiotions)
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();



                return null;
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }
    }
}
