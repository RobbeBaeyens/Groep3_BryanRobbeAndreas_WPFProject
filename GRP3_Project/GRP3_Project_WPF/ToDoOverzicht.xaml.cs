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
        List<ToDo> todoList = new List<ToDo>();
        List<MoreToDoInfo> moreToDoInfo = new List<MoreToDoInfo>();
        public ToDoOverzicht()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddToDo_Click(object sender, RoutedEventArgs e)
        {
            Window todoBewerken = new ToDoBewerken(1);
            Close();
            todoBewerken.Show();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
