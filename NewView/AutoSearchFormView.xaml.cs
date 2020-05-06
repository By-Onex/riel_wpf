using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RieltorApp.NewView
{
    /// <summary>
    /// Логика взаимодействия для AutoSearchFormView.xaml
    /// </summary>
    public partial class AutoSearchFormView : UserControl
    {
        public AutoSearchFormView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(c => c >= '0' && c <= '9');
        }
    }
}
