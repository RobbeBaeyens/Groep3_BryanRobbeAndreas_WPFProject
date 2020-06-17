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
        List<Event> eventList = new List<Event>();
        List<MoreEventInfo> moreEventInfo = new List<MoreEventInfo>();
        Random random = new Random();
        public int selectedEventId;

        public EventsOverzicht()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eventList = DatabaseOperations.OphalenEvents();

            foreach (var item in eventList)
            {
                var eventId = item.EventID;
                var eventNaam = item.Eventnaam;
                var dag = item.Datum.ToString("dddd");
                var eventTypeNaam = item.Eventtype.Naam;
                var datumTijd = item.Datum.ToString("dd MMM yyyy") + "  " + item.Startuur + " - " + item.Einduur;
                var logo = item.Eventtype.Logo;
                var color = String.Format("#{0:X6}", random.Next(0x1000000));
                moreEventInfo.Add(new MoreEventInfo() { EventId = eventId, Eventnaam = eventNaam, Dag = dag, EventtypeNaam = eventTypeNaam, DatumTijd = datumTijd, Logo = logo, Color = color });
            }

            ListboxEvents.ItemsSource = moreEventInfo;
        }

        private void btnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Window eventBewerken = new EventBewerken();
            eventBewerken.Show();
            Close();
        }

        private void btnEventBewerken_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            Window eventBewerken = new EventBewerken(int.Parse(b.Tag.ToString()));
            Close();
            eventBewerken.Show();
        }

        private void btnEventVerwijderen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
