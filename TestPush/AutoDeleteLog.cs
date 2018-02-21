using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class AutoDeleteLog
    {
        private static string formatLog = "dd.MM.yyyy";
        private static int holdDay = 3;
        public static void Run()
        {
            CreateLog();
            DeleteLog();
        }

        private static void DeleteLog()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (Directory.Exists(path))
            {
                DateTime holdDate = DateTime.Now.AddDays(-holdDay).Date;
                var dir = new DirectoryInfo(path);
                var files = dir.GetFiles().Where(o => DbFunctions.TruncateTime(o.CreationTime) < DbFunctions.TruncateTime(holdDate)).ToArray();
                foreach (var file in files)
                    file.Delete();

            }
        }

        private static void CreateLog()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var listFilePath = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

            for (DateTime date = DateTime.Now.AddDays(-10); date.Date <= DateTime.Now.Date; date = date.AddDays(1))
            {
                string pathFile = Path.Combine(path, date.ToString(formatLog) + ".log");
                if (!File.Exists(pathFile))
                {
                    using (FileStream fs = File.Create(pathFile))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes(pathFile);
                        fs.Write(info, 0, info.Length);
                    }
                }
            }
        }
    }
}
