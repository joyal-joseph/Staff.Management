using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using StaffManagementConsole;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace StaffManagementConsole
{
    class Program
    {
        
        public static int ID { get; set; }
        private static List<Staff> staffList = new();
        private static Staff GetStaff()
        {
            int id = ValidateStaffID();
            Staff staff = staffList.FirstOrDefault(_staff => _staff.StaffID == id);
            if (staff == null)
            {
                Console.WriteLine(string.Format("No staff with ID{0}", id));
            }
            return staff;
        }
        private static int ValidateStaffID()
        {
            bool IDFlag = true;
            int id=0;
            while (IDFlag)
            {
                Console.WriteLine("Enter ID : ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    IDFlag = false;
                }
                catch
                {
                    Console.WriteLine("Wrong input type.");
                }
            }
            return id;
        }
        private static void StaffChildDetails(Staff staff)
        {
            if (staff is Teaching)
            {
                Teaching x = (Teaching)staff;
                x.ViewStaff();
            }
            else if (staff is Support)
            {
                Support x = (Support)staff;
                x.ViewStaff();
            }
            else if (staff is Administrative)
            {
                Administrative x = (Administrative)staff;
                x.ViewStaff();
            }
        }
        private static int ChoiceInput(int NumberOfChoices)
        {
            bool flag = true;
            while (flag)
            {
                int y;
                try
                {
                    y = Convert.ToInt32(Console.ReadLine());
                    if (y <1 || y > NumberOfChoices)
                    {
                        Console.WriteLine(string.Format("Choice out of Range. Choice should be in between 1 and {0}.",NumberOfChoices));
                    }
                    else
                    {
                        flag = false;
                        return y;
                    }
                }
                catch
                {
                    Console.WriteLine("The choice should be an integer. You entered wrong type.");
                    flag = true;
                }
                flag = true;
            }
            return 0;
        }

        static void Main(string[] args)
        {
            bool flagAttribute = true;
            do
            {
                Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
                Console.WriteLine("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n6)Exit\n7)Write and Read XML\n8)Write and Read JSON");
                Console.Write("\nSelect the operation: ");
                int Choice = ChoiceInput(8);
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\t4)Back\nChoose one.");
                        int Choice2 = ChoiceInput(4);
                        ID++;
                        switch (Choice2)
                        {
                            case 1:
                                Teaching teacher = new Teaching();
                                teacher.AddStaff(ID);
                                staffList.Add(teacher);
                                break;
                            case 2:
                                Support supportStaff = new Support();
                                supportStaff.AddStaff(ID);
                                staffList.Add(supportStaff);
                                break;
                            case 3:
                                Administrative admininstrator = new Administrative();
                                admininstrator.AddStaff(ID);
                                staffList.Add(admininstrator);
                                break;
                            case 4:
                                break;
                            default: // no need of default
                                Console.WriteLine("Wrong choice");
                                break;
                        }
                        break;
                    case 2:
                        Staff staff = GetStaff();
                        if(staff != null)
                        {
                            StaffChildDetails(staff);
                        }

                        break;
                    case 3:
                        Console.WriteLine("Update");
                        Staff staffToUpdate = GetStaff();
                        if(staffToUpdate != null)
                        {
                            staffToUpdate.UpdateStaff(0);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Delete.");
                        Staff staffToDelete = GetStaff();
                        if(staffToDelete != null)
                        {
                            staffList.Remove(staffToDelete);
                            Console.WriteLine("Staff deleted.");
                        }
                        break;
                    case 5:
                        bool isNull = true;
                        foreach (var SingleStaff in staffList)
                        {
                            isNull = false;
                            StaffChildDetails(SingleStaff);
                            Console.WriteLine("");
                        }
                        if (isNull)
                        {
                            Console.WriteLine("No staffs found!!!");
                        }
                        break;
                    case 6:
                        flagAttribute = false;
                        break;
                    case 7:
                        WriteXML(staffList);
                        ReadXML();
                        break;
                    case 8:
                        WriteJSON(staffList);
                        ReadJSON();
                        break;
                    default:
                        Console.WriteLine("Wrong choice."); //no need of default
                        break;
                }                
            } while (flagAttribute == true);
            Console.ReadLine();
        }
        public static void WriteXML(List<Staff> staffList)
        {
            //List<Staff> staffList = new();// set this stafflist to parameter
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            var path = new System.IO.StreamWriter(@"C:\Work\JoyalTraining\Staff.Management\XMLSerialisation.xml");
            writer.Serialize(path, staffList);
            path.Close();
        }
        public static void ReadXML()
        {
            //List<Staff> staffList = new();// set this stafflist to parameter
            var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Staff>));
            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Work\JoyalTraining\Staff.Management\XMLSerialisation.xml");
            List<Staff> deserializedStaffList = (List<Staff>)reader.Deserialize(file);
            file.Close();

            bool isNull = true;
            foreach (var SingleStaff in staffList)
            {
                isNull = false;
                StaffChildDetails(SingleStaff);
                Console.WriteLine("");
            }
            if (isNull)
            {
                Console.WriteLine("No staffs found!!!");
            }
        }
        public static void WriteJSON(List<Staff> staffList)
        {
            var path = @"C:\Work\JoyalTraining\Staff.Management\JSONSerialisation.json";
            using (var stream = new FileStream(path, FileMode.Create))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                JSONStream.WriteObject(stream, staffList);
            }
        }
        public static void ReadJSON()
        {
            var path = @"C:\Work\JoyalTraining\Staff.Management\JSONSerialisation.json";
            using (var stream = new FileStream(path, FileMode.Open))
            {
                DataContractJsonSerializer JSONStream = new DataContractJsonSerializer(typeof(List<Staff>));
                List<Staff> JSONStaffList = (List<Staff>)JSONStream.ReadObject(stream);
                bool isNull = true;
                foreach (var SingleStaff in JSONStaffList)
                {
                    isNull = false;
                    StaffChildDetails(SingleStaff);
                    Console.WriteLine("");
                }
                if (isNull)
                {
                    Console.WriteLine("No staffs found!!!");
                }

            }
        }
    }
}
