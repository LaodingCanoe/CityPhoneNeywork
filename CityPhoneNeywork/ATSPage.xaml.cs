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

namespace CityPhoneNeywork
{
    /// <summary>
    /// Логика взаимодействия для ATSPage.xaml
    /// </summary>
    public partial class ATSPage : Page
    {
        int AllCount = 0;
        public ATSPage()
        {
            InitializeComponent();
            Update();
            ComboFilter.SelectedIndex = 0;
            ComboType.SelectedIndex = 0;
        }

        public void Update()
        {
            var currentProduct = SharpovEntities.GetContext().ATS.ToList();
            AllCount = currentProduct.Count;

            if (ComboType.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => p.Вид == "Городские").ToList();
            }

            if (ComboType.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => p.Вид == "Ведомственные").ToList();
            }

            if (ComboFilter.SelectedIndex == 1)
            {
                currentProduct = currentProduct.OrderBy(p => p.Кол_во_абонентов).ToList();
            }
            if (ComboFilter.SelectedIndex == 2)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.Кол_во_абонентов).ToList();
            }
            if (ComboFilter.SelectedIndex == 3)
            {
                currentProduct = currentProduct.OrderBy(p => p.CityPrice).ToList();
            }
            if (ComboFilter.SelectedIndex == 4)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.CityPrice).ToList();
            }
            if (ComboFilter.SelectedIndex == 5)
            {
                currentProduct = currentProduct.OrderBy(p => p.MezhPrice).ToList();
            }
            if (ComboFilter.SelectedIndex == 6)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.MezhPrice).ToList();
            }
            currentProduct = currentProduct.Where(p => p.Название.ToLower().Contains(Search.Text.ToLower()) ||
            p.CityToString.ToLower().Contains(Search.Text.ToLower())).ToList();
            CountTB.Text = currentProduct.Count.ToString() + " из " + AllCount.ToString();
            ATSList.ItemsSource = currentProduct;
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void EditBT_Click(object sender, RoutedEventArgs e)
        {
            var selected = ATSList.SelectedItem as ATS;
            if(selected != null)
            {
                Manager.MainFrame.Navigate(new AddEditPage(selected));
            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            var selected = ATSList.SelectedItem as ATS;
            if (selected != null)
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete related records in Price_List
                        var relatedPriceList = SharpovEntities.GetContext().Price_List
                            .Where(p => p.Код_АТС == selected.Код_АТС)
                            .ToList();

                        foreach (var price in relatedPriceList)
                        {
                            SharpovEntities.GetContext().Price_List.Remove(price);
                        }

                        // Delete the main ATS record
                        SharpovEntities.GetContext().ATS.Remove(selected);

                        // Save changes
                        SharpovEntities.GetContext().SaveChanges();

                        MessageBox.Show("Запись и связанные данные успешно удалены.");

                        // Refresh the list after deletion
                        Update();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении данных: {ex.Message}");
                    }
                }
            }
            Update();
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

    }
}
