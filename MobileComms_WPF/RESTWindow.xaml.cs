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
using System.Windows.Media.Imaging;
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
        private JSONEditor_StackPanelParent StkJsonData = new JSONEditor_StackPanelParent() { Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0AFFFF00")), Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
        public RESTWindow(Window owner)
        {
            this.DataContext = App.Settings;
            Owner = owner;
            InitializeComponent();

            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("MobileComms_WPF.Support.copy.png");
            PngBitmapDecoder decoder = new PngBitmapDecoder(myStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            ImageBrush imgbrush = new ImageBrush(decoder.Frames[0]);

            BtnCopyResource.Background = imgbrush;
            BtnCopyJSON.Background = imgbrush;

            myStream = myAssembly.GetManifestResourceStream("MobileComms_WPF.Support.browser.png");
            decoder = new PngBitmapDecoder(myStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            imgbrush = new ImageBrush(decoder.Frames[0]);

            BtnBrowseResource.Background = imgbrush;

            myStream = myAssembly.GetManifestResourceStream("MobileComms_WPF.Support.swagger.png");
            decoder = new PngBitmapDecoder(myStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            imgbrush = new ImageBrush(decoder.Frames[0]);

            BtnOpenSwagger.Background = imgbrush;

            myStream = myAssembly.GetManifestResourceStream("MobileComms_WPF.Support.paste.png");
            decoder = new PngBitmapDecoder(myStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            imgbrush = new ImageBrush(decoder.Frames[0]);

            BtnPasteJSON.Background = imgbrush;

            ScvJSONData.Content = StkJsonData;

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
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            TxtErrorResponse.Text = string.Empty;
            TxtErrorResponse.Visibility = Visibility.Collapsed;

            TxtReturnedJSON.Text = string.Empty;

            switch(RestAction)
            {
                case REST.Actions.GET:

                    string resp = await REST.Get(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text), TxtPassword.Password);

                    if(REST.IsException)
                    {
                        TxtErrorResponse.Text = REST.RESTException.Message;
                        TxtErrorResponse.Visibility = Visibility.Visible;
                    }

                    else
                    {
                        if(resp.StartsWith("{\"code") || resp.StartsWith("<html>"))
                        {
                            TxtErrorResponse.Text = resp;
                            TxtErrorResponse.Visibility = Visibility.Visible;
                            return;
                        }
                        TxtReturnedJSON.Text = resp;
                        DeserializeJSONtoDataGrid((REST.Command)((TreeViewItem)TrvCommandList.SelectedItem).Tag, resp);
                    }
                    break;

                case REST.Actions.PUT:
                    TxtErrorResponse.Text = await REST.Put(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text), TxtPassword.Password, StkJsonData.Text);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    break;

                case REST.Actions.POST:
                    TxtErrorResponse.Text = await REST.Post(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text), TxtPassword.Password, StkJsonData.Text);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    break;

                case REST.Actions.DELETE:
                    TxtErrorResponse.Text = await REST.Delete(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text), TxtPassword.Password);
                    TxtErrorResponse.Visibility = Visibility.Visible;
                    break;
                case REST.Actions.STREAM:
                    if(Stream == null)
                    {
                        Stream = await REST.Stream(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text), TxtPassword.Password);
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

        private void DeserializeJSONtoDataGrid(REST.Command cmd, string json)
        {

            DataTable datatable = new DataTable();
            foreach(PropertyInfo prop in cmd.JSONType.GetProperties())
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
                foreach(PropertyInfo prop in cmd.JSONType.GetProperties())
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

        private class Resource_StackPanel : StackPanel
        {
            public delegate void StatusStatusChangedDel(bool state);
            public event StatusStatusChangedDel StatusChanged;

            private bool _Status = false;
            public bool Status
            {
                get
                {
                    return _Status;
                }
                set
                {
                    _Status = value;
                    StatusChanged?.Invoke(value);
                }
            }
            private TextBlock Start = new TextBlock() { Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            private TextBox Value1 = new TextBox() { Margin = new Thickness(1, 0, 5, 0), MinWidth = 40, VerticalContentAlignment = VerticalAlignment.Center, Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0AFFFF00")), BorderBrush = null };

            private bool HasValues2;

            private TextBlock Mid = new TextBlock() { Margin = new Thickness(0, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            private TextBox Value2 = new TextBox() { Margin = new Thickness(3, 0, 5, 0), MinWidth = 40, VerticalContentAlignment = VerticalAlignment.Center, Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0AFFFF00")), BorderBrush = null };

            public Resource_StackPanel(string resource, StatusStatusChangedDel statusChanged)
            {
                StatusChanged += statusChanged;

                Orientation = Orientation.Horizontal;

                string[] spl = resource.Split('{');

                Start.Text = spl[0];
                Children.Add(Start);

                if(spl.Length == 3)
                {
                    HasValues2 = true;

                    string[] spl1;
                    if(spl[1].Contains("&"))
                    {
                        spl1 = spl[1].Split('&');
                        Mid.Text = "&" + spl1[1];
                    }
                    else
                    {
                        spl1 = spl[1].Split(':');
                        Mid.Text = ":";
                    }



                    Value1.Tag = spl1[0].TrimEnd('}');
                    Children.Add(Value1);
                    Value1.TextChanged += Value1_TextChanged;
                    Value1.GotFocus += Value1_GotFocus;
                    Value1.LostFocus += Value1_LostFocus;
                    Value1.Text = (string)Value1.Tag;

                        Children.Add(Mid);

                    Value2.Tag = spl[2].TrimEnd('}');
                    Children.Add(Value2);
                    Value2.TextChanged += Value2_TextChanged;
                    Value2.LostFocus += Value2_LostFocus;
                    Value2.GotFocus += Value2_GotFocus;
                    Value2.Text = (string)Value2.Tag;
                }
                else if(spl.Length == 2)
                {


                    string [] spl1 = spl[1].Split(':');

                    if(spl1.Length == 2)
                    {
                        HasValues2 = true;

                        Value1.Tag = spl[1].Replace("}:*", "");
                        Children.Add(Value1);
                        Value1.TextChanged += Value1_TextChanged;
                        Value1.GotFocus += Value1_GotFocus;
                        Value1.LostFocus += Value1_LostFocus;
                        Value1.Text = (string)Value1.Tag;

                        Mid.Text = ":";
                        Children.Add(Mid);

                        Value2.Text = "*";
                        Value2.IsEnabled = false;
                        Value2.Background = null;
                        Value2.MinWidth = 0;

                        Children.Add(Value2);
                    }
                    else
                    {
                        HasValues2 = false;

                        Value1.Tag = spl[1].TrimEnd('}');
                        Children.Add(Value1);
                        Value1.TextChanged += Value1_TextChanged;
                        Value1.GotFocus += Value1_GotFocus;
                        Value1.LostFocus += Value1_LostFocus;
                        Value1.Text = (string)Value1.Tag;
                    }
                }
                else
                {
                    Value1.Text = string.Empty;
                    Start.Margin = Margin = new Thickness(3, 0, 5, 0);
                }
            }

            private void Value1_GotFocus(object sender, RoutedEventArgs e)
            {
                if(Value1.Text.Equals((string)Value1.Tag))
                    Value1.Text = string.Empty;
            }
            private void Value1_TextChanged(object sender, TextChangedEventArgs e)
            {
                if(Value1.Text.Equals(Value1.Tag))
                    Status = false;
                else if(Value1.Text.Length == 0)
                    Status = false;
                else if(HasValues2)
                {
                    if(Value2.Text.Length == 0)
                        Status = false;
                    else
                         Status = true;
                }
                else
                    Status = true;
            }
            private void Value1_LostFocus(object sender, RoutedEventArgs e)
            {
                if(Value1.Text.Length == 0)
                    Value1.Text = (string)Value1.Tag;
            }

            private void Value2_GotFocus(object sender, RoutedEventArgs e)
            {
                if(Value2.Text.Equals((string)Value2.Tag))
                    Value2.Text = string.Empty;
            }
            private void Value2_TextChanged(object sender, TextChangedEventArgs e)
            {
                if(Value2.Text.Equals(Value2.Tag))
                    Status = false;
                else if(Value2.Text.Length == 0)
                    Status = false;
                else if(HasValues2)
                {
                    if(Value1.Text.Equals(Value1.Tag))
                        Status = false;
                    else if(Value1.Text.Length == 0)
                        Status = false;
                    else
                        Status = true;
                }
            }
            private void Value2_LostFocus(object sender, RoutedEventArgs e)
            {
                if(Value2.Text.Length == 0)
                    Value2.Text = (string)Value2.Tag;
            }
            public string Text => !HasValues2 ? $"{Start.Text}{Value1.Text}" : $"{Start.Text}{Value1.Text}{Mid.Text}{Value2.Text}";
        }

        private class JSONEditor_StackPanelParent : StackPanel
        {

            public string Text
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var item in Children)
                    {
                        if(item is TextBlock tb)
                            sb.Append(tb.Text);
                        else
                        sb.Append($"{((JSONEditor_StackPanel)item).Text.TrimStart()}");
                    }
                    return sb.ToString();
                }
            }

        }
        private class JSONEditor_StackPanel : StackPanel
        {
            public delegate void TextChangedEvent(string data);
            public event TextChangedEvent TextChanged;

            private TextBlock Start = new TextBlock();
            private TextBlock End = new TextBlock();

            private bool _isString;
            private TextBox MidText = new TextBox() { MinWidth = 20, VerticalContentAlignment = VerticalAlignment.Center, Background = null, BorderBrush = null };

            private bool _isEnum;
            private ComboBox Combo = new ComboBox() { BorderBrush = null, MinWidth = 40, Style = Application.Current.TryFindResource(ToolBar.ComboBoxStyleKey) as Style };

            private bool _isBool;
            private ComboBox Check = new ComboBox() { BorderBrush = null, MinWidth = 40, Style = Application.Current.TryFindResource(ToolBar.ComboBoxStyleKey) as Style };

            private bool _isClass;

            public JSONEditor_StackPanel(PropertyInfo property, bool last)
            {
                Orientation = Orientation.Horizontal;
                _isString = property.PropertyType == typeof(string);
                _isEnum = property.PropertyType.IsEnum;
                _isBool = property.PropertyType == typeof(bool);
                _isClass = property.PropertyType.IsClass & !_isString;

                string name = Regex.Replace(property.Name, @"^[A-Z]", delegate (Match match)
                {
                    return $"{char.ToLower(match.ToString()[0])}";
                });

                if(_isString)
                {
                    Start.Text = $"  \"{name}\":\"";
                    MidText.TextChanged += Mid_TextChanged;

                    Children.Add(Start);
                    Children.Add(MidText);
                    if(!last)
                        End.Text = "\",";
                    else
                        End.Text = "\"";
                    Children.Add(End);
                }
                else if(_isEnum)
                {
                    Start.Text = $"  \"{name}\":\"";
                    Combo.ItemsSource = new List<string>(Regex.Split(property.PropertyType.GetEnumNames()[0], "__"));
                    ((List<string>)Combo.ItemsSource).Insert(0, "");

                    Combo.SelectionChanged += Combo_SelectionChanged;

                    Children.Add(Start);
                    Children.Add(Combo);
                    if(!last)
                        End.Text = "\",";
                    else
                        End.Text = "\"";
                    Children.Add(End);
                }
                else if(_isBool)
                {
                    Start.Text = $"  \"{name}\":\"";

                    Check.Items.Add("");
                    Check.Items.Add("true");
                    Check.Items.Add("false");
                    Check.SelectionChanged += Combo_SelectionChanged;

                    Children.Add(Start);
                    Children.Add(Check);
                    if(!last)
                        End.Text = "\",";
                    else
                        End.Text = "\"";
                    Children.Add(End);
                }
                else
                {
                    MidText.TextChanged += Mid_TextChanged;

                    Start.Text = $"  \"{name}\":";
                    Children.Add(Start);
                    Children.Add(MidText);
                    if(!last)
                        End.Text = ",";
                    else
                        End.Text = "";
                    Children.Add(End);
                }
            }

            public void SetFocus() =>
                Keyboard.Focus(MidText);

            private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
                TextChanged?.Invoke(Text);
            private void Date_TextInput(object sender, TextCompositionEventArgs e) =>
                TextChanged?.Invoke(Text);
            private void Mid_TextChanged(object sender, TextChangedEventArgs e) =>
                TextChanged?.Invoke(Text);

            public string Text => _isString ? $"{Start.Text}{MidText.Text}{End.Text}" : _isEnum ? $"{Start.Text}{Combo.SelectedValue}{End.Text}" : _isBool ? $"{Start.Text}{Check.SelectedValue}{End.Text}" : $"{Start.Text}{MidText.Text}{End.Text}";
        }

        Resource_StackPanel ResourcePanel;
        private void Tvic_Selected(object sender, RoutedEventArgs e)
        {

            TreeViewItem selected = (TreeViewItem)sender;
            REST.Command cmd = (REST.Command)selected.Tag;
            var obj = Activator.CreateInstance(cmd.JSONType);

            RestAction = cmd.Action;

            if(ResourcePanel != null)
                StkResource.Children.Remove(ResourcePanel);

            ResourcePanel = new Resource_StackPanel((string)selected.Header, ResourcePanel_StatusChanged);

            StkResource.Children.Add(ResourcePanel);

            BtnSend.Content = Enum.GetName(typeof(REST.Actions), RestAction);

            TxtErrorResponse.Text = string.Empty;
            TxtErrorResponse.Visibility = Visibility.Collapsed;

            if(RestAction == REST.Actions.POST || RestAction == REST.Actions.PUT)
            {
                BtnSend.IsEnabled = true;

                ScvJSONSchema.Visibility = Visibility.Collapsed;
                TxtJSONSchema.Text = string.Empty;

                BtnPasteJSON.Visibility = Visibility.Visible;

                ScvJSONData.Visibility = Visibility.Visible;
                StkJsonData.Children.Clear();

                StkJsonData.Children.Add(new TextBlock() { Text = "{", Margin = new Thickness(0) });

                int i = 1;
                int len = cmd.JSONType.GetProperties().Length;
                foreach(PropertyInfo prop in cmd.JSONType.GetProperties())
                {
                    if(prop.Name.Equals("details"))
                    {
                        StkJsonData.Children.Add(new TextBlock() { Text = "  \"details\": [", Margin = new Thickness(0) });
                        StkJsonData.Children.Add(new TextBlock() { Text = "  {", Margin = new Thickness(0) });
                        i = 1;
                        len = typeof(JobRequestDetail_POST).GetProperties().Length;
                        foreach(PropertyInfo prop1 in typeof(JobRequestDetail_POST).GetProperties())
                        {
                            JSONEditor_StackPanel stk1 = new JSONEditor_StackPanel(prop1, !(++i <= len));
                            StkJsonData.Children.Add(stk1);
                        }
                        StkJsonData.Children.Add(new TextBlock() { Text = "  },", Margin = new Thickness(0) });
                        StkJsonData.Children.Add(new TextBlock() { Text = "  {", Margin = new Thickness(0) });
                        i = 1;
                        foreach(PropertyInfo prop1 in typeof(JobRequestDetail_POST).GetProperties())
                        {
                            JSONEditor_StackPanel stk1 = new JSONEditor_StackPanel(prop1, !(++i <= len));
                            StkJsonData.Children.Add(stk1);
                        }
                        StkJsonData.Children.Add(new TextBlock() { Text = "  }]", Margin = new Thickness(0) });
                        continue;
                    }

                    JSONEditor_StackPanel stk = new JSONEditor_StackPanel(prop, !(++i <= len));
                    StkJsonData.Children.Add(stk);
                }

                StkJsonData.Children.Add(new TextBlock() { Text = "}" });

                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        ((JSONEditor_StackPanel)StkJsonData.Children[1]).SetFocus();
                    }));

            }
            else
            {
                ScvJSONData.Visibility = Visibility.Collapsed;
                StkJsonData.Children.Clear();

                BtnPasteJSON.Visibility = Visibility.Collapsed;

                ScvJSONSchema.Visibility = Visibility.Visible;
                TxtJSONSchema.Text = JsonConvert.SerializeObject(obj, cmd.JSONType, Formatting.Indented, new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Reuse }).Replace("null", "\"\"");

                if(RestAction == REST.Actions.STREAM)
                    BtnSend.IsEnabled = true;
                //if(RestAction == REST.Actions.GET)
                //{
                //    Dispatcher.BeginInvoke(DispatcherPriority.Render,
                //        (Action)(() =>
                //        {
                //            Keyboard.Focus(TxtResourceValue);
                //        }));

                //}
                //else
                //{
                //    TxtResourceValue.Visibility = Visibility.Collapsed;
                //    TxtResourceValue.Text = string.Empty;
                //}
            }
        }

        private void ResourcePanel_StatusChanged(bool state)
        {
            if(state)
                BtnSend.IsEnabled = true;
            else
                BtnSend.IsEnabled = false;
        }

        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;
            if(tvi.IsSelected)
                tvi.IsSelected = false;
        }

        private void BtnOpenSwagger_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start($"https://toolkitadmin:{TxtPassword.Password}@{App.Settings.RESTHost}:8443/swagger/");



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

        private void BtnCopyResource_Click(object sender, RoutedEventArgs e) =>
            Clipboard.SetText(REST.ConnectionString(App.Settings.RESTHost, ResourcePanel.Text));

        private void BtnBrowseResource_Click(object sender, RoutedEventArgs e) =>
            System.Diagnostics.Process.Start($"https://toolkitadmin:{TxtPassword.Password}@{App.Settings.RESTHost}:8443{ResourcePanel.Text}");

        private void BtnCopyJSON_Click(object sender, RoutedEventArgs e) =>
            Clipboard.SetText(StkJsonData.Text);
    }
}
