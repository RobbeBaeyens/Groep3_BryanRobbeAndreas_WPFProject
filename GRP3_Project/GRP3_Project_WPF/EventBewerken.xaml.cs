/*
 * Robbe Baeyens
 */


using GRP3_Project_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EventBewerken.xaml
    /// </summary>
    public partial class EventBewerken : Window
    {
        //variabele init
        int eventId = -1;
        Event eventitem = new Event();
        List<Eventtype> eventTypes = new List<Eventtype>();
        string eventName = "";
        string date = DateTime.Today.Date.ToString("d MMM yyyy");
        string startTime = "00u00";
        string endTime = "00u00";

        public EventBewerken()
        {
            //nieuw event aanmaken
            InitializeComponent();
        }
        public EventBewerken(int eventId)
        {
            //bestaand event bewerken (eventId)
            InitializeComponent();
            //krijg he eventId mee van de vorige pagina
            this.eventId = eventId;
            //haal het event op volgens eventId
            eventitem = DatabaseOperations.OphalenEvent(eventId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //haal de eventtypes op en steek ze in de combobox
            eventTypes = DatabaseOperations.OphalenEventTypes();
            cmbxTypes.DisplayMemberPath = "Naam";
            cmbxTypes.ItemsSource = eventTypes;

            //als een event bewerkt wordt vullen we de gegevens in
            if(eventId != -1)
            {
                for (int i = 0; i < eventTypes.Count; i++)
                {
                    //zet juiste eventtype in de comboboxselectie
                    if (eventitem.Eventtype.Naam == eventTypes[i].Naam)
                    {
                        cmbxTypes.SelectedIndex = i;
                    }
                }
                eventName = eventitem.Eventnaam;
                date = eventitem.Datum.ToString("d MMM yyyy");
                startTime = eventitem.Startuur;
                endTime = eventitem.Einduur;
            }
            txtEventName.Text = eventName;
            dpDate.SelectedDate = DateTime.Parse(date);
            //zet de minimumdatum vandaag, je kan geen events inplannen voor gisteren ;P
            dpDate.DisplayDateStart = DateTime.Today;
            txtStartTime.Text = startTime;
            txtEndTime.Text = endTime;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //ga terug naar EventOverzicht en sluit deze window
            Window eventsOverzicht = new EventsOverzicht();
            Close();
            eventsOverzicht.Show();
        }

        private void btnSaveEvent_Click(object sender, RoutedEventArgs e)
        {
            //zet data voor event
            eventitem.Eventnaam = txtEventName.Text;
            eventitem.Datum = (DateTime)dpDate.SelectedDate;
            eventitem.Startuur = txtStartTime.Text;
            eventitem.Einduur = txtEndTime.Text;

            //kijk of de combobox is ingevuld
            string foutmeldingen = Valideer("Eventtype");

            //test of de combobox is ingevuld
            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                //zet het geselecteerde eventtype voor event
                Eventtype selectedEventtype = (Eventtype)cmbxTypes.SelectedItem;
                eventitem.EventtypeID = selectedEventtype.EventtypeID;

                //kijk of alle data geldig is
                if (eventitem.IsGeldig())
                {
                    //test of het een nieuw event is of een event dat je bewerkt
                    if (eventId != -1)
                    {
                        eventitem.EventID = eventId;
                        eventitem.Eventtype = selectedEventtype;

                        //kijk of de update kan uitgevoerd worden
                        int ok = DatabaseOperations.UpdateEvent(eventitem);
                        if (ok > 0)
                        {
                            //update event en ga terug naar vorige pagina
                            DatabaseOperations.UpdateEvent(eventitem);
                            btnBack_Click(sender, e);
                        }
                        else
                        {
                            //bericht als event niet is aangepast => komt normaal gezien niet voor
                            MessageBox.Show("Event is niet aangepast!");
                        }
                    }
                    else
                    {
                        //voeg nieuw event toe en ga terug naar vorige pagina
                        DatabaseOperations.AddEvent(eventitem);
                        btnBack_Click(sender, e);
                    }
                }
                else
                {
                    //toon welke vakken niet correct ingevuld zijn
                    MessageBox.Show(eventitem.Error);
                }
            }
            else
            {
                //toon welke vakken niet correct ingevuld zijn
                MessageBox.Show($"{foutmeldingen} {eventitem.Error}");
            }
        }

        private string Valideer(string tekstvakNaam)
        {
            //valideer of combobox een geselecteerd item heeft, zoniet return bericht
            if (tekstvakNaam == "Eventtype" && cmbxTypes.SelectedItem == null)
            {
                return "Selecteer een event type!" + Environment.NewLine;
            }
            return "";
        }
    }
}
