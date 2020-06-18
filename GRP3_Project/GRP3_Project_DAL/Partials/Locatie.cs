using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GRP3_Project_MODEL;

//BRYAN ANTONIS

namespace GRP3_Project_DAL
{
    public partial class Locatie : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                //Check of Naam, Straat, Gemeente, Manager, Email, Telefoon leeg is en return een String voor validatie
                if (columnName == "Naam" && string.IsNullOrWhiteSpace(Naam))
                {
                    return "Vul een locatie naam in!";
                }

                if (columnName == "Straat" && string.IsNullOrWhiteSpace(Straat))
                {
                    return "Vul een straat in!";
                }

                if (columnName == "Gemeente" && string.IsNullOrWhiteSpace(Gemeente))
                {
                    return "Vul een gemeente in!";
                }

                if (columnName == "Manager" && string.IsNullOrWhiteSpace(Manager))
                {
                    return "Vul een manager in!";
                }

                //Regex voor validatie van Email
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
