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

        Locatie locatie = new Locatie();

        Event eventItem = new Event();


        public LocationSelector(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;

            eventItem = DatabaseOperations.OphalenEvent(eventId);
        }


        public LocationSelector(int eventId, int locatieId)
        {
            InitializeComponent();
            this.eventId = eventId;

            eventItem = DatabaseOperations.OphalenEvent(eventId);

            locatie = DatabaseOperations.OphalenLocatie(locatieId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txtName.Text = locatie.Naam;
            txtStreet.Text = $"{locatie.Straat} {locatie.Huisnr}";
            txtPostCode.Text = $"{locatie.Postcode} {locatie.Gemeente}";
            txtManager.Text = locatie.Manager;
            txtEmail.Text = locatie.Email;
            txtPhone.Text = locatie.Telefoon;



            txtHeaderSubtekst.Text = eventItem.Eventnaam;
            txtEventnaam.Text = eventItem.Eventnaam;

            cmbSelectLocatie.DisplayMemberPath = "Naam";
            cmbSelectLocatie.ItemsSource = DatabaseOperations.OphalenLocaties();

            
        }

        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationAdd = new LocationAdd(eventId);
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
            locatie = cmbSelectLocatie.SelectedItem as Locatie;

            txtName.Text = locatie.Naam;
            txtStreet.Text = $"{locatie.Straat} {locatie.Huisnr}";
            txtPostCode.Text = $"{locatie.Postcode} {locatie.Gemeente}";
            txtManager.Text = locatie.Manager;
            txtEmail.Text = locatie.Email;
            txtPhone.Text = locatie.Telefoon;
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            eventItem.LocatieID = null;

            if (eventItem.IsGeldig())
            {


                MessageBoxResult result = MessageBox.Show("Wil je deze Locatie uit het event halen?", "Locatie verwijderen", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseOperations.UpdateEvent(eventItem);
                }
            }
            else
            {
                MessageBox.Show("Event is niet geldig!");
            }


        }

        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSelectLocatie.SelectedItem != null)
            {
                eventItem.LocatieID = locatie.LocatieID;

                DatabaseOperations.UpdateEvent(eventItem);

                MessageBox.Show("Locatie aan event toegevoegd!");
            }
            else
            {
                MessageBox.Show("Selecteer of voeg een locatie toe!");
            }
        }
    }
}
