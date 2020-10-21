using ApplicationSettingsNS;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using static Classes.IntegrationToolkit.SQL;
using System.Text;
using System.Data;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabitMQWindow.xaml
    /// </summary>
    public partial class SQLWindow : Window
    {


        public SQLWindow(Window owner)
        {
            Owner = owner;

            InitializeComponent();
            Window_LoadSettings();
            LoadQueueList();
        }
        private void Window_LoadSettings()
        {
            if(Keyboard.IsKeyDown(Key.LeftShift))
                App.Settings.SQLWindow = new ApplicationSettings_Serializer.ApplicationSettings.WindowSettings();

            if(double.IsNaN(App.Settings.SQLWindow.Left))
            {
                App.Settings.SQLWindow.Left = Owner.Left;
                App.Settings.SQLWindow.Top = Owner.Top + Owner.Height;
                App.Settings.SQLWindow.Height = 768;
                App.Settings.SQLWindow.Width = 1024;
            }

            this.Left = App.Settings.SQLWindow.Left;
            this.Top = App.Settings.SQLWindow.Top;
            this.Height = App.Settings.SQLWindow.Height;
            this.Width = App.Settings.SQLWindow.Width;

            if(!CheckOnScreen.IsOnScreen(this))
            {
                App.Settings.SQLWindow.Left = Owner.Left;
                App.Settings.SQLWindow.Top = Owner.Top + Owner.Height;
                App.Settings.SQLWindow.Height = 768;
                App.Settings.SQLWindow.Width = 1024;

                this.Left = App.Settings.SQLWindow.Left;
                this.Top = App.Settings.SQLWindow.Top;
                this.Height = App.Settings.SQLWindow.Height;
                this.Width = App.Settings.SQLWindow.Width;
            }
        }
        private void LoadQueueList()
        {
            foreach(var kv in Classes.IntegrationToolkit.SQL.Views)
            {
                TreeViewItem tvi = new TreeViewItem { Header = kv.Key };
                tvi.Tag = kv;
                tvi.Selected += Tvi_Selected;
                TrvQueueList.Items.Add(tvi);
            }

        }

        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {
            if(sender is TreeViewItem tv)
            {
                if(tv.Tag is KeyValuePair<string, Dictionary<QueryTypes, string>> lst)
                {
                    CmdMoveType.ItemsSource = lst.Value.Keys;
                    CmdMoveType.SelectedIndex = 0;

                    if(CmdMoveType.SelectedItem is QueryTypes type)
                    {
                        if(type == QueryTypes.SELECT)
                        {
                            TxtQueryStart.Text = $"SELECT * FROM {tv.Header}";
                            //var obj = Activator.CreateInstance(t);
                        }
                        else if(type == QueryTypes.INSERT)
                        {
                            TxtQueryStart.Text = $"INSERT INTO {tv.Header}";
                        }
                        else if(type == QueryTypes.UPDATE)
                        {

                        }

                        TxtJsonData.Text = "";
                        Type t = Type.GetType($"Classes.IntegrationToolkit.JSON_Types.{lst.Value[type]}");

                        if(t == null)
                        {
                            TxtJsonData.Text = string.Empty;
                            return;
                        }
                        foreach(var prop in t.GetProperties())
                        {
                            string name = Regex.Replace(prop.Name, @"[A-Z]", delegate (Match match)
                            {
                                return $"_{char.ToLower(match.ToString()[0])}";
                            });
                            if(prop.PropertyType == typeof(string))
                                TxtJsonData.Text += $"{name}=''\r\n";
                            else
                                TxtJsonData.Text += $"{name}=\r\n";
                        }
                    }
                }
            }

            //if(CmdMoveType.SelectedIndex == 0)
            //    TxtJsonData.Text = $"SELECT * FROM {TxtViewName.Text}";
            //if(CmdMoveType.SelectedIndex == 1)
            //    TxtJsonData.Text = $"SELECT * FROM {TxtViewName.Text}";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if(CmdMoveType.SelectedItem == null) return;

            if(((QueryTypes)CmdMoveType.SelectedItem) == QueryTypes.SELECT)
            {
                //DgvTableRows.ItemsSource = Select($"{TxtQueryStart.Text} {TxtQueryDetails.Text}").Tables[0].DefaultView;
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            System.Data.DataSet ds = Select($"{TxtQueryStart.Text} {TxtQueryDetails.Text}");
                            if(ds.Tables.Count > 0)
                            {
                                DgvTableRows.ItemsSource = ds.Tables[0].DefaultView;
                                TxtResponse.Text = "";
                            }

                            else
                            {
                                if(IsException)
                                    TxtResponse.Text = DbException.Message;
                                DgvTableRows.ItemsSource = null;
                            }

                        }));
            }
            else
            {
                string ret = Insert($"{TxtQueryStart.Text} {TxtQueryDetails.Text}");
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action<string>)((s) =>
                        {
                            TxtResponse.Text = s;
                        }), ret);
            }

        }


        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.SQLWindow.Top = Top;
            App.Settings.SQLWindow.Left = Left;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.SQLWindow.WindowState = this.WindowState;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(!IsLoaded) return;

            App.Settings.SQLWindow.Height = Height;
            App.Settings.SQLWindow.Width = Width;
        }

        private void TxtJsonData_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] spl = TxtJsonData.Text.Split('\n');

            if(((QueryTypes)CmdMoveType.SelectedItem) == QueryTypes.SELECT)
            {
                List<string> lst = new List<string>();

                foreach(string s in spl)
                    if(Regex.IsMatch(s, @"^.*=(['].+[']|[0-9]+)\r$"))
                        lst.Add(s.Trim('\r'));

                if(lst.Count > 0)
                    TxtQueryDetails.Text = "WHERE";
                else
                {
                    TxtQueryDetails.Text = "";
                    return;
                }

                foreach(string s in lst)
                    TxtQueryDetails.Text += $" {s}";
            }
            if(((QueryTypes)CmdMoveType.SelectedItem) == QueryTypes.INSERT)
            {
                List<string> lst = new List<string>();

                foreach(string s in spl)
                    if(Regex.IsMatch(s, @"^.*=(['].+[']|[0-9]+)\r$"))
                        lst.Add(s.Trim('\r'));

                TxtQueryDetails.Text = "";

                if(lst.Count == 0)
                    return;


                int i = 0;
                foreach(string s in lst)
                {
                    if(i == 0)
                        TxtQueryDetails.Text += $"({Regex.Match(s, @"^\w*(?=)")}";
                    else
                        TxtQueryDetails.Text += $", {Regex.Match(s, @"^\w*(?=)")}";

                    if(++i == lst.Count)
                    {
                        TxtQueryDetails.Text += $")";
                        continue;
                    }
                }

                TxtQueryDetails.Text += $" VALUES ";

                i = 0;
                foreach(string s in lst)
                {
                    if(i == 0)
                        TxtQueryDetails.Text += $"({Regex.Match(s, @"(?<==).*$")}";
                    else
                        TxtQueryDetails.Text += $", {Regex.Match(s, @"(?<==).*$")}";

                    if(++i == lst.Count)
                    {
                        TxtQueryDetails.Text += $")";
                        continue;
                    }
                }
            }
        }

        private void BtnGetScheme_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            DgvTableRows.ItemsSource = GetScheme($"{((TreeViewItem)TrvQueueList.SelectedItem).Header}").Tables[0].DefaultView;

            //Dispatcher.BeginInvoke(DispatcherPriority.Render,
            //        (Action<string>)((s) =>
            //        {
            //            TxtResponse.Text = s;
            //        }), ret);
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(Connect(TxtConnectionString.Text, TxtPassword.Password))
                BtnConnect.Background = Brushes.LightGreen;
            else
                BtnConnect.Background = Brushes.Salmon;
        }
    }
}
