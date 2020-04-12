using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.NewViewModel
{
    public class TypeSearchViewModel : BaseViewModel
    {
        public ICommand OpenPage { get; set; }

        public TypeSearchViewModel()
        {
            OpenPage = new BaseCommand((nextPage) =>
            {
                MainViewModel.Instance.ChangePage(((UserControl)Activator.CreateInstance((Type)nextPage)).Content, "Заполните необходимые критерии");
            });
        }
    }
}
