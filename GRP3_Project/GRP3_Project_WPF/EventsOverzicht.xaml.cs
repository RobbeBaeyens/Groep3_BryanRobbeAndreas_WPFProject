/*
 * Robbe Baeyens
 */


using GRP3_Project_MODEL;
using GRP3_Project_DAL;
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
    /// Interaction logic for EventsOverzicht.xaml
    /// </summary>
    public partial class EventsOverzicht : Window
    {
        //variabele init
        Random random = new Random();
        public int selectedEventId;

        public EventsOverzicht()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //laad alle events wanneer het scherm laadt
            LoadEvents();
        }

        private void LoadEvents()
        {
            List<MoreEventInfo> moreEventInfo = new List<MoreEventInfo>();
            List<Event> eventList = new List<Event>();
            //ophalen events
            eventList = DatabaseOperations.OphalenEvents();

            //vul de gegevens in voor elk event in een aparte klasse MoreEventInfo om betere strings weer te kunnen geven
            foreach (var item in eventList)
            {
                //variabelen gelijkstellen aan het geselecteerde event in de foreach
                var eventId = item.EventID;
                var eventNaam = item.Eventnaam;
                var dag = item.Datum.ToString("dddd");
                var eventTypeNaam = item.Eventtype.Naam;
                var datumTijd = item.Datum.ToString("dd MMM yyyy") + "  " + item.Startuur + " - " + item.Einduur;
                var logo = item.Eventtype.Logo;
                var color = String.Format("#{0:X6}", random.Next(0x1000000));
                //variabelen in klasse MoreEventInfo stoppen
                moreEventInfo.Add(new MoreEventInfo() { EventId = eventId, Eventnaam = eventNaam, Dag = dag, EventtypeNaam = eventTypeNaam, DatumTijd = datumTijd, Logo = logo, Color = color });
            }

            //zet alle events via aparte klasse MoreEventInfo in listbox
            ListboxEvents.ItemsSource = moreEventInfo;
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            //ga naar EventBewerken window en sluit deze window
            Window eventBewerken = new EventBewerken();
            eventBewerken.Show();
            Close();
        }

        private void btnEventBewerken_Click(object sender, RoutedEventArgs e)
        {
            //defineer b als de geklikte knop
            Button b = sender as Button;

            //haal de eventID op via een tag in de knop in XAML
            //ga naar EventBewerken window met doorgestuurde variabele eventID en sluit deze window
            Window eventBewerken = new EventBewerken(int.Parse(b.Tag.ToString()));
            Close();
            eventBewerken.Show();
        }

        private void btnEventVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            //defineer b als de geklikte knop
            Button b = sender as Button;

            //haal de event op via een tag in de knop in XAML met variabele eventID
            Event eventitem = new Event();
            eventitem = DatabaseOperations.OphalenEvent(int.Parse(b.Tag.ToString()));

            //kijk of event nog een locatie bevat, anders vragen om de locatie te verwijderen
            if (eventitem.LocatieID == null)
            {
                //kijk of event nog todo's bevat, anders vragen om de todo's te verwijderen
                if (eventitem.ToDo.Count == 0)
                {
                    //bevestiging vragen om het event permanent te verwijderen
                    MessageBoxResult result = MessageBox.Show("Wil je dit Event permanent verwijderen?", eventitem.Eventnaam, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        //event verwijderen
                        DatabaseOperations.DeleteEvent(eventitem);
                        //eventlijst updaten
                        LoadEvents();
                    }
                }
                else
                {
                    //vragen om de locatie te verwijderen
                    MessageBox.Show("Er zijn nog todo's gekoppeld met dit event." + Environment.NewLine +
                        "Verwijder alle todo's om het event te kunnen verwijderen");
                }
            }
            else
            {
                //vragen om de todo's te verwijderen
                MessageBox.Show("Er is nog een locatie gekoppeld met dit event." + Environment.NewLine +
                    "Verwijder de locatie om het event te kunnen verwijderen");
            }
        }

        private void btnEventSelect_Click(object sender, RoutedEventArgs e)
        {
            //defineer b als de geklikte knop
            Button b = sender as Button;

            //haal de eventID op via een tag in de knop in XAML
            //ga naar EventDetail window met doorgestuurde variabele eventID en sluit deze window
            Window eventDetail = new EventDetail(int.Parse(b.Tag.ToString()));
            Close();
            eventDetail.Show();
        }
    }
}
