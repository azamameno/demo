using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestPush
{
    public class GenerateCode
    {
        public static void Run()
        {
            System.Collections.Generic.List<string> listCode = new System.Collections.Generic.List<string>();
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                string code = "";
                //int count = 0;
                do
                {
                    //count += 1;
                    //if (count != 1)
                    //    Thread.Sleep(20);
                    code = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 40).Select(s => s[random.Next(s.Length)]).ToArray());
                } while (listCode.Contains(code));
                listCode.Add(code);
                Console.WriteLine(i + 1 + "\t" + code);
            }
        }
    }
}
