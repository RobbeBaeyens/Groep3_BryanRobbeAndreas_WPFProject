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
        //Andreas Steenackers
        public override string this[string columnName]
        {
            get
            {
                //kijkt na of Titel leegstaat
                if (columnName == "Titel" && String.IsNullOrWhiteSpace(Titel))
                {
                    return "Vul een titel in!";
                }
                //kijkt na of Omschrijving leeg is
                if (columnName == "Omschrijving" && String.IsNullOrWhiteSpace(Omschrijving))
                {
                    return "Vul een omschijving in!";
                }
                //kijkt na of Afgewerkt ene type is van ToDo
                if (columnName == "Afgewerkt" && Afgewerkt.GetType() != typeof(bool))
                {
                    return "Vink aan of af!";
                }
                //kijkt na of Volgnr een type is van ToDo
                if (columnName == "Volgnr" && Volgnr.GetType() != typeof(int))
                {
                    return "Volgnummer incorrect!";
                }

                return "";
            }
        }
    }
}
