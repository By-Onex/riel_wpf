using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class TypeSearchViewModel : BaseViewModel
    {
        public ICommand OpenPageSearch { get; set; }

        public TypeSearchViewModel()
        {
            OpenPageSearch = new BaseCommand((st) =>
            {
                MainViewModel.Instance.SearchArgumentModel.SearchType = (string)st == "Buy"  ? NewModel.SearchType.Buy : NewModel.SearchType.Arenda;
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance(typeof(NewView.SearchView))).Content, "Заполните необходимые критерии");
            });
        }
    }
}
