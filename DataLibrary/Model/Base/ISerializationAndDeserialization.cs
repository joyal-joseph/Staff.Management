using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;

namespace DataLibrary
{
    public interface ISerializationAndDeserialization
    {
        public void Serialize(List<Staff> staffs);
        public List<Staff> DeSerialize();
    }
}
