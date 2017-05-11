using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushSharp.Apple;
using PushSharp.Google;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TestPush
{
    public class DemoPushNotification
    {
        public static void Run()
        {
            List<string> listDeviceToken = new List<string>()
            {
                "f3-xGV1opog:APA91bEAEmHL3pH69gSYknXSusnmE5pMqgmYqZwiNGNNF3UyRXENNC9oTQBbaHxtHsOdWCKnMeKCqY3i5RuwCuLsiP3jKmMKZyuinQkNUGJYYixzTFNb7703ohr_73M6gEjtlwGw9-xm",
            };
            bool isContinue = false;
            do
            {
                bool isPushSharp = false;
                Console.WriteLine("######################### Start ########################");

                if (isPushSharp)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        PushNotifications("test test", "test test", listDeviceToken, false);
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        foreach (var deviceRegId in listDeviceToken)
                        {
                            Console.WriteLine("#Server response: " + SendNotification(deviceRegId, "body" + i, "title" + i, 0));
                        }
                        Thread.Sleep(2000);
                    }
                }

                Console.WriteLine("########################## End #########################");
                Console.WriteLine("## Press Enter to continue, ESC to exit");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter) isContinue = true;
                if (key == ConsoleKey.Escape) isContinue = false;
            } while (isContinue);
        }

        public static string SendNotification(string deviceRegId, string message, string title, long id)
        {
            try
            {
                string SERVER_API_KEY = "AIzaSyCYBfpo0fH_3owQdmB2RfX1HgbG4vx3epM";
                string SENDER_ID = "29694662630";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                PushFCMNotification data = new PushFCMNotification();
                //data.notification.title = title;
                //data.notification.body = message;
                data.data.title = title;
                data.data.body = message;
                data.to = deviceRegId;

                string postData1 = new JavaScriptSerializer().Serialize(data);
                Console.WriteLine("###");
                Console.WriteLine("#Json Request: " + postData1);

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData1);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);
                string serverResponse = tReader.ReadToEnd();


                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                return serverResponse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return "";
        }

        public static async void PushNotifications(string _title, string _mess, List<string> listDeviceToken, bool isApple)
        {
            try
            {
                string json = "";
                if (isApple)
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

                    json = JsonConvert.SerializeObject(obj);
                    foreach (var item in listDeviceToken)
                    {
                        broker.QueueNotification(new ApnsNotification()
                        {
                            DeviceToken = item,   /* token of device to push */
                            Payload = JObject.Parse(json)
                        });
                    }

                    broker.Stop();
                }
                else
                {
                    string SERVER_API_KEY = "AIzaSyCYBfpo0fH_3owQdmB2RfX1HgbG4vx3epM";
                    string SENDER_ID = "29694662630";
                    //string senderKey = "29694662630";
                    //string apiKey = "AIzaSyAXGWom6HmfBm5iJVGP5G8InK7KxOmBeVY";
                    var config = new GcmConfiguration(SENDER_ID, SERVER_API_KEY, null);
                    config.GcmUrl = "https://fcm.googleapis.com/fcm/send";

                    var gcmBroker = new GcmServiceBroker(config);
                    gcmBroker.OnNotificationFailed += GcmBroker_OnNotificationFailed;
                    gcmBroker.OnNotificationSucceeded += GcmBroker_OnNotificationSucceeded;

                    gcmBroker.Start();

                    foreach (var item in listDeviceToken)
                    {
                        gcmBroker.QueueNotification(new GcmNotification
                        {
                            To = item,
                            // RegistrationIds = new List<string> { item },
                            ContentAvailable = true,
                            Priority = GcmNotificationPriority.High,
                            DryRun = true,
                            Notification = JObject.Parse(
                                            "{" +
                                                "\"title\" : \"" + _title + "\"," +
                                                "\"body\" : \"" + _mess + "\"," +
                                                "\"sound\" : \"default\"" +
                                            "}"),
                            Data = JObject.Parse(
                                            "{" +
                                                "\"content_id\":\"9daacefd-47bd-421d-a13d-efda4f13935f\"," +
                                                "\"content_noti_id\":\"transferNoti\"," +
                                                "\"content_code\":8," +
                                                "\"badge\":1" +
                                            "}")
                        });
                    }

                    gcmBroker.Stop();
                }

                Debug.WriteLine("Push Notifications Json: ", json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Push Notifications Error: ", ex);
            }
        }

        private static void GcmBroker_OnNotificationSucceeded(GcmNotification notification)
        {
            Debug.WriteLine("Push Notifications Succeeded.");
        }

        private static void GcmBroker_OnNotificationFailed(GcmNotification notification, AggregateException exception)
        {
            Debug.WriteLine("Push Notifications Failed.");
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

    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
    }

    public class Data
    {
        public string content_id { get; set; }
        public int content_code { get; set; }
        public string content_noti_id { get; set; }

        public Data()
        {
            content_id = "9daacefd-47bd-421d-a13d-efda4f13935f";
            content_code = 8;
            content_noti_id = "transferNoti";
        }
    }

    public class AndroidNotificationObject
    {
        public string to { get; set; }
        public string priority { get; set; }
        public Notification notification { get; set; }
        public Data data { get; set; }

        public AndroidNotificationObject()
        {
            priority = "normal";
            notification = new Notification();
            data = new Data();
        }
    }

    public class PushFCMNotification
    {
        public bool content_available { get; set; }
        public string collapse_key { get; set; }
        public int time_to_live { get; set; }
        public bool delay_while_idle { get; set; }
        public string priority { get; set; }
        public string to { get; set; }
        // public FCMNotification notification { get; set; }
        public FCMData data { get; set; }

        public PushFCMNotification()
        {
            content_available = true;
            collapse_key = "score_update";
            time_to_live = 10;
            delay_while_idle = true;
            priority = "normal";
            //notification = new FCMNotification();
            data = new FCMData();
        }
    }

    public class FCMNotification
    {
        public string title { get; set; }
        public string body { get; set; }
        public string sound { get; set; }
        public FCMNotification()
        {
            title = "";
            body = "";
            sound = "default";
        }
    }

    public class FCMData
    {
        public string content_id { get; set; }
        public string content_noti_id { get; set; }
        public int content_code { get; set; }
        public int badge { get; set; }

        public string title { get; set; }
        public string body { get; set; }
        public string sound { get; set; }

        public FCMData()
        {
            content_id = "9daacefd-47bd-421d-a13d-efda4f13935f";
            content_code = 8;
            content_noti_id = "transferNoti";
            badge = 1;

            title = "";
            body = "";
            sound = "default";
        }
    }
}
