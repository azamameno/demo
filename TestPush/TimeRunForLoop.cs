using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class TimeRunForLoop
    {
        public static void Run()
        {
            int sum1 = 0;
            int sum2 = 0;
            System.Collections.Generic.List<int> listInt = new System.Collections.Generic.List<int>();

            for (int i = 0; i < 10000000; i++)
            {
                listInt.Add(i);
            }

            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            sw1.Start();
            foreach (var i in listInt)
            {
                sum1 += i;
            }
            sw1.Stop();

            sw2.Start();
            listInt.ForEach(i => sum2 += i);
            sw2.Stop();

            Console.WriteLine(sum1);
            Console.WriteLine(sw1.ElapsedMilliseconds);
            Console.WriteLine(sum2);
            Console.WriteLine(sw2.ElapsedMilliseconds);
        }
    }
}
