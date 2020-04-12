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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RieltorApp.NewView
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
           // Anim();
        }



        private void Anim()
        {

            ThicknessAnimation verticalAnimation = new ThicknessAnimation();

            verticalAnimation.From = new Thickness(0, 0, 0, 0);
            verticalAnimation.To = new Thickness(0, -450, 0, 0);
            verticalAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(verticalAnimation);
            Storyboard.SetTarget(verticalAnimation, UpperPanel);
            Storyboard.SetTargetProperty(verticalAnimation, new PropertyPath(DockPanel.MarginProperty));
            
            storyboard.Begin();
        }
    }
}
