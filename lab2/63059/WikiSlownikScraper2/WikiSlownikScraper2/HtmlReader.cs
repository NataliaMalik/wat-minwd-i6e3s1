using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;
using Nancy.Json;

namespace WikiSlownikScraper2
{
    class HtmlReader
    {
        public static async void Read(string url)               
        {
            var httpClient = new HttpClient();                  // docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
            var html = await httpClient.GetStringAsync(url);    

            var htmlDocument = new HtmlDocument();              // html-agility-pack.net/from-web
            htmlDocument.LoadHtml(html);

            var Table = htmlDocument.DocumentNode.Descendants("table")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("wikitable sortable")).ToList();

            var BaseLanguage = Table[0].SelectNodes("//tbody/tr/td[1]").ToList();
            var NewLanguage = Table[0].SelectNodes("//tbody/tr/td[3]").ToList();
            var PhotoSource = Table[0].SelectNodes("//tbody/tr/td[2]/a/img").ToList();

            string[] Photo = new string[PhotoSource.Count()];
            int j = 0;

            foreach (var elements in PhotoSource)
            {
                Photo[j] = "https:" + PhotoSource[j].Attributes["src"].Value;
                j++;
            }

            Console.WriteLine();

            int i = 0;
            List<Translation> words = new List<Translation>();

            foreach (var element in BaseLanguage)
            {
                words.Add(new Translation(BaseLanguage[i].InnerText.Trim(), NewLanguage[i].InnerText.Trim(), Photo[i]));
                ++i;
            }

            var json = new JavaScriptSerializer().Serialize(words);
            Console.WriteLine(json);
        }
    }
}
