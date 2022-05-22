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
            List<Okpd2> okpdsLetters;
            List<Okpd2> res = new();

            using (var dbContext = new SearchAndRangeContext())
            {
                okpdsLetters = dbContext.Okpd2s.Where(okpd => okpd.Code.Length == 1).ToList();

                if (top == null)
                    okpd2s = dbContext.Okpd2s.Take(dbContext.Okpd2s.Count() - okpdsLetters.Count).ToList();
                else
                    okpd2s = dbContext.Okpd2s.Take(top.Value - okpdsLetters.Count).ToList();
            }


            for (int i = 0; i < okpd2s.Count; i++)
            {
                if (okpdsLetters.Count > 0 && okpd2s[i].Parent == okpdsLetters[0].Code)
                {
                    res.Add(okpdsLetters[0]);

                    okpdsLetters.RemoveAt(0);
                }

                res.Add(okpd2s[i]);
            }

            return res;
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
