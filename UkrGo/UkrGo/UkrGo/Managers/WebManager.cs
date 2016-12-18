using System;

using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;
using HtmlAgilityPack;
using UkrGo.Model;

namespace UkrGo
{
    internal static class WebManager
    {
        public static async Task<ObservableCollection<RowData>> LoadItemsAsync(string link)
        {

            var client = new HttpClient();

            string s = await client.GetStringAsync(link);

            var doc = new HtmlDocument(); 
            doc.LoadHtml(s); 

            HtmlNode parent = doc.DocumentNode
                    .Descendants("div")
                    .FirstOrDefault(o => o.GetAttributeValue("class", "")
                                         == "main-content");
            HtmlNodeCollection dd = parent.ChildNodes;


            ObservableCollection<RowData> l = new ObservableCollection<RowData>();

            foreach (var n in dd)
            {
                try
                {
                    var r = new RowData
                    {
                        Link =
                            n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1]
                                .Attributes[0].Value,
                        Text = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText,
                        Description = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[5].InnerText,
                        MainImage =
                        {
                            ImageLink =
                                n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1]
                                    .ChildNodes[1].Attributes[1].Value
                        }
                    };
                    l.Add(r);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
           // await Task.Delay(30000);
            return l;
        }

        public static async Task<DetailData> GetDataByLink(string link)
        {
            var client = new HttpClient();
            DetailData dd = new DetailData();
            try
            {
                string s = await client.GetStringAsync(link);

                var doc = new HtmlDocument();
                doc.LoadHtml(s);

                HtmlNode parent = doc.DocumentNode
                    .Descendants("table")
                    .FirstOrDefault(o => o.GetAttributeValue("style", "")
                                         ==
                                         "width: 90%; margin-top: 19px; margin-left: auto; margin-right: auto; padding-top: 10px; text-align: left;");


               
                if (parent == null) return dd;

                dd.Caption = parent.ChildNodes[1].InnerText.Trim();
                dd.DetailString = parent.ChildNodes[3].OuterHtml.Split('\n');
                dd.Text = parent.ChildNodes[5].ChildNodes[1].ChildNodes[1].InnerText.Trim();
               
                dd.ImageLinks = new ObservableCollection<ImageData>();

                foreach (
                    var n in parent.ChildNodes[9].ChildNodes[1].ChildNodes[1].ChildNodes[0].ChildNodes[1].ChildNodes)
                {
                    if (n.Name != "a") continue;
                    try
                    {
                        var r = new ImageData {ImageLink = n.ChildNodes[0].Attributes[0].Value};

                        dd.ImageLinks.Add(r);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                int idx = dd.Text.IndexOf("Контактные телефоны", StringComparison.Ordinal);
                if (idx != -1)
                {
                    dd.Contact = dd.Text.Substring(idx, dd.Text.Length - idx);
                    dd.Text = dd.Text.Substring(0, idx).Trim();
                }
                return dd;
            }
            catch (Exception)
            {
                return dd;
            }
            return new DetailData();
        }
    }
}
