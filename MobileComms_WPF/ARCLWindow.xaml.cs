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
using System.ComponentModel;
using ARCLTypes;
using ARCL;
using Microsoft.Win32;
using System.IO;

namespace MobileComms_WPF
{
    /// <summary>
    /// Interaction logic for ARCLWindow.xaml
    /// </summary>
    public partial class ARCLWindow : Window
    {


        ARCL.ARCLConnection Connection { get; } = new ARCL.ARCLConnection();
        public ARCLWindow(Window owner)
        {
            Owner = owner;

            InitializeComponent();

            Window_LoadSettings();

            LoadARCLList();

            Connection.ConnectState += Connection_ConnectState;
            Connection.DataReceived += Connection_DataReceived;
        }
        private void Window_LoadSettings()
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
        }

        private double TopLast;
        private double TopLeft;
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            TopLast = App.Settings.ARCLWindow.Top;
            TopLeft = App.Settings.ARCLWindow.Left;

            App.Settings.ARCLWindow.Top = Top;
            App.Settings.ARCLWindow.Left = Left;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(!IsLoaded) return;
            if(WindowState != WindowState.Normal) return;

            App.Settings.ARCLWindow.Height = Height;
            App.Settings.ARCLWindow.Width = Width;
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(!IsLoaded) return;

            if(this.WindowState != WindowState.Normal)
            {
                App.Settings.ARCLWindow.Top = TopLast;
                App.Settings.ARCLWindow.Left = TopLeft;
            }
            if(this.WindowState == WindowState.Minimized) return;

            App.Settings.ARCLWindow.WindowState = this.WindowState;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddGoalComboBoxesToGrid();

            BrdQueueManager.IsEnabled = false;
            GrdJMMain.IsEnabled = false;

