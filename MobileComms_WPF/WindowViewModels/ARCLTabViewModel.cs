using MobileComms_WPF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileComms_WPF.WindowViewModels
{
    public class ARCLTabViewModel : Core.ViewModelBase
    {
        public string TargetIPAddress
        {
            get { return App.Settings.GetValue("TargetIPAddress"); }
        }
        public string TargetPort
        {
            get { return App.Settings.GetValue("TargetPort", "7171"); }
            set { App.Settings.SetValue("TargetPort", value); }
        }
        public string TargetPassword
        {
            get { return App.Settings.GetValue("TargetARCLPassword", "adept"); }
            set { App.Settings.SetValue("TargetARCLPassword", value); }
        }
        public string ConnectButtonText { get => connectButtonText; set => Set(ref connectButtonText, value); }
        private string connectButtonText = "Connect";
        public bool ConnectionState { get => connectionState; set => Set(ref connectionState, value); }
        private bool connectionState;
        public string ConnectMessage { get => connectMessage; set { _ = Set(ref connectMessage, value); OnPropertyChanged("IsMessage"); } }
        private string connectMessage;

        public ObservableCollection<ARCLCommand> ARCLCommands { get; } = new ObservableCollection<ARCLCommand>();

        public ARCLTabViewModel()
        {
            ConnectCommand = new RelayCommand(ConnectAction, c => true);

            LoadARCLComandList();
        }

        public void UpdateTargetIPAddress() => OnPropertyChanged("TargetIPAddress");

        public ICommand ConnectCommand { get; }
        private void ConnectAction(object parameter)
        {
            //if (Socket.IsConnected)
            //{
            //    Socket.Close();
            //}
            //else
            //{
            //    ConnectMessage = string.Empty;

            //    Task.Run(() =>
            //    {
            //        ConnectButtonText = "Trying";

            //        if (!Socket.Connect(App.Settings.RobotIP, 5891))
            //            ConnectMessage = "Unable to connect!";
            //    });
            //}
        }

        public enum ARCLCommandState
        {
            OK,
            Obsolete,

        }
        public class ARCLCommand
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public ARCLCommandState State { get; set; }
        }
        private void LoadARCLComandList()
        {
            ARCLTypes.ARCLCommands commands = new ARCLTypes.ARCLCommands();
            foreach (var kv in commands.ARCLCommands_2016_ARCL_en)
            {
                ARCLCommand tvi = new ARCLCommand();
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");

                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if (string.IsNullOrEmpty(item.Key))
                {
                    tvi.Name = kv.Value;
                    tvi.State = ARCLCommandState.Obsolete;
                }
                else
                    tvi.Name = kv.Value;

                ARCLCommands.Add(tvi);
            }

            foreach (var kv in commands.ARCLCommands_I617_E_01)
            {
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_2016_ARCL_en.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if (string.IsNullOrEmpty(item.Key))
                {
                    ARCLCommand tvi = new ARCLCommand
                    {
                        Name = kv.Value,
                    };
                    ARCLCommands.Add(tvi);
                }
            }

            foreach (var kv in commands.ARCLCommands_Help_4_10_1)
            {
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if (string.IsNullOrEmpty(item.Key))
                {
                    ARCLCommand tvi = new ARCLCommand
                    {
                        State = ARCLCommandState.Obsolete,
                    Name = kv.Key,
                     };
                    ARCLCommands.Add(tvi);
                }
            }
        }

    }
}
