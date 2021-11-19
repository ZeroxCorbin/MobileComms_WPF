using MahApps.Metro.Controls.Dialogs;
using MobileComms_WPF.Core;
using MobileComms_WPF.TabViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MobileComms_WPF.WindowViewModels
{
    public class MainWindowViewModel : Core.ViewModelBase
    {
        public string Title => "Mobile Debug (Beta)";
        public double Left { get => App.Settings.GetValue("MainWindowView.Left", 0.0); set { if (WindowState == WindowState.Normal) { App.Settings.SetValue("MainWindowView.Left", value); OnPropertyChanged(); } } }
        public double Top { get => App.Settings.GetValue("MainWindowView.Top", 0.0); set { if (WindowState == WindowState.Normal) { App.Settings.SetValue("MainWindowView.Top", value); OnPropertyChanged(); } } }
        public double Width { get => App.Settings.GetValue("MainWindowView.Width", 1024.0); set { if (WindowState == WindowState.Normal) { App.Settings.SetValue("MainWindowView.Width", value); OnPropertyChanged(); } } }
        public double Height { get => App.Settings.GetValue("MainWindowView.Height", 768.0); set { if (WindowState == WindowState.Normal) { App.Settings.SetValue("MainWindowView.Height", value); OnPropertyChanged(); } } }
        public WindowState WindowState
        {
            get => App.Settings.GetValue("MainWindowView.State", WindowState.Normal);
            set
            {

                if (value != WindowState.Minimized)
                    App.Settings.SetValue("MainWindowView.State", value);

                OnPropertyChanged();
            }
        }

        public object SelectedTab
        {
            get { return _SelectedTab; }
            set { _SelectedTab = value; }
        }
        private object _SelectedTab;

        public string TargetIPAddress
        {
            get { return App.Settings.GetValue("TargetIPAddress"); }
            set
            { 
                App.Settings.SetValue("TargetIPAddress", value);
                ARCLTab.UpdateTargetIPAddress();
            }
        }


        public ARCLTabViewModel ARCLTab { get; } = new ARCLTabViewModel();
        public RESTTabViewModel RESTTab { get; } = new RESTTabViewModel();
        public SQLTabViewModel SQLTab { get; } = new SQLTabViewModel();
        public RabbitMQTabViewModel RabbitMQTab { get; } = new RabbitMQTabViewModel();
        //public SystemInformationViewModel SystemInformation { get; } = new SystemInformationViewModel();
        //public TableOfContentsViewModel TableOfContents { get; } = new TableOfContentsViewModel();
        //public LogViewerViewModel LogViewer { get; } = new LogViewerViewModel();
        //public WiFiViewerViewModel WiFiViewer { get; } = new WiFiViewerViewModel();
        //public BatteryViewerViewModel BatteryViewer { get; } = new BatteryViewerViewModel();
        //public HeatMapViewModel HeatMapViewer { get; } = new HeatMapViewModel();

        public ICommand OpenCommand { get; }
        private void OpenCallback(object parameter)
        {
            switch ((string)parameter)
            {
                case "Open Zip File":
                    Task.Run(() => OpenZipFile());
                    break;
                case "Open Folder":
                    break;
                case "Open From LD/EM":
                    break;
                default:
                    break;
            }
        }

        private void ResetAll()
        {
            SelectedTab = null;

            //DragDrop.IsVisible = true;
            //SelectedTab = DragDrop;

            //SystemInformation.Reset();
            //TableOfContents.Reset();
            //LogViewer.Reset();
            //WiFiViewer.Reset();
            //BatteryViewer.Reset();
            //HeatMapViewer.Reset();
        }
        private void OpenZipFile()
        {
            //string file = null;
            //if (App.OpenFile(ref file, "Zip file (*.zip)|*.zip"))
            //{
            //    if (ExtractFile(file))
            //    {
            //        ResetAll();

            //        DragDrop.IsVisible = false;

            //        // AddToHistory(file.FileName);
            //        SystemInformation.Load();
            //        TableOfContents.Load();
            //        LogViewer.Load(SystemInformation.IsEM);
            //        if (!SystemInformation.IsEM)
            //        {
            //            WiFiViewer.Load();
            //            BatteryViewer.Load();
            //            HeatMapViewer.Load(WiFiViewer.GetAllEntries());
            //        }
            //        else
            //        {
            //            HeatMapViewer.Load(null);
            //        }
            //    }
            //}
        }
        private bool ExtractFile(string fileName)
        {
            int i = 0;
            while (Directory.Exists(App.WorkingDirectory))
                try
                {
                    Directory.Delete(App.WorkingDirectory, true);
                    if (i++ > 100)
                        return false;

                    Thread.Sleep(10);
                }
                catch (Exception)
                {

                }

            try
            {
                ZipFile.ExtractToDirectory(fileName, App.WorkingDirectory);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private class FileHistory
        {
            public string Path { get; set; }
            public bool IsDirectory { get; set; } = false;
        }
        private void AddToHistory(string filePath)
        {
            Dictionary<string, FileHistory> history = GetOpenHistory();

            FileHistory fhFile;
            if (!File.Exists(filePath) && !Directory.Exists(filePath))
            {
                var his = history.Where(s => s.Value.Path.Equals(filePath));

                foreach (var fh in history.ToList())
                    history.Remove(fh.Key);
            }
            else
            {
                fhFile = new FileHistory()
                {
                    Path = filePath,
                };
                if (File.Exists(filePath))
                    fhFile.IsDirectory = false;
                else
                    fhFile.IsDirectory = true;

                if (!history.ContainsKey(Path.GetFileName(fhFile.Path)))
                    history.Add(Path.GetFileName(fhFile.Path), fhFile);
            }

            App.Settings.SetValue("FileHistory", history);

            //BuildOpenMenu();
            //UpdateHistoryMenuItems(history);
        }
        private Dictionary<string, FileHistory> GetOpenHistory() => App.Settings.GetValue("FileHistory", new Dictionary<string, FileHistory>());

        IDialogCoordinator _DialogCoordinator;

        public MainWindowViewModel(IDialogCoordinator controller)
        {
            _DialogCoordinator = controller;

            OpenCommand = new RelayCommand(OpenCallback, c => true);
        }

    }
}
