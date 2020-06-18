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

//BRYAN ANTONIS

namespace GRP3_Project_WPF
{
    /// <summary>
    /// Interaction logic for LocationSelector.xaml
    /// </summary>
    public partial class LocationSelector : Window
    {
        //Declaratie eventId
        int eventId;

        //Declaratie locatie en eventItem objecten
        Locatie locatie = new Locatie();
        Event eventItem = new Event();

        //Gebruik bij GEEN locatie geselecteerd
        public LocationSelector(int eventId)
        {
            InitializeComponent();

            //eventId koppelen aan DataBase ==> eventId
            this.eventId = eventId;
            eventItem = DatabaseOperations.OphalenEvent(eventId);
        }

        //Gebruik bij EEN locatie geselecteerd
        public LocationSelector(int eventId, int locatieId)
        {
            InitializeComponent();

            //eventId koppelen aan DataBase ==> eventId
            this.eventId = eventId;
            eventItem = DatabaseOperations.OphalenEvent(eventId);
            locatie = DatabaseOperations.OphalenLocatie(locatieId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Lijst aanmaken met locaties uit DataBase
            List<Locatie> locaties = new List<Locatie>();
            locaties = DatabaseOperations.OphalenLocaties();

            //Header tekst inladen
            txtHeaderSubtekst.Text = eventItem.Eventnaam;
            txtEventnaam.Text = eventItem.Eventnaam;

            //Invoegen van Locatie attributen in textBoxen (bij laden van pagina)
            txtName.Text = locatie.Naam;
            if (eventItem.LocatieID != null) //===> deze "if" statement dient om een nul (0) / (NULL) uit te sluiten bij het laden van de integer TextBoxen (indien locatie niet geselecteerd is word er niets ingeladen!)
            {
                txtStreet.Text = $"{locatie.Straat} {locatie.Huisnr}";
                txtPostCode.Text = $"{locatie.Postcode} {locatie.Gemeente}";
            }
            txtManager.Text = locatie.Manager;
            txtEmail.Text = locatie.Email;
            txtPhone.Text = locatie.Telefoon;

            //ComboBox inhoud laden uit database Locatie en zoeken op Naam
            cmbSelectLocatie.DisplayMemberPath = "Naam";
            cmbSelectLocatie.ItemsSource = locaties;

            //Locaties uit DataBase in ComboBox zetten
            for (int i = 0; i < locaties.Count; i++)
            {
                if (locatie.Naam == locaties[i].Naam)
                {
                    cmbSelectLocatie.SelectedIndex = i;
                }
            }
        }

        //Knop voor naar Locatie toevoegen-window te gaan
        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationAdd = new LocationAdd(eventId);
            locationAdd.Show();
            Close();
        }

        //Knop voor terug naar Evenement-window te gaan
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventDetail = new EventDetail(eventId);
            eventDetail.Show();
            Close();
        }


        private void cmbSelectLocatie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //locatie wordt ingesteld in de geselecteerd ComboBox-Locatie 
            locatie = cmbSelectLocatie.SelectedItem as Locatie;

            //Invoegen van Locatie attributen in textBoxen (adhv geselecteerde ComboBox-Locatie)
            txtName.Text = locatie.Naam;
            txtStreet.Text = $"{locatie.Straat} {locatie.Huisnr}";
            txtPostCode.Text = $"{locatie.Postcode} {locatie.Gemeente}";
            txtManager.Text = locatie.Manager;
            txtEmail.Text = locatie.Email;
            txtPhone.Text = locatie.Telefoon;
        }

        private void btnDeleteLocation_Click(object sender, RoutedEventArgs e)
        {
            //De locatie wordt in de Database bij EVENT op null gezet
            eventItem.LocatieID = null;

            //Controleren of eventItem geldig is
            if (eventItem.IsGeldig())
            {
                //MessageBox met mogelijkheid om een keuze (Ja of Nee) te kiezen
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
            //Indien ComboBox NIET leeg (NULL) is zal de Locatie aan het Evenement worden toegevoegd
            if (cmbSelectLocatie.SelectedItem != null)
            {
                //Locatie krijgt op Evenement en Locatie dezelfde ID
                eventItem.LocatieID = locatie.LocatieID;

                //Updaten in DataBase en melden dat locatie is toegevoegd
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
