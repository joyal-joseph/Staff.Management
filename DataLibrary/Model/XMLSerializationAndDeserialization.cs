using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;

namespace DataLibrary
{
    public class XMLSerializationAndDeserialization : ISerializationAndDeserialization
    {
        public void Serialize(List<Staff> staffList)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            var path = new System.IO.StreamWriter(@"C:\Work\JoyalTraining\Staff.Management\XMLSerialisation.xml");
            writer.Serialize(path, staffList);
            path.Close();
        }
        public List<Staff> DeSerialize()
        {
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Work\JoyalTraining\Staff.Management\XMLSerialisation.xml");
            List<Staff> deserializedStaffList = (List<Staff>)reader.Deserialize(file);
            file.Close();
            return deserializedStaffList;
        }
    }
}
