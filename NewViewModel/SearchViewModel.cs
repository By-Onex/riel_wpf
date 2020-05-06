using RieltorApp.NewModel;
using RieltorApp.NewView;
using RieltorApp.Scraper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchArgumentModel _searchModel { get; private set; }
        public ICommand SearchCommand { get; set; }
        public ICommand OpenPage { get; set; }
        public string District
        {
            get => _searchModel.District;
            set
            {
                if (_searchModel.District != value)
                {
                    _searchModel.District = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string RoomCount
        {
            get => ConvertToText(_searchModel.RoomCount);
            set
            {
                RoomCount val = ConvertToRoomCount(value);
               
                if (_searchModel.RoomCount != val)
                {
                    _searchModel.RoomCount = val;
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
        public string Description
        {
            get => _searchModel.Description;
            set
            {
                _searchModel.Description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _buttonText = "Добавить";
        public string ButtonText { get => _buttonText; set { _buttonText = value; NotifyPropertyChanged("ButtonText"); } }

        private int CheckValue(string val)
        {
            int num;
            int.TryParse(val, out num);
            return num;
        }
        public SearchViewModel()
        {
            _searchModel = new SearchArgumentModel();//MainViewModel.Instance.SearchArgumentModel;
            SearchCommand = new BaseCommand( (o)=>
            {
                MainView.GoBottom();
                ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                _searchModel.SearchType = MainViewModel.Instance.SearchArgumentModel.SearchType;
                _searchModel.GetAparts();
            });

            OpenPage = new BaseCommand((nextPage) =>
            {
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "С чем работать?");
            });
        }

        public string ConvertToText(RoomCount rc)
        {
            switch (rc)
            {
                case NewModel.RoomCount.One:
                case NewModel.RoomCount.Two:
                case NewModel.RoomCount.Three:
                    return ((int)rc).ToString();
                case NewModel.RoomCount.Many:
                    return "4+";
                default:
                    return "Любое";
            }
        }
        private RoomCount ConvertToRoomCount(string value)
        {
            switch (value)
            {
                case "1":
                case "2":
                case "3":
                    return (RoomCount)int.Parse(value);
                case "4+":
                    return NewModel.RoomCount.Many;
                default:
                    return NewModel.RoomCount.Any;
            }
        }
    }
}
