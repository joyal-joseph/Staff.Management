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
            var staffList=_staffList.Select(s => (Staff)s).ToList();
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            var stream = new System.IO.StreamWriter(path);
            writer.Serialize(stream, staffList);
            stream.Close();
        }
        public List<IStaffOperation> DeSerialize(string path)
        {
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            List<Staff> deserializedStaffList = (List<Staff>)reader.Deserialize(file);
            file.Close();
            return deserializedStaffList.Select(s=>(IStaffOperation) s).ToList();
        }
    }
}
