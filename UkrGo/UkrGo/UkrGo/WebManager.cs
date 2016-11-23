using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using UkrGo.Model;

namespace UkrGo
{
    static class WebManager
    {
        public static async Task<IList<RowData>> LoadItemsAsync()
        {

            var client = new HttpClient();

            string s = await client.GetStringAsync(@"http://kiev.ukrgo.com/view_subsection.php?id_subsection=146");

            var doc = new HtmlDocument(); /* инсталляция объекта парсера HTML Agility Pack*/
            doc.LoadHtml(s); /*помещаем в парсер полученный html */

        

            HtmlNode parent = doc.DocumentNode
                    .Descendants("div")
                    .FirstOrDefault(o => o.GetAttributeValue("class", "")
                                         == "main-content");
            HtmlNodeCollection dd = parent.ChildNodes;

          
            IList<RowData> l = new List<RowData>();

            foreach (var n in dd)
            {
                try
                {
                    var r = new RowData();
                    r.Link = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes[0].Value;
                    r.ImageLink = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[1].Attributes[1].Value;
                    r.Text = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].InnerText;
                    r.Description = n.ChildNodes[1].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[5].InnerText;
                    l.Add(r);
                }
                catch (Exception) { }
            }
            return l;
        }
    }
}
