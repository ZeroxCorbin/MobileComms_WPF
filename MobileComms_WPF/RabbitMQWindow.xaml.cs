using ApplicationSettingsNS;
using MobileComms_ITK;
using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabitMQWindow.xaml
    /// </summary>
    public partial class RabbitMQWindow : Window
    {
        private RabbitMQ RabbitMQ { get; } = new RabbitMQ();
        public RabbitMQWindow(Window owner)
        {
            DataContext = App.Settings;
            Owner = owner;

            InitializeComponent();

            Window_LoadSettings();

            LoadQueueList();
        }

        private void Window_LoadSettings()
        {
            if(double.IsNaN(App.Settings.RabbitMQWindow.Left)
                || !CheckOnScreen.IsOnScreen(this)
                || Keyboard.IsKeyDown(Key.LeftShift))
            {
                Left = Owner.Left;
                Top = Owner.Top + Owner.Height;
                Height = 768;
                Width = 1024;
            }

            TxtPassword.Password = App.Settings.RabbitMQPassword;
        }

        //Window Changes
        private double TopLast;
        private double TopLeft;
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            TopLast = App.Settings.RabbitMQWindow.Top;
            TopLeft = App.Settings.RabbitMQWindow.Left;

            App.Settings.RabbitMQWindow.Top = Top;
            App.Settings.RabbitMQWindow.Left = Left;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(!IsLoaded) return;
            if(WindowState != WindowState.Normal) return;

            App.Settings.RabbitMQWindow.Height = Height;
            App.Settings.RabbitMQWindow.Width = Width;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState != WindowState.Normal)
            {
                App.Settings.RabbitMQWindow.Top = TopLast;
                App.Settings.RabbitMQWindow.Left = TopLeft;
            }
            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.RabbitMQWindow.WindowState = this.WindowState;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void LoadQueueList()
        {
            foreach(var kv in RabbitMQ.InboundQueues)
            {
                ListViewItem lvi = new ListViewItem { Content = kv.Key.Replace("inbound.", "") };
                lvi.Selected += Lvi_InboundSelected;
                LstInboundQueueList.Items.Add(lvi);
            }

            foreach(var kv in RabbitMQ.OutboundQueues)
            {
                ListViewItem lvi = new ListViewItem { Content = kv.Key.Replace("outbound.", "") };
                lvi.Selected += Lvi_OutboundSelected;
                LstOutboundQueueList.Items.Add(lvi);
            }
        }

        private void Lvi_InboundSelected(object sender, RoutedEventArgs e)
        {
            TxtInboundQueueName.Text = $"inbound.{(string)((ListViewItem)sender).Content}";

            Type t = Type.GetType($"MobileComms_ITK.JSON_Types.{RabbitMQ.InboundQueues[TxtInboundQueueName.Text]},MobileComms_ITK");
            if(t == null)
            {
                TxtJsonSchema.Text = string.Empty;
            }
            else
            {
                var obj = Activator.CreateInstance(t);
                TxtJsonSchema.Text = JsonConvert.SerializeObject(obj).Replace(",\"", ",\r\n\"").Replace("null", "\"\"");
            }
        }
        private void Lvi_OutboundSelected(object sender, RoutedEventArgs e)
        {

        }


        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

           RabbitMQ.Put(TxtInboundQueueName.Text, TxtJsonSchema.Text);
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!IsLoaded) return;
            App.Settings.RabbitMQPassword = TxtPassword.Password;
        }

        private void TxtHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            TxtDBConnectionString.Text = RabbitMQ.ConnectionString(TxtHost.Text, "Your_ITK_Password");
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(RabbitMQ.Connect(TxtHost.Text, TxtPassword.Password))
            {
                BtnConnect.Background = Brushes.LightGreen;
            }
            else
            {
                if(RabbitMQ.IsException)
                {
                    TxtResponse.Text = RabbitMQ.RabbitMQException.Message;
                }
                BtnConnect.Background = Brushes.LightSalmon;
            }
        }

        private void BtnMonitorQueue_Click(object sender, RoutedEventArgs e)
        {
            WplMain.Children.Add(new RabbitMQQueueView(RabbitMQ, $"outbound.{((ListViewItem)LstOutboundQueueList.SelectedItem).Content}"));
        }
    }
}
