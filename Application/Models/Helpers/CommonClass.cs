using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Helpers
{
    public class CommonClass
    {
        public byte[] GenerateFileCSV(string content)
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                streamWriter.WriteLine(content);
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }
        public string GetMonthString(int month)
        {
            string strMonth = string.Empty;

            switch (month)
            {
                case 1:
                    strMonth = "JAN";
                    break;
                case 2:
                    strMonth = "FEB";
                    break;
                case 3:
                    strMonth = "MAR";
                    break;
                case 4:
                    strMonth = "APR";
                    break;
                case 5:
                    strMonth = "MAY";
                    break;
                case 6:
                    strMonth = "JUN";
                    break;
                case 7:
                    strMonth = "JUL";
                    break;
                case 8:
                    strMonth = "AUG";
                    break;
                case 9:
                    strMonth = "SEP";
                    break;
                case 10:
                    strMonth = "OCT";
                    break;
                case 11:
                    strMonth = "NOV";
                    break;
                case 12:
                    strMonth = "DEC";
                    break;
                default:
                    break;
            }

            return strMonth;

        }
    }
}
