using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BIOSTAR.Core; 

namespace BIOSTAR.View.Pages
{
    public partial class ChangeUserPass : Window
    {
        public ChangeUserPass()
        {
            InitializeComponent();
            BtnCheck.Click += BtnCheck_Click; 
        }

        private void PackIcon_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string newPassword = CPsbPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string hashedPassword = HashPassword(newPassword);
                UpdateUserPassword(username, hashedPassword);
                MessageBox.Show("Пароль успешно изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при смене пароля: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void UpdateUserPassword(string username, string hashedPassword)
        {
            var user = FrameNavigate.DB.Users.FirstOrDefault(u => u.username == username && u.is_admin == false);
            if (user != null)
            {
                user.password_hash = hashedPassword;
                FrameNavigate.DB.SaveChanges();
            }
            else
            {
                throw new Exception("Пользователь не найден или не является обычным пользователем");
            }
        }
    }
}
