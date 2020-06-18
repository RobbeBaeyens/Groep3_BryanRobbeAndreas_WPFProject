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
    /// <summary>
    /// Interaction logic for ToDoOverzicht.xaml
    /// </summary>
    public partial class ToDoOverzicht : Window
    {
        Event eventitem = new Event();
        ToDo todo = new ToDo();
        int eventID;
        public ToDoOverzicht(int eventID)
        {
            InitializeComponent();
            this.eventID = eventID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            eventitem = DatabaseOperations.OphalenEvent(eventID);
            txtHeaderSubText.Text = eventitem.Eventnaam;
            txtEventNaam.Text = eventitem.Eventnaam;
            updateList();
        }

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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            Window toDoBewerken = new ToDoBewerken(int.Parse(b.Tag.ToString()), eventID);
            Close();
            toDoBewerken.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            todo = DatabaseOperations.OphalenTodo(int.Parse(b.Tag.ToString()));

            MessageBoxResult result = MessageBox.Show("Wil je deze ToDO permanent verwijderen?", "#" + todo.Volgnr + " " + todo.Titel, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if(result == MessageBoxResult.Yes)
            {
                DatabaseOperations.DeleteToDo(todo);
                updateList();
            }
        }

        private void btnAddToDo_Click(object sender, RoutedEventArgs e)
        {
            Window todoBewerken = new ToDoBewerken(eventID);
            Close();
            todoBewerken.Show();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Window eventDetail = new EventDetail(eventID);
            Close();
            eventDetail.Show();
        }
    }
}
