using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RieltorApp.DesigndViewModel
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public static ItemViewModel Instance => new ItemViewModel();

        public string price { get; set; }
        public string info { get; set; }
        public ItemViewModel()
        {
            price = "2 000 ₽";
            info = "1-к квартира, 45 м², 4/5 эт.";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
