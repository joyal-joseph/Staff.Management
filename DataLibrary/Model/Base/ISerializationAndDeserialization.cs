using System.Collections.Generic;
using StaffManagementConsole;

namespace DataLibrary
{
    public interface ISerializationAndDeserialization
    {
        public void Serialize(List<IStaffOperation> staffs, string path);
        public List<IStaffOperation> DeSerialize(string path);
    }
}
