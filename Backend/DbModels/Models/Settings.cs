using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Backend.Models
{
    [DataContract]
    [XmlRoot()]
    public class Settings
    {
        static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }
        private static string _defaultConfigPath = "Settings.json";
        private static string _ConfigPath = "Settings.json";
        public static string ConfigPath
        {
            get
            {
                return _ConfigPath;
            }
            set
            {

                _ConfigPath = value;
            }
        }
        public static string ApplicationPath
        {
            get
            {
                return Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            }
        }
        private static Settings _Instance = null;
        public static Settings Instance
        {
            get { return _Instance; }
            set
            {
                if(_Instance==null)
                    _Instance = value;
            }
        }

        public static Settings CreateDefault()
        {
            try
            {
                Logger.Trace("Settings.CreateDefault() IN");
                _Instance = new Settings();
                _Instance.Save(_defaultConfigPath);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Create DefaultConfiguration error. Path: {0}", _defaultConfigPath));
            }
            finally
            {
                Logger.Trace("Settings.CreateDefault() OUT");
            }
            return _Instance;
        }



        public static Settings Create()
        {
            if (Instance != null)
                return _Instance;
            //Инициализируем NLog
            Logger.Trace("Settings.Create() IN");
            try
            {
                // Пробуем загрузить из указанного пути
                _Instance = LoadFromFile(ConfigPath);
            }
            catch (FileNotFoundException ex)
            {
                //Загрузить не удалось, т.к. файл не найден. Создаём конфигурацию в папке с программой
                return CreateDefault();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Configuration initialization error");
                throw ex;
            }
            finally
            {
                Logger.Trace("Settings.Create() OUT");
            }
            return null;
        }

        public static Settings LoadFromFile(string FilePath)
        {
            Logger.Trace("Settings.LoadFromFile() IN");
            if (!File.Exists(FilePath))
                throw new FileNotFoundException(string.Format("Configuration file not found: {0}", FilePath));
            try
            {
                //XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Settings));
                using (Stream fileStream = new FileStream(FilePath, FileMode.Open))
                {

                    //XmlReader reader = new XmlTextReader(fileStream);
                    //if (!serializer.CanDeserialize(reader))
                    //{
                    //    Logger.Fatal("Error while reading the configuration");
                    //    throw new ApplicationException("Configuration reading error");
                    //}
                    fileStream.Position = 0;
                    //Settings result = (Settings)serializer.Deserialize(fileStream);
                    Settings result = (Settings)serializer.ReadObject(fileStream);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Configuration load error");
            }
            finally
            {
                Logger.Trace("Settings.LoadFromFile() OUT");
            }
            return null;
        }

        [DataMember]
        public string DbConnectionString { get; set; }

        public Settings()
        {
            Logger.Trace("Settings() IN");
            DbConnectionString = @"Host=localhost;Port=5432;Database=cap01dev;Username=postgres;Password=postgres";
            Logger.Trace("Settings() OUT");
        }





        public void Save(string FilePath)
        {
            try
            {
                Logger.Trace("Settings.Save() IN");
                using (FileStream fileStream = new FileStream(FilePath, FileMode.OpenOrCreate))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Settings));
                    ser.WriteObject(fileStream, this);
                    //XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                    //xmlSerializer.Serialize(fileStream, this);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Configuration save error");
            }
            finally
            {
                Logger.Trace("Settings.Save() OUT");
            }
        }
    }
}
