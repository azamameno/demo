using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestPush
{
    public class CheckAppProcess
    {
        public static void Run()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000); 
            }
        }

    }
}
