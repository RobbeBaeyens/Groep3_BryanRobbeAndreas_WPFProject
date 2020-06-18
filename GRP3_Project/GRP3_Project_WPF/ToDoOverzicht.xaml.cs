using GRP3_Project_DAL;
using GRP3_Project_MODEL;
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
    // Andreas Steenackers
    /// <summary>
    /// Interaction logic for ToDoOverzicht.xaml
    /// </summary>
    public partial class ToDoOverzicht : Window
    {
        //hier wordt er een nieuwe eventitem gemaakt en ToDo
        Event eventitem = new Event();
        ToDo todo = new ToDo();
        int eventID;

        //Hier wordt de eventId opgevraagd zodat er niet dezelfde ToDo komt te staan bij een andere event
        public ToDoOverzicht(int eventID)
        {
            InitializeComponent();
            this.eventID = eventID;
        }

        //laad alles op het wpf scherm
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eventitem = DatabaseOperations.OphalenEvent(eventID);
            txtHeaderSubText.Text = eventitem.Eventnaam;
            txtEventNaam.Text = eventitem.Eventnaam;
            updateList();
        }

        // listbox updaten zodat er meerdere To Do's kunnen staan
        private void updateList()
        {
            List<ToDo> todoList = new List<ToDo>();
            List<MoreToDoInfo> moreToDoInfo = new List<MoreToDoInfo>();
            todoList = DatabaseOperations.OphalenToDos(eventID);

            foreach (var item in todoList)
            {
                var toDoId = item.ToDoID;
                var titel = item.Titel;
                var omschrijving = item.Omschrijving;
                var volgnr = "#" + item.Volgnr;
                var imgChecked = "";
                switch (item.Afgewerkt)
                {
                    case true:
                        imgChecked = "/pictogrammen/check.png";
                        break;
                    case false:
                        imgChecked = "/pictogrammen/nocheck.png";
                        break;
                }
                moreToDoInfo.Add(new MoreToDoInfo() { ToDoId = toDoId, Titel = titel, Omschrijving = omschrijving, Volgnr = volgnr, ImgChecked = imgChecked });
            }

            ListboxToDo.ItemsSource = moreToDoInfo;
        }

        //Zorgt voor navigatie naar ToDoBewerken met de gegevens die er al stonden zodat je deze kan aanpassen
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            Window toDoBewerken = new ToDoBewerken(int.Parse(b.Tag.ToString()), eventID);
            Close();
            toDoBewerken.Show();
        }

        //Deze knop verwijderd een ToDo
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            todo = DatabaseOperations.OphalenTodo(int.Parse(b.Tag.ToString()));

            MessageBoxResult result = MessageBox.Show("Wil je deze ToDo permanent verwijderen?", "#" + todo.Volgnr + " " + todo.Titel, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if(result == MessageBoxResult.Yes)
            {
                DatabaseOperations.DeleteToDo(todo);
                updateList();
            }
        }

        //Met deze knop kan men een nieuwe ToDo toevoegen
        private void btnAddToDo_Click(object sender, RoutedEventArgs e)
        {
            Window todoBewerken = new ToDoBewerken(eventID);
            Close();
            todoBewerken.Show();
        }

        //Dit is een navigatie knop naar de vorige pagina
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventDetail = new EventDetail(eventID);
            Close();
            eventDetail.Show();
        }
    }
}
