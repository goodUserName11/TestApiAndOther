using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using TestApi.Data;
using TestApi.Entity;
//using AngleSharp.Js;
//using AngleSharp.Js.Dom;

namespace TestApi.Parser
{
    public class Okpd2Parser
    {
        private string getStringBetweenTags(string innerHtml)
        {
            string resStr = "";
            bool passedFirstTag = false;

            for(int i = 1; i < innerHtml.Length - 1; i++)
            {
                if (innerHtml[i - 1] == '>')
                    passedFirstTag = true;

                if (passedFirstTag)
                    resStr += innerHtml[i];

                if (passedFirstTag && innerHtml[i + 1] == '<')
                    break;

            }

            return resStr;
        }
        public async void ParseOkpd2()
        {
            var requester = new DefaultHttpRequester();
            requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.44";

            var config = new Configuration();

            List<Okpd2> okpd2Elements = new List<Okpd2>();
            Dictionary<string, string> okpd2sDictionary = new Dictionary<string, string>();

            using (var context = BrowsingContext.New(Configuration.Default.With(requester).WithJs().WithDefaultLoader()))
            {
                string url = "https://base.garant.ru/70650730/#friends";

                using var doc = await context.OpenAsync(url);

                await doc.WaitForReadyAsync();
                await doc.WaitUntilAvailable();

                var okpd2Container = doc.QuerySelector("#ul_num1");

                foreach (var item in okpd2Container.Children)
                {
                    var keyValue = item.QuerySelector("a").Text().Substring(7).Split(". ");

                    //okpd2sDictionary.Add(keyValue[0], keyValue[1]);
                    okpd2Elements.Add(new Okpd2(keyValue[0], keyValue[1]));

                    var innerOkpdDoc = await ((IHtmlAnchorElement)item.QuerySelector("a")).NavigateAsync();

                    await innerOkpdDoc.WaitForReadyAsync();
                    await innerOkpdDoc.WaitUntilAvailable();

                    //"p.s_1 span.s_10"

                    var okpd2InnerList = innerOkpdDoc.QuerySelectorAll("tr");
                    //innerOkpdDoc.All.Where
                    //(
                    //    el =>
                    //    el.ClassName == "s_1" || el.ClassName = "s_10"
                    //);



                    for (int i = 0; i < okpd2InnerList.Count(); i++)
                    {
                        var listItem = okpd2InnerList[i].QuerySelectorAll("p");

                        string okpdKey, okpdValue;

                        if (listItem.Count() == 0 || listItem.Count() > 2)
                            continue;

                        if (listItem[0] == null || listItem[1] == null)
                            continue;

                        if (string.IsNullOrWhiteSpace(listItem[0].InnerHtml) || !char.IsDigit(listItem[0].InnerHtml[0]))
                            continue;

                        if (listItem[1].InnerHtml.Contains("href"))
                            continue;

                        if (listItem[0].InnerHtml.StartsWith('<'))
                        {
                            okpdKey =
                                getStringBetweenTags(listItem[0].InnerHtml);

                            okpdValue =
                                getStringBetweenTags(listItem[1].InnerHtml);
                            //okpd2InnerList[i + 1]
                            //.InnerHtml.Remove(0, okpd2InnerList[i + 1].InnerHtml.IndexOf('>'))
                            //.Remove(okpd2InnerList[i + 1].InnerHtml.IndexOf('<'));
                        }
                        else
                        {
                            okpdKey = listItem[0].InnerHtml;
                            okpdValue = listItem[1].InnerHtml;
                        }
                        //Console.WriteLine($"{okpdKey}: {okpdValue}");

                        //okpd2sDictionary.TryAdd(okpdKey, okpdValue);
                        okpd2Elements.Add(new Okpd2(okpdKey, okpdValue));

                    }
                }

                okpd2Elements = okpd2Elements.DistinctBy(okpd => okpd.Code).ToList();

                okpd2Elements.RemoveAll(x => x.Name == null || x.Code == null);

                using (SearchAndRangeContext dbContext = new SearchAndRangeContext())
                {
                    //foreach (var item in okpd2Elements)
                    //{
                    //    var dbOkpd2 = dbContext.Okpd2s.Find(item.Code);

                    //    if (dbOkpd2 == null)
                    //        await dbContext.Okpd2s.AddRangeAsync(item);
                    //}

                    await dbContext.Okpd2s.AddRangeAsync(okpd2Elements);

                    dbContext.SaveChanges();
                }

                return;

                ////IHtmlAnchorElement opt = 
                ////    (IHtmlAnchorElement)doc.QuerySelector("a.search-form__btn-advanced-search");

                ////opt.DoClick();

                //IHtmlButtonElement okpdButton = 
                //    (IHtmlButtonElement)doc.QuerySelector("#OKPD2_open_button");

                //okpdButton.DoClick();
                //okpdButton.Dispatch(new Event("click"));
                //okpdButton.Dispatch(new Event("focus"));

                //await doc.WhenStable();

                ////okpdButton.InvokeEventListener(new Event("click"));

                ////doc.Scripts[0].
                ////Jint.Engine engine = new Jint.Engine(); engine.
                //IHtmlCollection<IElement> okpd2DocEls = null;

                //await doc.Then(
                //    doc => okpd2DocEls =
                //    doc.ChildNodes.QuerySelectorAll("a.jstree-node-name"));


                //foreach (var item in okpd2DocEls)
                //{
                //    okpd2sDictionary.Add(item.Children[0].TextContent, item.NodeValue);
                //}

                //return okpd2Elements;
            }
        } 
    }
}
