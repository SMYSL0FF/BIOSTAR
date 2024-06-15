﻿using BIOSTAR.Core;
using BIOSTAR.Model;
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
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        public Client()
        {
            InitializeComponent();
            ClientInf.ItemsSource = FrameNavigate.DB.Clients.ToList();
        }
        private async void Save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await FrameNavigate.DB.SaveChangesAsync();
            MessageBox.Show("Изменения сохранены!",
                    "Системное уведомление",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }

    }
}
    

