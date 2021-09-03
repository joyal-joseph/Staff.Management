using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary;
using System.Configuration;
using DocumentFormat.OpenXml;

namespace StaffManagementConsole
{
    class Program
    {
        public static int ID { get; set; }
        
        private static List<IStaffOperation> staffList = new();
        private static List<IStaffOperation> GetStoredData(int choice) 
        {
            switch (choice) 
            { 
                case 1:
                    //XML
                    ISerializationAndDeserialization deserializeObjectXML = new XMLSerializationAndDeserialization();
                    var _appSettings__ = ConfigurationManager.AppSettings;
                    string XMLPath = _appSettings__["XMLDataPath"] ?? "not found";
                    var deserializedXMLData = deserializeObjectXML.DeSerialize(XMLPath);
                    return deserializedXMLData;
                case 2:
                    //JSON
                    ISerializationAndDeserialization deserializeDataJSON = new JSONSerializationAndDeserialization();
                    var appSettings = ConfigurationManager.AppSettings;
                    string jsonPath = appSettings["JSONDataPath"] ?? "not found";
                    var deserializedJSONData = deserializeDataJSON.DeSerialize(jsonPath);
                    return deserializedJSONData;

            }
            return null; 
        }
        public static void PrintStaffList(List<IStaffOperation> stafflist)
        {
            bool isNull = true;
            foreach (var singleStaff in stafflist)
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
            ID = GetStoredData(1).Count;
            bool dataTakenFromStorage = false;
            do
            {
                Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
                Console.Write("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n");
                Console.WriteLine("6)Save data\n7)Read Data\n8)Exit");
                Console.Write("\nSelect the operation: ");
                int Choice = ChoiceInput(10);
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
                        //return first.Concat(second).ToList()
                        //PrintStaffList(staffList.Concat(GetStoredData(1)).ToList());
                        if (dataTakenFromStorage == false)
                        {
                            Console.WriteLine("Read data from option 7 to include stored data.");
                        }
                        PrintStaffList(staffList);
                        break;
                    
                    
                    case 8:
                        flagAttribute = false;
                        break;
                    case 6:
                        //save data
                        Console.WriteLine("1)Save as XML\n2)Save as JSON");
                        int dataChoiceForRead = ChoiceInput(2);
                        var _appSettings = ConfigurationManager.AppSettings;
                        switch (dataChoiceForRead)
                        {
                            case 1:
                                //XML
                                ISerializationAndDeserialization serializeDataXML = new XMLSerializationAndDeserialization();
                                string _XMLPath = _appSettings["XMLDataPath"] ?? "not found";
                                serializeDataXML.Serialize(staffList, _XMLPath);
                                break;
                            case 2:
                                //JSON
                                ISerializationAndDeserialization serializeDataJSON = new JSONSerializationAndDeserialization();
                                string _JSONPath = _appSettings["JSONDataPath"] ?? "not found";
                                serializeDataJSON.Serialize(staffList, _JSONPath);
                                break;
                        }
                        break;
                    case 7:
                        if (dataTakenFromStorage == false)
                        {
                            Console.WriteLine("1)Read as XML\n2)Read as JSON");
                            int choice = ChoiceInput(2);
                            staffList = staffList.Concat(GetStoredData(choice)).ToList();
                            dataTakenFromStorage = true;
                        }
                        else
                        {
                            Console.WriteLine("Data already included!!!");
                        }
                        PrintStaffList(staffList);
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


