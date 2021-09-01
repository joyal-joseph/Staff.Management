using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using StaffManagementConsole;
namespace DataLibrary
{
    public class JSONSerializationAndDeserialization : ISerializationAndDeserialization
    {
        public void Serialize(List<Staff> staffList)
        {
            var path = @"C:\Work\JoyalTraining\Staff.Management\JSONSerialisation.json";
            using (var stream = new FileStream(path, FileMode.Create))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                JSONStream.WriteObject(stream, staffList);
            }
        }
        public List<Staff> DeSerialize()
        {
            var path = @"C:\Work\JoyalTraining\Staff.Management\JSONSerialisation.json";
            using (var stream = new FileStream(path, FileMode.Open))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                List<Staff> JSONStaffList = (List<Staff>)JSONStream.ReadObject(stream);
                return JSONStaffList;
            }
        }
    }
}
