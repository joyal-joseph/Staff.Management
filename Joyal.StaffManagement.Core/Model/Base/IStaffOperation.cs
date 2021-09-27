using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;
using static StaffManagementConsole.Staff;

namespace StaffManagementConsole
{
    public interface IStaffOperation
    {
        
        
            public string JobType { get; set; }
            public int StaffID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public GenderType Gender { get; set; }
            public int DailyWage { get; set; }

            public void AddOrUpdateStaff(int x);
            public void ViewStaff();
            public void UpdateStaff(int choice);
        
    }
}
