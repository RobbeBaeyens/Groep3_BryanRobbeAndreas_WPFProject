using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRP3_Project_DAL
{
    class FileOperations
    {
        public static void FoutLoggen(Exception fout)
        {
            using (StreamWriter schrijver = new StreamWriter("gelogdeFouten.txt", true))
            {
                schrijver.WriteLine(DateTime.Now.ToString("HH=mm:ss tt"));
                schrijver.WriteLine(fout.Message);
                schrijver.WriteLine(new String('-', 30));
                schrijver.WriteLine();
                schrijver.WriteLine();
            }
        }
    }
}
