using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestPush
{
    public class RegX
    {
        public static void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            bool success = Regex.Match(input, "^[a-zA-Z0-9_.]*$").Success;
            Console.WriteLine(success);
        }
    }
}
