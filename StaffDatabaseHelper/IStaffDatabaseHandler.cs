using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;

namespace StaffDatabaseHelper
{
    public interface IStaffDatabaseHandler
    {
        public void BulkInsert(List<IStaffOperation> staffList);
        public void AddStaff(IStaffOperation staff);
        public IStaffOperation ViewStaff(int StaffID);
        public void DeleteStaff(int StaffID);
        public void UpdateStaff(IStaffOperation staff, int StaffID);
        public List<IStaffOperation> ViewAllStaff();
    }
}
