using TestApi.Data;
using TestApi.Entity;
using TestApi.Parser;

namespace TestApi.Adapter
{
    public class Okpd2ParserAdapter : AbstractOkpd2Adapter
    {
        public override async void AddToDb()
        {
            Console.WriteLine("Info: initialization...");

            var okpd2s = await ParserContainer.Okpd2Parser.ParseOkpd2();

            using (SearchAndRangeContext dbContext = new SearchAndRangeContext())
            {
                await dbContext.Okpd2s.AddRangeAsync(okpd2s);

                dbContext.SaveChanges();

                dbContext.Dispose();
            }

            okpd2s.Clear();

            GC.Collect();

            Console.WriteLine("Info: Ready to work");
        }

        public override List<Okpd2> GetAllOkpd2s(int? top = null)
        {
            List<Okpd2> okpd2s;
            using (var dbContext = new SearchAndRangeContext())
            {
                if (top == null)
                    okpd2s = dbContext.Okpd2s.ToList();
                else
                    okpd2s = dbContext.Okpd2s.Take(top.Value).ToList();
            }

            return okpd2s;
        }

        public override List<Okpd2> GetOkpd2sByText(string text)
        {
            List<Okpd2> okpd2s;
            using (var dbContext = new SearchAndRangeContext())
            {
                okpd2s =
                    dbContext
                    .Okpd2s
                    .Where
                    (
                        okpd =>
                        okpd.Code.Contains(text)
                        || okpd.Name.Contains(text)
                    )
                    .ToList();
            }

            return okpd2s;
        }
    }
}
