using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class GenericClassSupport
    {
        public GenericClassSupport()
        {

        }

        public static T GetT<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class Info
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
