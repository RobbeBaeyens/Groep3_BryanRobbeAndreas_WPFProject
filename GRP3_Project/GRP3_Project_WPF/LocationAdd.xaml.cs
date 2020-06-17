﻿using System;
using System.Collections;
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
using GRP3_Project_DAL;
using GRP3_Project_MODEL;

//Bryan Antonis

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackToLocationSelector_Click(object sender, RoutedEventArgs e)
        {
            Window backToLocSel = new LocationSelector();
            backToLocSel.Show();
            Close();
        }

        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            ArrayList errors = new ArrayList();

            LocationFormProperties lfp = new LocationFormProperties();

            string name = txtName.Text;
            string street = txtStreet.Text;
            int number = int.Parse(txtNumber.Text);
            int postal = int.Parse(txtPostal.Text);
            string city = txtCity.ToString();
            string manager = txtManager.ToString();
            string email = txtEmail.ToString();
            string phone = txtPhone.ToString();

            if (lfp.CheckString(name) == false) 
            {

            }
        }
    }
}
