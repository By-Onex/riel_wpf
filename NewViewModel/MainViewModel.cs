using RieltorApp.NewModel;
using RieltorApp.NewView;
using RieltorApp.DB;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RieltorApp.NewViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance { get; } = new MainViewModel();
        private object _pageContent;
        public object PageContent
        {
            set
            {
                _pageContent = value;
                NotifyPropertyChanged();
            }
            get => _pageContent;
        }
        private string _pageStateText;
        public string PageStateText
        {
            get => _pageStateText;
            set {
                _pageStateText = value;
                NotifyPropertyChanged();
            }
        }
        public SearchArgumentModel SearchArgumentModel = new SearchArgumentModel();
        private MainViewModel()
        {
            DataBase.Connect();
            PageContent = new StartMenuView().Content;
            PageStateText = "Добро пожаловать! С чем работать?";
        }

        public void ChangePage(object contetPage)
        {
            PageContent = contetPage;
        }
        public void ChangePage(object contetPage, string newTitle)
        {
            PageContent = contetPage;
            PageStateText = newTitle;
        }


        private bool _goBottom;
        public bool GoBottom
        {
            get => _goBottom;
            set
            {
                if (value) GoTop = false;
                _goBottom = value;
                NotifyPropertyChanged();
            }
        }

        private bool _goTop;
        public bool GoTop
        {
            get => _goTop;
            set
            {

                if (value) GoBottom = false;
                _goTop = value;
                NotifyPropertyChanged();
            }
        }
    }
}
