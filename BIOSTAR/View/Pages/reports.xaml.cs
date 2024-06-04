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

             GenerateReportButton.Click += GenerateReportButton_Click;

            // Получение данных из базы данных
            var orders = FrameNavigate.DB.RepairOrders.ToList();

            // Сортировка и фильтрация для отображения последних 5 заказов
            var sortedOrders = orders.OrderByDescending(o => o.appointment_date).Take(5).ToList();

            // Привязка данных к DataGrid
            OrderInf.ItemsSource = sortedOrders;
        }
        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный тип отчета
            string reportType = ((ComboBoxItem)ReportType.SelectedItem)?.Content?.ToString();

            // Получаем начальную и конечную даты
            DateTime? startDate = StartDate.SelectedDate;
            DateTime? endDate = EndDate.SelectedDate;

            // Проверяем, что все параметры заполнены
            if (string.IsNullOrEmpty(reportType))
            {
                MessageBox.Show("Пожалуйста, выберите тип отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!startDate.HasValue || !endDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите начальную и конечную даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создаем отчет (логика создания отчета будет зависеть от ваших требований)
            CreateReport(reportType, startDate.Value, endDate.Value);
        }

        private void CreateReport(string reportType, DateTime startDate, DateTime endDate)
        {
            // Здесь добавьте вашу логику для создания отчета
            // Например, запрос данных из базы данных и генерация отчета в нужном формате

            MessageBox.Show($"Создается {reportType} отчет с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
