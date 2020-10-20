using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace ApplicationSettingsNS
{
    public class ApplicationSettings_Serializer
    {
        public static ApplicationSettings Load(string file)
        {
            StreamReader sr;
            ApplicationSettings app;
            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
            try
            {
                sr = new StreamReader(file);
            }
            catch (FileNotFoundException)
            {
                ApplicationSettings_Serializer.Save(file, new ApplicationSettings());
                sr = new StreamReader(file);
            }

            app = (ApplicationSettings)serializer.Deserialize(sr);
            sr.Close();
            return app;
        }
        public static void Save(string file, ApplicationSettings app)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSettings));
            using (StreamWriter sw = new StreamWriter(file))
            {
                serializer.Serialize(sw, app);
            }
        }

        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class ApplicationSettings
        {
            public string ARCLConnectionString { get; set; } = "192.168.0.20:7171:adept";

            public string RESTServerPath { get; set; } = "https://[IP]:8443";
            public string RESTUserName { get; set; } = "toolkitadmin";
            public string RESTPassword { get; set; } = "";

            public WindowSettings MainWindow { get; set; } = new WindowSettings();
            public WindowSettings ARCLWindow { get; set; } = new WindowSettings();
            public WindowSettings RESTWindow { get; set; } = new WindowSettings();
            public WindowSettings RabbitMQWindow { get; set; } = new WindowSettings();
            public WindowSettings SQLWindow { get; set; } = new WindowSettings();
            public class WindowSettings
            {
                public double Left { get; set; } = double.NaN;
                public double Top { get; set; } = double.NaN;
                public double Width { get; set; } = double.NaN;
                public double Height { get; set; } = double.NaN;
                public WindowState WindowState { get; set; } = WindowState.Normal;
            }
        }
    }
}
