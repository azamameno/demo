using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestPush
{
    public class ParallelTest
    {
        public static void Run()
        {
            Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 10 }, count =>
             {
                 string threadName = Thread.CurrentThread.Name;
                 int threadID = Thread.CurrentThread.ManagedThreadId;
                 Console.WriteLine("Thread name [" + threadName + "] Thread ID [" + threadID + "] Count [" + count + "]");
                 Thread.Sleep(1000);
             });
        }
    }
}
