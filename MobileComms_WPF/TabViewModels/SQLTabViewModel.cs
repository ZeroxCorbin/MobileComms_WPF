using MobileComms_WPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileComms_WPF.TabViewModels
{
    public  class SQLTabViewModel : Core.ViewModelBase
    {
        public string TargetIPAddress
        {
            get { return App.Settings.GetValue("TargetIPAddress"); }
        }
        public string ITKPassword
        {
            get { return App.Settings.GetValue("ITKPassword", "adept"); }
            set { App.Settings.SetValue("ITKPassword", value); }
        }
        public string ConnectButtonText { get => connectButtonText; set => Set(ref connectButtonText, value); }
        private string connectButtonText = "Connect";
        public bool ConnectionState { get => connectionState; set => Set(ref connectionState, value); }
        private bool connectionState;
        public string ConnectMessage { get => connectMessage; set { _ = Set(ref connectMessage, value); OnPropertyChanged("IsMessage"); } }
        private string connectMessage;

        public ICommand ConnectCommand { get; }
        private void ConnectAction(object parameter)
        {

        }
        public SQLTabViewModel()
        {
            ConnectCommand = new RelayCommand(ConnectAction, c => true);
        }

        public void UpdateConnectionInfo()
        {
            OnPropertyChanged("TargetIPAddress");
            OnPropertyChanged("ITKPassword");
        }

    }
}
