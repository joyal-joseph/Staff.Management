using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary;
using System.Configuration;

namespace StaffManagementConsole
{
    class Program
    {
        public static int ID { get; set; }
        private static List<IStaffOperation> staffList = new();
        public static void SerializeAndPrintDeserializedData(ISerializationAndDeserialization serializeData, string path)
        {
            serializeData.Serialize(staffList, path); 
            var deserializedData = serializeData.DeSerialize(path);
            PrintStaffList(deserializedData);

        }
        public static void PrintStaffList(List<IStaffOperation> stafflist)
        {
            bool isNull = true;
            foreach (var singleStaff in staffList)
            {
                isNull = false;
                singleStaff.ViewStaff();
                Console.WriteLine("");
            }
            if (isNull)
            {
                Console.WriteLine("No staffs found!!!");
            }
        }
        private static IStaffOperation GetStaff()
        {
            int id = ValidateStaffID();
            IStaffOperation staff = staffList.FirstOrDefault(_staff => _staff.StaffID == id);
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
                Console.WriteLine("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n6)Write and Read XML\n7)Write and Read JSON\n8)Exit");
                Console.Write("\nSelect the operation: ");
                int Choice = ChoiceInput(8);
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\t4)Back\nChoose one.");
                        int Choice2 = ChoiceInput(4);
                        ID++;
                        IStaffOperation newStaff=null;//new
                        switch (Choice2)
                        {
                            case 1:
                                newStaff = new Teaching();
                                break;
                            case 2:
                                newStaff = new Support();
                                break;
                            case 3:
                                newStaff = new Administrative();
                                break;
                            case 4:
                                break;
                            default: // no need of default
                                Console.WriteLine("Wrong choice");
                                break;
                        }
                        newStaff.AddStaff(ID);
                        staffList.Add(newStaff);
                        break;
                    case 2:
                        IStaffOperation staff = GetStaff();
                        if(staff != null)
                        {
                            staff.ViewStaff();
                        }

                        break;
                    case 3:
                        Console.WriteLine("Update");
                        IStaffOperation staffToUpdate = GetStaff();
                        if(staffToUpdate != null)
                        {
                            staffToUpdate.UpdateStaff(0);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Delete.");
                        IStaffOperation staffToDelete = GetStaff();
                        if(staffToDelete != null)
                        {
                            staffList.Remove(staffToDelete);
                            Console.WriteLine("Staff deleted.");
                        }
                        break;
                    case 5:
                        PrintStaffList(staffList);
                        break;
                    
                    case 6:
                        var _appSettings = ConfigurationManager.AppSettings;
                        string _path = _appSettings["XMLDataPath"] ?? "not found";
                        SerializeAndPrintDeserializedData(new XMLSerializationAndDeserialization(), _path);
                        break;
                    case 7:
                        var appSettings = ConfigurationManager.AppSettings;
                        string path = appSettings["JSONDataPath"]??"not found";
                        
                        SerializeAndPrintDeserializedData(new JSONSerializationAndDeserialization(), path);
                        break;
                    case 8:
                        flagAttribute = false;
                        break;
                    default:
                        Console.WriteLine("Wrong choice."); //no need of default
                        break;
                }                
            } while (flagAttribute == true);
            Console.ReadLine();
        }
    }
}
