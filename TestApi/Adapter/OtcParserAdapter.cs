using TestApi.Entity;
using TestApi.Parser;

namespace TestApi.Adapter
{
    public class OtcParserAdapter : AbstractSearchAdapter
    {
        public override async Task<List<Supplier>> Find(string product)
        {
            OtcParser otcParcer = new OtcParser();

            return await otcParcer.Parse(product);
        }
    }
}
