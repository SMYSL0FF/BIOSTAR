using LiveCharts.Wpf;
using LiveCharts;
using BIOSTAR.View.Pages;
using System.Windows.Media;

namespace BIOSTAR.ViewModels
{
    internal class ChartViewModel
    {
        public SeriesCollection PieSeriesCollection { get; set; }

        public ChartViewModel()
        {
            // Инициализация PieSeriesCollection
            PieSeriesCollection = new SeriesCollection

            {new PieSeries
                {
                    Title = "На рабочем месте",
                    Values = new ChartValues<double> { 14 },
                    Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#003366"))
                },

            new PieSeries
            {
                Title = "Отсутствуют",
                Values = new ChartValues<double> { 8 },
                Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6699CC"))
            },
            
        };
        }
    }
}
