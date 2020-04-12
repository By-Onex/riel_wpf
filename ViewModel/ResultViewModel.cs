using RieltorApp.Model;
using RieltorApp.Scraper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.ViewModel
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public static ResultViewModel Instatce => new ResultViewModel(); 
        public ICommand ChangePageCommand { get; set; }
        private List<ApartmentModel> _items;
        public List<ApartmentModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                NotifyPropertyChanged();
            }
        }

        public ResultViewModel()
        {
            Items = new List<ApartmentModel>()
            {
                new ApartmentModel()
                {
                    price = "20000000r",
                    info = "test info2222"
                }
            };
            /*
            ChangePageCommand = new BaseCommand(ChangePage);
            StartMode.Instanse.TextState = "Идет поиск";
            GetAparts();*/
        }

        /*private void ChangePage(object obj)
        {
            StartMode.Instanse.UpdateView((Page)Activator.CreateInstance((Type)obj), "С чем работать?");
        }*/


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
