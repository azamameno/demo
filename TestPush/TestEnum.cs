using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class TestEnum
    {
        public static void Run(int num)
        {
            Console.WriteLine("Number: " + num);
            Console.WriteLine("Enum: " + (ETest)num);
        }

        public enum ETest
        {
            A = 1,
            B = 2,
            C = 3,
        }

    }
}
