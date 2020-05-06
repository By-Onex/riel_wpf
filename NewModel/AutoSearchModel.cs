using RieltorApp.Base;
using RieltorApp.DB;
using RieltorApp.NewViewModel;
using RieltorApp.Scraper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace RieltorApp.NewModel
{
    public class AutoSearchModel
    {
        public AutoSearchModel()
        {

        }
        static List<BaseScraper> _scrapers = new List<BaseScraper>() { new AvitoScraper(), new VariantScraper() };
        public static void StartSearchApartment()
        {
            Console.WriteLine("StartSearchApartment");
            Timer searchTimer = new Timer();

            searchTimer.Interval = 10 * 60 * 1000; //10 min

            searchTimer.Elapsed += Tick;
            searchTimer.Enabled = true;
            searchTimer.AutoReset = true;

            searchTimer.Start();

            Tick(null, null);
        }
        private static async void Tick(object o, EventArgs e)
        {
            Console.WriteLine("Start search");
            var searchArgument = DataBase.GetCollectionList<AutoSearchItem>(DBTable.AutoSearchApartment);

            if (searchArgument.Count == 0) return;

            var buyArg = searchArgument.Where(s => s.SearchType == SearchType.Buy).Select(s => SearchArgumentModel.Convert(s)).ToList();
            var arendaArg = searchArgument.Where(s => s.SearchType == SearchType.Arenda).Select(s => SearchArgumentModel.Convert(s)).ToList();

            var tasksBuy = _scrapers.ToList().Select(async s => await s.GetApartments(buyArg, SearchType.Buy));
            var tasksArenda = _scrapers.ToList().Select(async s => await s.GetApartments(arendaArg, SearchType.Arenda));

            var tasks = new List<Task<List<FoundApartment>>>();
            tasks.AddRange(tasksBuy);
            tasks.AddRange(tasksArenda);

            var tasks_result = await Task.WhenAll(tasks);

            var aparts = new List<FoundApartment>();
            tasks_result.ToList().ForEach((t) => aparts.AddRange(t));

            var col = DataBase.GetCollectionList<FoundApartment>(DBTable.FoundApartment);

            aparts.ForEach(res =>
            {
                if (DataBase.Query<FoundApartment>(DBTable.FoundApartment).Where(apart =>
                  apart.apartment.Address == res.apartment.Address &&
                  apart.apartment.Area == res.apartment.Area &&
                  apart.apartment.Price == res.apartment.Price &&
                  apart.apartment.Floor == res.apartment.Floor &&
                  apart.apartment.Storeys == res.apartment.Storeys).Limit(1).ToList().Count == 0)
                {
                    DataBase.Insert(res, DBTable.FoundApartment);
                    if (AutoSearchViewModel.Instance.Items != null)
                    {
                        var asItem = AutoSearchViewModel.Instance.Items.FirstOrDefault(a => a.Id == res.AutoSearchId);
                        if(asItem != null)
                        {
                            asItem.FoundApart++;
                            asItem.Num = asItem.Num;
                            DataBase.Update(asItem, DBTable.AutoSearchApartment);
                        }
                    }
                }
            });
        }

        private static void SearchApart()
        {

        }
        public void AddNewAutoSearchItem(SearchArgumentModel searchArgument)
        {
            var item = new AutoSearchItem();
            item.MaxArea = searchArgument.MaxArea;
            item.MinArea = searchArgument.MinArea;

            item.MaxPrice = searchArgument.MaxPrice;
            item.MinPrice = searchArgument.MinPrice;

            item.MaxFloor = searchArgument.MaxFloor;
            item.MinFloor = searchArgument.MinFloor;

            item.MaxStoreys = searchArgument.MaxStoreys;
            item.MinStoreys = searchArgument.MinStoreys;

            item.District = searchArgument.District;
            item.RoomCount = searchArgument.RoomCount;

            item.Description = searchArgument.Description;
            item.Num = (AutoSearchViewModel.Instance.Items.Count + 1).ToString();
            DataBase.Insert(item, DBTable.AutoSearchApartment);
            AutoSearchViewModel.Instance.Items.Add(item);
        }

        public async void GetAutoSearchItem()
        {
            var items = await Task.Run(() => { return DataBase.GetCollectionList<AutoSearchItem>(DBTable.AutoSearchApartment); });
            Console.WriteLine(items.Count);
            var foundAparts = DataBase.GetCollectionList<FoundApartment>(DBTable.FoundApartment);
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Num = (i+1).ToString();
                items[i].FoundApart = foundAparts.Where(a => a.AutoSearchId == items[i].Id).ToList().Count;
            }
            AutoSearchViewModel.Instance.Items = new System.Collections.ObjectModel.ObservableCollection<AutoSearchItem>(items);

            if (items.Count == 0)
            {
                AutoSearchViewModel.Instance.ShowStatus = Visibility.Visible;
            }
        }
   
        public void DeleteAutoSearchItem(AutoSearchItem item)
        {
            DataBase.Delete<FoundApartment>(apart => item.Id == apart.AutoSearchId, DBTable.FoundApartment);
            
            DataBase.Delete(item.Id, DBTable.AutoSearchApartment);
            AutoSearchViewModel.Instance.Items.Remove(item);

            for (int i = 0; i < AutoSearchViewModel.Instance.Items.Count; i++)
            {
                AutoSearchViewModel.Instance.Items[i].Num = (i + 1).ToString();
            }
        }

        public void UpdateAutoSearchItem(AutoSearchItem item, SearchArgumentModel searchArgument)
        {
            item.SearchType = searchArgument.SearchType;
            item.MaxArea = searchArgument.MaxArea;
            item.MinArea = searchArgument.MinArea;

            item.MaxPrice = searchArgument.MaxPrice;
            item.MinPrice = searchArgument.MinPrice;

            item.MaxFloor = searchArgument.MaxFloor;
            item.MinFloor = searchArgument.MinFloor;

            item.MaxStoreys = searchArgument.MaxStoreys;
            item.MinStoreys = searchArgument.MinStoreys;

            item.District = searchArgument.District;
            item.RoomCount = searchArgument.RoomCount;

            item.Description = searchArgument.Description;
            item.Num = item.Num;
            AutoSearchViewModel.Instance.Items[int.Parse(item.Num) - 1] = item;

            DataBase.Update(item, DBTable.AutoSearchApartment);
        }
   
        public void GetFoundApartment(AutoSearchItem item)
        {
            ResultViewModel.Instance.Items = DataBase.Query<FoundApartment>(DBTable.FoundApartment)
                .Where(a => a.AutoSearchId == item.Id)
                .Select(a => a.apartment).ToList();

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;

            if (ResultViewModel.Instance.Items.Count == 0)
            {
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
            }
        }
    }
}
