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
using System.Windows.Shapes;

namespace CityPhoneNeywork
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow(ATS selected)
        {
            InitializeComponent();
            if (selected != null)
            {
                DataContext = selected;
            }
        }

        private void PhotoBT_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
