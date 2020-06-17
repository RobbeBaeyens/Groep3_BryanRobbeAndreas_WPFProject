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
using GRP3_Project_DAL;

//Bryan Antonis

namespace GRP3_Project_WPF
{
    /// <summary>
    /// Interaction logic for LocationSelector.xaml
    /// </summary>
    public partial class LocationSelector : Window
    {
        int eventId;
        List<Locatie> locaties = new List<Locatie>();


        public LocationSelector(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cmbSelectLocatie.DisplayMemberPath = "Naam";

            
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationAdd = new LocationAdd();
            locationAdd.Show();
            Close();
        }
    }
}
