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
            var l = Enum.GetValues(typeof(ETest)).Cast<ETest>().ToList();

            foreach (var i in l)
            {
                Console.WriteLine(i.GetTypeCode());
            }

            //foreach (var e in Enum.GetValues(typeof(ETest)))
            //{
            //    Console.WriteLine(e.ToString());
            //}
            

            
        }

        public enum ETest
        {
            A = 1,
            B = 2,
            C = 3,
        }

    }
}
