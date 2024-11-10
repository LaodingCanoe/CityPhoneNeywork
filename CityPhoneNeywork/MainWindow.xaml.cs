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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ATSPage());
            Manager.MainFrame = MainFrame;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BackBtn.Visibility = Visibility.Visible;
            }
            else
            {
                BackBtn.Visibility = Visibility.Hidden;
            }
        }
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Check if the current page is of type ATSPage
            if (e.Content is ATSPage atsPage)
            {
                atsPage.Update(); // Call Update() whenever navigating to ATSPage
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.GoBack();
        }
    }
}
