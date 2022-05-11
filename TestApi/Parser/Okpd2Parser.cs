using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using TestApi.Data;
using TestApi.Entity;

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
        public async Task<List<Okpd2>> ParseOkpd2()
        {
            var requester = new DefaultHttpRequester();
            requester.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36 Edg/100.0.1185.44";

            List<Okpd2> okpd2Elements = new List<Okpd2>();

            using (var context = BrowsingContext.New(Configuration.Default.With(requester).WithDefaultLoader()))
            {
                string url = "https://base.garant.ru/70650730/#friends";

                using var doc = await context.OpenAsync(url);

                await doc.WaitForReadyAsync();

                var okpd2Container = doc.QuerySelector("#ul_num1");

                foreach (var item in okpd2Container.Children)
                {
                    var okpdValues = item.QuerySelector("a").Text().Substring(7).Split(". ");

                    okpd2Elements.Add(new Okpd2(okpdValues[0], okpdValues[1]));

                    var innerOkpdDoc = await ((IHtmlAnchorElement)item.QuerySelector("a")).NavigateAsync();

                    await innerOkpdDoc.WaitForReadyAsync();

                    var okpd2InnerList = innerOkpdDoc.QuerySelectorAll("tr");

                    for (int i = 0; i < okpd2InnerList.Count(); i++)
                    {
                        var listItem = okpd2InnerList[i].QuerySelectorAll("p");

                        string okpdKey, okpdValue;

                        if (listItem.Count() == 0 || listItem.Count() > 2)
                            continue;

                        //if (listItem[0] == null || listItem[1] == null)
                        //    continue;

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
                        }
                        else
                        {
                            okpdKey = listItem[0].InnerHtml;
                            okpdValue = listItem[1].InnerHtml;
                        }
                        //Console.WriteLine($"{okpdKey}: {okpdValue}");

                        if (okpdValue.Contains('"'))
                            okpdValue = okpdValue.Replace("\"", "");
                                
                        okpd2Elements.Add(new Okpd2(okpdKey, okpdValue));

                        innerOkpdDoc.Dispose();
                    }
                }

                okpd2Elements = okpd2Elements.DistinctBy(okpd => okpd.Code).ToList();

                okpd2Elements = okpd2Elements.DistinctBy(okpd => okpd.Name).ToList();

                Stack<string> parentsCodes = new(); 

                for (int i = 0; i < okpd2Elements.Count; i++)
                {
                    var currCodeParts = okpd2Elements[i].Code.Split('.');

                    string[] parentsCodeParst;

                    if (currCodeParts.Length == 1)
                    {
                        parentsCodes.Clear();
                        parentsCodes.Push(okpd2Elements[i].Code);
                        continue;
                    }

                    bool canStop = false;
                    while (parentsCodes.Count > 0 && !canStop)
                    {
                        parentsCodeParst = parentsCodes.Peek().Split('.');

                        for (int j = currCodeParts.Length - 2; j >= 0; j--)
                        {
                            if (currCodeParts.Length > 1 &&
                                (currCodeParts[j] == parentsCodeParst[^1]
                                 && currCodeParts.Length > parentsCodeParst.Length
                                )
                                || (parentsCodeParst.Length == 1))
                            {
                                okpd2Elements[i].Parent = parentsCodes.Peek();
                                canStop = true;
                                break;
                            }
                        }

                        if (!canStop)
                            parentsCodes.Pop();
                    }

                    parentsCodes.Push(okpd2Elements[i].Code);
                }

                doc.Dispose();

                return okpd2Elements;
            }
        } 
    }
}
