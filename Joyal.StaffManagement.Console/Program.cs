using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary;
using System.Configuration;
using StaffDatabaseHelper;

namespace StaffManagementConsole
{
    class Program
    {
        public static int ID { get; set; }

        private static List<IStaffOperation> staffList = new();
        static string DataMode = ConfigurationManager.AppSettings["DataMode"];
        static IStaffDatabaseHandler DBHandler = new StaffDatabaseSQLHandler(ConfigurationManager.AppSettings["SQLConnectionString"]);

        private static List<IStaffOperation> GetStoredData(string choice)
        {
            List<IStaffOperation> storedStaffList = new();
            switch (choice)
            {
                case "XML":
                    ISerializationAndDeserialization deserializeObjectXML = new XMLSerializationAndDeserialization();
                    var _appSettings__ = ConfigurationManager.AppSettings;
                    string XMLPath = _appSettings__["XMLDataPath"];
                    var deserializedXMLData = deserializeObjectXML.DeSerialize(XMLPath);
                    storedStaffList = deserializedXMLData;
                    break;
                case "JSON":
                    ISerializationAndDeserialization deserializeDataJSON = new JSONSerializationAndDeserialization();
                    var appSettings = ConfigurationManager.AppSettings;
                    string jsonPath = appSettings["JSONDataPath"];
                    var deserializedJSONData = deserializeDataJSON.DeSerialize(jsonPath);
                    storedStaffList = deserializedJSONData;
                    break;
            }
            return storedStaffList;
        }
        public static void PrintStaffList(List<IStaffOperation> stafflist)
        {
            foreach (var singleStaff in stafflist)
            {
                singleStaff.ViewStaff();
                Console.WriteLine("");
            }
            if (stafflist is null)
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
            int id = 0;
            while (true)
            {
                Console.WriteLine("Enter ID : ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    break;
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
            while (true)
            {
                int y;
                try
                {
                    y = Convert.ToInt32(Console.ReadLine());
                    if (y < 1 || y > NumberOfChoices)
                    {
                        Console.WriteLine(string.Format("Choice out of Range. Choice should be in between 1 and {0}.", NumberOfChoices));
                    }
                    else
                    {
                        return y;
                    }
                }
                catch
                {
                    Console.WriteLine("The choice should be an integer. You entered wrong type.");
                }
            }
        }
        public static void FirstMessageOnConsole()
        {
            Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
            Console.Write("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n");
        }
        public static IStaffOperation AddOrUpdateStaffInSwitch(int ID)
        {
            Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\nChoose one.");
            int Choice2 = ChoiceInput(4);
            IStaffOperation newStaff = null;
            switch (Choice2)
            {
                case 1:
                    newStaff = new Teaching { JobType = "Teacher" };
                    break;
                case 2:
                    newStaff = new Support { JobType = "Support" };
                    break;
                case 3:
                    newStaff = new Administrative { JobType = "Admin" };
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    break;
            }
            newStaff.AddOrUpdateStaff(ID);
            return newStaff;
        }
        public static void ViewStaffInCaseTwo(IStaffOperation _staff)
        {
            if (_staff != null)
            {
                _staff.ViewStaff();
            }
            else
            {
                Console.WriteLine("No staff found!!!");
            }
        }
        public static void SerializeInCaseSix(string SerializeMode)
        {
            var _appSettings = ConfigurationManager.AppSettings;
            switch (SerializeMode)
            {
                case "XML":
                    ISerializationAndDeserialization serializeDataXML = new XMLSerializationAndDeserialization();
                    string _XMLPath = _appSettings["XMLDataPath"];
                    serializeDataXML.Serialize(staffList, _XMLPath);
                    break;
                case "JSON":
                    ISerializationAndDeserialization serializeDataJSON = new JSONSerializationAndDeserialization();
                    string _JSONPath = _appSettings["JSONDataPath"];
                    serializeDataJSON.Serialize(staffList, _JSONPath);
                    break;
            }
        }

        static void Main(string[] args)
        {
            bool flagAttribute = true;
            bool dataTakenFromStorage = false;
            //######## NON-DB SCENARIO #######
            if (DataMode != "DB")
            {
                do
                {
                    ID = GetStoredData(DataMode).Count;
                    FirstMessageOnConsole();
                    Console.WriteLine("6)Export data\n7)Import Data\n8)Exit");
                    Console.Write("\nSelect the operation: ");
                    int Choice = ChoiceInput(8);
                    switch (Choice)
                    {
                        case 1:
                            ID++;
                            staffList.Add(AddOrUpdateStaffInSwitch(ID));
                            break;
                        case 2:
                            Console.WriteLine("Use StaffID in DB to ViewStaff");
                            IStaffOperation staff = GetStaff();
                            ViewStaffInCaseTwo(staff);
                            break;
                        case 3:
                            IStaffOperation staffToUpdate = GetStaff();
                            if (staffToUpdate != null)
                            {
                                staffToUpdate.UpdateStaff(0);
                            }
                            break;

                        case 4:
                            Console.WriteLine("Delete.");
                            IStaffOperation staffToDelete = GetStaff();
                            if (staffToDelete != null)
                            {
                                staffList.Remove(staffToDelete);
                                Console.WriteLine("Staff deleted.");
                            }
                            break;
                        case 5:
                            if (dataTakenFromStorage == false)
                            {
                                Console.WriteLine("Read data from option 7 to include stored data.");
                            }
                            PrintStaffList(staffList);
                            break;

                        case 6:
                            SerializeInCaseSix(DataMode);
                            break;
                        case 7:
                            if (dataTakenFromStorage == false)
                            {
                                staffList = staffList.Concat(GetStoredData(DataMode)).ToList();
                                dataTakenFromStorage = true;
                            }
                            else
                            {
                                Console.WriteLine("Data already included!!!");
                            }
                            PrintStaffList(staffList);
                            break;

                        case 8:
                            flagAttribute = false;
                            break;
                        default:
                            Console.WriteLine("Wrong choice.");
                            break;
                    }
                } while (flagAttribute == true);
            }
            //#######   USING DB STARTS HERE ##########

            if (DataMode == "DB")
            {
                bool DBflagAttribute = true;
                do
                {
                    FirstMessageOnConsole();
                    Console.WriteLine("6)Import data from DB\n7)Import Data and export to DB\n8)Exit");
                    Console.Write("\nSelect the operation: ");
                    int Choice = ChoiceInput(8);
                    switch (Choice)
                    {
                        case 1:
                            DBHandler.AddStaff(AddOrUpdateStaffInSwitch(ID));
                            break;

                        case 2:
                            Console.WriteLine("Use StaffID in DB to ViewStaff");
                            int _id = ValidateStaffID();
                            IStaffOperation _staff = DBHandler.ViewStaff(_id);
                            ViewStaffInCaseTwo(_staff);
                            break;

                        case 3:
                            Console.Write("Enter ID in DB to update: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            DBHandler.UpdateStaff(AddOrUpdateStaffInSwitch(id), id);
                            break;

                        case 4:
                            Console.WriteLine("Use StaffID in DB to delete");
                            int Id = ValidateStaffID();
                            DBHandler.DeleteStaff(Id);
                            break;

                        case 5:
                            Console.WriteLine("View All Staff");
                            List<IStaffOperation> staff_List = DBHandler.ViewAllStaff();
                            PrintStaffList(staff_List);
                            break;

                        case 6:
                            staffList = DBHandler.ViewAllStaff();
                            Console.WriteLine("1)Save as XML\n2)Save as JSON");
                            int dataChoiceForSerialize = ChoiceInput(2);
                            string SerializingMode = (dataChoiceForSerialize == 1) ? "XML" : "JSON";
                            SerializeInCaseSix(SerializingMode);
                            break;
                        case 7:
                            if (dataTakenFromStorage == false)
                            {
                                Console.WriteLine("1)Read from XML and export to DB\n2)Read from JSON and export to DB");
                                int choice = ChoiceInput(2);
                                string importChoice;
                                if (choice == 1)
                                { importChoice = "XML"; }
                                else
                                { importChoice = "JSON"; }

                                staffList = GetStoredData(importChoice);
                                DBHandler.BulkInsert(staffList);
                                dataTakenFromStorage = true;
                            }
                            else
                            {
                                Console.WriteLine("Data already included!!!");
                            }
                            break;
                        case 8:
                            DBflagAttribute = false;
                            break;
                        default:
                            Console.WriteLine("Wrong choice.");
                            break;
                    }
                } while (DBflagAttribute == true);
            }
            Console.ReadLine();
        }
    }
}


