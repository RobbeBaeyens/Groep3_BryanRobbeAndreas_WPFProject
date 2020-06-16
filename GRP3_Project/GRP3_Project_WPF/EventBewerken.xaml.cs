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
    /// Interaction logic for EventBewerken.xaml
    /// </summary>
    public partial class EventBewerken : Window
    {
        int eventId;

        public EventBewerken()
        {
            InitializeComponent();
        }
        public EventBewerken(int eventId)
        {
            InitializeComponent();
            this.eventId = eventId;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventsOverzicht = new EventsOverzicht();
            eventsOverzicht.Show();
            Close();
        }
    }
}
