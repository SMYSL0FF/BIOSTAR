using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BIOSTAR.Core;
using BIOSTAR.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

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

        private string ComputeSha256Hash(string rawData)
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
            LPsbPassword.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста при фокусе
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LPsbPassword.Password))
            {
                hintText.Visibility = Visibility.Visible; // Показываем текст подсказки, если пароль не введен
                LPsbPassword.Foreground = new SolidColorBrush(Color.FromArgb(0xB3, 0x49, 0x50, 0x57)); // Цвет текста с прозрачностью 73%
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



        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на null для базы данных
                if (FrameNavigate.DB == null)
                {
                    MessageBox.Show("Соединение с базой данных не установлено.",
                        "Системная ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                // Проверка на null для таблицы пользователей
                if (FrameNavigate.DB.Users == null)
                {
                    MessageBox.Show("Таблица пользователей не найдена.",
                        "Системная ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                // Хэширование введенного пароля
                string inputPasswordHash = ComputeSha256Hash(LPsbPassword.Password);

                // Получаем список пользователей с указанным логином
                var users = FrameNavigate.DB.Users
                    .Where(u => u.username.Equals(LTxbLogin.Text, StringComparison.Ordinal))
                    .ToList();

                // Проверяем пароли у пользователей с указанным логином
                var userModel = users.FirstOrDefault(u => u.password_hash == inputPasswordHash);

                // Проверка на null для найденного пользователя
                if (userModel == null)
                {
                    MessageBox.Show("Неверный логин или пароль. Пожалуйста, проверьте введенные данные.",
                        "Ошибка данных",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    // Проверка на null для FrameObject перед навигацией
                    if (FrameNavigate.FrameObject == null)
                    {
                        MessageBox.Show("Навигационная рамка (FrameObject) не инициализирована.",
                            "Системная ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    // В зависимости от роли пользователя, перенаправить его на соответствующую страницу
                    Window1 window1 = new Window1();
                    window1.Show();
                    MainWindow.GetWindow(this).Close();
                }
            }
            catch (Exception ex)
            {
                // В случае исключения показать сообщение об ошибке
                MessageBox.Show("Произошла системная ошибка: " + ex.Message,
                    "Системная ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private void TxbForgot_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Функция восстановления пароля еще не реализована.",
                "Системное сообщение",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
