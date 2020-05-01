using RieltorApp.NewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class StartMenuViewModel : BaseViewModel
    {
        public ICommand OpenPage { get; set; }
        public ICommand FavoritePage { get; set; }
        public StartMenuViewModel()
        {
            OpenPage = new BaseCommand((nextPage) =>
            {
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "Выберите тип поиска");
            });

            FavoritePage = new BaseCommand((o) =>
            {
                MainViewModel.Instance.GoBottom = true;
                ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;

                FavoriteModel.GetAparts();
            });
        }
    }
}
