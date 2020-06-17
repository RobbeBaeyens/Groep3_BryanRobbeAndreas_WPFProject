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
               
                return "";
            }
        }
    }
}
