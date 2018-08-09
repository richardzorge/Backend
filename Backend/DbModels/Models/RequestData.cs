using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Backend.Models
{
    [DataContract]
    public class RequestData
    {
        private static NLog.Logger Logger
        {
            get
            {
                return NLog.LogManager.GetCurrentClassLogger();
            }
        }

        public static RequestData Deserialize(Stream stream)
        {
            Logger.Trace("RequestData.Deserialize() IN");
            if (stream == null)
                return null;
            try
            {
                StreamReader sr = new StreamReader(stream);
                string data = sr.ReadToEnd();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RequestData));
                stream.Position = 0;
                RequestData result = (RequestData)serializer.ReadObject(stream);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "RequestData deserialization error");
            }
            finally
            {
                Logger.Trace("RequestData.Deserialize() OUT");
            }
            return null;
        }

        public RequestData()
        {
            Result = new Dictionary<string, object>();
        }

        [DataMember(Name = "data")]
        public Dictionary<string, object> Result { get; set; }

        public object GetValue(string Attribute)
        {
            if (Result.ContainsKey(Attribute))
                return Result[Attribute];
            return null;
        }

        public void Add(string Key, object Value)
        {
            Result.Add(Key, Value);
        }

        public string Serialize()
        {
            string result = string.Empty;
            try
            {
                Logger.Trace("RequestData.Serialize() IN");
                using (MemoryStream stream = new MemoryStream())
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RequestData));
                    ser.WriteObject(stream, this);
                    stream.Position = 0;
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Serialize error");
            }
            finally
            {
                Logger.Trace("RequestData.Serialize() OUT");
            }
            return null;
        }
    }
}
