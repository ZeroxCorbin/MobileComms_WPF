using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using ApplicationSettingsNS;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for ARCLWindow.xaml
    /// </summary>
    public partial class ARCLWindow : Window
    {


        ARCL.ARCLConnection Connection { get; } = new ARCL.ARCLConnection();
        public ARCLWindow()
        {
            InitializeComponent();


            ARCLTypes.ARCLCommands commands = new ARCLTypes.ARCLCommands();
            foreach(var kv in commands.ARCLCommands_2016_ARCL_en)
            {
                TreeViewItem tvi = new TreeViewItem();
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");

                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    tvi.Header = "--" + kv.Key;
                }
                else
                {
                    tvi.Header = kv.Key;
                }
                //tvi.Items.Add(kv.Value);
                TvARCLCommands1.Items.Add(tvi);
            }

            foreach(var kv in commands.ARCLCommands_I617_E_01)
            {
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = kv.Key;

                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_02.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    tvi.Header = "--" + kv.Key;
                }
                else
                {
                    tvi.Header = kv.Key;
                }

                item = commands.ARCLCommands_2016_ARCL_en.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    tvi.Background = Brushes.LightGreen;
                }

                //tvi.Items.Add(kv.Value);
                TvARCLCommands2.Items.Add(tvi);

            }

            foreach(var kv in commands.ARCLCommands_I617_E_02)
            {
                TreeViewItem tvi = new TreeViewItem();
                tvi.Header = kv.Key;

                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    tvi.Background = Brushes.LightGreen;
                }
                //tvi.Items.Add(kv.Value);
                TvARCLCommands3.Items.Add(tvi);

            }
            foreach(var kv in commands.ARCLCommands_Help_4_10_1)
            {


                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = kv.Key;
                    tvi.Background = Brushes.LightYellow;
                    TvARCLCommands2.Items.Add(tvi);
                }

                //tvi.Items.Add(kv.Value);


            }
            foreach(var kv in commands.ARCLCommands_Help_5_4_1)
            {
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_02.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    TreeViewItem tvi = new TreeViewItem();
                    tvi.Header = kv.Key;
                    tvi.Background = Brushes.LightYellow;
                    TvARCLCommands3.Items.Add(tvi);
                }

            }
            Connection.ConnectState += Connection_ConnectState;
            Connection.DataReceived += Connection_DataReceived;
        }


        private void Connection_ConnectState(object sender, bool state)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        if(state)
                        {
                            Connection.StartReceiveAsync();
                            BtnConnect.Background = Brushes.LightGreen;
                        }
                        else
                            BtnConnect.Background = Brushes.Salmon;
                    }));
        }

        private RoutedPropertyChangedEventArgs<object> SelectedItem { get; set; }
        private void TvARCLCommands_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) => SelectedItem = e;

        private void TvARCLCommands_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(SelectedItem != null)
                if(SelectedItem.NewValue is TreeViewItem tv)
                    TxtCommand2Send.Text = (string)tv.Header;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(Connection.IsConnected)
            {
                Connection.Close();
            }
            else
            {
                if(ARCL.ARCLConnection.ValidateConnectionString(TxtConnectionString.Text))
                {
                    TxtConnectionString.Background = Brushes.WhiteSmoke;
                    Connection.ConnectionString = TxtConnectionString.Text;
                    Connection.Connect();
                }
                else
                {
                    TxtConnectionString.Background = Brushes.Salmon;
                }
            }

        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            Connection.Write(TxtCommand2Send.Text);
        }


        private int CommandInCheckResult { get; set; } = 1;

        class CommandData
        {
            public bool Result { get; set; } = false;
            public string Command { get; set; } = string.Empty;

            public CommandData(string cmd, bool res)
            {
                Command = cmd;
                Result = res;

            }
        }
        private CommandData CommandInCheck { get; set; }
        private List<CommandData> Commands2Check { get; set; }
        private bool IsLoading { get; set; } = true;

        private void BtnTest1_Click(object sender, RoutedEventArgs e)
        {
            Commands2Check = new List<CommandData>();

            foreach(TreeViewItem tvi in TvARCLCommands1.Items)
                Commands2Check.Add(new CommandData(Regex.Match(((string)tvi.Header).Trim('-'), @"^[a-zA-Z]*").Value, false));

            ThreadPool.QueueUserWorkItem(CheckCommandsThread_DoWork);
        }

        private void CheckCommandsThread_DoWork(object sender)
        {
            foreach(var command in Commands2Check)
            {
                CommandInCheck = command;
                Connection.Write(command.Command);
                CommandInCheckResult = -1;

                int count = 0;
                while(CommandInCheckResult == -1)
                {
                    Thread.Sleep(1000);
                    count++;
                    if(count > 4)
                        break;
                }
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        DisplayResults();
                    }));
        }

        private void DisplayResults()
        {
            foreach(TreeViewItem tvi in TvARCLCommands1.Items)
            {
                Match key = Regex.Match(((string)tvi.Header).Trim('-'), @"^[a-zA-Z]*");
                var item = Commands2Check.FirstOrDefault(x => x.Command.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Command))
                {
                    if(item.Result)
                        tvi.Background = Brushes.LightGreen;
                    else
                        tvi.Background = Brushes.LightPink;
                }
            }

        }
        private void Connection_DataReceived(object sender, string data)
        {
            string[] spl = data.Split('\n');
            foreach(string s in spl)
            {

                if(CommandInCheckResult == -1)
                {
                    if(s.StartsWith($"End of {CommandInCheck.Command}", StringComparison.InvariantCultureIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = true;
                    }
                    if(s.StartsWith($"End{CommandInCheck.Command}", StringComparison.InvariantCultureIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = true;
                    }
                    if(s.StartsWith($"End of", StringComparison.InvariantCultureIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = true;
                    }

                    if(s.StartsWith($"Setup", StringComparison.InvariantCultureIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = true;
                    }
                    if(s.StartsWith("CommandError:", StringComparison.OrdinalIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = true;
                    }
                    if(s.StartsWith($"Unknown command", StringComparison.InvariantCultureIgnoreCase))
                    {
                        CommandInCheckResult = 1;
                        CommandInCheck.Result = false;
                    }
                }
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        TxtResponse.Text += data;
                    }));
        }

        //Window Changes
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(IsLoading) return;

            App.Settings.ARCLWindow.Top = Top;
            App.Settings.ARCLWindow.Left = Left;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(IsLoading) return;
            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.ARCLWindow.WindowState = this.WindowState;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if(Keyboard.IsKeyDown(Key.LeftShift))
                App.Settings.ARCLWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();

            if(double.IsNaN(App.Settings.ARCLWindow.Left))
            {
                App.Settings.ARCLWindow.Left = Owner.Left;
                App.Settings.ARCLWindow.Top = Owner.Top + Owner.Height;
                App.Settings.ARCLWindow.Height = 768;
                App.Settings.ARCLWindow.Width = 1024;
            }

            this.Left = App.Settings.ARCLWindow.Left;
            this.Top = App.Settings.ARCLWindow.Top;
            this.Height = App.Settings.ARCLWindow.Height;
            this.Width = App.Settings.ARCLWindow.Width;

            if(!CheckOnScreen.IsOnScreen(this))
            {
                App.Settings.ARCLWindow.Left = Owner.Left;
                App.Settings.ARCLWindow.Top = Owner.Top + Owner.Height;
                App.Settings.ARCLWindow.Height = 768;
                App.Settings.ARCLWindow.Width = 1024;

                this.Left = App.Settings.ARCLWindow.Left;
                this.Top = App.Settings.ARCLWindow.Top;
                this.Height = App.Settings.ARCLWindow.Height;
                this.Width = App.Settings.ARCLWindow.Width;
            }

            IsLoading = false;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(IsLoading) return;

            App.Settings.ARCLWindow.Height = Height;
            App.Settings.ARCLWindow.Width = Width;
        }
    }
}
