
using HtmlAgilityPack;
using RieltorApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace RieltorApp.Scraper
{
    public class AvitoScraper
    {
        public async Task<List<ApartmentModel>> GetApartments()
        {
            List<ApartmentModel> apartaments = new List<ApartmentModel>();

            var url = "https://www.avito.ru/novokuznetsk/kvartiry?cd=1";
            
            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var all_variants = document.DocumentNode.SelectNodes("//div[contains(@class, 'item item_table')]");
            
            foreach (var variant in all_variants)
            {
                //Console.WriteLine(variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim());
                //Console.WriteLine(variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim());
                
              
                apartaments.Add(new ApartmentModel()
                {
                    info = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim(),
                    price = variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim()
                });
            }
            return apartaments;
        }
    }
}
