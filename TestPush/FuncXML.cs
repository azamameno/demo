using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Xml;

namespace TestPush
{
    public class FuncXML
    {
        public static void Run()
        {
            //XmlDocument xdoc = new XmlDocument();
            //string path ="InitializationDb.config";
            //xdoc.Load(path);

            //var listParentNode = xdoc.SelectNodes("//hierarchySection/modules/module");


            //foreach (XmlNode node in listParentNode)
            //{
            //    Console.WriteLine("Name: " + node.Attributes["display"].Value);
            //    if (node.HasChildNodes)
            //    {
            //        foreach (XmlNode child in node.FirstChild)
            //        {
            //            Console.WriteLine("----Name: " + child.Attributes["display"].Value);
            //        }
            //    }
            //}

            while (true)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XMLFile.xml");
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(path);
                int min = Convert.ToInt32(xdoc.SelectSingleNode("//deplay").InnerText);
                Console.WriteLine(string.Format("Starting timer with callback every {0} minutes.", min));

                List<ItemNode> listData = new List<ItemNode>();
                var listItem = xdoc.SelectNodes("//item");
                foreach (XmlNode node in listItem)
                {
                    ItemNode itemNode = new ItemNode();
                    var item = node.Clone();
                    itemNode.Name = item.SelectSingleNode("//name").InnerText;
                    itemNode.LinkAPI = item.SelectSingleNode("//api").InnerText;
                    itemNode.Method = item.SelectSingleNode("//method").InnerText;
                    itemNode.Time = Convert.ToInt32(item.SelectSingleNode("//time").InnerText);

                    foreach (XmlNode url in item.SelectNodes("//url"))
                        itemNode.ListURL.Add(url.InnerText);

                    if (itemNode.Method == "POST")
                    {
                        List<string> listParam = new List<string>();
                        foreach (XmlNode paramNode in item.SelectNodes("//param"))
                        {
                            var param = paramNode.Clone();
                            listParam.Add(GetJsonParam(param));
                        }

                        var data = JsonConvert.SerializeXmlNode(item);
                        Console.WriteLine(data);

                        itemNode.JsonData = "{" + string.Join(",", listParam.ToArray()) + "}";
                    }

                    listData.Add(itemNode);
                }

                foreach (var item in listData)
                {
                    if (!string.IsNullOrEmpty(item.LinkAPI) && item.ListURL.Count > 0)
                    {
                        bool isRun = true;
                        if (item.Time != -1 && DateTime.Now.Hour != item.Time) isRun = false;

                        if (isRun)
                        {
                            foreach (var url in item.ListURL)
                            {

                                Console.WriteLine(string.Format("Call back {0} at = {1}", item.Name, DateTime.Now));
                                //if (item.Method == "GET")
                                //    new Thread(() => Get(url, item.LinkAPI)).Start();
                                //else if (item.Method == "POST")
                                //    new Thread(() => Post(url, item.LinkAPI, item.JsonData)).Start();

                            }
                        }
                    }
                }

                Console.WriteLine(string.Format("Sleep {0} minutes.", min));
                Thread.Sleep((int)TimeSpan.FromMinutes(min).TotalMilliseconds);
            }

        }

        private static async Task Get(string url, string api)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP POST to other server
                HttpResponseMessage saveData = await client.GetAsync(api);
            }
        }

        private static async Task Post(string url, string api, string json)
        {
            using (var client = new HttpClient())
            {
                object obj = JsonConvert.DeserializeObject(json);

                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP POST to other server
                var saveData = await client.PostAsJsonAsync(api, obj);
            }
        }

        private static string GetJsonParam(XmlNode node)
        {
            string name = node.Attributes["name"].InnerText;
            string value = "";
            string dataType = node.Attributes["datatype"].InnerText;
            
            if (node.SelectNodes("//child").Count > 0)
            {
                List<string> listValue = new List<string>();
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (dataType.ToLower().Trim() == "string")
                        listValue.Add("\"" + child.InnerText + "\"");
                    else
                        listValue.Add(child.InnerText);
                }
                value = "[" + string.Join(",", listValue.ToArray()) + "]";
            }
            else
            {
                if (dataType.ToLower().Trim() == "string")
                    value = "\"" + node.InnerText + "\"";
                else
                    value = node.InnerText;
            }

            return "\"" + name + "\":" + value;
        }
    }

    public class ItemNode
    {
        public string Name { get; set; }
        public List<string> ListURL { get; set; }
        public string LinkAPI { get; set; }
        public string Method { get; set; }
        public int Time { get; set; }
        public string JsonData { get; set; }

        public ItemNode()
        {
            ListURL = new List<string>();
        }
    }

    public class Module
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int Status { get; set; }
    }
}
