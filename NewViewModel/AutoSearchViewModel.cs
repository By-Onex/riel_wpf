using RieltorApp.NewModel;
using RieltorApp.NewView;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class AutoSearchViewModel : BaseViewModel
    {
        public static AutoSearchViewModel Instance { get; } = new AutoSearchViewModel();

        private AutoSearchModel searchModel = new AutoSearchModel();
        public ICommand BackToMain { get; set; }
        public ICommand ToggledForm { get; set; }
        public ICommand ViewResults { get; set; }
        public ICommand ChangeItem { get; set; }
        public ICommand DeleteItem { get; set; }

        private Visibility _showForm = Visibility.Collapsed;
        public Visibility ShowForm
        {
            get => _showForm;
            set
            {
                _showForm = value;
                NotifyPropertyChanged("ShowForm");

                if(_showForm == Visibility.Visible)
                {
                    ShowElements = Visibility.Hidden;
                    ShowStatus = Visibility.Hidden;
                }
                else
                {
                    ShowElements = Visibility.Visible;
                    ShowStatus = _items.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
                }
            }
        }

        private Visibility _showElements = Visibility.Visible;
        public Visibility ShowElements
        {
            get => _showElements;
            set
            {
                _showElements = value;
                NotifyPropertyChanged("ShowElements");
            }
        }

        private Visibility _showStatus = Visibility.Collapsed;
        public Visibility ShowStatus
        {
            get => _showStatus;
            set
            {
                _showStatus = value;
                NotifyPropertyChanged("ShowStatus");
            }
        }

        private SearchViewModel _searchViewModel;
        public SearchViewModel SearchViewModel
        {
            get => _searchViewModel;
            set
            {
                _searchViewModel = value;
                NotifyPropertyChanged("SearchViewModel");
            }
        }
        public ObservableCollection<DB.AutoSearchItem> _items = new ObservableCollection<DB.AutoSearchItem>();
        public ObservableCollection<DB.AutoSearchItem> Items
        {
            get => _items; set
            {
                _items = value;
                NotifyPropertyChanged("Items"); 
            }
        }
        private DB.AutoSearchItem _selectedItem;
        private AutoSearchViewModel()
        {
            Items = new ObservableCollection<DB.AutoSearchItem>();

            searchModel.GetAutoSearchItem();

            var vm = new SearchViewModel();
            vm.OpenPage = new BaseCommand(o =>
            {
                ShowForm = Visibility.Collapsed;
            });

            vm.SearchCommand = new BaseCommand(o =>
            {
                ShowForm = Visibility.Collapsed;
                if (_searchViewModel.ButtonText == "Добавить")
                    searchModel.AddNewAutoSearchItem(vm._searchModel);
                else searchModel.UpdateAutoSearchItem(_selectedItem, vm._searchModel);
                ShowStatus = _items.Count > 0 ?
                     Visibility.Collapsed : Visibility.Visible;

            });

            SearchViewModel = vm;

            ToggledForm = new BaseCommand(o =>
            {
                _searchViewModel.ButtonText = "Добавить";
                _searchViewModel.Description = "";
                ShowForm = ShowForm == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            });

            BackToMain = new BaseCommand(o =>
            {
                MainView.GoLeft();
            });

            ChangeItem = new BaseCommand(item =>
            {
                if (!(item is DB.AutoSearchItem)) return;
                _selectedItem = item as DB.AutoSearchItem;

                _searchViewModel.ButtonText = "Изменить";
                _searchViewModel.Description = _selectedItem.Description;
                _searchViewModel.RoomCount = _searchViewModel.ConvertToText(_selectedItem.RoomCount);

                _searchViewModel.MinPrice = _selectedItem.MinPrice.ToString();
                _searchViewModel.MaxPrice = _selectedItem.MaxPrice.ToString();

                _searchViewModel.MinArea = _selectedItem.MinArea.ToString();
                _searchViewModel.MaxArea = _selectedItem.MaxArea.ToString();

                _searchViewModel.MinFloor = _selectedItem.MinFloor.ToString();
                _searchViewModel.MaxFloor = _selectedItem.MaxFloor.ToString();

                _searchViewModel.MinStoreys = _selectedItem.MinStoreys.ToString();
                _searchViewModel.MaxStoreys = _selectedItem.MaxStoreys.ToString();

                ShowForm = Visibility.Visible;
            });

            DeleteItem = new BaseCommand(item =>
            {
                if (item is DB.AutoSearchItem)
                {
                    searchModel.DeleteAutoSearchItem(item as DB.AutoSearchItem);
                }
            });

            ViewResults = new BaseCommand(item =>
            {
                if (item is DB.AutoSearchItem)
                {
                    ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                    ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                    MainView.GoBottom();

                    searchModel.GetFoundApartment(item as DB.AutoSearchItem);
                }
            });
        }
    }
}
