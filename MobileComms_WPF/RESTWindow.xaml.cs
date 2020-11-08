using ApplicationSettingsNS;
using MobileComms_ITK.JSON.Types;
using MobileComms_ITK.REST;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MobileComms_WPF
{

    /// <summary>
    /// Interaction logic for REST.xaml
    /// </summary>
    public partial class RESTWindow : Window
    {
        private REST REST { get; } = new REST();
        REST.Actions RestAction { get; set; } = REST.Actions.GET;

        private Stream Stream { get; set; } = null;
        public void BeginReading()
        {
            byte[] buffer = new byte[10000];


            Stream.BeginRead(
                buffer, 0, 10000,
                new AsyncCallback(EndReading),
                buffer);
        }
        public void EndReading(IAsyncResult ar)
        {
            if(Stream == null) return;

            int numberOfBytesRead = Stream.EndRead(ar);

            if(numberOfBytesRead == 0)
                return;

            Dispatcher.Invoke(DispatcherPriority.Render,
                (Action)(() =>
                {
                    TxtErrorResponse.Text = Encoding.ASCII.GetString((byte[])ar.AsyncState, 0, numberOfBytesRead);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    //LblErrorResponse.Visibility = Visibility.Visible;
                }));

            BeginReading();
        }

        private void StreamStarted()
        {
            TrvCommandList.IsEnabled = false;
            BtnSend.Background = Brushes.LightGreen;
        }

        private void StreamStoped()
        {
            TrvCommandList.IsEnabled = true;
            BtnSend.Background = Brushes.LightGray;
        }

        //private bool IsLoading { get; set; } = true;
        public RESTWindow(Window owner)
        {
            this.DataContext = App.Settings;
            Owner = owner;
            InitializeComponent();

            Window_LoadSettings();

            LoadResourceList();
        }

        private void Window_LoadSettings()
        {
            if(double.IsNaN(App.Settings.RESTWindow.Left)
                || !CheckOnScreen.IsOnScreen(this)
                || Keyboard.IsKeyDown(Key.LeftShift))
            {
                Left = Owner.Left;
                Top = Owner.Top + Owner.Height;
                Height = 768;
                Width = 1024;
            }

            TxtPassword.Password = App.Settings.RESTPassword;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.RESTWindow.WindowState = this.WindowState;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetJSONVisibility();

            ((TreeViewItem)TrvCommandList.Items[0]).IsExpanded = true;
            ((TreeViewItem)((TreeViewItem)TrvCommandList.Items[0]).Items[1]).IsSelected = true;
            TxtResourceValue.Text = "0";
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void LoadResourceList()
        {
            foreach(var cmd in REST.Commands)
            {
                bool found = false;
                TreeViewItem tvi = null;

                foreach(TreeViewItem item in TrvCommandList.Items)
                    if(item.Header.ToString().Equals(cmd.TypeName))
                    {
                        found = true;
                        tvi = item;
                        break;
                    }

                if(!found)
                {
                    tvi = new TreeViewItem { Header = cmd.TypeName };
                    tvi.Selected += Tvi_Selected;
                }

                foreach(string s in cmd.Resources)
                {
                    TreeViewItem tvic = new TreeViewItem() { Header = s };
                    tvic.Selected += Tvic_Selected;
                    tvic.Tag = cmd;
                    tvi.Items.Add(tvic);
                }

                if(!found)
                {
                    TrvCommandList.Items.Add(tvi);
                }

            }

            //Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            //Schema shc = new Schema();
            //foreach(JProperty prop in shc.Root["paths"])
            //{
            //    string name = Regex.Match(prop.Name, @"^\/\w+(?!=\/)").Value.TrimStart('/');
            //    if(dict.ContainsKey(name))
            //    {
            //        dict[name].Add(prop.Name);
            //    }
            //    else
            //    {
            //        dict.Add(name, new List<string>());
            //        dict[name].Add(prop.Name);
            //    }
            //}

            //using(System.IO.StreamWriter file =
            //    new System.IO.StreamWriter(@"Commands.txt"))
            //{
            //    foreach(var kv in dict)
            //    {
            //        file.WriteLine(kv.Key);
            //        foreach(string s in kv.Value)
            //        {
            //            file.WriteLine(s);
            //        }
            //    }

            //    foreach(var kv in REST.Commands)
            //    {
            //        file.WriteLine(kv.Key);
            //        foreach(string s in kv.Value)
            //        {
            //            file.WriteLine(s);
            //        }
            //    }
            //}
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            TxtErrorResponse.Text = string.Empty;
            TxtErrorResponse.Visibility = Visibility.Collapsed;
            //LblErrorResponse.Visibility = Visibility.Collapsed;

            TxtReturnedJSON.Text = string.Empty;

            switch(RestAction)
            {
                case REST.Actions.GET:

                    string resp = await REST.Get($"https://{App.Settings.RESTHost}:8443{TxtResourceName.Text}", TxtPassword.Password);

                    if(REST.IsException)
                    {
                        TxtErrorResponse.Text = REST.RESTException.Message;
                        TxtErrorResponse.Visibility = Visibility.Visible;
                        //LblErrorResponse.Visibility = Visibility.Visible;
                    }

                    else
                    {
                        if(resp.StartsWith("{\"code") || resp.StartsWith("<html>"))
                        {
                            TxtErrorResponse.Text = resp;
                            TxtErrorResponse.Visibility = Visibility.Visible;
                            //LblErrorResponse.Visibility = Visibility.Visible;
                            return;
                        }
                        //var lst = JsonConvert.DeserializeObject<IList>(resp);
                        //DgvReturnedJSON.ItemsSource = lst;
                        TxtReturnedJSON.Text = resp;

                        string name;
                        TreeViewItem selected = (TreeViewItem)TrvCommandList.SelectedItem;
                        if(selected.Parent != null)
                            name = (string)((TreeViewItem)selected.Parent).Header;
                        else
                            name = (string)selected.Header;

                        DeserializeJSONtoDataGrid(name, resp);

                    }
                    break;

                case REST.Actions.PUT:
                    TxtErrorResponse.Text = await REST.Put($"https://{App.Settings.RESTHost}:8443{TxtResourceName.Text}", TxtPassword.Password, TxtJSONSchema.Text);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    //LblErrorResponse.Visibility = Visibility.Visible;
                    break;

                case REST.Actions.POST:
                    TxtErrorResponse.Text = await REST.Post($"https://{App.Settings.RESTHost}:8443{TxtResourceName.Text}", TxtPassword.Password, TxtJSONSchema.Text);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    //LblErrorResponse.Visibility = Visibility.Visible;
                    break;

                case REST.Actions.DELETE:
                    TxtErrorResponse.Text = await REST.Delete($"https://{App.Settings.RESTHost}:8443{TxtResourceName.Text}", TxtPassword.Password);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    //LblErrorResponse.Visibility = Visibility.Visible;
                    break;
                case REST.Actions.STREAM:
                    if(Stream == null)
                    {
                        Stream = await REST.Stream(REST.ConnectionString(App.Settings.RESTHost, TxtResourceName.Text), TxtPassword.Password);
                        if(Stream.CanRead)
                        {
                            BeginReading();
                            StreamStarted();
                        }

                    }
                    else
                    {
                        Stream.Close();
                        Stream = null;
                        StreamStoped();
                    }
                    break;
            }

        }

        private void DeserializeJSONtoDataGrid(string className, string json)
        {

            Type t = Type.GetType($"MobileComms_ITK.JSON.Types.{className},MobileComms_ITK");
            if(t == null)
            {
                TxtJSONSchema.Text = string.Empty;
            }
            else
            {
                DataTable datatable = new DataTable();
                foreach(PropertyInfo prop in t.GetProperties())
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

                JArray lst;
                if(json.StartsWith("["))
                    lst = JsonConvert.DeserializeObject<JArray>(json);
                else
                    lst = JsonConvert.DeserializeObject<JArray>($"[{json}]");

                foreach(JObject elem in lst)
                {
                    DataRow dr = datatable.NewRow();
                    foreach(PropertyInfo prop in t.GetProperties())
                    {
                        if(prop.Name.Equals("AdditionalProperties")) continue;
                        if(prop.Name.Equals("Details")) continue;

                        if(prop.PropertyType == typeof(Timestamp))
                        {
                            dr[prop.Name] = DateTimeOffset.FromUnixTimeMilliseconds((long)elem.Property($"{char.ToLower(prop.Name[0])}{prop.Name.Substring(1)}").Value["millis"]).DateTime;
                        }
                        else
                        {
                            JProperty prop1;
                            if((prop1 = elem.Property($"{char.ToLower(prop.Name[0])}{prop.Name.Substring(1)}")) != null)
                                dr[prop.Name] = prop1.Value;
                        }

                    }
                    datatable.Rows.Add(dr);
                }

                DgvReturnedJSON.ItemsSource = null;
                DgvReturnedJSON.ItemsSource = datatable.DefaultView;
                if(datatable.Columns.Contains("upd"))
                    DgvReturnedJSON.Items.SortDescriptions.Add(new SortDescription("upd", ListSortDirection.Descending));
            }
        }

        private void Tvic_Selected(object sender, RoutedEventArgs e)
        {

            TreeViewItem selected = (TreeViewItem)sender;
            REST.Command cmd = (REST.Command)selected.Tag;
            var obj = Activator.CreateInstance(cmd.JSONType);

            RestAction = cmd.Action;

            string resource = (string)selected.Header;

            TxtResourceName.Text = resource;
            TxtResourceName.Tag = resource;


            //LblResourceValue.Content = Regex.Match(resource, @"{.*}").Value.Replace("{", "").Replace("}", "");
            TxtJSONSchema.Text = JsonConvert.SerializeObject(obj, cmd.JSONType, Formatting.Indented, new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Reuse }).Replace("null", "\"\"");

            BtnSend.Content = Enum.GetName(typeof(REST.Actions), RestAction);

            TxtResourceValue.Text = "";

            TxtErrorResponse.Text = string.Empty;
            TxtErrorResponse.Visibility = Visibility.Collapsed;
            //LblErrorResponse.Visibility = Visibility.Collapsed;

            TxtResourceValue.Visibility = Visibility.Collapsed;
            //LblResourceValue.Visibility = Visibility.Collapsed;

            TxtJSONSchema.Visibility = Visibility.Visible;
            //LblJSONSchema.Visibility = Visibility.Visible;
            switch(RestAction)
            {
                case REST.Actions.GET:
                    TxtJSONSchema.IsReadOnly = true;
                    TxtJSONSchema.Background = null;
                    //LblJSONSchema.Content = "JSON Schema";

                    TxtResourceValue.Visibility = Visibility.Visible;
                    //LblResourceValue.Visibility = Visibility.Visible;

                    break;
                case REST.Actions.PUT:
                    TxtJSONSchema.IsReadOnly = false;
                    TxtJSONSchema.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0AFFFF00"));
                    //LblJSONSchema.Content = "JSON Body to Send";

                    break;
                case REST.Actions.POST:
                    TxtJSONSchema.IsReadOnly = false;
                    TxtJSONSchema.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0AFFFF00"));
                    //LblJSONSchema.Content = "JSON Body to Send";

                    break;
                case REST.Actions.DELETE:
                    TxtJSONSchema.Visibility = Visibility.Collapsed;
                    //LblJSONSchema.Visibility = Visibility.Collapsed;

                    TxtResourceValue.Visibility = Visibility.Visible;
                    //LblResourceValue.Visibility = Visibility.Visible;

                    break;
                case REST.Actions.STREAM:
                    TxtJSONSchema.IsReadOnly = true;
                    TxtJSONSchema.Background = null;
                    //LblJSONSchema.Content = "JSON Schema";

                    break;
            }
        }
        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;
            if(tvi.IsSelected)
                tvi.IsSelected = false;

            //if(!tvi.IsExpanded)
            //    tvi.IsExpanded = true;
        }

        private void BtnOpenSwagger_Click(object sender, RoutedEventArgs e)
        {
            //using(WebBrowser WebBrowser1 = new WebBrowser())
            //{
            //    String auth =
            //        System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(App.Settings.RESTHost + ":" + TxtPassword.Password));
            //    string headers = "Authorization: Basic " + auth + "\r\n";
            //    WebBrowser1.Navigate($"https://{App.Settings.RESTHost}/swagger/", "_blank", null, headers);

            //}
            ////string authHdr = "Authorization: Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(App.Settings.RESTHost + ":" + TxtPassword.Password)) + "\r\n";

            ////webBrowserCtl.Navigate("http://example.com", null, null, authHdr);
            System.Diagnostics.Process.Start($"https://{App.Settings.RESTHost}/swagger/");
        }

        private void TxtResourceValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TxtResourceValue.Text.Length > 0)
            {
                TxtResourceName.Text = Regex.Replace((string)TxtResourceName.Tag, @"{.*}", delegate (Match match)
                {
                    return TxtResourceValue.Text;
                });
            }
            else
            {
                TxtResourceName.Text = (string)TxtResourceName.Tag;
            }
        }

        private void ChkJSONString_Click(object sender, RoutedEventArgs e)
        {
            SetJSONVisibility();


        }
        private void SetJSONVisibility()
        {
            if((bool)ChkJSONString.IsChecked)
            {
                TxtReturnedJSON.Visibility = Visibility.Visible;
                TxtReturnedJSON.IsEnabled = true;

                DgvReturnedJSON.Visibility = Visibility.Collapsed;
                DgvReturnedJSON.IsEnabled = false;
            }
            else
            {
                TxtReturnedJSON.Visibility = Visibility.Collapsed;
                TxtReturnedJSON.IsEnabled = false;

                DgvReturnedJSON.Visibility = Visibility.Visible;
                DgvReturnedJSON.IsEnabled = true;
            }
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!IsLoaded) return;
            App.Settings.RESTPassword = TxtPassword.Password;
        }

        private void TxtHost_TextChanged(object sender, TextChangedEventArgs e)
        {

            //if(!IsLoaded) return;
            //App.Settings.RESTHost = App.Settings.RESTHost;
        }
    }
}
