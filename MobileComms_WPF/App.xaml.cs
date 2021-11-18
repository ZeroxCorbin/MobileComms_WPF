using ControlzEx.Theming;
using MobileComms_WPF.Core;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MobileComms_WPF
{
    public partial class App : Application
    {

        public static SimpleDataBase Settings;

#if DEBUG
        public static string RootDirectory { get; set; } = Path.Join(System.IO.Directory.GetCurrentDirectory(), "\\42Nexus\\MobileDebug_WPF\\");
#else        
        public static string RootDirectory { get; set; } = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "\\42Nexus\\MobileDebug_WPF\\");
#endif
        public static string WorkingDirectory => Path.Join(RootDirectory, "Working\\");
        public static string UserDataDirectory => Path.Join(RootDirectory, "UserData\\");

        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //GetCommandData d = new GetCommandData();
            base.OnStartup(e);

            if (!Directory.Exists(UserDataDirectory))
            {
                Console.WriteLine($"Creating directory: {UserDataDirectory}");
                Directory.CreateDirectory(UserDataDirectory);
            }

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        .WriteTo.File($"{Path.Join(UserDataDirectory, "\\logs\\")}log-.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            Log.Information("Logger Loaded");

            //FileStream filestream = new FileStream(Path.Join(UserDataDirectory, "\\log.txt"), FileMode.Append);
            //var streamwriter = new StreamWriter(filestream)
            //{
            //    AutoFlush = true
            //};
            //Console.SetOut(streamwriter);
            //Console.SetError(streamwriter);

            Settings = new SimpleDataBase().Init(Path.Join(UserDataDirectory, "\\ApplicationSettings.sqlite"), false);
            if (Settings == null)
            {
                Console.WriteLine($"Could not initialize the application settings database: {Path.Join(UserDataDirectory, "\\ApplicationSettings.sqlite")}");
                throw new Exception();
            }
            else
            {
                Console.WriteLine("Application settings loaded.");
            }

            _ = ThemeManager.Current.ChangeTheme(this, Settings.GetValue("App.Theme", "Light.Steel"));

            ThemeManager.Current.ThemeChanged += Current_ThemeChanged;
            ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Settings.Dispose();
        }

        private void Current_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            Settings.SetValue("App.Theme", e.NewTheme.Name);
        }
    }

    //public class GetCommandData
    //{

    //    public Dictionary<string, List<string>> Commands { get; private set; } = new Dictionary<string, List<string>>();
    //    public GetCommandData()
    //    {
    //        //using(var fs = new StreamReader("Processed_ARCL_Commands.json"))
    //        //{
    //        //    JsonSerializer js = new JsonSerializer();
    //        //    Commands = (Dictionary<string, List<string>>)js.Deserialize(fs, typeof(Dictionary<string, List<string>>));
    //        //}

    //        using(StreamReader file = new StreamReader("4_I617-E-02_ARCL_en_RawCommandData.txt"))
    //        {
    //            StringBuilder sb = new StringBuilder();

    //            bool build = false;
    //            string ln;
    //            string CommandName = "";
    //            while((ln = file.ReadLine()) != null)
    //            {
    //                if(Regex.IsMatch(ln, @"^[a-z]\w+\s(Command)($|\s[(](\w+:\s+\w+)[)]$)") | Regex.IsMatch(ln, @"^[0-9].[0-9]*\s[a-z]\w+\s(Command)($|\s[(](\w+:\s+\w+)[)]$)"))
    //                {
    //                    CommandName = ln.Replace(" Command", "").Trim(new char[] { '\r', '\n' });
    //                    if(!Commands.ContainsKey(CommandName))
    //                        Commands.Add(CommandName, new List<string>());
    //                    continue;
    //                }

    //                if(build)
    //                {
    //                    if(ln.StartsWith("Usage"))
    //                    {
    //                        build = false;
    //                        Commands[CommandName].Add(sb.ToString());
    //                        sb.Clear();
    //                        continue;
    //                    }

    //                    if(Regex.IsMatch(ln, @"^[A-Z]"))
    //                        continue;

    //                    sb.Append(ln.Trim(new char[] { '\r', '\n' }));


    //                }
    //                if(ln.StartsWith("syntax", StringComparison.OrdinalIgnoreCase))
    //                    build = true;
    //            }
    //        }



    //        using(var fs = new StreamWriter("4_I617-E-02_ARCL_en_ProcessedCommandData.json"))
    //        {
    //            JsonSerializer js = new JsonSerializer();
    //            js.Serialize(fs, Commands);
    //        }

    //    }
    //}
}
