using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using TestApi.Entity;

namespace TestApi.Parser
{
    public class OtcParser : AbstractWebSiteParser
    {
        public override async Task<List<Supplier>> Parse(string product)
        {
            
            var requester = new DefaultHttpRequester();
            requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.44";

            using (var context = BrowsingContext.New(Configuration.Default.With(requester).WithDefaultLoader()))
            {
                string url = "https://otc.ru/marketplace-b2b/";

                if (!string.IsNullOrWhiteSpace(product))
                    if (int.TryParse(product, out int okpd2))
                        url += $"?ct={product}";
                    else
                        url += $"?k={product}";

                using var doc = await context.OpenAsync(url);

                var links =
                    doc.QuerySelectorAll
                    <IHtmlAnchorElement>("a");

                links = links.Where(item => item.Attributes["href"].Value.Contains("marketplace-b2b/offer"));

                List<Supplier> suppliers = new List<Supplier>();

                int i = 0;
                foreach (var item in links)
                {
                    i++;
                    if (i == 6) break;
                    //suppliersNameList.Add(item.Text.Trim('\n',' '));

                    using var secondDoc = await item.NavigateAsync();

                    var supplierLink = secondDoc.QuerySelectorAll<IHtmlAnchorElement>("a").
                        FirstOrDefault(item => item.Attributes["href"].Value.StartsWith("https://otc.ru/counterparty"));

                    //if (supplierLink == null)
                    //    return BadRequest();

                    Supplier supplier = new Supplier();

                    //using var supplierDoc = await context.OpenAsync(supplierLink.Href);
                    using var supplierDoc = await supplierLink.NavigateAsync();

                    await supplierDoc.WaitForReadyAsync();
                    supplierDoc.Load(supplierDoc.BaseUri);
                    //supplierDoc.Scripts[0].CreateRequestFor

                    //var body = (await  }).Body;


                    //var InnEl =
                    //    supplierDoc.
                    //    All.Where(
                    //        div =>
                    //            div.LocalName.ToLower() == "div" &&
                    //            div.TextContent.StartsWith("инн")
                    //    );

                    //var divs = supplierDoc.
                    //    All.Where(
                    //        div =>
                    //            div.TagName == "DIV"
                    //    );

                    //var el1 = divs.ElementAt(19);
                    //var el = InnEl.ElementAt(0).InnerHtml;

                    Console.WriteLine(supplierDoc.Minify());
                    Console.WriteLine(supplierDoc.ReadyState.ToString());
                    Console.WriteLine(supplierDoc.Body.NodeName);
                    Console.WriteLine(supplierDoc.Body.NodeType);
                    Console.WriteLine(supplierDoc.Body.NodeValue);

                    //foreach (var div in supplierDoc.Body.Children)
                    //{
                    //    if (div.TagName == "DIV" /*&& div.LocalName == "div"*/ && div.Text().ToLower().StartsWith("инн"))
                    //        Console.WriteLine($"{div.Text()}");
                    //}

                    //if (null == null)
                    //    return BadRequest();

                    //return Ok(suppliers);

                    //supplier.Inn = InnEl?.Parent
                    //        ?.ChildNodes[1].Text();

                    //var statusEl = supplierDoc.
                    //    All.FirstOrDefault(
                    //        div =>
                    //            div.LocalName == "div" &&
                    //             div.HasAttribute("class") &&
                    //            div.GetAttribute("class").Contains("statusClass")
                    //    );

                    //if (statusEl == null)
                    //    return BadRequest();

                    //if (statusEl.HasAttribute("style") &&
                    //        statusEl.GetAttribute("style").Contains("rgb(95, 209, 143)")
                    //)
                    //    supplier.status = true;
                    //else supplier.status = false;

                    //supplier.Name = statusEl.Parent
                    //     .ChildNodes[0].Text();

                    suppliers.Add(supplier);
                }

                //return Ok(suppliers);
                //using var secondDoc = await lnk.NavigateAsync();

                //var supplierInfoElements =
                //    secondDoc.
                //    All.Where(
                //        div =>
                //            div.LocalName == "div" &&
                //            div.HasAttribute("id") &&
                //            div.GetAttribute("id").
                //            StartsWith("specifications")).
                //            ToList();

                //if (supplierInfoElements is null)
                //    return NotFound();

                //List<string> supplierInfos = new List<string>();

                //foreach(var item in supplierInfoElements)
                //{
                //    supplierInfos.Add(item.Text());
                //}

                return new List<Supplier>();
            }
        }
    }
}
