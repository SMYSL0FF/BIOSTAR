using BIOSTAR.Core;
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
using BIOSTAR.View;
using BIOSTAR.Model;
using System.Security.Cryptography;

namespace BIOSTAR.View.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        public string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
            RPsbPassword.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста при фокусе
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RPsbPassword.Password))
            {
                hintText.Visibility = Visibility.Visible; // Показываем текст подсказки, если пароль не введен
                RPsbPassword.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста с прозрачностью 73%
            }
            else
            {
                hintText.Visibility = Visibility.Collapsed; // Скрываем текст подсказки, если пароль введен
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.FrameObject.Navigate(new Login());
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RTxbLogin.Text) || string.IsNullOrEmpty(RPsbPassword.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены!",
                "Системное сообщение",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            else
            {
                if (FrameNavigate.DB.Users.Count(u => u.username == RTxbLogin.Text) > 0)
                {
                    MessageBox.Show("Пользователь с таким именем уже зарегистрирован!",
                        "Системная ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    // Хэширование пароля
                    string passwordHash = ComputeSha256Hash(RPsbPassword.Password);

                    // Добавление пользователя с хэшированным паролем
                    FrameNavigate.DB.Users.Add(new Users
                    {
                        is_admin = false,
                        username = RTxbLogin.Text,
                        password_hash = passwordHash
                    });

                    await FrameNavigate.DB.SaveChangesAsync();
                    MessageBox.Show("Учетная запись создана!",
                            "Системное уведомление",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                    Window1 window1 = new Window1();
                    window1.Show();
                    MainWindow.GetWindow(this).Close();
                }
            }
        }

    }
}
