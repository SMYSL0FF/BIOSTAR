using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BIOSTAR.Core;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BIOSTAR.View.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Login")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57));
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Login";
                textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57));
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            hintText.Visibility = Visibility.Collapsed; // Скрываем текст подсказки
            myPasswordBox.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста при фокусе
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(myPasswordBox.Password))
            {
                hintText.Visibility = Visibility.Visible; // Показываем текст подсказки, если пароль не введен
                myPasswordBox.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста с прозрачностью 73%
            }
            else
            {
                hintText.Visibility = Visibility.Collapsed; // Скрываем текст подсказки, если пароль введен
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Registration());
        }

        private void TextBlock_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.Show();
            MainWindow.GetWindow(this).Close();
        }
    }
}
