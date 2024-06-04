using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BIOSTAR.Core; 
namespace BIOSTAR.View.Pages
{
    public partial class ChangeAdminPass : Window
    {
        public ChangeAdminPass()
        {
            InitializeComponent();
            BtnCheck.Click += BtnCheck_Click; // Добавьте обработчик события нажатия на кнопку
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
            string newPassword = CPsbPassword.Password;
            string confirmPassword = CPsbPassword1.Password;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Пожалуйста, введите оба пароля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string hashedPassword = HashPassword(newPassword);
                UpdateAdminPassword(hashedPassword);
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

        private void UpdateAdminPassword(string hashedPassword)
        {
            var adminUser = FrameNavigate.DB.Users.FirstOrDefault(user => user.username == "admin" && user.is_admin == true);
            if (adminUser != null)
            {
                adminUser.password_hash = hashedPassword;
                FrameNavigate.DB.SaveChanges();
            }
            else
            {
                throw new Exception("Администратор не найден");
            }
        }

    }
}
