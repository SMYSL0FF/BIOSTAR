using BIOSTAR.Core;
using BIOSTAR.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BIOSTAR.View.Pages
{
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем данные из текстовых полей
                string fullName = FIO.Text;
                string address = Address.Text;
                string phoneNumber = PhoneNumber.Text;
                string modelDevice = ModelDevice.Text;
                string malfunction = Malfunction.Text;
                string devise_type = DeviseType.Text;
                string email = Email.Text;
                string comment = Comment.Text;

                // Проверка на пустоту
                if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber) ||
                    string.IsNullOrEmpty(devise_type) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(modelDevice) || string.IsNullOrEmpty(malfunction))
                {
                    MessageBox.Show("Все поля, кроме комментария, обязательны для заполнения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Создание нового клиента
                var client = new Clients
                {
                    full_name = fullName,
                    address = address,
                    email = email,
                    phone_number = phoneNumber,
                    registration_date = DateTime.Now
                };

                // Добавление клиента в базу данных
                FrameNavigate.DB.Clients.Add(client);
                FrameNavigate.DB.SaveChanges();

                // Получение ID добавленного клиента
                int client_id = client.client_id;

                // Создание нового устройства
                var device = new Devices
                {
                    client_id = client_id,
                    model = modelDevice,
                    brand = "не указано",
                    device_type = devise_type 
                };

                // Добавление устройства в базу данных
                FrameNavigate.DB.Devices.Add(device);
                FrameNavigate.DB.SaveChanges();

                // Получение ID добавленного устройства
                int deviceId = device.device_id;

                // Создание новой заявки на ремонт
                var repairOrder = new RepairOrders
                {
                    client_id = client_id,
                    device_id = deviceId,
                    description = malfunction,
                    status = "Pending",
                    repair_notes = comment,
                    appointment_date = DateTime.Now
                };

                // Добавление заявки в базу данных
                FrameNavigate.DB.RepairOrders.Add(repairOrder);
                FrameNavigate.DB.SaveChanges();

                MessageBox.Show("Заявка успешно создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Очистка текстовых полей после успешного добавления
                ClearForm();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Свойство: {validationError.PropertyName} Ошибка: {validationError.ErrorMessage}", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearForm()
        {
            FIO.Clear();
            Address.Clear();
            PhoneNumber.Clear();
            ModelDevice.Clear();
            Malfunction.Clear();
            Comment.Clear();
            Email.Clear();
            DeviseType.Clear();
        }
    }
}
