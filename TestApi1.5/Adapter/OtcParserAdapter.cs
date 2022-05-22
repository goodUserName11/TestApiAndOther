using TestApi.Entity;
using TestApi.Parser;

namespace TestApi.Adapter
{
    public class OtcParserAdapter : AbstractSearchAdapter
    {
        public override async Task<List<Supplier>> Find(string okpd)
        {
            return await ParserContainer.OtcParser.Parse(okpd);
        }
    }
}
