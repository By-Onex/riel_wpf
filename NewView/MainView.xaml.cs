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
            resultView = resultGridView;

            margin = new Thickness();
        }
        private static bool IsTop = true;
        private static bool IsLeft = true;
        private static Grid animGrid;
        private static ResultView resultView;

        private static Thickness margin;

        private static int WIDTH = 800;
        private static int HEIGHT = 450;

        public static void GoBottom()
        {
            margin.Top -= HEIGHT;

            ThicknessAnimation animation = new ThicknessAnimation(margin, TimeSpan.FromMilliseconds(ANIM_DURATION));
            animation.Completed += (o, e) =>
            {
                animGrid.BeginAnimation(Grid.MarginProperty, null);
                animGrid.Margin = margin;
            };
            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsTop = false;
        }

        public static void GoTop()
        {
            margin.Top += HEIGHT;

            ThicknessAnimation animation = new ThicknessAnimation(margin, TimeSpan.FromMilliseconds(ANIM_DURATION));
           
            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsTop = true;
        }
        public static void GoRight()
        {
            margin.Left -= WIDTH;
            ThicknessAnimation animation = new ThicknessAnimation(margin, TimeSpan.FromMilliseconds(ANIM_DURATION));
            animation.Completed += (o, e) =>
            {
                animGrid.BeginAnimation(Grid.MarginProperty, null);
                animGrid.Margin = margin;
            };

            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsLeft = false;

            resultView.SetValue(Grid.ColumnProperty, 1);

        }
        public static void GoLeft()
        {
            margin.Left += WIDTH;
            ThicknessAnimation animation = new ThicknessAnimation(margin, TimeSpan.FromMilliseconds(ANIM_DURATION));

            animGrid.BeginAnimation(Grid.MarginProperty, animation);
            IsLeft = true;

            resultView.SetValue(Grid.ColumnProperty, 0);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainViewModel.Instance.WidthGrid = WIDTH = (int)e.NewSize.Width;
            MainViewModel.Instance.HeightGrid = HEIGHT = (int)e.NewSize.Height - 30;

            if (!IsTop || !IsLeft)
            {
                margin.Top = margin.Top != 0 ? -HEIGHT : 0;
                margin.Left = margin.Left != 0 ? -WIDTH : 0;
                animGrid.Margin = margin;
            }
        }
    }
}
