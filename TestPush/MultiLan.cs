using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class MultiLan
    {
        public static void Run()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>()
            {
                { 0,"a0"},
                { 1,"b1"},
                { 2,"c2"},
                { 3,"d3"},
                { 4,"e4"},
                { 5,"f5"},
                { 6,"g6"},
            };

            string str = "1 new earn rule: Earn items in {2} (Category) when buying {3} on {5} is just applied at";
            Console.WriteLine(str);
            str = string.Format(str, dict.OrderBy(o => o.Key).Select(o => o.Value).ToArray());
            Console.WriteLine(str);

        }
    }
}
