using RieltorApp.DB;
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
    public class AutoSearchViewModel : BaseViewModel
    {
        public static AutoSearchViewModel Instance { get; } = new AutoSearchViewModel();

        public ICommand BackToMain { get; set; }

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

        public List<DB.AutoSearchItem> _items;
        public List<DB.AutoSearchItem> Items
        {
            get => _items; set
            {
                _items = value;
                NotifyPropertyChanged("Items");
            }
        }
        private AutoSearchViewModel()
        {
            BackToMain = new BaseCommand(o =>
            {
                MainView.GoLeft();
            });
        }
    }
}
