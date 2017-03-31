using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            //push noti
            DemoPushNotification.Run();

            //hash pass
            //HashCode.Run();

            //Check Process
            //CheckAppProcess.Run();

            //Convert to unsign
            //ConvertToUnSign.Run();

            Console.Read();
        }

    }
}
