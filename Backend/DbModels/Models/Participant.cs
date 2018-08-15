using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DbModels.Models
{
    [DataContract]
    public class Participant
    {
        public uint ID { get; set; }
        public uint school_id { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string surname { get; set; }
        public string photo_id { get; set; }
        public string email { get; set; }
        public bool is_active { get; set; }
        public int weight { get; set; }

        public int year_of_birth { get; set; }
        public DateTime change_dt { get; set; }

        public Participant()
        {
            change_dt = DateTime.Now;
        }
    }
}
