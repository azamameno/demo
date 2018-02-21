using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class ExtentionObject
    {
        public static void Run()
        {
            DemoObj obj = new DemoObj()
            {
                IntProp = 1,
                StringProp = "abc",
                BoolProp = true,
                DateTimeProp = new DateTime(2000, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            };

            ChangeObj(obj);

            if (obj != null)
                Console.WriteLine("aaaa");

        }

        public static void ChangeObj(object obj)
        {
            var listProp = obj.GetType().GetProperties();
            foreach (var prop in listProp)
            {
                if (prop.PropertyType == typeof(Nullable<DateTime>) || prop.PropertyType == typeof(DateTime))
                {
                    DateTime? date = (DateTime?)prop.GetValue(obj, null);
                    if (date.HasValue)
                        prop.SetValue(obj, date.Value.ToLocalTime(), null);
                }
                Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(obj, null));
            }
        }
    }

    public class DemoObj
    {
        public int IntProp { get; set; }
        public string StringProp { get; set; }
        public DateTime DateTimeProp { get; set; }
        public bool BoolProp { get; set; }
    }
}
