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
using MobileComms_ITK;
using SqlKata;
using SqlKata.Extensions;
using SqlKata.Compilers;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for RabitMQWindow.xaml
    /// </summary>
    public partial class SQLWindow : Window, INotifyPropertyChanged
    {
        private SQL.QueryType SQLQueryType { get; set; } = SQL.QueryType.SELECT;
        private SQL SQL { get; } = new SQL();
        public SQLWindow(Window owner)
        {
            DataContext = App.Settings;
            Owner = owner;

            InitializeComponent();

            Window_LoadSettings();

            LoadQueueList();
        }



        private void Window_LoadSettings()
        {
            if(double.IsNaN(App.Settings.SQLWindow.Left)
                || !CheckOnScreen.IsOnScreen(this)
                || Keyboard.IsKeyDown(Key.LeftShift))
            {
                Left = Owner.Left;
                Top = Owner.Top + Owner.Height;
                Height = 768;
                Width = 1024;
            }

            TxtPassword.Password = App.Settings.SQLPassword;

            DisConnected();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;
            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.SQLWindow.WindowState = this.WindowState;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((TreeViewItem)TrvQueueList.Items[0]).IsSelected = true;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void LoadQueueList()
        {
            foreach(SQL.View kv in SQL.Views)
            {
                TreeViewItem tvi = new TreeViewItem { Header = kv.Name };
                tvi.Tag = kv;
                tvi.Selected += Tvi_Selected;
                TrvQueueList.Items.Add(tvi);
            }

        }

        private void Tvi_Selected(object sender, RoutedEventArgs e)
        {
            if(LstQueryTypes.ItemsSource == null)
                LstQueryTypes.Items.Clear();

            if(sender is TreeViewItem tvi)
                if(tvi.Tag is SQL.View cmd)
                {
                    LstQueryTypes.Tag = cmd;
                    LstQueryTypes.ItemsSource = cmd.QueryTypes.Keys;
                    LstQueryTypes.SelectedIndex = -1;
                    LstQueryTypes.SelectedIndex = 0;
                }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            if(LstQueryTypes.SelectedItem == null) return;

            if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.SELECT)
            {
                //DgvTableRows.ItemsSource = Select($"{TxtQueryStart.Text} {TxtQueryDetails.Text}").Tables[0].DefaultView;
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            System.Data.DataSet ds = SQL.Select($"{TxtQueryStart.Text}");

                            if(ds.Tables.Count > 0)
                            {
                                DgvTableRows.ItemsSource = ds.Tables[0].DefaultView;
                                if(ds.Tables[0].Columns.Contains("upd"))
                                    DgvTableRows.Items.SortDescriptions.Add(new SortDescription("upd", ListSortDirection.Descending));
                                TxtResponse.Text = "";
                            }

                            else
                            {
                                if(SQL.IsException)
                                    TxtResponse.Text = SQL.SQLException.Message;
                                DgvTableRows.ItemsSource = null;
                            }

                        }));
            }
            else if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.INSERT)
            {

                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            int ret = SQL.Insert($"{TxtQueryStart.Text}");
                            if(ret < 0)
                            {
                                if(SQL.IsException)
                                    TxtResponse.Text = SQL.SQLException.Message;
                                else
                                    TxtResponse.Text = "The SQL Connection is not Active.";
                            }
                            else
                                TxtResponse.Text = $"INSERT affected {ret} rows.";


                        }));
            }
            else if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.UPDATE)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            int ret = SQL.Update($"{TxtQueryStart.Text}");
                            if(ret < 0)
                            {
                                if(SQL.IsException)
                                    TxtResponse.Text = SQL.SQLException.Message;
                                else
                                    TxtResponse.Text = "The SQL Connection is not Active.";
                            }
                            else
                                TxtResponse.Text = $"UPDATE affected {ret} rows.";
                        }));
            }

        }

        private void JsonData_TextChanged()
        {
            if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.SELECT)
            {
                var compiler = new PostgresCompiler();
                Query select = new Query((string)((TreeViewItem)TrvQueueList.SelectedItem).Header);

                bool found = false;
                foreach(StackPanelLocal stkl in StkJsonData.Children)
                    if(Regex.IsMatch(stkl.Text, @"^.*=(['].+[']|[0-9]+)$"))
                    {
                        found = true;

                        if(Regex.IsMatch(stkl.Text, @"='true'$"))
                            select.WhereTrue(Regex.Match(stkl.Text, @"^.*(?=[=])").Value);
                        else if(Regex.IsMatch(stkl.Text, @"='false'$"))
                            select.WhereFalse(Regex.Match(stkl.Text, @"^.*(?=[=])").Value);
                        else
                            select.WhereLike(Regex.Match(stkl.Text, @"^.*(?=[=])").Value, Regex.Match(stkl.Text, @"(?<=[=]).*$").Value, true);
                    }

                if(!found)
                {
                    var res1 = compiler.Compile(select);
                    TxtQueryStart.Text = res1.Sql;
                    return;
                }

                SqlResult result = compiler.Compile(select);
                string sql = result.Sql;

                List<object> bindings = result.Bindings;
                int i = 0;
                foreach(object o in bindings)
                    sql = sql.Replace($"@p{i++}", o.ToString());

                TxtQueryStart.Text = sql;
            }
            else if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.INSERT)
            {
                var compiler = new PostgresCompiler();
                Query insert = new Query((string)((TreeViewItem)TrvQueueList.SelectedItem).Header);
                List<string> lst = new List<string>();

                foreach(StackPanelLocal stkl in StkJsonData.Children)
                    if(Regex.IsMatch(stkl.Text, @"^.*=(['].+[']|[0-9]+)$"))
                        lst.Add(stkl.Text);

                if(lst.Count == 0)
                {
                    TxtQueryStart.Text = $"INSERT INTO {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                    return;
                }

                List<string> names = new List<string>();
                List<string> values = new List<string>();
                foreach(string s in lst)
                {
                    names.Add(Regex.Match(s, @"^.*(?=[=])").Value);
                    values.Add(Regex.Match(s, @"(?<=[=]).*$").Value);
                }

                insert.AsInsert(names.ToArray(), values.ToArray());

                SqlResult result = compiler.Compile(insert);
                string sql = result.Sql;

                List<object> bindings = result.Bindings;
                int i = 0;
                foreach(object o in bindings)
                    sql = sql.Replace($"@p{i++}", o.ToString());

                TxtQueryStart.Text = sql;
            }
            else if(((SQL.QueryType)LstQueryTypes.SelectedItem) == SQL.QueryType.UPDATE)
            {

            }
        }


        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if(BtnConnect.Tag == null)
            {
                if(SQL.Connect(App.Settings.SQLHost, TxtPassword.Password))
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
            BtnSend.IsEnabled = true;
            BtnShowSchema.IsEnabled = true;

            TxtPassword.IsEnabled = false;
            TxtHost.IsEnabled = false;
        }
        private void DisConnected()
        {
            BtnSend.IsEnabled = false;
            BtnShowSchema.IsEnabled = false;

            TxtPassword.IsEnabled = true;
            TxtHost.IsEnabled = true;
        }


        private class StackPanelLocal : StackPanel
        {
            public delegate void TextChangedEvent(string data);
            public event TextChangedEvent TextChanged;

            private TextBlock Start = new TextBlock() { Padding = new Thickness(0), Margin = new Thickness(0, 5, 0, 0), Background = null };
            private TextBlock End = new TextBlock() { Text = $"'", Padding = new Thickness(0), Margin = new Thickness(0, 5, 0, 0), Background = null };

            private bool _isString;
            private TextBox MidText = new TextBox() { MinWidth = 20, Padding = new Thickness(0), Margin = new Thickness(0, 5, 0, 0), VerticalContentAlignment = VerticalAlignment.Center, Background = null, BorderBrush = null };

            private bool _isEnum;
            private ComboBox Combo = new ComboBox() { BorderBrush = null, MinWidth = 40, Style = Application.Current.TryFindResource(ToolBar.ComboBoxStyleKey) as Style, Margin = new Thickness(0, 3, 0, 0) };

            private bool _isBool;
            private ComboBox Check = new ComboBox() { BorderBrush = null, MinWidth = 40, Style = Application.Current.TryFindResource(ToolBar.ComboBoxStyleKey) as Style, Margin = new Thickness(0, 3, 0, 0) };

            public StackPanelLocal(PropertyInfo property)
            {
                Orientation = Orientation.Horizontal;
                _isString = property.PropertyType == typeof(string);
                _isEnum = property.PropertyType.IsEnum;
                _isBool = property.PropertyType == typeof(bool);

                string name = Regex.Replace(property.Name, @"(?!^)[A-Z]", delegate (Match match)
                {
                    return $"_{char.ToLower(match.ToString()[0])}";
                });

                if(_isString)
                {
                    Start.Text = $"{name.ToLower()}='";
                    MidText.TextChanged += Mid_TextChanged;

                    Children.Add(Start);
                    Children.Add(MidText);
                    Children.Add(End);
                }
                else if(_isEnum)
                {
                    Start.Text = $"{name.ToLower()}=";
                    Combo.ItemsSource = new List<string>(Regex.Split(property.PropertyType.GetEnumNames()[0], "__"));
                    ((List<string>)Combo.ItemsSource).Insert(0, "");

                    Combo.SelectionChanged += Combo_SelectionChanged;

                    Children.Add(Start);
                    Children.Add(Combo);
                }
                else if(_isBool)
                {
                    Start.Text = $"{name.ToLower()}=";

                    Check.Items.Add("");
                    Check.Items.Add("true");
                    Check.Items.Add("false");
                    Check.SelectionChanged += Combo_SelectionChanged;

                    Children.Add(Start);
                    Children.Add(Check);
                }
                else
                {
                    MidText.TextChanged += Mid_TextChanged;

                    Start.Text = $"{name.ToLower()}=";
                    Children.Add(Start);
                    Children.Add(MidText);
                }
            }

            private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e) => TextChanged?.Invoke(Text);
            private void Date_TextInput(object sender, TextCompositionEventArgs e) => TextChanged?.Invoke(Text);
            private void Mid_TextChanged(object sender, TextChangedEventArgs e) => TextChanged?.Invoke(Text);

            public string Text => _isString ? $"{Start.Text}{MidText.Text}{End.Text}" : _isEnum ? $"{Start.Text}'{Combo.SelectedValue}'" : _isBool ? $"{Start.Text}'{Check.SelectedValue}'" : $"{Start.Text}{MidText.Text}";
        }
        private void LstQueryTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!IsLoaded) return;

            SQL.View view = (SQL.View)LstQueryTypes.Tag;

            //BtnSend.Content = LstQueryTypes.SelectedItem;

            if(LstQueryTypes.SelectedItem is SQL.QueryType type)
            {
                if(type == SQL.QueryType.SELECT)
                {
                    TxtQueryStart.Text = $"SELECT * FROM {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                }
                else if(type == SQL.QueryType.INSERT)
                {
                    TxtQueryStart.Text = $"INSERT INTO {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                }
                else if(type == SQL.QueryType.UPDATE)
                {
                    TxtQueryStart.Text = $"UPDATE {((TreeViewItem)TrvQueueList.SelectedItem).Header}";
                }

                StkJsonData.Children.Clear();
                foreach(PropertyInfo prop in view.QueryTypes[type].GetProperties())
                {
                    if(prop.Name.Equals("AdditionalProperties")) continue;
                    if(prop.Name.Equals("Upd")) continue;

                    StackPanelLocal stkl = new StackPanelLocal(prop);

                    stkl.TextChanged += Stkl_TextChanged;
                    StkJsonData.Children.Add(stkl);
                }
            }
        }

        private void Stkl_TextChanged(string data)
        {
            JsonData_TextChanged();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(!IsLoaded) return;
            App.Settings.SQLPassword = TxtPassword.Password;
        }
        public double TxtWidth { get; set; }
        public double TxtHeight { get; set; }

        private void BrdQuery_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double test = (BrdQuery.ActualWidth - 120) / 2;
            if(test > StkJsonData.ActualWidth)
                TxtWidth = (BrdQuery.ActualWidth - 120) - StkJsonData.ActualWidth;
            else
                TxtWidth = test;


            RaisePropertyChanged("TxtWidth");
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void BtnShowSchema_Click(object sender, RoutedEventArgs e)
        {
            TxtSchema.Text = SQL.GetScheme($"{((TreeViewItem)TrvQueueList.SelectedItem).Header}");
        }


    }
}
