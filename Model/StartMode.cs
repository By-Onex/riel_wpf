using RieltorApp.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RieltorApp.Model
{
    public delegate void UpdatePage(Page page);

    public class UpdateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private UpdatePage _updatePage;

        public UpdateCommand(UpdatePage updatePage)
        {
            _updatePage = updatePage;
        }
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter) =>
            _updatePage?.Invoke((Page)Activator.CreateInstance((Type)parameter));
    }

    class StartMode : INotifyPropertyChanged
    {
        public static StartMode Instanse;
        public ICommand OpenArgCommand { get; set; }
        private ICommand update;
        public ICommand Update
        {
            get { return this.update; }
        }
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

        private string _textState;
        public string TextState
        {
            set
            {
                _textState = value;
                NotifyPropertyChanged();
            }
            get => _textState;
        }

        public StartMode()
        {
            this.update = new UpdateCommand(UpdatePage);
            OpenArgCommand = new BaseCommand(OpenArg);
        }

        public void UpdateView(Page page, string title)
        {
            PageContent = page.Content;
            TextState = title;
        }

        public void UpdateView(UserControl page, string title)
        {
            PageContent = page;
            TextState = title;
        }

        private void OpenArg(object obj)
        {
            UpdateView((UserControl)Activator.CreateInstance((Type)obj), "Заполните необходимые критерии");
        }

        private void UpdatePage(Page page)
        {
            PageContent = page.Content;
            TextState = page.Title;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
