using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary;
using System.Configuration;
using DocumentFormat.OpenXml;
using StaffDatabaseHelper;

namespace StaffManagementConsole
{
    class Program
    {
        public static int ID { get; set; }
        
        private static List<IStaffOperation> staffList = new();
        static string SQLConnectionString = ConfigurationManager.AppSettings["SQLConnectionString"] ?? "not found";
        static string UseDB = ConfigurationManager.AppSettings["UseDB"] ?? "not found";
        static IStaffDatabaseHandler DBHandler = new StaffDatabaseSQLHandler(SQLConnectionString);
        private static List<IStaffOperation> GetStoredData(string choice) 
        {
            switch (choice) 
            { 
                case "XML":
                    //XML
                    ISerializationAndDeserialization deserializeObjectXML = new XMLSerializationAndDeserialization();
                    var _appSettings__ = ConfigurationManager.AppSettings;
                    string XMLPath = _appSettings__["XMLDataPath"] ?? "not found";
                    var deserializedXMLData = deserializeObjectXML.DeSerialize(XMLPath);
                    return deserializedXMLData;
                case "JSON":
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
            bool dataTakenFromStorage = false;
            //######### NON-DB SCENARIO ############################
            if (UseDB != "True")
            {
                do
                {
                    ID = GetStoredData(UseDB).Count;
                    //ID = 0;
                    Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
                    Console.Write("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n");
                    Console.WriteLine("6)Export data\n7)Import Data\n8)Exit");
                    Console.Write("\nSelect the operation: ");
                    int Choice = ChoiceInput(8);
                    switch (Choice)
                    {
                        case 1:
                            Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\nChoose one.");
                            int Choice2 = ChoiceInput(4);
                            ID++;
                            IStaffOperation newStaff = null;//new
                            switch (Choice2)
                            {
                                case 1:
                                    newStaff = new Teaching { JobType = "Teacher" };
                                    //newStaff.JobType = 'Teacher';
                                    break;
                                case 2:
                                    newStaff = new Support { JobType = "Support" };
                                    break;
                                case 3:
                                    newStaff = new Administrative { JobType = "Admin" };
                                    break;
                                default: // no need of default
                                    Console.WriteLine("Wrong choice");
                                    break;
                            }
                            newStaff.AddOrUpdateStaff(ID);
                            staffList.Add(newStaff);
                            break;
                        case 2:
                            Console.WriteLine("Use StaffID in DB to ViewStaff");
                            IStaffOperation staff = GetStaff();

                            if (staff != null)
                            {
                                staff.ViewStaff();
                            }
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
                            //save data
                            var _appSettings = ConfigurationManager.AppSettings;
                            switch (UseDB)
                            {
                                case "XML":
                                    //XML
                                    ISerializationAndDeserialization serializeDataXML = new XMLSerializationAndDeserialization();
                                    string _XMLPath = _appSettings["XMLDataPath"] ?? "not found";
                                    serializeDataXML.Serialize(staffList, _XMLPath);
                                    break;
                                case "JSON":
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
                                staffList = staffList.Concat(GetStoredData(UseDB)).ToList();
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
                            Console.WriteLine("Wrong choice."); //no need of default
                            break;
                    }
                } while (flagAttribute == true);
            }
            //#######   USING DB STARTS HERE ##########

            if (UseDB == "True")
            {
                bool DBflagAttribute = true;
                do
                {
                    Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
                    Console.Write("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n");
                    Console.WriteLine("6)Export data from DB\n7)Import Data and export to DB\n8)Exit");
                    Console.Write("\nSelect the operation: ");
                    int Choice = ChoiceInput(8);
                    switch (Choice)
                    {
                        case 1:
                            Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\nChoose one.");
                            int Choice2 = ChoiceInput(3);
                            IStaffOperation newStaff = null;//new
                            switch (Choice2)
                            {
                                case 1:
                                    newStaff = new Teaching { JobType = "Teacher" };
                                    //newStaff.JobType = 'Teacher';
                                    break;
                                case 2:
                                    newStaff = new Support { JobType = "Support" };
                                    break;
                                case 3:
                                    newStaff = new Administrative { JobType = "Admin" };
                                    break;
                                default: // no need of default
                                    Console.WriteLine("Wrong choice");
                                    break;
                            }
                            newStaff.AddOrUpdateStaff(ID);
                            DBHandler.AddStaff(newStaff);
                            break;

                        case 2:
                            Console.WriteLine("Use StaffID in DB to ViewStaff");
                            int _id = ValidateStaffID();
                            IStaffOperation _staff = DBHandler.ViewStaff(_id);

                            if (_staff != null)
                            {
                                _staff.ViewStaff();
                            }
                            else if (_staff == null)
                            { 
                                Console.WriteLine("No staff found!!!"); 
                            }

                            break;
                            
                        case 3:
                            Console.Write("Enter ID in DB to update: ");
                            int id = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\t4)Back\nChoose one.");
                            int Choice3 = ChoiceInput(4);
                            IStaffOperation UpdateStaff = null;//new
                            switch (Choice3)
                            {
                                case 1:
                                    UpdateStaff = new Teaching { JobType = "Teacher" };
                                    //newStaff.JobType = 'Teacher';
                                    break;
                                case 2:
                                    UpdateStaff = new Support { JobType = "Support" };
                                    break;
                                case 3:
                                    UpdateStaff = new Administrative { JobType = "Admin" };
                                    break;
                                default: 
                                    Console.WriteLine("Wrong choice");
                                    break;
                            }
                            UpdateStaff.AddOrUpdateStaff(id);
                            DBHandler.UpdateStaff(UpdateStaff, id);
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
                            //take data from DB and added to XML/JSON
                            Console.WriteLine("1)Save as XML\n2)Save as JSON");
                            int dataChoiceForRead = ChoiceInput(2);
                            staffList = DBHandler.ViewAllStaff();  //read from DB and store to XML/JSON as choice
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
                            //ReadData from XML/JSON and store it to DB (bulk insert)
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
                                DBHandler.ViewAllStaff();
                                dataTakenFromStorage = true;
                            }
                            else
                            {
                                Console.WriteLine("Data already included!!!");
                            }
                            PrintStaffList(staffList);
                            break;
                        case 8:
                            DBflagAttribute = false;
                            break;
                        default:
                            Console.WriteLine("Wrong choice."); //no need of default
                            break;
                    }
                } while (DBflagAttribute == true);
            }
            Console.ReadLine();
        }
    }
}


