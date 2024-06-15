using BIOSTAR.Core;
using BIOSTAR.Model;
using BIOSTAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)

        {
            if (!FrameNavigate.IsAdminAuthenticated)
            {
                MessageBox.Show("Вы не авторизованы как администратор.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                // Перейти на страницу настроек
                CheckAdmin window = new CheckAdmin();
                window.Show();
                return;
            }

            if (OrderInf.SelectedItem is RepairOrders selectedOrder)
            {
                // Подтверждение удаления
                var result = MessageBox.Show("Вы уверены, что хотите удалить этот заказ?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Удаление заявки из базы данных
                        FrameNavigate.DB.RepairOrders.Remove(selectedOrder);
                        FrameNavigate.DB.SaveChanges();

                        // Обновление интерфейса
                        LoadRepairOrders(); // Метод для обновления списка заявок
                        MessageBox.Show("Заказ успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Произошла ошибка при удалении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите заказ для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Метод для загрузки заявок в DataGrid
        private void LoadRepairOrders()
        {
            OrderInf.ItemsSource = FrameNavigate.DB.RepairOrders.ToList();
        }
    }


    

}
