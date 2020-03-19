using RieltorApp.Model;
using RieltorApp.View;
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
    public class SearchArgumentViewModel : INotifyPropertyChanged
    {

        public ICommand SearchCommand { get; set; }

        private RangeSlider priceSlider;

        private string _minPrice;
        public string MinPrice
        {
            get => _minPrice;
            set {
                _minPrice = value;
                NotifyPropertyChanged("MinPrice");
            }
        }

        private string _maxPrice;
        public string MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                NotifyPropertyChanged("MaxPrice");
            }
        }

        private string _maxFloor;
        public string MaxFloor
        {
            get => _maxFloor;
            set
            {
                _maxFloor = value;
                NotifyPropertyChanged("MaxFloor");
            }
        }

        private string _minFloor;
        public string MinFloor
        {
            get => _minFloor;
            set
            {
                _minFloor = value;
                NotifyPropertyChanged("MaxFloor");
            }
        }

        public SearchArgumentViewModel(RangeSlider priceSlider)
        {
            this.priceSlider = priceSlider;
            priceSlider.Slider_ValueChanged += Price_ValueChange;
            MinPrice = "0";
            
            priceSlider.Minimum = 0;
            priceSlider.Maximum = 1000;

           SearchCommand = new BaseCommand(ResultPage);

        }

        private void ResultPage(object obj)
        {
            StartMode.Instanse.UpdateView((UserControl)Activator.CreateInstance((Type)obj), "Идет поиск");
        }

        private void Price_ValueChange(double x1, double x2)
        {
            MinPrice = ((int)x1).ToString();
            MaxPrice = ((int)x2).ToString();
        }

        private void Floor_ValueChange(double x1, double x2)
        {
            MinFloor = ((int)x1).ToString();
            MaxFloor = ((int)x2).ToString();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
