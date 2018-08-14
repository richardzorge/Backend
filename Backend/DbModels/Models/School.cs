using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DbModels.Models
{
    [DataContract]
    public class School
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; } 
        public string address { get; set; }
        public string city { get; set; }
        public string email { get; set; }

        public bool is_active { get; set; }
        public DateTime change_dt { get; set; }

        public School()
        {
            change_dt = DateTime.Now;
        }
    }
}
