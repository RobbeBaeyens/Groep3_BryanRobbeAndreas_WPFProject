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
    /// Interaction logic for ToDoBewerken.xaml
    /// </summary>
    public partial class ToDoBewerken : Window
    {
        int toDoID = -1;
        int eventID;
        ToDo todo = new ToDo();
        Event eventItem = new Event();

        public ToDoBewerken(int eventID)
        {
            InitializeComponent();
            init(eventID);
            

        }

        public ToDoBewerken(int toDoId, int eventID)
        {
            InitializeComponent();
            this.toDoID = toDoId;
            todo = DatabaseOperations.OphalenTodo(toDoID);
            init(eventID);
        }


        private void init(int eventID)
        {
            this.eventID = eventID;
            eventItem = DatabaseOperations.OphalenEvent(eventID);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txtHeaderSubText.Text = eventItem.Eventnaam;
            txtEventNaam.Text = eventItem.Eventnaam;

            if(toDoID != -1)
            {
                txtToDoTitel.Text = todo.Titel;
                txtbOmschrijving.Text = todo.Omschrijving;
                tgbCheck.IsChecked = todo.Afgewerkt;
                lblVolgnr.Content = todo.Volgnr;

               
            }



        }



        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Window todoOverzicht = new ToDoOverzicht();
            Close();
            todoOverzicht.Show();
        }

        private void btnSaveToDo_Click(object sender, RoutedEventArgs e)
        {
            todo.Titel = txtToDoTitel.Text;
            todo.Omschrijving = txtbOmschrijving.Text;
            todo.Afgewerkt = (bool) tgbCheck.IsChecked;
            todo.Volgnr = 0;
            todo.EventID = eventItem.EventID;

            if (todo.IsGeldig())
            {
                if (toDoID != -1)
                {
                    todo.ToDoID = toDoID;
                    todo.Event = eventItem;
                    int ok = DatabaseOperations.UpdateToDo(todo);

                    if (ok > 0)
                    {
                        DatabaseOperations.UpdateToDo(todo);
                        btnGoBack_Click(sender, e);

                    }
                    else
                    {
                        MessageBox.Show("ToDo is niet aangepast!");
                    }
                }
                else
                {
                    
                    DatabaseOperations.AddToDo(todo);
                    btnGoBack_Click(sender, e);
                }
            }
            else
            {
                MessageBox.Show(todo.Error);
            }

        }
    }
}
