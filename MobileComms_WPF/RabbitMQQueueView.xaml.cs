using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabbitMQQueueView.xaml
    /// </summary>
    public partial class RabbitMQQueueView : UserControl
    {
        private MobileComms_ITK.RabbitMQ RabbitMQ { get; }
        private string QueueName { get; set; }
        public RabbitMQQueueView(MobileComms_ITK.RabbitMQ rabbitMQ, string queueName)
        {
            InitializeComponent();

            RabbitMQ = rabbitMQ;
            QueueName = queueName;

            LblQueueName.Content = queueName;

            ThreadPool.QueueUserWorkItem(UpdateThread);
        }

        private void UpdateThread(object state)
        {
            while(true)
            {
                string res = RabbitMQ.Get(QueueName);
                if(RabbitMQ.IsException)
                    break;
                if(!string.IsNullOrEmpty(res))
                {
                    DeserializeJSONtoDataGrid(res);
                }
                else
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Render,
                            (Action)(() =>
                            {
                                LblState.Content = "Loaded";
                            }));
                }

                Thread.Sleep(1000);
            }
        }

        private void DeserializeJSONtoDataGrid(string json)
        {

            var lst = JsonConvert.DeserializeObject<JArray>(json);
                bool hasUpd = false;
            foreach(JObject elem in lst)
            {

                if(elem.ContainsKey("upd"))
                {
                    hasUpd = true;
                    DateTime dt = DateTimeOffset.FromUnixTimeMilliseconds((long)elem.Property("upd").Value["millis"]).DateTime;
                    elem.Property("upd").Remove();
                    elem.Property("namekey").AddAfterSelf(new JProperty("upd", dt));
                }
                else
                {
                    break;
                }
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        if(DgMain.ItemsSource == null)
                        {
                            if(hasUpd)
                                DgMain.ItemsSource = lst.OrderByDescending(x => x["upd"]);
                        else
                                DgMain.ItemsSource = lst;
                        }
                        else
                            foreach(var item in lst)
                                ((JArray)DgMain.ItemsSource).Insert(0, item);
                        DgMain.Items.Refresh();
                    }));

            //Type t = Type.GetType($"Classes.MobileComms_ITK.JSON_Types.{className}");
            //if(t == null)
            //{
            //    //TxtJsonSchema.Text = string.Empty;
            //    //TxtJsonData.Text = string.Empty;
            //}
            //else
            //{
            //    //Type genericListType = typeof(IList<>).MakeGenericType(t);

            //    //PropertyInfo prop;
            //    //if((prop = t.GetProperty("upd")) != null)
            //    //{
            //    //    if(prop.PropertyType == typeof(Timestamp))
            //    //    {
            //    //        Timestamp ts = (Timestamp)Activator.CreateInstance(typeof(Timestamp));

            //    //        prop.SetValue(t, ts, null);
            //    //    }

            //    //}




            //}
        }
    }
}
