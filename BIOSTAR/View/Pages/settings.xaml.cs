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

namespace BIOSTAR.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для settings.xaml
    /// </summary>
    public partial class settings : Page
    {
        public settings()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigate.IsAdminAuthenticated = false;

            MessageBox.Show("Возможности администратора отключены!",
                "Системное уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangeAdminPass window = new ChangeAdminPass();
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ChangeUserPass window = new ChangeUserPass();
            window.Show();
        }
    }
}
