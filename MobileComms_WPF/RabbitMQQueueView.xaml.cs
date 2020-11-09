using MobileComms_ITK;
using MobileComms_ITK.JSON.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabbitMQQueueView.xaml
    /// </summary>
    public partial class RabbitMQQueueView : UserControl
    {
        private RabbitMQ RabbitMQ { get; }
        private string QueueName { get; set; }
        private string ChannelName { get; } = Guid.NewGuid().ToString();
        private Type ClassType { get; set; }

        public RabbitMQQueueView(RabbitMQ rabbitMQ, KeyValuePair<string, Type> cmd)
        {
            InitializeComponent();

            RabbitMQ = rabbitMQ;

            RabbitMQ.CreateNewChannel(ChannelName);

            QueueName = cmd.Key;
            ClassType = cmd.Value;

            LblQueueName.Content = QueueName;

            ThreadPool.QueueUserWorkItem(UpdateThread);
        }

        private void UpdateThread(object state)
        {
            while(true)
            {
                RabbitMQ.Messages lst = RabbitMQ.Get(QueueName, ChannelName);

                if(RabbitMQ.IsException)
                    break;
                if(lst.Count > 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Render,
                            (Action)(() =>
                            {
                                DeserializeJSONtoDataGrid(lst);
                            }));

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
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        LblState.Content = "Exited";
                    }));
        }
        private void DeserializeJSONtoDataGrid(RabbitMQ.Messages messages)
        {

            DataTable datatable = new DataTable();
            foreach(PropertyInfo prop in ClassType.GetProperties())
            {
                if(prop.Name.Equals("AdditionalProperties")) continue;
                if(prop.Name.Equals("Details")) continue;

                DataColumn column;
                if(prop.PropertyType == typeof(Timestamp))
                {
                    column = new DataColumn
                    {
                        DataType = typeof(DateTime),
                        ColumnName = prop.Name,
                        ReadOnly = true
                    };
                }
                else if(prop.PropertyType.IsEnum)
                {
                    column = new DataColumn
                    {
                        DataType = typeof(string),
                        ColumnName = prop.Name,
                        ReadOnly = true
                    };
                }
                else
                {
                    column = new DataColumn
                    {
                        DataType = prop.PropertyType,
                        ColumnName = prop.Name,
                        ReadOnly = true
                    };
                }

                datatable.Columns.Add(column);
            }

            DataColumn col = new DataColumn
            {
                DataType = typeof(ulong),
                ColumnName = "DeliveryTag",
                ReadOnly = true
            };
            datatable.Columns.Add(col);

            col = new DataColumn
            {
                DataType = typeof(bool),
                ColumnName = "Redelivered",
                ReadOnly = true
            };
            datatable.Columns.Add(col);

            foreach(RabbitMQ.Message message in messages)
            {
                JObject elem = JsonConvert.DeserializeObject<JObject>(message.Body);

                DataRow dr = datatable.NewRow();
                foreach(PropertyInfo prop in ClassType.GetProperties())
                {
                    if(prop.Name.Equals("AdditionalProperties")) continue;
                    if(prop.Name.Equals("Details")) continue;

                    if(prop.PropertyType == typeof(Timestamp))
                    {
                        JProperty prop1;
                        if((prop1 = elem.Property($"{char.ToLower(prop.Name[0])}{prop.Name.Substring(1)}")) != null)
                            dr[prop.Name] = DateTimeOffset.FromUnixTimeMilliseconds((long)prop1.Value["millis"]).DateTime;
                    }
                    else
                    {
                        JProperty prop1;
                        if((prop1 = elem.Property($"{char.ToLower(prop.Name[0])}{prop.Name.Substring(1)}")) != null)
                            dr[prop.Name] = prop1.Value;
                    }
                }

                dr["DeliveryTag"] = message.DeliveryTag;
                dr["Redelivered"] = message.Redelivered;
                datatable.Rows.Add(dr);
            }

            if(DgMain.ItemsSource == null)
            {
                DgMain.ItemsSource = datatable.DefaultView;
                if(datatable.Columns.Contains("DeliveryTag"))
                    DgMain.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("DeliveryTag", System.ComponentModel.ListSortDirection.Descending));
                DgMain.Tag = datatable;
            }
            else
                ((DataTable)DgMain.Tag).Merge(datatable);

        }

        //private void DeserializeJSONtoDataGrid(string json)
        //{

        //    var lst = JsonConvert.DeserializeObject<JArray>(json);
        //    bool hasUpd = false;
        //    foreach(JObject elem in lst)
        //    {

        //        if(elem.ContainsKey("upd"))
        //        {
        //            hasUpd = true;
        //            DateTime dt = DateTimeOffset.FromUnixTimeMilliseconds((long)elem.Property("upd").Value["millis"]).DateTime;
        //            elem.Property("upd").Remove();
        //            elem.Property("namekey").AddAfterSelf(new JProperty("upd", dt));
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    Dispatcher.BeginInvoke(DispatcherPriority.Render,
        //            (Action)(() =>
        //            {
        //                if(DgMain.ItemsSource == null)
        //                {
        //                    if(hasUpd)
        //                        DgMain.ItemsSource = lst.OrderByDescending(x => x["upd"]);
        //                    else
        //                        DgMain.ItemsSource = lst;
        //                }
        //                else
        //                    foreach(var item in lst)
        //                        ((JArray)DgMain.ItemsSource).Insert(0, item);
        //                DgMain.Items.Refresh();
        //            }));

        //    //Type t = Type.GetType($"Classes.MobileComms_ITK.JSON_Types.{className}");
        //    //if(t == null)
        //    //{
        //    //    //TxtJsonSchema.Text = string.Empty;
        //    //    //TxtJsonData.Text = string.Empty;
        //    //}
        //    //else
        //    //{
        //    //    //Type genericListType = typeof(IList<>).MakeGenericType(t);

        //    //    //PropertyInfo prop;
        //    //    //if((prop = t.GetProperty("upd")) != null)
        //    //    //{
        //    //    //    if(prop.PropertyType == typeof(Timestamp))
        //    //    //    {
        //    //    //        Timestamp ts = (Timestamp)Activator.CreateInstance(typeof(Timestamp));

        //    //    //        prop.SetValue(t, ts, null);
        //    //    //    }

        //    //    //}




        //    //}
        //}
    }
}
