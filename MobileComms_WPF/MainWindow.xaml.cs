using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;

using ApplicationSettingsNS;

namespace MobileComms_WPF
{
    public partial class MainWindow : Window
    {
        private bool IsLoading { get; set; } = true;
        public MainWindow()
        {
            InitializeComponent();

            if(Keyboard.IsKeyDown(Key.LeftShift))
            {
                App.Settings.MainWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();
                App.Settings.ARCLWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();
                App.Settings.RESTWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();
            }

            if(double.IsNaN(App.Settings.MainWindow.Left))
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
            {
                this.Left = App.Settings.MainWindow.Left;
                this.Top = App.Settings.MainWindow.Top;

                if(!CheckOnScreen.IsOnScreen(this))
                    this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(WindowStartupLocation == WindowStartupLocation.CenterScreen)
            {
                WindowStartupLocation = WindowStartupLocation.Manual;
                IsLoading = false;
                this.Top /= 2;
            }
            IsLoading = false;
        }


        private void WindowClose()
        {

        }

        private ARCLWindow ARCLWindow { get; set; } = null;
        private void BtnARCLWindow_Click(object sender, RoutedEventArgs e)
        {
            if(ARCLWindow == null)
            {
                ARCLWindow = new ARCLWindow(this);
                ARCLWindow.Closed += ARCLWindow_Closed;
                ARCLWindow.Activated += AnyWindow_Activated;
                ARCLWindow.Show();

                ARCLWindow.Owner = null;
            }
            else if(ARCLWindow.WindowState == WindowState.Minimized)
            {
                ARCLWindow.WindowState = App.Settings.ARCLWindow.WindowState;
            }
            else
            {
                ARCLWindow.Focus();
            }
        }
        private void ARCLWindow_Closed(object sender, EventArgs e)
        {
            ARCLWindow = null;
            WindowClose();
        }
        private RESTWindow RESTWindow { get; set; } = null;
        private void BtnRESTWindow_Click(object sender, RoutedEventArgs e)
        {
            if(RESTWindow == null)
            {
                RESTWindow = new RESTWindow(this);
                RESTWindow.Closed += RESTWindow_Closed;
                RESTWindow.Activated += AnyWindow_Activated;
                RESTWindow.Show();

                RESTWindow.Owner = null;
            }
            else if(RESTWindow.WindowState == WindowState.Minimized)
            {
                RESTWindow.WindowState = App.Settings.RESTWindow.WindowState;
            }
            else
            {
                RESTWindow.Focus();
            }

        }
        private void RESTWindow_Closed(object sender, EventArgs e)
        {
            RESTWindow = null;
            WindowClose();
        }

        private RabbitMQWindow RabbitMQWindow { get; set; } = null;
        private void BtnRabbitMQWindow_Click(object sender, RoutedEventArgs e)
        {
            if(RabbitMQWindow == null)
            {
                RabbitMQWindow = new RabbitMQWindow(this);
                RabbitMQWindow.Closed += RabbitMQWindow_Closed;
                RabbitMQWindow.Activated += AnyWindow_Activated;
                RabbitMQWindow.Show();

                //WindowShow();
            }
        }
        private void RabbitMQWindow_Closed(object sender, EventArgs e)
        {
            RabbitMQWindow = null;
            WindowClose();
        }

        private SQLWindow SQLWindow { get; set; } = null;
        private void BtnSQLWindow_Click(object sender, RoutedEventArgs e)
        {
            if(SQLWindow == null)
            {
                SQLWindow = new SQLWindow(this);
                SQLWindow.Closed += SQLWindow_Closed;
                SQLWindow.Activated += AnyWindow_Activated;
                SQLWindow.Show();

                SQLWindow.Owner = null;
            }
            else if(SQLWindow.WindowState == WindowState.Minimized)
            {
                SQLWindow.WindowState = App.Settings.SQLWindow.WindowState;
            }
            else
            {
                SQLWindow.Focus();
            }
        }
        private void SQLWindow_Closed(object sender, EventArgs e)
        {
            SQLWindow = null;
            WindowClose();
        }

        private void AnyWindow_Activated(object sender, EventArgs e)
        {
            MoveToForeground.DoOnProcess("TM_Comms_WPF");

            if(SQLWindow != null && SQLWindow.WindowState == WindowState.Maximized)
            {
                SQLWindow.Owner = null;
                this.Focus();
            }
               
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(IsLoading) return;

            App.Settings.MainWindow.Top = Top;
            App.Settings.MainWindow.Left = Left;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ARCLWindow?.Close();
            RESTWindow?.Close();
            SQLWindow?.Close();
        }


    }
}
