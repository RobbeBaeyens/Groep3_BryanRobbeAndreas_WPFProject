using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GRP3_Project_MODEL;

//Bryan Antonis

namespace GRP3_Project_DAL
{
    public partial class Locatie : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Vul een locatie naam in!";
                }

                if (columnName == "Straat" && string.IsNullOrWhiteSpace(Straat))
                {
                    return "Vul een straat in!";
                }

                //if (columnName == "Huisnr" && Huisnr.GetType() != typeof(int))
                //{
                //    return "Vul een huisnummer in!";
                //}

                //if (columnName == "Postcode" && Postcode.GetType() != typeof(int))
                //{
                //    return "Vul een postcode in!";
                //}

                if (columnName == "Gemeente" && string.IsNullOrWhiteSpace(Gemeente))
                {
                    return "Vul een gemeente in!";
                }

                if (columnName == "Manager" && string.IsNullOrWhiteSpace(Manager))
                {
                    return "Vul een manager in!";
                }

                if (columnName == "Email" && (string.IsNullOrWhiteSpace(Email) || 
                    !Regex.IsMatch(Email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$")))
                {
                    return "Vul een geldige E-mail in!";
                }

                if (columnName == "Telefoon" && string.IsNullOrWhiteSpace(Telefoon))
                {
                    return "Vul een telefoonnummer in!"; 
                }

                return "";
            }
        }
    }
}
