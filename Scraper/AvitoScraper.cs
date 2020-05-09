
using HtmlAgilityPack;
using RieltorApp.Base;
using RieltorApp.DB;
using RieltorApp.NewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace RieltorApp.Scraper
{
    public class AvitoScraper : BaseScraper
    {
        public override async Task<List<ApartmentItem>> GetApartments(SearchArgumentModel argumentModel)
        {
            List<ApartmentItem> apartaments = new List<ApartmentItem>();

            if (argumentModel == null) return apartaments;

            var url = argumentModel.SearchType == SearchType.Buy ? "https://www.avito.ru/novokuznetsk/kvartiry/prodam-ASgBAgICAUSSA8YQ" : "https://www.avito.ru/novokuznetsk/kvartiry/sdam-ASgBAgICAUSSA8gQ";

            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var all_variants = document.DocumentNode.SelectNodes("//div[contains(@class, 'item item_table')]");

            foreach (var variant in all_variants)
            {
                try
                {
                    string price = variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim();
                    price = Regex.Replace(price, "[^0-9]", "");
                    if (string.IsNullOrEmpty(price)) continue;

                    string[] info = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim().Split(',');
                    if (info.Length != 3) continue;
                    string roomCount = Regex.Replace(info[0], "[^0-9]", "");
                    if (string.IsNullOrEmpty(roomCount)) continue;
                    string area = Regex.Replace(info[1], "[^0-9.]", "").Replace('.', ',');
                    string[] floors = info[2].Split('/');

                    string floor = Regex.Replace(floors[0], "[^0-9]", "");
                    string storeys = Regex.Replace(floors[1], "[^0-9]", "");

                    string[] address = variant.SelectSingleNode(".//span[contains(@class, 'item-address__string')]").InnerText.Trim().Split(',');

                    if (address.Length < 2) continue;

                    var street_vel = address[address.Length - 2].Split(' ');
                    if (street_vel.Length == 0) continue;
                    string street = string.Join(" ", street_vel.Where(v => v != null && v.Length > 0 && v[0].ToString().ToUpper() == v[0].ToString()));
                    string num = address[address.Length - 1];

                    if (variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]") == null)
                        continue;

                    var district_vel = variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]").InnerText.Trim().Split(' ');
                    string district = string.Join(" ", district_vel.Where(v => v[0].ToString().ToUpper() == v[0].ToString()));

                    string page_url = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").GetAttributeValue("href", "");

                    var img_src = variant.SelectSingleNode(".//img[contains(@class, 'large-picture-img')]");

                    string url_img = img_src == null ? "/RieltorApp;component/Resource/newBg.jpg" : img_src.GetAttributeValue("src", "/RieltorApp;component/Resource/newBg.jpg");

                    var apart = new ApartmentItem()
                    {
                        Area = float.Parse(area),
                        RoomCount = int.Parse(roomCount),
                        Floor = int.Parse(floor),
                        Storeys = int.Parse(storeys),
                        Price = int.Parse(price),
                        Address = new Address()
                        {
                            Street = street,
                            Num = num,
                            District = district,
                        },
                        PageUrl = "https://www.avito.ru" + page_url,
                        ImageUrl = url_img
                    };

                    if (argumentModel.MaxPrice * 1000 >= apart.Price && argumentModel.MinPrice * 1000 <= apart.Price &&
                        argumentModel.MaxArea >= apart.Area && argumentModel.MinArea <= apart.Area &&
                        argumentModel.MaxFloor >= apart.Floor && argumentModel.MinFloor <= apart.Floor &&
                        argumentModel.MaxStoreys >= apart.Storeys && argumentModel.MinStoreys <= apart.Storeys &&
                        (argumentModel.District == "Любой" || apart.Address.District.Contains(argumentModel.District) || apart.Address.District.Equals(argumentModel.District)) &&
                        (argumentModel.RoomCount == RoomCount.Any || (argumentModel.RoomCount == RoomCount.Many && apart.RoomCount >= 4) || (int)argumentModel.RoomCount == apart.RoomCount))
                        apartaments.Add(apart);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            return apartaments;
        }

        public override async Task<List<FoundApartment>> GetApartments(List<SearchArgumentModel> argumentModels, SearchType searchType)
        {
            List<FoundApartment> apartaments = new List<FoundApartment>();

            if (argumentModels == null || argumentModels.Count == 0) return apartaments;

            var url = searchType == SearchType.Buy ? "https://www.avito.ru/novokuznetsk/kvartiry/prodam-ASgBAgICAUSSA8YQ" : "https://www.avito.ru/novokuznetsk/kvartiry/sdam-ASgBAgICAUSSA8gQ";

            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);


            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var all_variants = document.DocumentNode.SelectNodes("//div[contains(@class, 'item item_table')]");

            foreach (var variant in all_variants)
            {
                try
                {
                    string price = variant.SelectSingleNode(".//span[contains(@class, 'snippet-price')]").InnerText.Trim();
                    price = Regex.Replace(price, "[^0-9]", "");
                    if (string.IsNullOrEmpty(price)) continue;

                    string[] info = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").InnerText.Trim().Split(',');
                    if (info.Length != 3) continue;
                    string roomCount = Regex.Replace(info[0], "[^0-9]", "");
                    if (string.IsNullOrEmpty(roomCount)) continue;
                    string area = Regex.Replace(info[1], "[^0-9.]", "").Replace('.', ',');
                    string[] floors = info[2].Split('/');

                    string floor = Regex.Replace(floors[0], "[^0-9]", "");
                    string storeys = Regex.Replace(floors[1], "[^0-9]", "");

                    string[] address = variant.SelectSingleNode(".//span[contains(@class, 'item-address__string')]").InnerText.Trim().Split(',');

                    if (address.Length < 2) continue;

                    var street_vel = address[address.Length - 2].Split(' ');
                    if (street_vel.Length == 0) continue;
                    string street = string.Join(" ", street_vel.Where(v => v != null && v.Length > 0 && v[0].ToString().ToUpper() == v[0].ToString()));
                    string num = address[address.Length - 1];

                    if (variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]") == null)
                        continue;

                    var district_vel = variant.SelectSingleNode(".//span[contains(@class, 'item-address-georeferences')]").InnerText.Trim().Split(' ');
                    string district = string.Join(" ", district_vel.Where(v => v[0].ToString().ToUpper() == v[0].ToString()));

                    string page_url = variant.SelectSingleNode(".//a[contains(@class, 'snippet-link')]").GetAttributeValue("href", "");

                    var img_src = variant.SelectSingleNode(".//img[contains(@class, 'large-picture-img')]");

                    string url_img = img_src == null ? "/RieltorApp;component/Resource/newBg.jpg" : img_src.GetAttributeValue("src", "/RieltorApp;component/Resource/newBg.jpg");

                    var apart = new ApartmentItem()
                    {
                        Area = float.Parse(area),
                        RoomCount = int.Parse(roomCount),
                        Floor = int.Parse(floor),
                        Storeys = int.Parse(storeys),
                        Price = int.Parse(price),
                        Address = new Address()
                        {
                            Street = street,
                            Num = num,
                            District = district,
                        },
                        PageUrl = "https://www.avito.ru" + page_url,
                        ImageUrl = url_img
                    };

                    argumentModels.ForEach(argumentModel =>
                    {
                        if (argumentModel.MaxPrice * 1000 >= apart.Price && argumentModel.MinPrice * 1000 <= apart.Price &&
                       argumentModel.MaxFloor >= apart.Floor && argumentModel.MinFloor <= apart.Floor &&
                       argumentModel.MaxStoreys >= apart.Storeys && argumentModel.MinStoreys <= apart.Storeys &&
                       (argumentModel.District == "Любой" || apart.Address.District.Contains(argumentModel.District) || apart.Address.District.Equals(argumentModel.District)) &&
                       (argumentModel.RoomCount == RoomCount.Any || (argumentModel.RoomCount == RoomCount.Many && apart.RoomCount >= 4) || (int)argumentModel.RoomCount == apart.RoomCount))
                            apartaments.Add(new FoundApartment() { apartment = apart, AutoSearchId = argumentModel.Id });
                    });

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            return apartaments;
        }
    }
}
