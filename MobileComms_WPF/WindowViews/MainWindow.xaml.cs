using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MobileComms_WPF.WindowViews
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new WindowViewModels.MainWindowViewModel(MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance);

            ARCLTabView.DataContext = ((WindowViewModels.MainWindowViewModel)DataContext).ARCLTab;
            RESTTabView.DataContext = ((WindowViewModels.MainWindowViewModel)DataContext).RESTTab;
            SQLTabView.DataContext = ((WindowViewModels.MainWindowViewModel)DataContext).SQLTab;
            RabbitMQTabView.DataContext = ((WindowViewModels.MainWindowViewModel)DataContext).RabbitMQTab;
        }

        private void btnLightTheme_Click(object sender, RoutedEventArgs e) => ThemeManager.Current.ChangeTheme(App.Current, "Light.Steel");

        private void btnDarkTheme_Click(object sender, RoutedEventArgs e) => ThemeManager.Current.ChangeTheme(App.Current, "Dark.Steel");
    }
}
