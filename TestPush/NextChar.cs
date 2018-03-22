using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class NextChar
    {
        public static void Run()
        {
            Console.Write("Input: ");
            Console.WriteLine("Output: " + GetNextStr(Console.ReadLine()));
        }

        private static string GetNextStr(string str)
        {
            str = str.ToUpper();
            char chr = str[0];
            int nu = int.Parse(str.Substring(1));

            if (nu == 999)
            {
                nu = 1;
                if (chr == 'Z') chr = 'A';
                else chr = (char)(((int)chr) + 1);
            }
            else nu++;

            return chr + nu.ToString().PadLeft(3, '0');
        }

    }
}
