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
        Event eventItem = new Event();


        public LocationSelector(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;

            eventItem = DatabaseOperations.OphalenEvent(eventId);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cmbSelectLocatie.DisplayMemberPath = "Naam";

            txtHeaderSubtekst.Text = eventItem.Eventnaam;
            txtEventnaam.Text = eventItem.Eventnaam;

            cmbSelectLocatie.DisplayMemberPath = "Naam";
            cmbSelectLocatie.ItemsSource = DatabaseOperations.OphalenLocaties();
            
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationAdd = new LocationAdd();
            locationAdd.Show();
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventDetail = new EventDetail(eventId);

            eventDetail.Show();

            Close();

        }

        private void cmbSelectLocatie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Locatie locatie = cmbSelectLocatie.SelectedItem as Locatie;

            txtName.Text = locatie.Naam;
            txtStreet.Text = locatie.Straat + " " + locatie.Huisnr.ToString();
            txtPostCode.Text = locatie.Postcode.ToString() + ", " + locatie.Gemeente;
            txtManager.Text = locatie.Manager;
            txtEmail.Text = locatie.Email;
            txtPhone.Text = locatie.Telefoon;
        }
    }
}
