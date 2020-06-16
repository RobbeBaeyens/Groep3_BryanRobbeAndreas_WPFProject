﻿using System;
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
    /// Interaction logic for LocationAdd.xaml
    /// </summary>
    public partial class LocationAdd : Window
    {
        public LocationAdd()
        {
            InitializeComponent();
        }

        private void btnBackToLocationSelector_Click(object sender, RoutedEventArgs e)
        {
            Window backToLocSel = new LocationSelector();
            backToLocSel.Show();
            Close();
        }
    }
}
