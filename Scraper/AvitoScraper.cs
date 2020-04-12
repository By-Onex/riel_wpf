
using HtmlAgilityPack;
using RieltorApp.NewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RieltorApp.Scraper
{
    public class AvitoScraper
    {
        public async Task<List<ApartmentModel>> GetApartments(SearchArgumentModel argumentModel)
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
                string price = variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim();
                price = Regex.Replace(price, "[^0-9]", "");
                if (string.IsNullOrEmpty(price)) continue;

                string[] info = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim().Split(',');
                if (info.Length != 3) continue;
                string roomCount = Regex.Replace(info[0], "[^0-9]", "");
                string area = Regex.Replace(info[1], "[^0-9.]", "").Replace('.', ',');
                string[] floors = info[2].Split('/');

                string floor = Regex.Replace(floors[0], "[^0-9]", "");
                string storeys = Regex.Replace(floors[1], "[^0-9]", "");

                string[] address = variant.SelectSingleNode(".//span[contains(@class, 'item-address__string')]").InnerText.Trim().Split(',');

                if (address.Length < 2) continue;
                string street = address[address.Length - 2];
                string num = address[address.Length - 1];

                string district = variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]").InnerText.Trim();

                string page_url = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").GetAttributeValue("href", "");

                var apart = new ApartmentModel()
                {
                    Area = float.Parse(area),
                    RoomCount = int.Parse(roomCount),
                    Floor = int.Parse(floor),
                    Storeys = int.Parse(storeys),
                    Price = int.Parse(price),
                    Street = street,
                    Num = num,
                    District = district,
                    Url = "https://www.avito.ru" + page_url
                };

                if (argumentModel.MaxPrice * 1000 >= apart.Price && argumentModel.MinPrice * 1000 <= apart.Price &&
                    argumentModel.MaxFloor >= apart.Floor && argumentModel.MinFloor <= apart.Floor && 
                    argumentModel.MaxStoreys >= apart.Storeys && argumentModel.MinStoreys <= apart.Storeys)
                    apartaments.Add(apart);
            }
            return apartaments;
        }

        public List<ApartmentModel> GetSyncApartments()
        {
            List<ApartmentModel> apartaments = new List<ApartmentModel>();

            var url = "https://www.avito.ru/novokuznetsk/kvartiry?cd=1";

            var httpClient = new HttpClient();
            string html =  httpClient.GetStringAsync(url).Result;


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var all_variants = document.DocumentNode.SelectNodes("//div[contains(@class, 'item item_table')]");

            foreach (var variant in all_variants)
            {
                string price = variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim();
                price = Regex.Replace(price, "[^0-9]", "");

                string[] info = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim().Split(',');
                if (info.Length != 3) continue;
                string roomCount = Regex.Replace(info[0], "[^0-9]", "");
                string area = Regex.Replace(info[1], "[^0-9.]", "").Replace('.',',');
                string[] floors = info[2].Split('/');

                string floor = Regex.Replace(floors[0], "[^0-9]", "");
                string storeys = Regex.Replace(floors[1], "[^0-9]", ""); 

                string[] address = variant.SelectSingleNode(".//span[contains(@class, 'item-address__string')]").InnerText.Trim().Split(',');

                if (address.Length < 2) continue;
                string street = address[address.Length - 2];
                string num = address[address.Length - 1];
                
                string district = variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]").InnerText.Trim();

                string page_url = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").GetAttributeValue("href", "");
                if (!string.IsNullOrEmpty(price))
                    apartaments.Add(new ApartmentModel()
                    {
                        Area = float.Parse(area),
                        RoomCount = int.Parse(roomCount),
                        Floor = int.Parse(floor),
                        Storeys = int.Parse(storeys),
                        Price = int.Parse(price),
                        Street = street,
                        Num = num,
                        District = district,
                        Url = "https://www.avito.ru" + page_url
                    });
            }
            return apartaments;
        }
    }
}
