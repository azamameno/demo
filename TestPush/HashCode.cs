using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class HashCode
    {
        public static void Run()
        {
            Console.Write("No level hash: ");
            int level = int.Parse(Console.ReadLine());
            Console.WriteLine("====================================");
            Console.Write("Pass: ");
            string pass = Console.ReadLine();
            string code = GetCode(level);
            string hash = HashPass(pass, code);
            Console.WriteLine("Code: " + code);
            Console.WriteLine("Hash: " + hash);
            Console.WriteLine("====================================");
            Console.Write("Enter pass: ");
            pass = Console.ReadLine();
            Console.Write("Enter code: ");
            code = Console.ReadLine();
            hash = HashPass(pass, code);
            Console.WriteLine("Hash: " + hash);
        }

        private static string HashPass(string text, string code)
        {
            string result = text;
            foreach (var item in code.ToCharArray())
            {
                switch (item)
                {
                    case '1':
                    case '3':
                    case '5':
                    case '7':
                    case '9':
                        result = HashSHA512(result);
                        break;
                    case '2':
                    case '4':
                    case '6':
                    case '8':
                        result = HashMD5(result);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private static string GetCode(int len)
        {
            Random random = new Random();
            return new string(Enumerable.Repeat("123456789", len).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string HashSHA512(string text)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(text);

            SHA512 hashString = new SHA512Managed();
            string hex = string.Empty;

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += x.ToString("x2");
            }
            return hex;
        }

        private static string HashMD5(string text)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}
