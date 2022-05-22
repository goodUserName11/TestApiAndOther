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

            for (int i = 1; i < innerHtml.Length - 1; i++)
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

        private string getStringWithoutTag(string innerHtml)
        {
            string str = getStringBetweenTags(innerHtml);
            string res = "";

            foreach (var item in innerHtml)
            {
                if (item == '<')
                    break;

                res += item;
            }

            return $"{res} {str}";
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

                        if (listItem.Count() < 2)
                            continue;

                        if (listItem[0].FirstElementChild != null)
                            listItem[0].InnerHtml = listItem[0].FirstElementChild.InnerHtml;
                        if (listItem[1].FirstElementChild != null)
                            listItem[1].InnerHtml = listItem[1].FirstElementChild.InnerHtml;


                        if (string.IsNullOrWhiteSpace(listItem[0].InnerHtml) || !char.IsDigit(listItem[0].InnerHtml[0]))
                            continue;

                        if (listItem[1].InnerHtml.Contains("href"))
                            listItem[1].InnerHtml = getStringWithoutTag(listItem[1].InnerHtml);

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

                        if (okpdValue.Contains('"'))
                            okpdValue = okpdValue.Replace("\"", "");

                        okpd2Elements.Add(new Okpd2(okpdKey, okpdValue));

                        innerOkpdDoc.Dispose();
                    }
                }

                okpd2Elements = okpd2Elements.DistinctBy(okpd => okpd.Code).ToList();

                Stack<string> parentsCodes = new();

                for (int i = 0; i < okpd2Elements.Count; i++)
                {
                    if (char.IsUpper((okpd2Elements[i].Code[0])))
                        parentsCodes.Clear();

                    while (parentsCodes.Count > 0)
                    {
                        string? parentCodeWithoutZeroes = null;
                        var splittedParent = parentsCodes.Peek().Split(".");

                        if (okpd2Elements[i].Code == "86.22.19.900" || okpd2Elements[i].Code == "86.23"
                                || okpd2Elements[i].Code == "86.9" || okpd2Elements[i].Code == "96.02"
                                || okpd2Elements[i].Code == "20.59.59.100" || okpd2Elements[i].Code == "31.09.91.115")
                        {
                            if (okpd2Elements[i].Code == "96.02")
                                okpd2Elements[i].Parent = "96.0";

                            else if (okpd2Elements[i].Code == "86.22.19.900")
                                okpd2Elements[i].Parent = "86.22.19";

                            else if (okpd2Elements[i].Code == "86.23")
                                okpd2Elements[i].Parent = "86.2";

                            else if (okpd2Elements[i].Code == "86.9")
                                okpd2Elements[i].Parent = "86";

                            else if (okpd2Elements[i].Code == "20.59.59.100")
                                okpd2Elements[i].Parent = "20.59.59";

                            if (okpd2Elements[i].Code == "31.09.91.115")
                                okpd2Elements[i].Parent = "31.09.91.110";

                            break;
                        }

                        if (splittedParent.Length == 4)
                        {
                            if (splittedParent[3].EndsWith("00") || splittedParent[3].EndsWith("0"))
                                parentCodeWithoutZeroes = $"{splittedParent[0]}.{splittedParent[1]}.{splittedParent[2]}.{splittedParent[3].Trim('0')}";
                        }

                        if (okpd2Elements[i].Code.StartsWith(parentsCodes.Peek())
                            || (splittedParent.Length == 4 && parentCodeWithoutZeroes != null && okpd2Elements[i].Code.StartsWith(parentCodeWithoutZeroes))
                            || char.IsUpper(parentsCodes.Peek()[0]))
                        {
                            okpd2Elements[i].Parent = parentsCodes.Peek();
                            break;
                        }
                        else
                        {
                            parentsCodes.Pop();
                            continue;
                        }
                    }

                    parentsCodes.Push(okpd2Elements[i].Code);
                }

                doc.Dispose();

                return okpd2Elements;
            }
        }
    }
}
