using ApplicationSettingsNS;
using Classes.IntegrationToolkit;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for REST.xaml
    /// </summary>
    public partial class RESTWindow : Window
    {
        REST.REST_ACTIONS RestAction { get; set; } = REST.REST_ACTIONS.GET;

        private Stream Stream { get; set; }

       
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
            int numberOfBytesRead = Stream.EndRead(ar);

            if(numberOfBytesRead == 0)
                return;

            Dispatcher.Invoke(DispatcherPriority.Render,
                (Action)(() =>
                {
                    TxtResponse.Text = Encoding.ASCII.GetString((byte[])ar.AsyncState, 0, numberOfBytesRead);
                }));

            BeginReading();
        }




        //private bool IsLoading { get; set; } = true;
        public RESTWindow(Window owner)
        {
            Owner = owner;
            InitializeComponent();

            Window_LoadSettings();

            LoadResourceList();
        }
        private void Window_LoadSettings()
        {
            if(Keyboard.IsKeyDown(Key.LeftShift))
                App.Settings.RESTWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();

            if(double.IsNaN(App.Settings.RESTWindow.Left))
            {
                App.Settings.RESTWindow.Left = Owner.Left;
                App.Settings.RESTWindow.Top = Owner.Top + Owner.Height;
                App.Settings.RESTWindow.Height = 768;
                App.Settings.RESTWindow.Width = 1024;
            }

            this.Left = App.Settings.RESTWindow.Left;
            this.Top = App.Settings.RESTWindow.Top;
            this.Height = App.Settings.RESTWindow.Height;
            this.Width = App.Settings.RESTWindow.Width;

            if(!CheckOnScreen.IsOnScreen(this))
            {
                App.Settings.RESTWindow.Left = Owner.Left;
                App.Settings.RESTWindow.Top = Owner.Top + Owner.Height;
                App.Settings.RESTWindow.Height = 768;
                App.Settings.RESTWindow.Width = 1024;

                this.Left = App.Settings.RESTWindow.Left;
                this.Top = App.Settings.RESTWindow.Top;
                this.Height = App.Settings.RESTWindow.Height;
                this.Width = App.Settings.RESTWindow.Width;
            }
        }
        private void LoadResourceList()
        {
            foreach(var kv in REST.Commands)
            {
                TreeViewItem tvi = new TreeViewItem { Header = kv.Key };
                tvi.Selected += Tvi_Selected;
                foreach(string s in kv.Value)
                {
                    TreeViewItem tvic = new TreeViewItem() { Header = s };
                    tvic.Selected += Tvic_Selected; ;
                    tvi.Items.Add(tvic);
                }
                TrvCommandList.Items.Add(tvi);
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            switch(RestAction)
            {
                case REST.REST_ACTIONS.GET:
                    if(TxtResourceName.Text.Contains("/Stream"))
                    {
                        Stream = REST.RESTStream($"https://{TxtConnectionString.Text}:8443{TxtResourceName.Text}", "toolkitadmin", TxtPassword.Password);//, TxtUserName.Text, TxtPassword.Password);
                        if(Stream.CanRead)
                            BeginReading();
                    }
                    else
                        TxtResponse.Text = REST.RESTGet($"https://{TxtConnectionString.Text}:8443{TxtResourceName.Text}", "toolkitadmin", TxtPassword.Password);
                    break;

                case REST.REST_ACTIONS.PUT:
                    TxtResponse.Text = REST.RESTPut($"https://{TxtConnectionString.Text}:8443{TxtResourceName.Text}", "toolkitadmin", TxtPassword.Password, TxtJsonData.Text);
                    break;

                case REST.REST_ACTIONS.POST:
                    TxtResponse.Text = REST.RESTPost($"https://{TxtConnectionString.Text}:8443{TxtResourceName.Text}", "toolkitadmin", TxtPassword.Password, TxtJsonData.Text);
                    break;

                case REST.REST_ACTIONS.DELETE:
                    TxtResponse.Text = REST.RESTDelete($"https://{TxtConnectionString.Text}:8443{TxtResourceName.Text}", "toolkitadmin", TxtPassword.Password);
                    break;
            }

        }



        private void Tvic_Selected(object sender, RoutedEventArgs e)
        {
            TxtResourceName.Text = (string)((TreeViewItem)sender).Header;

            string name = (string)((TreeViewItem)((TreeViewItem)sender).Parent).Header;

            Type t = Type.GetType($"Classes.IntegrationToolkit.JSON_Types.{name}");

            if(t == null)
            {
                TxtJsonSchema.Text = string.Empty;
                TxtJsonData.Text = string.Empty;
                return;
            }

            var obj = Activator.CreateInstance(t);

            if(name.Contains("GET"))
            {
                RestAction = REST.REST_ACTIONS.GET;
                BtnSend.Content = Enum.GetName(typeof(REST.REST_ACTIONS), RestAction );
                TxtJsonSchema.Text = JsonConvert.SerializeObject(obj).Replace(",\"", ",\r\n\"").Replace("null", "\"\"");
                TxtJsonData.Text = string.Empty;
            }
            else if(name.Contains("PUT"))
            {
                RestAction = REST.REST_ACTIONS.PUT;
                BtnSend.Content = Enum.GetName(typeof(REST.REST_ACTIONS), RestAction);
                TxtJsonData.Text = JsonConvert.SerializeObject(obj).Replace(",\"", ",\r\n\"").Replace("null", "\"\"");
                TxtJsonSchema.Text = TxtJsonData.Text;
            }
            else if(name.Contains("POST"))
            {
                RestAction = REST.REST_ACTIONS.POST;
                BtnSend.Content = Enum.GetName(typeof(REST.REST_ACTIONS), RestAction);
                TxtJsonData.Text = JsonConvert.SerializeObject(obj).Replace(",\"", ",\r\n\"").Replace("null", "\"\"");
                TxtJsonSchema.Text = TxtJsonData.Text;
            }
            else if(name.Contains("DELETE"))
            {
                RestAction = REST.REST_ACTIONS.DELETE;
                BtnSend.Content = Enum.GetName(typeof(REST.REST_ACTIONS), RestAction);
                TxtJsonSchema.Text = string.Empty;
                TxtJsonData.Text = string.Empty;
            }

            if(TxtResourceName.Text.Contains("/Stream"))
                BtnSend.Content = "GET: Stream";
        }

        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {


            //foreach(var prop in t.GetProperties())
            //{
            //    TxtJson.Text += prop.Name + "\r\n";
            //}
        }

        //Window Changes
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.RESTWindow.Top = Top;
            App.Settings.RESTWindow.Left = Left;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.RESTWindow.WindowState = this.WindowState;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.RESTWindow.Height = Height;
            App.Settings.RESTWindow.Width = Width;
        }
    }
}
