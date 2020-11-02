using ApplicationSettingsNS;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MobileComms_WPF
{
    public class MoveToForeground
    {
        [DllImportAttribute("User32.dll")]
        private static extern int FindWindow(String ClassName, String WindowName);

        const int SWP_NOMOVE = 0x0002;
        const int SWP_NOSIZE = 0x0001;
        const int SWP_SHOWWINDOW = 0x0040;
        const int SWP_NOACTIVATE = 0x0010;
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        public static void DoOnProcess(string winTitle)
        {
            int hWnd = FindWindow(null, winTitle);
            // Change behavior by settings the wFlags params. See http://msdn.microsoft.com/en-us/library/ms633545(VS.85).aspx
            if(hWnd != -1)
                SetWindowPos(new IntPtr(hWnd), 0, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW | SWP_NOACTIVATE);
        }
    }

    public class CheckOnScreen
    {

        public static bool IsOnScreen(Window window)
        {
            System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            foreach(System.Windows.Forms.Screen screen in screens)
            {
                System.Drawing.Rectangle formRectangle = new System.Drawing.Rectangle((int)window.Left * 2, (int)window.Top * 2,
                                                         (int)window.Width * 2, (int)window.Height * 2);

                if(screen.WorkingArea.Contains(formRectangle))
                {
                    return true;
                }
            }

            return false;
        }


    }
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static ApplicationSettings_Serializer.ApplicationSettings Settings { get; set; }
#if DEBUG
        public static string SettingsFileRootDir { get; set; } = System.IO.Directory.GetCurrentDirectory();
#else        
        public static string SettingsFileRootDir { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif
        public static string SettingsFileAppDir { get; set; } = "\\42Nexus\\MobileComms_WPF\\";
        public static string SettingsFileName { get; set; } = "appsettings.xml";
        public static string SettingsFilePath { get; set; } = SettingsFileRootDir + SettingsFileAppDir + SettingsFileName;

        public static string Path { get; set; } = System.IO.Directory.GetCurrentDirectory();
        public App()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Logger Loaded");

            if(!Directory.Exists(SettingsFileRootDir + SettingsFileAppDir))
            {
                try
                {
                    Directory.CreateDirectory(SettingsFileRootDir + SettingsFileAppDir);
                }
                catch(Exception)
                {
                }
            }
            try
            {
                Settings = ApplicationSettings_Serializer.Load(SettingsFilePath);
            }
            catch(Exception)
            {
                Settings = new ApplicationSettings_Serializer.ApplicationSettings();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //GetCommandData d = new GetCommandData();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                ApplicationSettings_Serializer.Save(SettingsFilePath, Settings);
            }
            catch(Exception)
            {
            }

            base.OnExit(e);
        }
    }

    public class GetCommandData
    {

        public Dictionary<string, List<string>> Commands { get; private set; } = new Dictionary<string, List<string>>();
        public GetCommandData()
        {
            //using(var fs = new StreamReader("Processed_ARCL_Commands.json"))
            //{
            //    JsonSerializer js = new JsonSerializer();
            //    Commands = (Dictionary<string, List<string>>)js.Deserialize(fs, typeof(Dictionary<string, List<string>>));
            //}

            using(StreamReader file = new StreamReader("4_I617-E-02_ARCL_en_RawCommandData.txt"))
            {
                StringBuilder sb = new StringBuilder();

                bool build = false;
                string ln;
                string CommandName = "";
                while((ln = file.ReadLine()) != null)
                {
                    if(Regex.IsMatch(ln, @"^[a-z]\w+\s(Command)($|\s[(](\w+:\s+\w+)[)]$)") | Regex.IsMatch(ln, @"^[0-9].[0-9]*\s[a-z]\w+\s(Command)($|\s[(](\w+:\s+\w+)[)]$)"))
                    {
                        CommandName = ln.Replace(" Command", "").Trim(new char[] { '\r', '\n' });
                        if(!Commands.ContainsKey(CommandName))
                            Commands.Add(CommandName, new List<string>());
                        continue;
                    }

                    if(build)
                    {
                        if(ln.StartsWith("Usage"))
                        {
                            build = false;
                            Commands[CommandName].Add(sb.ToString());
                            sb.Clear();
                            continue;
                        }

                        if(Regex.IsMatch(ln, @"^[A-Z]"))
                            continue;

                        sb.Append(ln.Trim(new char[] { '\r', '\n' }));


                    }
                    if(ln.StartsWith("syntax", StringComparison.OrdinalIgnoreCase))
                        build = true;
                }
            }



            using(var fs = new StreamWriter("4_I617-E-02_ARCL_en_ProcessedCommandData.json"))
            {
                JsonSerializer js = new JsonSerializer();
                js.Serialize(fs, Commands);
            }

        }
    }
}
