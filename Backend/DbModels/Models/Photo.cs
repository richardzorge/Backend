using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DbModels.Models
{
    [DataContract]
    public class Photo
    {
        public int ID { get; set; }
        public string path { get; set; }

        public DateTime change_dt { get; set; }

        public Photo()
        {
            change_dt = DateTime.Now;
        }
    }
}
