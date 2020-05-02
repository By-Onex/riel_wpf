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
    public class VariantScraper : BaseScraper
    {
        public override async Task<List<ApartmentItem>> GetApartments(SearchArgumentModel argumentModel)
        {
            List<ApartmentItem> apartaments = new List<ApartmentItem>();

            var url = argumentModel.SearchType == SearchType.Buy? "https://variant-nk.ru/object/search/novokuznetsk/kvartiry/prodam?page=1" : "https://variant-nk.ru/object/search/novokuznetsk/kvartiry/arenda?page=1";

            var httpClient = new HttpClient();
            string html = await httpClient.GetStringAsync(url);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var all_variants = document.DocumentNode.SelectNodes("//article[contains(@class, 'object-list')]");
            foreach (var variant in all_variants)
            {
                try
                {
                    string price = variant.SelectSingleNode(".//div[contains(@class, 'col-12 col-md-3')]").SelectSingleNode("./h4").InnerText.Trim();
                    price = Regex.Replace(price, "[^0-9]", "");
                    if (string.IsNullOrEmpty(price)) continue;

                    var root = variant.SelectSingleNode(".//div[contains(@class, 'col-12 col-md-7 object-to-link')]");

                    string[] info = root.SelectSingleNode("./h4").InnerText.Trim().Split(',');

                    if (info.Length != 3) continue;
                    string roomCount = Regex.Replace(info[0], "[^0-9]", "");
                    if (string.IsNullOrEmpty(roomCount)) continue;

                    string area = Regex.Replace(info[1], "[^0-9.]", "").Replace('.', ',');
                    area = area.Remove(area.Length - 1, 1);

                    string[] floors = info[2].Split('/');

                    string floor = Regex.Replace(floors[0], "[^0-9]", "");
                    string storeys = Regex.Replace(floors[1], "[^0-9]", "");
                    if (string.IsNullOrEmpty(floor) || string.IsNullOrEmpty(storeys)) continue;

                    string[] address = root.SelectSingleNode("./h5").InnerText.Trim().Split(',');

                    if (address.Length != 3) continue;

                    var district_vel = address[0].Trim().Split(' ');
                    string district = string.Join(" ", district_vel.Where(v => v[0].ToString().ToUpper() == v[0].ToString()));

                    string street = address[1];
                    string num = address[2];

                    string page_url = root.GetAttributeValue("href", "");
                    string url_img = variant.SelectSingleNode(".//img[contains(@class, 'img-responsive')]").GetAttributeValue("src", "/RieltorApp;component/Resource/newBg.jpg");

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
                        PageUrl = "https://variant-nk.ru" + page_url,
                        ImageUrl = url_img
                    };

                    if (argumentModel.MaxPrice * 1000 >= apart.Price && argumentModel.MinPrice * 1000 <= apart.Price &&
                        argumentModel.MaxFloor >= apart.Floor && argumentModel.MinFloor <= apart.Floor &&
                        argumentModel.MaxStoreys >= apart.Storeys && argumentModel.MinStoreys <= apart.Storeys &&
                        (argumentModel.District == "Любой" || apart.Address.District.Contains(argumentModel.District)) &&
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
    }
}
