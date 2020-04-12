using RieltorApp.NewModel;
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
        public List<ApartmentModel> _items;
        public List<ApartmentModel> Items
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
        public ICommand ReturnToTop { get; set; }
        public ICommand OpenWebPage { get; set; }
        private ResultViewModel()
        {
            Items = new List<ApartmentModel>();
            ShowAnimation = Visibility.Hidden;
            ShowResult = Visibility.Hidden;
            ReturnToTop = new BaseCommand(o =>
            {
                MainViewModel.Instance.GoTop = true;
                ShowResult = Visibility.Hidden;
                ShowAnimation = Visibility.Hidden;
            });

            OpenWebPage = new BaseCommand(url =>
            {
                System.Diagnostics.Process.Start(url.ToString());
            });
        }
    }
}
