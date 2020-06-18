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
        //variabele init
        int eventId;
        Event eventitem = new Event();
        bool hasLocation = false;

        public EventDetail(int eventId)
        {
            InitializeComponent();
            //krijg he eventId mee van de vorige pagina
            this.eventId = eventId;
            //haal het event op volgens eventId
            eventitem = DatabaseOperations.OphalenEvent(eventId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //zet de hoofding van de pagina
            txtheadertext.Text = eventitem.Eventnaam;
            txtheadersubtext.Text = eventitem.Eventtype.Naam;

            //test of alle todo's afgewerkt zijn
            //test of er een locatie toegevoegd is
            testIfComplete();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //ga terug naar EventOverzicht en sluit deze window
            Window eventsOverzicht = new EventsOverzicht();
            Close();
            eventsOverzicht.Show();
        }

        private void btnToDO_Click(object sender, RoutedEventArgs e)
        {
            //ga naar ToDoOverzicht window met eventId en sluit deze window
            Window toDoOverzicht = new ToDoOverzicht(eventId);
            Close();
            toDoOverzicht.Show();
        }

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {
            //test of het event een locatie bevat
            if (hasLocation)
            {
                //zo ja, ga naar LocatieSelector window met eventId en geef het geselecteerde locatieId mee
                Window locationSelector = new LocationSelector(eventId, (int)eventitem.LocatieID);
                Close();
                locationSelector.Show();
            }
            else
            {
                //zo ja, ga naar LocatieSelector window met eventId
                Window locationSelector = new LocationSelector(eventId);
                Close();
                locationSelector.Show();
            }
        }

        private void testIfComplete()
        {
            //zet afbeeldingfolder pad voor check.png en nocheck.png
            string startImagePath = "pack://application:,,,/pictogrammen/";
            /*===ToDo===
             * Haal alle todo's en afgewerkte todo's op volgens het geselecteerde event
             * Zet het aantal afgewerkte todo's zichtbaar op de knop
             * Toon een vinkje als alle todo's afgewerkt zijn, anders een kruisje
             */
            List<ToDo> toDos = DatabaseOperations.OphalenToDos(eventId);
            List<ToDo> toDosComplete = DatabaseOperations.OphalenToDosComplete(eventId);
            txtToDoCompleted.Content = $"{toDosComplete.Count}/{toDos.Count} Complete";
            imgToDoCheck.Source = (toDos.Count == toDosComplete.Count) ? new BitmapImage(new Uri(startImagePath + "check.png")) : new BitmapImage(new Uri(startImagePath + "nocheck.png"));

            /*===Locatie===
             * Toon een vinkje als er een locatie toegevoegd is, anders een kruisje
             * Als er een locatie is toegevoegd toon de naam en gemeente
             */
            imgLocationCheck.Source = (eventitem.LocatieID == null) ? new BitmapImage(new Uri(startImagePath + "nocheck.png")) : new BitmapImage(new Uri(startImagePath + "check.png"));
            if(eventitem.LocatieID != null)
            {
                eventitem.Locatie = DatabaseOperations.OphalenLocatie((int)eventitem.LocatieID);
                txtLocatieNaam.Content = eventitem.Locatie.Naam;
                txtGemeente.Content = eventitem.Locatie.Gemeente;
                //zet bevatlocatie op waar
                hasLocation = true;
            }
        }
    }
}
