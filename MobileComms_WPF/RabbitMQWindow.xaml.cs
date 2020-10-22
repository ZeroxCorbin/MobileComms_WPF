using ApplicationSettingsNS;
using Classes.IntegrationToolkit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RabbitMQ.Client;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabitMQWindow.xaml
    /// </summary>
    public partial class RabbitMQWindow : Window
    {

        public RabbitMQWindow(Window owner)
        {
            Owner = owner;

            InitializeComponent();
            Window_LoadSettings();
            LoadQueueList();
        }
        private void Window_LoadSettings()
        {
            if(Keyboard.IsKeyDown(Key.LeftShift))
                App.Settings.RabbitMQWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();

            if(double.IsNaN(App.Settings.RabbitMQWindow.Left))
            {
                App.Settings.RabbitMQWindow.Left = Owner.Left;
                App.Settings.RabbitMQWindow.Top = Owner.Top + Owner.Height;
                App.Settings.RabbitMQWindow.Height = 768;
                App.Settings.RabbitMQWindow.Width = 1024;
            }

            this.Left = App.Settings.RabbitMQWindow.Left;
            this.Top = App.Settings.RabbitMQWindow.Top;
            this.Height = App.Settings.RabbitMQWindow.Height;
            this.Width = App.Settings.RabbitMQWindow.Width;

            if(!CheckOnScreen.IsOnScreen(this))
            {
                App.Settings.RabbitMQWindow.Left = Owner.Left;
                App.Settings.RabbitMQWindow.Top = Owner.Top + Owner.Height;
                App.Settings.RabbitMQWindow.Height = 768;
                App.Settings.RabbitMQWindow.Width = 1024;

                this.Left = App.Settings.RabbitMQWindow.Left;
                this.Top = App.Settings.RabbitMQWindow.Top;
                this.Height = App.Settings.RabbitMQWindow.Height;
                this.Width = App.Settings.RabbitMQWindow.Width;
            }

            TxtHost.Text = App.Settings.RabbitMQHost;
            TxtPassword.Password = App.Settings.RabbitMQPassword;
        }
        private void LoadQueueList()
        {
            TreeViewItem tvi = new TreeViewItem { Header = "inbound." };
            foreach(var kv in Classes.IntegrationToolkit.RabbitMQ.InboundQueues)
            {
                TreeViewItem tvi1 = new TreeViewItem { Header = kv.Replace("inbound.", "") };
                tvi1.Selected += Tvi_Selected;
                tvi.Items.Add(tvi1);
            }
            TrvQueueList.Items.Add(tvi);

            TreeViewItem tvi2 = new TreeViewItem { Header = "outbound." };
            foreach(var kv in Classes.IntegrationToolkit.RabbitMQ.OutboundQueues)
            {
                TreeViewItem tvi3 = new TreeViewItem { Header = kv.Replace("outbound.", "") };
                tvi3.Selected += Tvi_Selected;
                tvi2.Items.Add(tvi3);
            }
            TrvQueueList.Items.Add(tvi2);
        }

        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {
            string last = (string)((TreeViewItem)sender).Header;
            string first = (string)((TreeViewItem)((TreeViewItem)sender).Parent).Header;

            TxtQueueName.Text = first + last;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

            ConnectionFactory factory = new ConnectionFactory { Uri = new Uri($"amqps://toolkitadmin:{TxtPassword.Password}@{TxtHost.Text}/") };
            factory.Ssl.AcceptablePolicyErrors = System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch | System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors;

            using IConnection conn = factory.CreateConnection();
           
                if(!conn.IsOpen)
                return;

            using var channel = conn.CreateModel();

            BasicGetResult res = channel.BasicGet(TxtQueueName.Text, false);
            if(res == null)
                TxtResponse.Text = "Nothing in Queue.";
            else
                TxtResponse.Text = Encoding.UTF8.GetString(res.Body.ToArray(), 0, res.Body.Length); 
            
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.RabbitMQWindow.Top = Top;
            App.Settings.RabbitMQWindow.Left = Left;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.RabbitMQWindow.WindowState = this.WindowState;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.RabbitMQWindow.Height = Height;
            App.Settings.RabbitMQWindow.Width = Width;
        }
    }
}
