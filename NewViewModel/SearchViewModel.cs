﻿using RieltorApp.NewModel;
using RieltorApp.Scraper;
using System.Windows;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        private SearchArgumentModel _searchModel;
        public ICommand SearchCommand { get; set; }

        private string _district;
        public string District
        {
            get => _district;
            set
            {
                if (_district != value)
                {
                    _district = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _roomCount;
        public string RoomCount
        {
            get => _roomCount;
            set
            {
                if (_roomCount != value)
                {
                    _roomCount = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string MinPrice {
            get => _searchModel.MinPrice.ToString();
            set
            {
                _searchModel.MinPrice = CheckValue(value);
                if(_searchModel.MinPrice > _searchModel.MaxPrice)
                {
                    int max = _searchModel.MinPrice;
                    _searchModel.MinPrice = _searchModel.MaxPrice;
                    MaxPrice = max.ToString();
                }
                NotifyPropertyChanged();
            }
        }
        public string MaxPrice
        {
            get => _searchModel.MaxPrice.ToString();
            set
            {
                _searchModel.MaxPrice = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        public string MinArea
        {
            get => _searchModel.MinArea.ToString();
            set
            {
                _searchModel.MinArea = CheckValue(value);
                if (_searchModel.MinArea > _searchModel.MaxArea)
                {
                    int max = _searchModel.MinPrice;
                    _searchModel.MinArea = _searchModel.MaxArea;
                    MaxArea = max.ToString();
                }
                
                NotifyPropertyChanged();
            }
        }
        public string MaxArea
        {
            get => _searchModel.MaxArea.ToString();
            set
            {
                _searchModel.MaxArea = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        public string MinStoreys
        {
            get => _searchModel.MinStoreys.ToString();
            set
            {
                _searchModel.MinStoreys = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        public string MaxStoreys
        {
            get => _searchModel.MaxStoreys.ToString();
            set
            {
                _searchModel.MaxStoreys = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        public string MinFloor
        {
            get => _searchModel.MinFloor.ToString();
            set
            {
                _searchModel.MinFloor = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        public string MaxFloor
        {
            get => _searchModel.MaxFloor.ToString();
            set
            {
                _searchModel.MaxFloor = CheckValue(value);
                NotifyPropertyChanged();
            }
        }
        private int CheckValue(string val)
        {
            int num;
            int.TryParse(val, out num);
            return num;
        }
        public SearchViewModel()
        {
            _searchModel = MainViewModel.Instance.SearchArgumentModel;
            SearchCommand = new BaseCommand( (o)=>
            {
                MainViewModel.Instance.GoBottom = true;
                ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                _searchModel.GetAparts();
            });
        }
    }
}
