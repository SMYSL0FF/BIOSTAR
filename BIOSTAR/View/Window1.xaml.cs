using BIOSTAR.Core;
using BIOSTAR.View.Authorization;
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

namespace BIOSTAR.View
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Opacity = 0;
            FrameNavigate.FrameObject = WindowFrame;
            WindowFrame.Navigate(new Pages.Home());
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard fadeInStoryboard = FindResource("fadeInStoryboard") as Storyboard;
            fadeInStoryboard?.Begin(this);
        }


        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            var storyboard = (Storyboard)FindResource("ToggleMenu");
            var animation = storyboard.Children.First() as DoubleAnimation;

            if (MenuBtn.IsChecked == true)
            {
                animation.To = 200; // Ширина при раскрытии
            }
            else
            {
                animation.To = 60; // Ширина при закрытии
            }

            storyboard.Begin(SlideMenu);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Pages.Home());
        }

        private void WindowFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Pages.Orders());
        }

        private void Grid_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Pages.Client());
        }

        private void Grid_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Pages.reports());
        }
    }
}
