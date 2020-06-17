using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRP3_Project_MODEL
{
    public class MoreToDoInfo
    {
        public int ToDoId { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public string Volgnr { get; set; }
        public string ImgChecked { get; set; }
        public byte[] Logo { get; set; }
        public string Color { get; set; }
    }
}
