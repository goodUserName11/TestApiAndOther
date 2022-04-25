using TestApi.Entity;
using TestApi.Parser;

namespace TestApi.Adapter
{
    public class Okpd2ParserAdapter : AbstractOkpd2Adapter
    {
        public override void AddToDb()
        {
            Okpd2Parser okpd2Parser = new Okpd2Parser();
            okpd2Parser.ParseOkpd2();
        }
    }
}
