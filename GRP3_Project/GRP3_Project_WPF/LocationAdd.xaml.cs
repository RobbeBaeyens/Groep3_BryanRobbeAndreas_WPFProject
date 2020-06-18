using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for LocationAdd.xaml
    /// </summary>
    public partial class LocationAdd : Window
    {
        //Declaratie van eventId en eventItem
        int eventId;
        Event eventItem = new Event();

        public LocationAdd(int eventId)
        {
            InitializeComponent();

            //eventId koppelen aan DataBase ==> eventId
            this.eventId = eventId;
            eventItem = DatabaseOperations.OphalenEvent(eventId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Inladen van juiste tekst uit database in zwarte balk bovenaan
            txtHeaderSubtekst.Text = eventItem.Eventnaam;
            txtEventName.Text = eventItem.Eventnaam;
        }

        //Terug naar LocatieSelectie-window
        private void btnBackToLocationSelector_Click(object sender, RoutedEventArgs e)
        {
            //Window selecter adhv eventId
            Window backToLocSel = new EventDetail(eventId);
            backToLocSel.Show();
            Close();
        }

        //Knop voor opslaan (initialiseren)
        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            //Validatie foutmeldingen (leeg + geen int)
            string foutmeldingen = Valideer("Huisnr");
            foutmeldingen += Valideer("Postcode");

            //Locatie variabele aanmaken
            Locatie locatie = new Locatie();

            //Check of velden leeg zijn (foutmeldingen)
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //Velden declareren naar locatie attributen
                locatie.Naam = txtName.Text;
                locatie.Straat = txtStreet.Text;
                locatie.Gemeente = txtCity.Text;
                locatie.Manager = txtManager.Text;
                locatie.Email = txtEmail.Text;
                locatie.Telefoon = txtPhone.Text;

                //Integer velden declareren naar locatie attributen
                if (int.TryParse(txtNumber.Text, out int huisnr))
                {
                    locatie.Huisnr = huisnr;
                }
                if (int.TryParse(txtPostal.Text, out int postcode))
                {
                    locatie.Postcode = postcode;
                }

                //Indien geldig wordt de locatie aan database toegevoegd
                if (locatie.IsGeldig())
                {
                    DatabaseOperations.ToevoegenLocatie(locatie);

                    btnBackToLocationSelector_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(locatie.Error);
                }
            }
            else
            {
                MessageBox.Show($"{foutmeldingen} {locatie.Error}");
            }
        }

        //Valideren van lege velden voor integer velden
        private string Valideer(string input)
        {
            if (input == "Huisnr" && (!int.TryParse(txtNumber.Text, out int huisnr) || string.IsNullOrWhiteSpace(txtNumber.Text)))
            {
                return $"Vul een correct huisnummer in! {Environment.NewLine}";
            }
            if (input == "Postcode" && (!int.TryParse(txtPostal.Text, out int postcode) || string.IsNullOrWhiteSpace(txtPostal.Text)))
            {
                return $"Vul een correcte postcode in! {Environment.NewLine}";
            }
            return "";
        }
    }
}
