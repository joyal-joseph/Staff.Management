using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StaffManagementConsole.Staff;

namespace StaffManagementAPI.Models
{
    public class Staff
    {
        public int DailyWage { get; set; }
        public int Age { get; set; }
        public string JobType { get; set; }
        public GenderType Gender { get; set; }
        public string Section  { get; set; }
        public string Lab { get; set; }
        public string ClassTeacher { get; set; }
        public string Subject { get; set; }
        public int StaffID { get; set; }
        public string Name { get; set; }

    }
}


