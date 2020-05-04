using RieltorApp.NewViewModel;
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
        private const int ANIM_DURATION = 300;
        public MainView()
        {
            InitializeComponent();
            animGrid = AnimGrid;

            zero = new Thickness();
            top = new Thickness(0, -MainViewModel.Instance.HeightGrid, 0, 0);
            left = new Thickness(-MainViewModel.Instance.WidthGrid, 0, 0, 0);
        }
        private static bool IsTop = true;
        private static bool IsLeft = true;
        private static Grid animGrid;
        private static Thickness top;
        private static Thickness left;
        private static Thickness zero;
        public static void GoBottom()
        {
            ThicknessAnimation animation = new ThicknessAnimation(top, TimeSpan.FromMilliseconds(ANIM_DURATION));
            animation.Completed += (o, e) =>
            {
                animGrid.BeginAnimation(Grid.MarginProperty, null);
                animGrid.Margin = top;
            };
            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsTop = false;
        }

        public static void GoTop()
        {
            ThicknessAnimation animation = new ThicknessAnimation(zero, TimeSpan.FromMilliseconds(ANIM_DURATION));
           
            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsTop = true;
        }

        public static void GoRight()
        {
            ThicknessAnimation animation = new ThicknessAnimation(left, TimeSpan.FromMilliseconds(ANIM_DURATION));
            animation.Completed += (o, e) =>
            {
                animGrid.BeginAnimation(Grid.MarginProperty, null);
                animGrid.Margin = left;
            };
            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsLeft = false;
        }

        public static void GoLeft()
        {
            ThicknessAnimation animation = new ThicknessAnimation(zero, TimeSpan.FromMilliseconds(ANIM_DURATION));

            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsLeft = true;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainViewModel.Instance.HeightGrid = (int)e.NewSize.Height - 30;
            MainViewModel.Instance.WidthGrid = (int)e.NewSize.Width;
            top.Top = -MainViewModel.Instance.HeightGrid;
            left.Left = -MainViewModel.Instance.WidthGrid;
            if (!IsTop)
            {
                animGrid.Margin = top;
            }
            if (!IsLeft)
            {
                animGrid.Margin = left;
            }
        }
    }
}
