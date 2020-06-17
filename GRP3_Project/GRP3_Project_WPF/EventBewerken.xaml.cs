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
        int eventId = -1;
        Event eventitem = new Event();
        List<Eventtype> eventTypes = new List<Eventtype>();
        string eventName = "";
        string date = DateTime.Today.Date.ToString("d MMM yyyy");
        string startTime = "00u00";
        string endTime = "00u00";

        public EventBewerken()
        {
            InitializeComponent();
        }
        public EventBewerken(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
            eventitem = DatabaseOperations.OphalenEvent(eventId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eventTypes = DatabaseOperations.OphalenEventTypes();
            cmbxTypes.DisplayMemberPath = "Naam";
            cmbxTypes.ItemsSource = eventTypes;

            if(eventId != -1)
            {
                cmbxTypes.SelectedIndex = eventitem.EventtypeID - 1;
                eventName = eventitem.Eventnaam;
                date = eventitem.Datum.ToString("d MMM yyyy");
                startTime = eventitem.Startuur;
                endTime = eventitem.Einduur;
            }
            txtEventName.Text = eventName;
            //txtDate.Text = date;
            dpDate.SelectedDate = DateTime.Parse(date);
            dpDate.DisplayDateStart = DateTime.Today;
            txtStartTime.Text = startTime;
            txtEndTime.Text = endTime;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventsOverzicht = new EventsOverzicht();
            Close();
            eventsOverzicht.Show();
        }

        private void btnSaveEvent_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("Eventtype");
            if(string.IsNullOrWhiteSpace(foutmeldingen))
            {
                eventitem.Eventnaam = txtEventName.Text;
                Eventtype selectedEventtype = (Eventtype)cmbxTypes.SelectedItem;
                eventitem.EventtypeID = selectedEventtype.EventtypeID;
                eventitem.Datum = (DateTime)dpDate.SelectedDate;
                eventitem.Startuur = txtStartTime.Text;
                eventitem.Einduur = txtEndTime.Text;

                if (eventitem.IsGeldig())
                {
                    if (eventId != -1)
                    {
                        eventitem.EventID = eventId;
                        DatabaseOperations.UpdateEvent(eventitem);
                    }
                    else
                    {
                        DatabaseOperations.AddEvent(eventitem);
                    }
                }
                else
                {
                    MessageBox.Show(eventitem.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        private string Valideer(string tekstvakNaam)
        {
            if (tekstvakNaam == "Eventtype" && cmbxTypes.SelectedItem == null)
            {
                return "Selecteer een event type!" + Environment.NewLine;
            }
            return "";
        }
    }
}
