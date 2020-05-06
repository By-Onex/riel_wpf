using RieltorApp.Base;
using RieltorApp.DB;
using RieltorApp.NewViewModel;
using RieltorApp.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RieltorApp.NewModel
{
    public enum SearchType
    {
        Buy,
        Arenda
    }

    public enum RoomCount
    {
        One = 1,
        Two = 2,
        Three = 3,
        Many = 4,
        Any = -1
    }
    public class SearchArgumentModel
    {
        public SearchType SearchType = SearchType.Buy;
        public int MinPrice = 0, MaxPrice = 10000;
        public int MinArea = 0, MaxArea = 100;
        public int MinFloor= 0, MaxFloor = 10;
        public int MinStoreys = 0, MaxStoreys = 10;
        public string District = "Любой";
        
        public RoomCount RoomCount = RoomCount.Any;

        public string Description = "";
        private List<BaseScraper> _scrapers;

        public int Id;
        public static SearchArgumentModel Convert(AutoSearchItem item)
        {
            var sam = new SearchArgumentModel();
            sam.Id = item.Id;

            sam.SearchType = item.SearchType;
            sam.MinPrice = item.MinPrice;
            sam.MaxPrice = item.MaxPrice;
            sam.MinArea = item.MinArea;
            sam.MaxArea = item.MaxArea;
            sam.MinFloor = item.MinFloor;
            sam.MaxFloor = item.MaxFloor;

            sam.MinStoreys = item.MinStoreys;
            sam.MaxStoreys = item.MaxStoreys;
            sam.District = item.District;

            return sam;
        }
        public SearchArgumentModel()
        {
            _scrapers = new List<BaseScraper>() { new AvitoScraper(), new VariantScraper() };
        }
        public async void GetAparts()
        {
            var tasks = _scrapers.ToList().Select(async s => await s.GetApartments(this));
            var tasks_result = await Task.WhenAll(tasks);

            var aparts = new List<ApartmentItem>();
            tasks_result.ToList().ForEach((t) => aparts.AddRange(t));

            ResultViewModel.Instance.Items = aparts;

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;
            
            if (aparts.Count == 0)
            {
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
            }
        }
    }
}
