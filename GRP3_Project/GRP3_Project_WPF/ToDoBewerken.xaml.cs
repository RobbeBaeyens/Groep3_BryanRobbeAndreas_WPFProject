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
{   // Andreas Steenackers
    /// <summary>
    /// Interaction logic for ToDoBewerken.xaml
    /// </summary>
    public partial class ToDoBewerken : Window
    {
        //Hier wordt een nieuwe eventitem aangemaakt en een todo
        int toDoID = -1;
        int eventID;
        ToDo todo = new ToDo();
        Event eventItem = new Event();

        //Hier wordt de eventID opgehaalt
        public ToDoBewerken(int eventID)
        {
            InitializeComponent();
            init(eventID);
        }

        //Zorgt ervoor dat de todo een ToDoid toegewezen krijgt 
        public ToDoBewerken(int toDoId, int eventID)
        {
            InitializeComponent();
            this.toDoID = toDoId;
            todo = DatabaseOperations.OphalenTodo(toDoID);
            init(eventID);
        }

        //hier wordt de eventID opgenomen en geplaats in een nieuwe eventitem
        private void init(int eventID)
        {
            this.eventID = eventID;
            eventItem = DatabaseOperations.OphalenEvent(eventID);
        }

        //Zorgt ervoor dat alles opgeladen wordt
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            txtHeaderSubText.Text = eventItem.Eventnaam;
            txtEventNaam.Text = eventItem.Eventnaam;
            lblVolgnr.Content = "#" + GetLastVolgnr();
            //Als de ToDo bewerkt wordt dan passen we dit aan in het scherm
            if(toDoID != -1)
            {
                txtToDoTitel.Text = todo.Titel;
                txtbOmschrijving.Text = todo.Omschrijving;
                tgbCheck.IsChecked = todo.Afgewerkt;
                lblVolgnr.Content = "#" + todo.Volgnr;
            }
        }


        //Zorgt voor een navigatie naar de vorige pagina
        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            Window todoOverzicht = new ToDoOverzicht(eventID);
            Close();
            todoOverzicht.Show();
        }

        //Dit gaat nakijken wat de laatste volgnr was en voegt hier 1 aan toe
        private int GetLastVolgnr()
        {
            List<ToDo> todoList = DatabaseOperations.OphalenToDos(eventID);
            if (todoList.Count == 0)
            {
                return 0;
            }
            return todoList[todoList.Count - 1].Volgnr + 1;
         
        }
        //Met deze knop kan je een nieuwe to do opslaan of als er een To Do bewerkt wordt, dan kan deze ook opgeslagen worden
        private void btnSaveToDo_Click(object sender, RoutedEventArgs e)
        {
            todo.Titel = txtToDoTitel.Text;
            todo.Omschrijving = txtbOmschrijving.Text;
            todo.Afgewerkt = (bool) tgbCheck.IsChecked;
            todo.EventID = eventItem.EventID;

            if (toDoID == -1)
            {
                todo.Volgnr = GetLastVolgnr();
            }
            //Nakijken of alles wel klopt
            if (todo.IsGeldig())
            {
                //Kijkt na of het een nieuw scherm is of een scherm dat je wil bewerken
                if (toDoID != -1)
                {
                    
                    todo.ToDoID = toDoID;
                    todo.Event = eventItem;

                    //kijkt of de update uitgevoerd kan worden
                    int ok = DatabaseOperations.UpdateToDo(todo);

                    if (ok > 0)
                    {
                        //Gaat de ToDo Update, daarna wordt er genavigeerd naar het vorige scherm.
                        DatabaseOperations.UpdateToDo(todo);
                        btnGoBack_Click(sender, e);

                    }
                    else
                    {
                        //geeft MessageBox als de ToDo niet is aangepast
                        MessageBox.Show("ToDo is niet aangepast!");
                    }
                }
                else
                {
                    //Gaat een ToDo aanmaken en dan wordt terug genavigeerd naar het vorig scherm
                    DatabaseOperations.AddToDo(todo);
                    btnGoBack_Click(sender, e);
                }
            }
            else
            {
                //laat een error zien
                MessageBox.Show(todo.Error);
            }

        }
    }
}
