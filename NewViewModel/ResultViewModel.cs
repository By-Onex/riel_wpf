using RieltorApp.DB;
using RieltorApp.NewModel;
using RieltorApp.NewView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class ResultViewModel : BaseViewModel
    {
        public static ResultViewModel Instance { get; } = new ResultViewModel();
        private List<ApartmentItem> _items;
        public List<ApartmentItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }

        private Visibility _showResult;
        public Visibility ShowResult
        {
            get => _showResult;
            set
            {
                _showResult = value;
                NotifyPropertyChanged("ShowResult");
            }
        }
        private Visibility _showAnimation;
        public Visibility ShowAnimation
        {
            get => _showAnimation;
            set
            {
                _showAnimation = value;
                NotifyPropertyChanged("ShowAnimation");
            }
        }
        private Visibility _showStatus;
        public Visibility ShowStatus
        {
            get => _showStatus;
            set
            {
                _showStatus = value;
                NotifyPropertyChanged("ShowStatus");
            }
        }

        private string _sort;
        public string Sort
        {
            get => _sort;
            set
            {
                if(_sort != value)
                {
                    _sort = value;
                    NotifyPropertyChanged("Sort");
                    SortList();
                }
            }
        }
        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value.Trim();
                NotifyPropertyChanged("SearchText");
                Search();
            }
        }
        public ICommand ReturnToTop { get; set; }
        public ICommand OpenWebPage { get; set; }
        public ICommand AddFavorite { get; set; }
       
        private ResultViewModel()
        {
            Items = new List<ApartmentItem>();

            ShowAnimation = Visibility.Hidden;
            ShowResult = Visibility.Hidden;
            ShowStatus = Visibility.Hidden;

            ReturnToTop = new BaseCommand(o =>
            {
                MainView.GoTop();
                ShowResult = Visibility.Hidden;
                ShowAnimation = Visibility.Hidden;
                ShowStatus = Visibility.Hidden;
            });
            OpenWebPage = new BaseCommand(url =>
            {
                System.Diagnostics.Process.Start(url.ToString());
            });
            AddFavorite = new BaseCommand(apart =>
            {
                if(apart is ApartmentItem)
                {
                    FavoriteModel.AddFavorite(apart as ApartmentItem);
                }
            });
        }

        private void SortList()
        {
            if(_items != null && _sort != null)
            {
                _items.Sort( (a1, a2) =>
                {
                    switch (_sort)
                    {
                        case "Цена ↑":
                            return a1.Price == a2.Price ? 0 : a1.Price > a2.Price ? 1 : -1;
                        case "Цена ↓":
                            return a1.Price == a2.Price ? 0 : a1.Price > a2.Price ? -1 : 1;
                        case "Комнаты ↑":
                            return a1.RoomCount == a2.RoomCount ? 0 : a1.RoomCount > a2.RoomCount ? 1 : -1;
                        case "Комнаты ↓":
                            return a1.RoomCount == a2.RoomCount ? 0 : a1.RoomCount > a2.RoomCount ? -1 : 1;
                        default: return 0;
                    }
                });
                Items = new List<ApartmentItem>(_items);
            }
        }
        
        private void Search()
        {
            //_tempItems = new List<ApartmentItem>()
            if (!string.IsNullOrWhiteSpace(_searchText))
                _items.ForEach(apart =>
                {
                    if (apart.Address.DistrictInfo.Contains(_searchText) || apart.Address.Info.Contains(_searchText) || apart.Info.Contains(_searchText))
                        apart.Visibility = Visibility.Visible;
                    else apart.Visibility = Visibility.Collapsed;
                });
            else _items.ForEach(apart => apart.Visibility = Visibility.Visible);
        }
    }
}
