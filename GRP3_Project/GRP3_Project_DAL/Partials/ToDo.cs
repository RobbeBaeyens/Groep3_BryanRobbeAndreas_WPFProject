using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GRP3_Project_MODEL;

namespace GRP3_Project_DAL
{
    public partial class ToDo : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Titel" && String.IsNullOrWhiteSpace(Titel))
                {
                    return "Vul een titel in!";
                }
                if (columnName == "Omschrijving" && String.IsNullOrWhiteSpace(Omschrijving))
                {
                    return "Vul een omschijving in!";
                }
                if (columnName == "Afgewerkt" && Afgewerkt.GetType() != typeof(bool))
                {
                    return "Vink aan of af!";
                }
                if (columnName == "Volgnr" && Volgnr.GetType() != typeof(int))
                {
                    return "Volgnummer incorrect!";
                }

                return "";
            }
        }
    }
}
