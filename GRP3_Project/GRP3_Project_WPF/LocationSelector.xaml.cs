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

namespace GRP3_Project_WPF
{
    /// <summary>
    /// Interaction logic for LocationSelector.xaml
    /// </summary>
    public partial class LocationSelector : Window
    {
        public LocationSelector()
        {
            InitializeComponent();
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationAdd = new LocationAdd();
            locationAdd.Show();
            Close();
        }
    }
}
