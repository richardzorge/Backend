using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DbModels
{
    [DataContract]
    public class Tournament
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public DateTime event_date_time { get; set; }
        public uint min_years_old { get; set; }
        public uint max_years_old { get; set; }
        public uint years_step { get; set; }
        public uint start_weight { get; set; }
        public uint weight_step { get; set; }

        public bool is_active { get; set; }
        public DateTime change_dt { get; set; }

        public Tournament()
        {
            change_dt = DateTime.Now;
        }
    }
}
