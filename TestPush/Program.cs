using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace TestPush
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            //push noti
            //DemoPushNotification.Run();

            //hash pass
            //HashCode.Run();

            //Check Process
            //CheckAppProcess.Run();

            //Convert to unsign
            //ConvertToUnSign.Run();

            //Enum
            //TestEnum.Run(1);

            //Parallel
            //ParallelTest.Run();

            //Time run lopp
            //TimeRunForLoop.Run();

            //Generate code
            //GenerateCode.Run();

            //Text list
            //TestList.Run();

            //XML
            //FuncXML.Run();

            //Multi lan
            //MultiLan.Run();

            //Get Method Name
            //MethodName.Run();

            //Generic class
            Info info = new Info()
            {
                Email = "abc@gmai.com",
                Password = "123"
            };

            var json = JsonConvert.SerializeObject(info);
            Console.WriteLine(json);
            var result = GenericClassSupport.GetT<Info>(json);


            Console.Read();
        }

    }
}
