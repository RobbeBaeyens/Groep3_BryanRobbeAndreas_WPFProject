/*
 * Robbe Baeyens
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GRP3_Project_MODEL;

namespace GRP3_Project_DAL
{
    public partial class Event : BasisKlasse
    {
        public override string this[string columnName]
        {
            get
            {
                if (columnName == "Eventnaam" && String.IsNullOrWhiteSpace(Eventnaam))
                {
                    return "Vul een event naam in!";
                }
                if (columnName == "Datum" && Datum.GetType() != typeof(DateTime) && Datum <= DateTime.Today.AddDays(-1))
                {
                    return "Selecteer een geldige datum die nog niet geweest is!";
                }
                if (columnName == "Startuur" && !Regex.IsMatch(Startuur, @"^(?:0?[0-9]|1[0-9]|2[0-3])u[0-5][0-9]$"))
                {
                    return "Vul een correct startuur in! HHuMM ==> vb. 12u30";
                }
                if (columnName == "Einduur" && !Regex.IsMatch(Einduur, @"^(?:0?[0-9]|1[0-9]|2[0-3])u[0-5][0-9]$"))
                {
                    return "Vul een correct einduur in! HHuMM ==> vb. 12u30";
                }
                return "";
            }
        }
    }
}
