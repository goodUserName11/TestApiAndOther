using AngleSharp;
using AngleSharp.Io;
using TestApi.Entity;

namespace TestApi.Parser
{
    public class RoseltorgParser : AbstractWebSiteParser
    {
        public override async Task<List<Supplier>> Parse(string product)
        {
            throw new NotImplementedException();
            //var requester = new DefaultHttpRequester();
            //requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.44";

            //using (var context = BrowsingContext.New(Configuration.Default.With(requester).WithDefaultLoader()))
            //{
            //    string url = "https://otc.ru/marketplace-b2b/";

            //    if (!string.IsNullOrWhiteSpace(product))
            //        if (int.TryParse(product, out int okpd2))
            //            url += $"?ct={product}";
            //        else
            //            url += $"?k={product}";

            //    using var doc = await context.OpenAsync(url);


            //}
        }
    }
}
