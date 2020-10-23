using ApplicationSettingsNS;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Text;
using System.Data;
using Classes.IntegrationToolkit;

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

            TxtHost.Text = App.Settings.SQLHost;
            TxtPassword.Password = App.Settings.SQLPassword;

            DisConnected();
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
            if(CmdMoveType.ItemsSource == null)
                CmdMoveType.Items.Clear();

            if(sender is TreeViewItem tv)
            {
                if(tv.Tag is KeyValuePair<string, Dictionary<SQL.QueryTypes, string>> lst)
                { 
                    CmdMoveType.Tag = lst;
                    CmdMoveType.ItemsSource = lst.Value.Keys;
                    CmdMoveType.SelectedIndex = -1;
                    CmdMoveType.SelectedIndex = 0;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((TreeViewItem)TrvQueueList.Items[0]).IsSelected = true;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if(CmdMoveType.SelectedItem == null) return;

            if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.SELECT)
            {
                //DgvTableRows.ItemsSource = Select($"{TxtQueryStart.Text} {TxtQueryDetails.Text}").Tables[0].DefaultView;
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            System.Data.DataSet ds = SQL.Select($"{TxtQueryStart.Text} {TxtQueryDetails.Text}");
                            if(ds.Tables.Count > 0)
                            {
                                DgvTableRows.ItemsSource = ds.Tables[0].DefaultView;
                                TxtResponse.Text = "";
                            }

                            else
                            {
                                if(SQL.IsException)
                                    TxtResponse.Text = SQL.DbException.Message;
                                DgvTableRows.ItemsSource = null;
                            }

                        }));
            }
            else if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.INSERT)
            {
                
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            int ret = SQL.Insert($"{TxtQueryStart.Text} {TxtQueryDetails.Text}");
                            if(ret < 0)
                            {
                                if(SQL.IsException)
                                    TxtResponse.Text = SQL.DbException.Message;
                                else
                                    TxtResponse.Text = "The SQL Connection is not Active.";
                            }
                            else
                                TxtResponse.Text = $"INSERT affected {ret} rows.";


                        }));
            }
            else if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.UPDATE)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            int ret = SQL.Update($"{TxtQueryStart.Text} {TxtQueryDetails.Text}");
                            if(ret < 0)
                            {
                                if(SQL.IsException) 
                                    TxtResponse.Text = SQL.DbException.Message;
                                else
                                    TxtResponse.Text = "The SQL Connection is not Active.";
                            }
                            else
                                TxtResponse.Text = $"UPDATE affected {ret} rows.";
                        }));
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

            if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.SELECT)
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
            else if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.INSERT)
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
            else if(((SQL.QueryTypes)CmdMoveType.SelectedItem) == SQL.QueryTypes.UPDATE)
            {
                
            }
        }

        private void BtnGetScheme_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            DgvTableRows.ItemsSource = SQL.GetScheme($"{((TreeViewItem)TrvQueueList.SelectedItem).Header}").Tables[0].DefaultView;

            //Dispatcher.BeginInvoke(DispatcherPriority.Render,
            //        (Action<string>)((s) =>
            //        {
            //            TxtResponse.Text = s;
            //        }), ret);
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(BtnConnect.Tag == null)
            {
                if(SQL.Connect(TxtHost.Text, TxtPassword.Password))
                {
                    BtnConnect.Tag = "";
                    BtnConnect.Background = Brushes.LightGreen;

                    Connected();
                }
                else
                {
                    BtnConnect.Background = Brushes.LightSalmon;

                    DisConnected();
                }
            }
            else
            {
                BtnConnect.Tag = null;

                SQL.Close();

                BtnConnect.Background = Brushes.LightSalmon;

                DisConnected();
            }


        }

        private void Connected()
        {
            BrdViewList.IsEnabled = true;

            if(TxtQueryStart.Text.Length > 0)
            {
                //BrdCommandResponse.IsEnabled = true;
                BrdQuery.IsEnabled = true;
                BrdTableRows.IsEnabled = true;
            }

            TxtPassword.IsEnabled = false;
            TxtHost.IsEnabled = false;
        }
        private void DisConnected()
        {
            //BrdCommandResponse.IsEnabled = false;
            BrdQuery.IsEnabled = false;
            BrdTableRows.IsEnabled = false;
            BrdViewList.IsEnabled = false;

            TxtPassword.IsEnabled = true;
            TxtHost.IsEnabled = true;
        }
        private void TxtHost_TextChanged(object sender, TextChangedEventArgs e)
        {
            App.Settings.SQLHost = TxtHost.Text;

            TxtDBConnectionString.Text = SQL.ConnectionString(TxtHost.Text, "Your_ITK_Password");
        }

        private void CmdMoveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!IsLoaded) return;

            BtnSend.Content = CmdMoveType.SelectedItem;

            if(CmdMoveType.SelectedItem is SQL.QueryTypes type)
            {
                if(type == SQL.QueryTypes.SELECT)
                {
                    TxtQueryStart.Text = $"SELECT * FROM {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                    TxtJsonData.IsEnabled = true;
                }
                else if(type == SQL.QueryTypes.INSERT)
                {
                    TxtQueryStart.Text = $"INSERT INTO {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                    TxtJsonData.IsEnabled = true;
                }
                else if(type == SQL.QueryTypes.UPDATE)
                {
                    TxtQueryStart.Text = $"UPDATE {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                    TxtQueryDetails.Text = "SET subscription_interval = '1s' WHERE namekey =''";
                    TxtJsonData.IsEnabled = false;
                }

                TxtJsonData.Text = "";
                Type t = Type.GetType($"Classes.IntegrationToolkit.JSON_Types.{((KeyValuePair<string, Dictionary<SQL.QueryTypes, string>>)CmdMoveType.Tag).Value[type]}");

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
}
