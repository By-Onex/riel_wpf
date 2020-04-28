using RieltorApp.Base;
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
    public class SearchArgumentModel
    {
        public SearchType SearchType = SearchType.Buy;
        public int MinPrice, MaxPrice;
        public int MinArea, MaxArea;
        public int MinFloor, MaxFloor;
        public int MinStoreys, MaxStoreys;
        public int RoomCount;

        private List<BaseScraper> _scrapers;

        public SearchArgumentModel()
        {
            _scrapers = new List<BaseScraper>() { new AvitoScraper(), new VariantScraper() };
        }
        public async void GetAparts()
        {
            var tasks = _scrapers.ToList().Select(async s => await s.GetApartments(this));
            var tasks_result = await Task.WhenAll(tasks);

            var aparts = new List<ApartmentModel>();
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
