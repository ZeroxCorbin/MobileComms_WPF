using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Web.Configuration;
using System.Windows;
using System.Windows.Forms;
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
        public partial class ApplicationSettings : INotifyPropertyChanged
        {


            public string ARCLConnectionString { get; set; } = "10.151.33.99:7171:adept";

            public string RESTHost { get; set; } = "10.151.33.99";
            public string RESTPassword { get; set; } = "dpACjp+JfD1ULDGQCvUj";
            [Newtonsoft.Json.JsonIgnore]
            public GridLength RESTSplitter
            {
                // Create the "GridLength" from the separate properties
                get => 
                    new GridLength(this.RESTSplitter_Value, (GridUnitType)Enum.Parse(typeof(GridUnitType), this.RESTSplitter_GridUnitType));
                set
                {
                    // store the "GridLength" properties in separate properties
                    this.RESTSplitter_GridUnitType = value.GridUnitType.ToString();
                    this.RESTSplitter_Value = value.Value;

                    RaisePropertyChanged("RESTSplitter");
                }
            }

            public string RESTSplitter_GridUnitType { get; set; } = "Pixel";
            public double RESTSplitter_Value { get; set; } = 320;

            public string SQLHost { get; set; } = "10.151.33.99";
            public string SQLPassword { get; set; } = "dpACjp+JfD1ULDGQCvUj";
            [Newtonsoft.Json.JsonIgnore]
            public GridLength SQLSplitter
            {
                get => new GridLength(this.SQLSplitter_Value, (GridUnitType)Enum.Parse(typeof(GridUnitType), this.SQLSplitter_GridUnitType));
                set
                {
                    this.SQLSplitter_GridUnitType = value.GridUnitType.ToString();
                    this.SQLSplitter_Value = value.Value;

                    RaisePropertyChanged("SQLSplitter");
                }
            }

            public string SQLSplitter_GridUnitType { get; set; } = "Pixel";
            public double SQLSplitter_Value { get; set; } = 320;

            public string RabbitMQHost { get; set; } = "10.151.33.99";
            public string RabbitMQPassword { get; set; } = "dpACjp+JfD1ULDGQCvUj";

            public WindowSettings MainWindow { get; set; } = new WindowSettings();
            public WindowSettings ARCLWindow { get; set; } = new WindowSettings();
            public WindowSettings RESTWindow { get; set; } = new WindowSettings();
            public WindowSettings RabbitMQWindow { get; set; } = new WindowSettings();
            public WindowSettings SQLWindow { get; set; } = new WindowSettings();

            public event PropertyChangedEventHandler PropertyChanged;
            private void RaisePropertyChanged(string propertyName) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            public class WindowSettings : INotifyPropertyChanged
            {
                private double _left = double.NaN;
                private double _top = double.NaN;
                private double _width = double.NaN; 
                private double _height = double.NaN;
                private WindowState windowState = WindowState.Normal;

                public double Left
                {
                    get { return _left; }
                    set { _left = value; RaisePropertyChanged("Left"); }
                }
                public double Top
                {
                    get { return _top; }
                    set { _top = value; RaisePropertyChanged("Top"); }
                }
                public double Width
                {
                    get { return _width; }
                    set { _width = value; RaisePropertyChanged("Width"); }
                }
                public double Height
                {
                    get { return _height; }
                    set { _height = value; RaisePropertyChanged("Height"); }
                }
                public WindowState WindowState
                {
                    get { return windowState; }
                    set { windowState = value; RaisePropertyChanged("WindowState"); }
                }

                public event PropertyChangedEventHandler PropertyChanged;
                private void RaisePropertyChanged(string propertyName) =>
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