            BrdConfigurationManager.IsEnabled = false;
            GrdCMMain.IsEnabled = false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //CleanSock();
        }

        private void LoadARCLList()
        {
            ARCLTypes.ARCLCommands commands = new ARCLTypes.ARCLCommands();
            foreach(var kv in commands.ARCLCommands_2016_ARCL_en)
            {
                TreeViewItem tvi = new TreeViewItem();
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");

                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                    tvi.Header = "--" + kv.Value;
                else
                    tvi.Header = kv.Value;

                //tvi.Items.Add(kv.Value);
                TvARCLCommands1.Items.Add(tvi);
            }

            foreach(var kv in commands.ARCLCommands_I617_E_01)
            {
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_2016_ARCL_en.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    TreeViewItem tvi = new TreeViewItem
                    {
                        //Header = kv.Key,
                        Header = kv.Value,
                        //Background = Brushes.LightGreen
                    };
                    //tvi.Items.Add(kv.Value);
                    TvARCLCommands1.Items.Add(tvi);
                }
            }

            foreach(var kv in commands.ARCLCommands_Help_4_10_1)
            {
                Match key = Regex.Match(kv.Key, @"^[a-zA-Z]*");
                var item = commands.ARCLCommands_I617_E_01.FirstOrDefault(x => x.Key.StartsWith(key.Value));
                if(string.IsNullOrEmpty(item.Key))
                {
                    TreeViewItem tvi = new TreeViewItem
                    {
                        Header = kv.Key,
                        Background = Brushes.LightYellow
                    };
                    //tvi.Items.Add(kv.Value);
                    TvARCLCommands1.Items.Add(tvi);
                }
            }
            TvARCLCommands1.Items.SortDescriptions.Clear();
            TvARCLCommands1.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
        }

        #region QueueManager

        private QueueJobManager QueueJobManager { get; set; } = new QueueJobManager();

        private static List<string> GoalNames { get; } = new List<string>();
        private List<ComboBox> GoalsComboBoxes { get; } = new List<ComboBox>() { new ComboBox() { ItemsSource = GoalNames, Height = 24 }, new ComboBox() { ItemsSource = GoalNames, Height = 24 }, new ComboBox() { ItemsSource = GoalNames, Height = 24 }, new ComboBox() { ItemsSource = GoalNames, Height = 24 } };

        private void BtnSendMulti_Click(object sender, RoutedEventArgs e)
        {
            List<QueueManagerJobSegment> goals = new List<QueueManagerJobSegment>();

            int i = 0;
            foreach(ComboBox cmb in GoalsComboBoxes)
            {
                if(i++ == 0)
                {
                    goals.Add(new QueueManagerJobSegment(null, ((string)cmb.SelectedItem).ToLower(), QueueManagerJobSegment.Types.pickup));
                    continue;
                }
                if(cmb.SelectedIndex >= 0)
                {
                    QueueManagerJobSegment.Types type = (cmb.SelectedIndex == 0) ? QueueManagerJobSegment.Types.pickup : QueueManagerJobSegment.Types.dropoff;
                    goals.Add(new QueueManagerJobSegment(null, ((string)cmb.SelectedItem).ToLower(), type));
                }
            }

            QueueJobManager.QueueMulti(goals);
        }
        private void BtnCancelJob_Click(object sender, RoutedEventArgs e)
        {
            QueueJobManager.CancelJob(((Button)sender).Tag.ToString());
        }

        private void BtnQueueManagerControl_Click(object sender, RoutedEventArgs e)
        {
            LoadGoalLists();

            QueueJobManager.Start(Connection);

            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueLoop));

            GrdJMMain.IsEnabled = true;

            //SychronousCommands sychronousCommands = new SychronousCommands(TxtConnectionString.Text);
            //List<string> goals;

            //if(sychronousCommands.Connect())
            //    goals = sychronousCommands.GetGoals();
            //else
            //{
            //    return;
            //}
            //sychronousCommands.Close();

            //if(Connection.Connect())
            //{
            //    btnConnect.Background = Brushes.Green;

            //    Connection.DataReceived += Connection_DataReceived;

            //    QueueJobManager = new QueueJobManager(Connection);

            //    QueueJobManager.Start();

            //    for(int i = 0; i < 4; i++)
            //    {
            //        ((ComboBox)stkGoalType.Children[i]).Items.Add("pickup");
            //        ((ComboBox)stkGoalType.Children[i]).Items.Add("dropoff");
            //    }
            //    foreach(ComboBox cmb in stkGoalName.Children)
            //    {
            //        cmb.ItemsSource = goals;
            //    }

            //    ThreadPool.QueueUserWorkItem(new WaitCallback(QueueLoop));
            //}
            //else
            //{
            //    btnConnect.Background = Brushes.Red;
            //}
        }

        private void LoadGoalLists()
        {
            GoalNames.Clear();

            SychronousCommands sychronousCommands = new SychronousCommands(Connection.ConnectionString);
            List<string> goals;

            if(sychronousCommands.Connect())
                goals = sychronousCommands.GetGoals();
            else
                return;

            foreach(string name in goals)
                GoalNames.Add(name);
        }

        private void QueueLoop(object sender)
        {
            while(Connection.IsConnected)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(QueueLoopLocal));
                Thread.Sleep(100);
            }
        }
        private void QueueLoopLocal()
        {
            if(QueueJobManager.SyncState.State == SyncStates.OK)
                LblIsSynced.Background = Brushes.LightGreen;
            else
                LblIsSynced.Background = Brushes.LightSalmon;

            LblJobCount.Content = $"Job Count = {QueueJobManager.Jobs.Count}";

            StkJobList.Children.Clear();
            foreach(KeyValuePair<string, QueueManagerJob> kv in QueueJobManager.Jobs)
            {
                bool found = false;
                foreach(StackPanel sp in StkJobList.Children)
                {
                    if((string)sp.Tag == kv.Value.ID)
                    {
                        found = true;
                        UpdateJobPanel(kv.Value, sp);
                        break;
                    }
                }
                if(!found)
                    StkJobList.Children.Add(GetJobPanel(kv.Value));
            }

        }
        private StackPanel GetJobPanel(QueueManagerJob job)
        {
            StackPanel stk = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Tag = job.ID
            };

            stk.Children.Add(new Label()
            {
                Content = job.ID,
                Tag = 1
            });

            stk.Children.Add(new Label()
            {
                Content = job.Status.ToString(),
                Tag = 2
            });
            stk.Children.Add(new Label()
            {
                Content = job.SubStatus.ToString(),
                Tag = 3
            });

            Button btn = new Button()
            {
                Content = "Cancel",
                Tag = job.ID,
            };
            stk.Children.Add(btn);

            btn.Click += BtnCancelJob_Click;
            return stk;
        }
        private void UpdateJobPanel(QueueManagerJob job, StackPanel stack)
        {
            foreach(object child in stack.Children)
            {
                if(child is Label myObjRef)
                {
                    switch((string)myObjRef.Tag)
                    {
                        case "1":
                            myObjRef.Content = job.ID;
                            break;
                        case "2":
                            myObjRef.Content = job.Status.ToString();
                            break;
                        case "3":
                            myObjRef.Content = job.SubStatus.ToString();
                            break;
                        default:

                            break;
                    }

                }
            }
        }

        private void AddGoalComboBoxesToGrid()
        {
            int i = 1;
            foreach(ComboBox cmb in GoalsComboBoxes)
            {
                Grid.SetRow(cmb, i++);
                GrdQueueManagerGoals.Children.Add(cmb);
            }
        }

        #endregion

        #region ConfigurationManager

        private ConfigManager ConfigManager { get; } = new ConfigManager();

        private void BtnConfigurationManagerControl_Click(object sender, RoutedEventArgs e)
        {
            ConfigManager.Start(Connection);
            ConfigManager.WaitForSync();

            ConfigManager.SyncStateChange += ConfigManager_SyncStateChange;

            UpdateAvailableSections();

            GrdCMMain.IsEnabled = true;
        }

        private void BtnCMReadSelectedSection_Click(object sender, RoutedEventArgs e)
        {
            if(LstConfigurationSections.SelectedIndex == -1) return;

            string name = ((GridViewRow)LstConfigurationSections.SelectedItem).SectionName;

            ConfigManager.ReadSectionValues(name);

            DgvSectionValues.ItemsSource = null;
            DgvSectionValues.ItemsSource = ConfigManager.Sections[name];
        }
        private void BtnCMReadAllSections_Click(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReadAllSectionsThread));
        }
        private void ReadAllSectionsThread(object sender)
        {
            foreach(string section in ConfigManager.Sections.Keys)
                ConfigManager.ReadSectionValues(section);

            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        if(LstConfigurationSections.SelectedIndex == -1) return;
                        DgvSectionValues.ItemsSource = null;
                        DgvSectionValues.ItemsSource = ConfigManager.Sections[((GridViewRow)LstConfigurationSections.SelectedItem).SectionName];
                    }));
        }
        private void BtnCMWriteSelectedSection_Click(object sender, RoutedEventArgs e)
        {
            if(LstConfigurationSections.SelectedIndex == -1) return;

            ConfigManager.WriteSectionValues(((GridViewRow)LstConfigurationSections.SelectedItem).SectionName);
        }


        private void MenCMSaveSelectedToFile_Click(object sender, RoutedEventArgs e)
        {
            if(LstConfigurationSections.SelectedIndex == -1) return;

            string name = ((GridViewRow)LstConfigurationSections.SelectedItem).SectionName;

            if(ConfigManager.Sections.ContainsKey(name))
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    DefaultExt = "txt",
                    Filter = "Text file (*.txt)|*.txt"
                };

                if((bool)sf.ShowDialog())
                    File.WriteAllText(sf.FileName, ConfigManager.SectionValuesToText(name));
            }
        }

        private void MenCMSaveAllToFile_Click(object sender, RoutedEventArgs e)
        {
            if(LstConfigurationSections.Items.Count == 0) return;

            SaveFileDialog sf = new SaveFileDialog
            {
                DefaultExt = "txt",
                Filter = "Text file (*.txt)|*.txt"
            };

            if((bool)sf.ShowDialog())
            {
                using StreamWriter sw = File.CreateText(sf.FileName);

                foreach(var cs in ConfigManager.Sections)
                    sw.WriteLine(ConfigManager.SectionValuesToText(cs.Key));
            }
        }

        private void MenCMLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog
            {
                CheckFileExists = true,
                DefaultExt = "txt",
                Filter = "Text file (*.txt)|*.txt"
            };

            if((bool)of.ShowDialog())
            {
                if(ConfigManager.TextToSectionValues(File.ReadAllText(of.FileName)))
                    UpdateAvailableSections();
            }
        }

        private class GridViewRow
        {
            public string SectionName { get; set; } = string.Empty;
            public string KeyValueCount { get; set; } = "0";
        }
        private void UpdateAvailableSections()
        {
            LstConfigurationSections.Items.Clear();

            foreach(var key in ConfigManager.Sections.OrderBy(x => x.Key))
                    LstConfigurationSections.Items.Add(new GridViewRow() { SectionName = key.Key, KeyValueCount = key.Value.Count.ToString() });

            LstConfigurationSections.Items.Refresh();
        }
        private void LstConfigurationSections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LstConfigurationSections.SelectedIndex == -1)
            {
                BtnCMReadSelectedSection.IsEnabled = false;
                BtnCMWriteSelectedSection.IsEnabled = false;
            }
            else
            {
                BtnCMReadSelectedSection.IsEnabled = true;
                BtnCMWriteSelectedSection.IsEnabled = true;
            }

            DgvSectionValues.ItemsSource = null;
            DgvSectionValues.ItemsSource = ConfigManager.Sections[((GridViewRow)LstConfigurationSections.SelectedItem).SectionName];
        }

        private void ConfigManager_SyncStateChange(object sender, SyncStateEventArgs syncState)
        {
            if(syncState.State == SyncStates.OK)
                Dispatcher.BeginInvoke(DispatcherPriority.Render,
                        (Action)(() =>
                        {
                            foreach(GridViewRow gvr in LstConfigurationSections.Items)
                                if(ConfigManager.Sections.ContainsKey(gvr.SectionName))
                                    gvr.KeyValueCount = ConfigManager.Sections[gvr.SectionName].Count.ToString();
                            LstConfigurationSections.Items.Refresh();
                        }));
        }


        #endregion
        private void Connection_ConnectState(object sender, bool state)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                    (Action)(() =>
                    {
                        if(state)
                        {
                            Connection.StartReceiveAsync();
                            BtnConnect.Background = Brushes.LightGreen;

                            BrdQueueManager.IsEnabled = true;
                            BrdConfigurationManager.IsEnabled = true;
                        }
                        else
                        {
                            BtnConnect.Background = Brushes.Salmon;
                            BrdQueueManager.IsEnabled = false;
                            BrdConfigurationManager.IsEnabled = false;
                        }

                    }));
        }

        private RoutedPropertyChangedEventArgs<object> SelectedItem { get; set; }
        private void TvARCLCommands_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) => SelectedItem = e;

        private void TvARCLCommands_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(SelectedItem != null)
                if(SelectedItem.NewValue is TreeViewItem tv)
                    TxtCommand2Send.Text = ((string)tv.Header).TrimStart('-');
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

        private void TxtResponse_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TxtResponse.ScrollToEnd();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuItem_SubmenuOpened(object sender, RoutedEventArgs e)
        {
            if(LstConfigurationSections.SelectedIndex == -1)
                MenCMSaveSelectedToFile.IsEnabled = false;
            else
                MenCMSaveSelectedToFile.IsEnabled = true;
        }
















        //Window Changes




    }
}
