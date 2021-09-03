using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using StaffManagementConsole;
using System.Linq;
using System.Xml;
using System.Text.Json;

namespace DataLibrary
{
    public class JSONSerializationAndDeserialization : ISerializationAndDeserialization
    {
        public void Serialize(List<IStaffOperation> _staffList, string path)
        {
            var staffList=_staffList.Select(s => (Staff) s).ToList();
            using (var stream = new FileStream(path, FileMode.Create))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                JSONStream.WriteObject(stream, staffList);
            }
        }
        public List<IStaffOperation> DeSerialize(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                List<Staff> JSONStaffList = (List<Staff>)JSONStream.ReadObject(stream);
                return JSONStaffList.Select(s => (IStaffOperation)s).ToList() ;

                
            }
        }
    }
}
