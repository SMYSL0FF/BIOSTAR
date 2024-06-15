using BIOSTAR.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BIOSTAR.View
{
    /// <summary>
    /// Логика взаимодействия для CheckAdmin.xaml
    /// </summary>
    public partial class CheckAdmin : Window
    {
        public CheckAdmin()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
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

                // Проверка состояния аутентификации администратора
                if (!FrameNavigate.IsAdminAuthenticated)
                {
                    // Хэширование введенного пароля
                    string inputPasswordHash = ComputeSha256Hash(CPsbPassword.Password);

                    // Получаем пользователя-администратора
                    var adminUser = FrameNavigate.DB.Users
                        .FirstOrDefault(u => u.username.Equals("admin", StringComparison.Ordinal));

                    // Проверка на null для пользователя-администратора
                    if (adminUser == null)
                    {
                        MessageBox.Show("Пользователь admin не найден.",
                            "Системная ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    // Проверяем пароль у пользователя-администратора
                    if (adminUser.password_hash != inputPasswordHash)
                    {
                        MessageBox.Show("Неверный пароль. Пожалуйста, проверьте введенные данные.",
                            "Ошибка данных",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    // Устанавливаем флаг аутентификации администратора
                    FrameNavigate.IsAdminAuthenticated = true;
                }

                // Проверка на null для FrameObject перед навигацией
                if (FrameNavigate.FrameObject == null)
                {
                    MessageBox.Show("Навигационная рамка (FrameObject) не инициализирована.",
                        "Системная ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

               
                CheckAdmin.GetWindow(this).Close();
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
    }
}
