using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BIOSTAR.ViewModels
{
    public class ChartViewModelOrders
    {
        public SeriesCollection PieSeriesCollection { get; set; }

        public ChartViewModelOrders()
        {
            // Пример инициализации
            PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Приняты в работу",
                    Values = new ChartValues<double> { 5 },
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"))// Пример значений
                    // Другие настройки

                },
                new PieSeries
            {
                Title = "В работе",
                Values = new ChartValues<double> { 8 },
                Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#336699"))
            },
                 new PieSeries
            {
                Title = "Готовы",
                Values = new ChartValues<double> { 2 },
                Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6699CC"))
            },
                // Другие серии
            };
        }
    }
}
