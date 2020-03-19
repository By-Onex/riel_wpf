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
        public ICommand ChangePageCommand { get; set; }
        private ListView _list;
        public ResultViewModel(ListView list)
        {
            _list = list;
            ChangePageCommand = new BaseCommand(ChangePage);
            StartMode.Instanse.TextState = "Идет поиск";
           GetAparts();
        }

        private void ChangePage(object obj)
        {
            StartMode.Instanse.UpdateView((Page)Activator.CreateInstance((Type)obj), "С чем работать?");
        }

        public async void GetAparts()
        {
            var aparts = await new AvitoScraper().GetApartments();
            _list.ItemsSource = aparts;
            StartMode.Instanse.TextState = "Ознакомьтесь с результатами поиска";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
