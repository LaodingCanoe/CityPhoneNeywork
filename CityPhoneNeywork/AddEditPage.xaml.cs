using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CityPhoneNeywork
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </sumpublic 
    public partial class AddEditPage : Page
    {
        private ATS _currentATS;
        public AddEditPage(ATS selected)
        {
            InitializeComponent();

            // Initialize the current ATS record (for new or edit)
            _currentATS = selected ?? new ATS();
            DataContext = _currentATS;

            var context = SharpovEntities.GetContext();

            // Populate the ComboBoxes
            SityCB.ItemsSource = context.City.ToList();
            SityCB.DisplayMemberPath = "Название_города";
            SityCB.SelectedValuePath = "Код_города";

            TypeCB.ItemsSource = context.ATS
                .Select(a => a.Вид)
                .Distinct()
                .ToList();

            // Set selected values if editing an existing ATS
            if (selected != null)
            {
                SityCB.SelectedValue = _currentATS.Город;
                TypeCB.SelectedItem = _currentATS.Вид;
            }
        }


        private void PhotoBT_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Update the image path in the ATS entity
                _currentATS.Картинка = openFileDialog.FileName;

                // Display the new image
                LogoImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        private void SaveBT_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentATS.Название))
                errors.AppendLine("Укажите наименование АТС");
            if (string.IsNullOrWhiteSpace(_currentATS.Адрес))
                errors.AppendLine("Укажите адрес АТС");
            if (TypeCB.SelectedItem == null)
                errors.AppendLine("Укажите вид АТС");
            if (SityCB.SelectedItem == null)
                errors.AppendLine("Укажите город");
            if (!decimal.TryParse(CityPriceTextBox.Text.Replace('.',','), out decimal cityPrice) || cityPrice <= 0)
            {
                errors.AppendLine("Укажите корректную цену на городские звонки (число больше нуля)");
            }
            if (!decimal.TryParse(MezhPriceTextBox.Text.Replace('.', ','), out decimal mezhPrice) || mezhPrice <= 0)
            {
                errors.AppendLine("Укажите корректную цену на межгородские звонки (число больше нуля)");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentATS.Вид = TypeCB.SelectedItem.ToString();
            _currentATS.Город = SityCB.SelectedIndex + 1;

            if (_currentATS.Код_АТС == 0)
            {
                SharpovEntities.GetContext().ATS.Add(_currentATS);
            }

            // Update or add to Price_List
            var priceList = _currentATS.Price_List.FirstOrDefault() ?? new Price_List { Код_АТС = _currentATS.Код_АТС };
            priceList.Цена_на_городские_звонки = cityPrice;
            priceList.Цена_на_межгородные_звонки = mezhPrice;

            if (_currentATS.Price_List.FirstOrDefault() == null)
            {
                _currentATS.Price_List.Add(priceList);
            }
            try
            {
                SharpovEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены успешно!");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
            Manager.MainFrame.GoBack();
        }



        
    }

}

