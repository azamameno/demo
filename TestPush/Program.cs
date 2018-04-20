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
            //Info info = new Info()
            //{
            //    Email = "abc@gmai.com",
            //    Password = "123"
            //};

            //var json = JsonConvert.SerializeObject(info);
            //Console.WriteLine(json);
            //var result = GenericClassSupport.GetT<Info>(json);

            //Extention check Datetime
            //ExtentionObject.Run();

            //Auto delete log
            //AutoDeleteLog.Run();

            //test split list
            //string dayStr = "-2-3-4-5-6-7-8-9-10-11-12-13-14-15-16-17-18-19-20-21-22-23-24-25-26-27-28-29-30-31-";
            //var listDay = dayStr.Split('-').Where(o => !string.IsNullOrEmpty(o)).Select(int.Parse).ToList();
            //bool isOk = dayStr.Contains("-1-");

            //round down
            //Rounding.Run();

            //next char
            //NextChar.Run();

            //regx
            RegX.Run();

            Console.Read();
        }

    }
}
