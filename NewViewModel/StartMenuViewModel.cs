using RieltorApp.NewModel;
using RieltorApp.NewView;
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
        public ICommand MovePage { get; set; }
        public StartMenuViewModel()
        {
            OpenPage = new BaseCommand((nextPage) =>
            {
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "Выберите тип поиска");
            });

            MovePage = new BaseCommand((p) =>
            {
                if (p as string == "Favorite")
                {
                    MainView.GoBottom();
                    ResultViewModel.Instance.ShowAnimation = Visibility.Visible;
                    ResultViewModel.Instance.ShowResult = Visibility.Hidden;
                    FavoriteModel.GetAparts();
                }
                else
                {
                    MainView.GoRight();
                }
            });
        }
    }
}
