using RieltorApp.Model;
using RieltorApp.Scraper;
using RieltorApp.View;
using System;
using System.Windows;


namespace RieltorApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            var sm = new StartMode();
            StartMode.Instanse = sm;
            DataContext = sm;
            InitializeComponent();
            sm.TextState = "Добро пожаловать!\nС чем работать?";
            sm.PageContent = new StartChangePage().Content;
        }
    }
}
