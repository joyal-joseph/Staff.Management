using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StaffManagementConsole.Staff;
using StaffManagementConsole;
using System.Text.Json.Serialization;

namespace StaffManagementAPI.Models
{
    public class Staff 
    {
        public int DailyWage { get; set; }
        public int Age { get; set; }
        public string JobType { get; set; }
        public GenderType Gender { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Section  { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Lab { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string ClassTeacher { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Subject { get; set; }
        public int StaffID { get; set; }
        public string Name { get; set; }

    }
}


