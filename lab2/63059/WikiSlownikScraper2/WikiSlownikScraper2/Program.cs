using System;
using HtmlAgilityPack;

namespace WikiSlownikScraper2
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlReader.Read("https://pl.wiktionary.org/wiki/Indeks:Angielski_-_Kraje_Afryki");
            Console.ReadLine();
        }
    }
}
