using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Utils
{
    public class CsvWriter
    {
       public static void WriteInfoToCsv(List<string> infoList)
        {
            string delimiter = ",";
            

            StringBuilder sb = new StringBuilder();
            foreach (string line in infoList)
            {
                sb.AppendLine(string.Join(delimiter, line));
            }               
            
            File.WriteAllText(TestData.outputFile, sb.ToString());
        }

    }
}
