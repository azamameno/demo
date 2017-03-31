using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPush
{
    public class DemoPushNotification
    {
        public static void Run()
        {
            List<string> listDeviceToken = new List<string>()
            {
                "2e08886ffbc7563f82a66afacc55e9609b3f9239090ce3dc083088b222c68760"
            };
            bool isContinue = false;
            do
            {
                Console.WriteLine("######################### Start ########################");
                PushNotifications("test test", "test test", listDeviceToken);
                Console.WriteLine("########################## End #########################");
                Console.WriteLine("## Press Enter to continue, ESC to exit");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter) isContinue = true;
                if (key == ConsoleKey.Escape) isContinue = false;
            } while (isContinue);
        }

        public static async void PushNotifications(string _title, string _mess, List<string> listDeviceToken)
        {
            try
            {
                string certificates = "http://115.78.191.245/poins.wallet/Certificates2.p12";
                byte[] appleCert = new System.Net.WebClient().DownloadData(certificates);
                var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, appleCert, "1234qwer");

                var broker = new ApnsServiceBroker(config);
                broker.OnNotificationSucceeded += Broker_OnNotificationSucceeded;
                broker.OnNotificationFailed += Broker_OnNotificationFailed;

                var fbs = new FeedbackService(config);
                fbs.FeedbackReceived += Fbs_FeedbackReceived;

                broker.Start();
                NotificationObject obj = new NotificationObject();
                if (string.IsNullOrEmpty(_title))
                    obj.aps.alert = _mess;
                else
                    obj.aps.alert = new { title = _title, body = _mess };

                string json = JsonConvert.SerializeObject(obj);
                foreach (var item in listDeviceToken)
                {
                    broker.QueueNotification(new ApnsNotification()
                    {
                        DeviceToken = item,   /* token of device to push */
                        Payload = JObject.Parse(json)
                    });
                }


                broker.Stop();
                Debug.WriteLine("Push Notifications Json: ", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Push Notifications Error: ", ex);
            }
        }

        private static void Fbs_FeedbackReceived(string deviceToken, DateTime timestamp)
        {
            Debug.WriteLine("Push Notifications Feedback.");
        }

        private static void Broker_OnNotificationFailed(ApnsNotification notification, AggregateException exception)
        {
            Debug.WriteLine("Push Notifications Failed.");
        }

        private static void Broker_OnNotificationSucceeded(ApnsNotification notification)
        {
            Debug.WriteLine("Push Notifications Succeeded.");
        }
    }

    public class PushNotiInfo
    {
        public string DeviceToken { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Badge { get; set; }
        public string FuncID { get; set; }
        public int FuncCode { get; set; }
    }

    public class NotificationObject
    {
        public Aps aps { get; set; }
        public string content_id { get; set; }
        public int content_code { get; set; }
        public string content_noti_id { get; set; }
        public NotificationObject()
        {
            aps = new Aps();
            content_id = "9daacefd-47bd-421d-a13d-efda4f13935f";
            content_code = 8;
            content_noti_id = "transferNoti";
        }
    }

    public class Aps
    {
        public object alert { get; set; }
        public int badge { get; set; }
        public string sound { get; set; }

        public Aps()
        {
            badge = 1;
            sound = "default";
        }
    }
}
