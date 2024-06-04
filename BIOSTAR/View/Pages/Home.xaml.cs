using BIOSTAR.Core;
using BIOSTAR.Model;
using BIOSTAR.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BIOSTAR.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            OrderInf.ItemsSource = FrameNavigate.DB.RepairOrders.ToList();
        }

        private async void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await FrameNavigate.DB.SaveChangesAsync();
            MessageBox.Show("Изменения сохранены!",
                    "Системное уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }

        private void BtnUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrderInf.ItemsSource = FrameNavigate.DB.RepairOrders.ToList();
        }
    }

    

}
