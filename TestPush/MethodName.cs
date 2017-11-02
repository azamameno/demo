using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace TestPush
{
    public class MethodName
    {
        public static void Run()
        {
            string[] _words = { "Sam", "Dot", "Perls" };

            // Initialize StringWriter instance.
            StringWriter stringWriter = new StringWriter();

            // Put HtmlTextWriter in using block because it needs to call Dispose.
            using (HtmlTextWriter writer = new HtmlTextWriter(stringWriter))
            {
                // Loop over some strings.
                foreach (var word in _words)
                {
                    // Some strings for the attributes.
                    string classValue = "ClassName";
                    string urlValue = "http://www.dotnetperls.com/";
                    string imageValue = "image.jpg";

                    // The important part:
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, classValue);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div); // Begin #1

                    writer.AddAttribute(HtmlTextWriterAttribute.Href, urlValue);
                    writer.RenderBeginTag(HtmlTextWriterTag.A); // Begin #2

                    writer.AddAttribute(HtmlTextWriterAttribute.Src, imageValue);
                    writer.AddAttribute(HtmlTextWriterAttribute.Width, "60");
                    writer.AddAttribute(HtmlTextWriterAttribute.Height, "60");
                    writer.AddAttribute(HtmlTextWriterAttribute.Alt, "");

                    writer.RenderBeginTag(HtmlTextWriterTag.Img); // Begin #3
                    writer.RenderEndTag(); // End #3

                    writer.Write(word);

                    writer.RenderEndTag(); // End #2
                    writer.RenderEndTag(); // End #1
                }
            }
            // Return the result.
            Console.WriteLine(stringWriter.ToString());

        }

        private static void abc()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private static void abqeqwec()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private static void asdasfh()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private static void zxc()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }

        private static void yiiyui()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
    }
}
