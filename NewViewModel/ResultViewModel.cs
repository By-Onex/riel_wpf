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
        public List<ApartmentItem> _items;
        public List<ApartmentItem> Items
        {
            get => _items; set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        private Visibility _showResult;
        public Visibility ShowResult
        {
            get => _showResult;
            set
            {
                _showResult = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _showAnimation;
        public Visibility ShowAnimation
        {
            get => _showAnimation;
            set
            {
                _showAnimation = value;
                NotifyPropertyChanged();
            }
        }
        private Visibility _showStatus;
        public Visibility ShowStatus
        {
            get => _showStatus;
            set
            {
                _showStatus = value;
                NotifyPropertyChanged();
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
    }
}
