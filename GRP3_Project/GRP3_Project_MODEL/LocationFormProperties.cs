using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Bryan Antonis

namespace GRP3_Project_MODEL
{
    public class LocationFormProperties
    {
        public bool CheckString(string input)
        {
            if (input != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
