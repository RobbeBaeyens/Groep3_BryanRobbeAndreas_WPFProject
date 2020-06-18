/*
 * Robbe Baeyens
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRP3_Project_MODEL
{
    //Zelfgemaakte extra klasse om betere strings door te geven aan de listbox view in XAML
    public class MoreEventInfo
    {
        public int EventId { get; set; }
        public string Eventnaam { get; set; }
        public string EventtypeNaam { get; set; }
        public string Dag { get; set; }
        public string DatumTijd { get; set; }
        public byte[] Logo { get; set; }
        public string Color { get; set; }
    }
}
