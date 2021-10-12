using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;
using System.IO;
using System.Configuration;

namespace DataLibrary
{
    public class XMLSerializationAndDeserialization : ISerializationAndDeserialization
    {
        
        public void Serialize(List<IStaffOperation>_staffList, string path)
        {
            using (var stream = new System.IO.StreamWriter(path)) 
            {
                var staffList = _staffList.Select(s => (Staff)s).ToList();
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
                writer.Serialize(stream, staffList);
                stream.Close();
            }
        }
        public List<IStaffOperation> DeSerialize(string path)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
                List<Staff> deserializedStaffList = (List<Staff>)reader.Deserialize(file);
                file.Close();
                return deserializedStaffList.Select(s => (IStaffOperation)s).ToList();
            }
        }
    }
}
