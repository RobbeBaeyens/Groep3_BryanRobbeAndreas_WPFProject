using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void btnBackToLocationSelector_Click(object sender, RoutedEventArgs e)
        {
            //Window backToLocSel = new LocationSelector();
            //backToLocSel.Show();
            //Close();
        }

        private void btnSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            string foutmeldingen = Valideer("Huisnr");
            foutmeldingen += Valideer("Postcode");

            if (string.IsNullOrWhiteSpace(foutmeldingen))
            {
                Locatie locatie = new Locatie();

                locatie.Naam = txtName.Text;
                locatie.Straat = txtStreet.Text;
                locatie.Gemeente = txtCity.Text;
                locatie.Manager = txtManager.Text;
                locatie.Email = txtEmail.Text;
                locatie.Telefoon = txtPhone.Text;

                if (int.TryParse(txtNumber.Text, out int huisnr))
                {
                    locatie.Huisnr = huisnr;
                }

                if (int.TryParse(txtPostal.Text, out int postcode))
                {
                    locatie.Postcode = postcode;
                }

                if (locatie.IsGeldig())
                {
                    DatabaseOperations.ToevoegenLocatie(locatie);

                    btnBackToLocationSelector_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(locatie.Error);
                }
            }
            else
            {
                MessageBox.Show(foutmeldingen);
            }
        }

        private string Valideer(string input)
        {
            if (input == "Huisnr" && (!int.TryParse(txtNumber.Text, out int huisnr) || string.IsNullOrWhiteSpace(txtNumber.Text)))
            {
                return "Vul een correct huisnummer in!" + Environment.NewLine; ;
            }
            if (input == "Postcode" && (!int.TryParse(txtPostal.Text, out int postcode) || string.IsNullOrWhiteSpace(txtPostal.Text)))
            {
                return "Vul een correcte postcode in!" + Environment.NewLine;
            }
            return "";
        }
    }
}
