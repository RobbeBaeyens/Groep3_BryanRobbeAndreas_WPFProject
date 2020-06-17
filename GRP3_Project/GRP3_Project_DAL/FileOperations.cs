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
        public static void Foutloggen(Exception fout)
        {
            using (StreamWriter schrijver = new StreamWriter("gelogdeFouten.txt", true))
            {
                schrijver.WriteLine(DateTime.Now.ToString("HH:mm:ss tt"));
                schrijver.WriteLine(fout.GetType().Name);
                schrijver.WriteLine(fout.Message);
                schrijver.WriteLine(fout.StackTrace);
                schrijver.WriteLine(new String('-', 80));
                schrijver.WriteLine();
            }
        }
    }
}
