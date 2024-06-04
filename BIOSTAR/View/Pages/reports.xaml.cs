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
    /// Логика взаимодействия для reports.xaml
    /// </summary>
    public partial class reports : Page
    {
        public reports()
        {
            InitializeComponent();
            
            // Получение данных из базы данных
            var orders = FrameNavigate.DB.RepairOrders.ToList();

            // Сортировка и фильтрация для отображения последних 5 заказов
            var sortedOrders = orders.OrderByDescending(o => o.appointment_date).Take(5).ToList();

            // Привязка данных к DataGrid
            OrderInf.ItemsSource = sortedOrders;
        }
    }
}
