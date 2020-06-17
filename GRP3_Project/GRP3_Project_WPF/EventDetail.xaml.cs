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
    public partial class EventDetail : Window
    {
        int eventId = -1;
        Event eventitem = new Event();

        public EventDetail()
        {
            InitializeComponent();
        }
        public EventDetail(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
            eventitem = DatabaseOperations.OphalenEvent(eventId);

            txtheadertext.Text = eventitem.Eventnaam;
            txtheadersubtext.Text = eventitem.Eventtype.Naam;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventsOverzicht = new EventsOverzicht();
            Close();
            eventsOverzicht.Show();
        }

        private void btnToDO_Click(object sender, RoutedEventArgs e)
        {
            Window toDoOverzicht = new ToDoOverzicht(eventId);
            Close();
            toDoOverzicht.Show();
        }

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            Window locationSelector = new LocationSelector(eventId);
            Close();
            locationSelector.Show();
        }
    }
}
