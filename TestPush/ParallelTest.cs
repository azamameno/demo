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
            var request = new
            {
                str = "123",
                so = 1,
                bo = true,
                date = new DateTime(5, 5, 5, 5, 5, 5),
                fl = 1.25
            };

            var response = new
            {
                str = "abc",
                so = 2,
                bo = true,
                date = new DateTime(10, 10, 10, 10, 10, 10),
                fl = 2.25
            };

            try
            {
                TLog.Logger.Info("info");
                TLog.Logger.Info("info", request);

                TLog.Logger.Debug("debug");
                TLog.Logger.Debug("debug", response);

                TLog.Logger.Warning("w");

                List<string> listStr = null;

                int c = listStr.Count;
            }
            catch (Exception ex)
            {
                TLog.Logger.Error(ex);
                TLog.Logger.Error("Error", ex);
                TLog.Logger.Error("Error", request, response, ex);
            }

            //Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 10 }, count =>
            // {
            //     string threadName = Thread.CurrentThread.Name;
            //     int threadID = Thread.CurrentThread.ManagedThreadId;
            //     Console.WriteLine("Thread name [" + threadName + "] Thread ID [" + threadID + "] Count [" + count + "]");
            //     Thread.Sleep(1000);
            // });
        }
    }
}
