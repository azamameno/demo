using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class TestList
    {
        public static void Run()
        {
            List<int> l1 = new List<int>() { 1, 2, 3, 4, 5 };
            List<int> l2 = new List<int>() { 6, 7, 8, 9, 10 };

            Console.Write("List 1: ");
            l1.ForEach(o => Console.Write(o + ","));
            Console.WriteLine();

            Console.Write("List 2: ");
            l2.ForEach(o => Console.Write(o + ","));
            Console.WriteLine();

            l1 += l2;
            Console.Write("List 3: ");
            l1.ForEach(o => Console.Write(o + ","));
        }
    }

    public class List<T> : System.Collections.Generic.List<T>
    {
        public static List<T> operator +(List<T> l1, List<T> l2)
        {
            List<T> result = new List<T>();
            result.AddRange(l1);
            result.AddRange(l2);
            return result;
        }
    }
}
