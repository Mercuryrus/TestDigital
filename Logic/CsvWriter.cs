using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestDigital.Logic
{
    class CsvWriter
    {
        public void CsvWrite(string filename, List<List<int>> res)
        {
            StreamWriter write = new StreamWriter(filename);
            foreach (var line in res)
            {
                foreach (var item in line)
                {
                    write.Write(item);
                    write.Write(";");
                }
                write.WriteLine();
            }
            write.Close();
        }
    }
}
