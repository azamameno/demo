using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestPush
{
    public class ConvertToUnSign
    {
        public static void Run()
        {
            Console.Write("Text: ");
            Console.WriteLine("To: " + ConvertTextToUnSign(Console.ReadLine()));
        }

        private static string ConvertTextToUnSign(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
