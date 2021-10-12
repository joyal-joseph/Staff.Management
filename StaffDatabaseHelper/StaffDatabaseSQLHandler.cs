using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StaffManagementConsole;
using System.Data;
using static StaffManagementConsole.Staff;

namespace StaffDatabaseHelper
{
    public class StaffDatabaseSQLHandler : IStaffDatabaseHandler
    {
        private string sQL_ConnectionString;
        public StaffDatabaseSQLHandler(string sQLConnectionString)
        {
            this.sQL_ConnectionString = sQLConnectionString;
        }
        public void BulkInsert(List<IStaffOperation> staffList) 
        {
            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString)) 
            {
                int tempID = 1;
                SQLConnection.Open();
                SqlCommand BulkInsertCommand = SQLConnection.CreateCommand();
                BulkInsertCommand.CommandType = CommandType.StoredProcedure;
                BulkInsertCommand.CommandText = "BulkInsert";

                DataTable UDT_BulkInsertStaffDetails = new DataTable();
                UDT_BulkInsertStaffDetails.Columns.Add("TempID", typeof(int));
                UDT_BulkInsertStaffDetails.Columns.Add("StaffName", typeof(string));
                UDT_BulkInsertStaffDetails.Columns.Add("age", typeof(int));
                UDT_BulkInsertStaffDetails.Columns.Add("Gender", typeof(string));
                UDT_BulkInsertStaffDetails.Columns.Add("Salary", typeof(int));

                DataTable UDT_StaffJobFieldsData = new DataTable();
                UDT_StaffJobFieldsData.Columns.Add("TempID", typeof(int));
                UDT_StaffJobFieldsData.Columns.Add("JobField", typeof(string));
                UDT_StaffJobFieldsData.Columns.Add("JobFieldData", typeof(string));

                for (int i=0; i<staffList.Count; i++ ) 
                {
                    if (staffList[i].JobType == "Teacher")
                    {
                        Teaching staff = (Teaching)staffList[i];
                        UDT_BulkInsertStaffDetails.Rows.Add(tempID, staff.Name, staff.Age, staff.Gender, staff.DailyWage);
                        if (staff.Subject != null)
                        {
                            UDT_StaffJobFieldsData.Rows.Add(tempID, "Subject", staff.Subject);
                        }
                        if (staff.ClassTeacher != "")
                        {
                            UDT_StaffJobFieldsData.Rows.Add(tempID, "Class Teacher", staff.ClassTeacher);
                        }
                        tempID++;
                    }
                    else if (staffList[i].JobType == "Support")
                    {
                        Support staff = (Support)staffList[i];
                        UDT_BulkInsertStaffDetails.Rows.Add(tempID, staff.Name, staff.Age, staff.Gender, staff.DailyWage);
                        if (staff.Lab != null)
                        {
                            UDT_StaffJobFieldsData.Rows.Add(tempID, "Department", staff.Lab);
                        }
                        tempID++;
                    }
                    else if(staffList[i].JobType == "Admin")
                    {
                        Administrative staff = (Administrative)staffList[i];
                        UDT_BulkInsertStaffDetails.Rows.Add(tempID, staff.Name, staff.Age, staff.Gender, staff.DailyWage);
                        if (staff.Section != null)
                        {
                            UDT_StaffJobFieldsData.Rows.Add(tempID, "Section", staff.Section);
                        }
                        tempID++;
                    }
                }
                BulkInsertCommand.Parameters.AddWithValue("UDT_BulkInsertStaffDetails", UDT_BulkInsertStaffDetails);
                BulkInsertCommand.Parameters.AddWithValue("UDT_StaffJobFieldsData", UDT_StaffJobFieldsData);
                BulkInsertCommand.ExecuteNonQuery();
            }
        }
        public void AddStaff(IStaffOperation staff)
        {
            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString))
            {

                SQLConnection.Open();
                SqlCommand AddStaffCommand = SQLConnection.CreateCommand();
                AddStaffCommand.CommandType = CommandType.StoredProcedure;
                AddStaffCommand.CommandText = "AddStaff";
                AddStaffCommand.Parameters.Add("@StaffName", SqlDbType.VarChar, 100).Value = staff.Name;
                AddStaffCommand.Parameters.Add("@Age", SqlDbType.Int).Value = staff.Age;
                AddStaffCommand.Parameters.Add("@Gender", SqlDbType.Char, 1).Value = staff.Gender;
                AddStaffCommand.Parameters.Add("@Salary", SqlDbType.Int).Value = staff.DailyWage;
                AddStaffCommand.Parameters.Add("@JobType", SqlDbType.VarChar, 100).Value = staff.JobType;

                DataTable UDT_JobField = new DataTable();
                UDT_JobField.Columns.Add("JobFieldId", typeof(int));
                UDT_JobField.Columns.Add("JobField", typeof(string));
                UDT_JobField.Columns.Add("JobFieldData", typeof(string));
                switch (staff.JobType)
                {
                    case "Teacher":
                        Teaching newTeacher = (Teaching)staff;
                        UDT_JobField.Rows.Add(1, "Subject", newTeacher.Subject);
                        if (newTeacher.ClassTeacher != "")
                        {
                            UDT_JobField.Rows.Add(2, "Class Teacher", newTeacher.ClassTeacher);
                        }
                        break;
                    case "Support":
                        Support newSupport = (Support)staff;
                        UDT_JobField.Rows.Add(1, "Department", newSupport.Lab);
                        break;
                    case "Admin":
                        Administrative newAdmin = (Administrative)staff;
                        UDT_JobField.Rows.Add(1, "Section", newAdmin.Section);
                        break;
                }

                AddStaffCommand.Parameters.AddWithValue("UDT_JobField", UDT_JobField);
                AddStaffCommand.ExecuteNonQuery();
            }
            Console.WriteLine("Staff added to DB successfully");
        }
        public IStaffOperation ViewStaff(int StaffID) {

            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString))
            {
                SQLConnection.Open();
                SqlCommand ViewStaffCommand = SQLConnection.CreateCommand();
                ViewStaffCommand.CommandType = CommandType.StoredProcedure;
                ViewStaffCommand.CommandText = "ViewStaff";
                ViewStaffCommand.Parameters.Add("@ID", SqlDbType.Int).Value = StaffID;
                try
                {
                    SqlDataReader reader = ViewStaffCommand.ExecuteReader();
                    Object[] QueryResult = new Object[10];

                    IStaffOperation staff = null;
                    while (reader.Read())
                    {
                        reader.GetValues(QueryResult);


                        switch (QueryResult[5])
                        {
                            case "Teacher":
                                staff = new Teaching();
                                ((Teaching)staff).Subject = Convert.ToString(reader.GetValue(reader.GetOrdinal("Subject")));
                                ((Teaching)staff).ClassTeacher = Convert.ToString(reader.GetValue(reader.GetOrdinal("Class Teacher")));

                                break;
                            case "Support":
                                staff = new Support();
                                ((Support)staff).Lab = Convert.ToString(reader.GetValue(reader.GetOrdinal("Support")));

                                break;
                            case "Admin":
                                staff = new Administrative();
                                ((Administrative)staff).Section = Convert.ToString(reader.GetValue(reader.GetOrdinal("Administrative"))); 

                                break;
                        }

                        staff.StaffID = Convert.ToInt32((reader.GetValue(reader.GetOrdinal("StaffID"))));
                        staff.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("StaffName")));
                        Enum.TryParse(Convert.ToString(reader.GetValue(reader.GetOrdinal("Gender"))), out GenderType Gender);
                        staff.Gender = Gender;
                        staff.Age = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("age")));
                        staff.DailyWage = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Salary")));
                        staff.JobType = Convert.ToString(reader.GetValue(reader.GetOrdinal("JobType")));
                        return staff;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return null;
            }
        }
        public void DeleteStaff(int StaffID)
        {
            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString))
            {
                SQLConnection.Open();
                SqlCommand DeleteStaffCommand = SQLConnection.CreateCommand();
                DeleteStaffCommand.CommandType = CommandType.StoredProcedure;
                DeleteStaffCommand.CommandText = "DeleteStaff";
                DeleteStaffCommand.Parameters.Add("@ID", SqlDbType.Int).Value = StaffID;
                Console.WriteLine("Staff deleted from DB successfully.");
                DeleteStaffCommand.ExecuteNonQuery();
            }
        }
        public void UpdateStaff(IStaffOperation staff, int StaffID)
        {
            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString))
            {
                SQLConnection.Open();
                SqlCommand UpdateStaffCommand = SQLConnection.CreateCommand();
                UpdateStaffCommand.CommandType = CommandType.StoredProcedure;
                UpdateStaffCommand.CommandText = "UpdateStaff";
                UpdateStaffCommand.Parameters.Add("@StaffName", SqlDbType.VarChar, 100).Value = staff.Name;
                UpdateStaffCommand.Parameters.Add("@Age", SqlDbType.Int).Value = staff.Age;
                UpdateStaffCommand.Parameters.Add("@Gender", SqlDbType.Char, 1).Value = staff.Gender;
                UpdateStaffCommand.Parameters.Add("@Salary", SqlDbType.Int).Value = staff.DailyWage;
                UpdateStaffCommand.Parameters.Add("@JobType", SqlDbType.VarChar, 100).Value = staff.JobType;
                UpdateStaffCommand.Parameters.Add("@StaffID", SqlDbType.Int).Value = StaffID;

                DataTable UDT_JobField = new DataTable();
                UDT_JobField.Columns.Add("JobFieldId", typeof(int));
                UDT_JobField.Columns.Add("JobField", typeof(string));
                UDT_JobField.Columns.Add("JobFieldData", typeof(string));
                switch (staff.JobType)
                {
                    case "Teacher":
                        Teaching newTeacher = (Teaching)staff;
                        UDT_JobField.Rows.Add(1, "Subject", newTeacher.Subject);
                        if (newTeacher.ClassTeacher != "")
                        {
                            UDT_JobField.Rows.Add(2, "Class Teacher", newTeacher.ClassTeacher);
                        }
                        break;
                    case "Support":
                        Support newSupport = (Support)staff;
                        UDT_JobField.Rows.Add(1, "Department", newSupport.Lab);
                        break;
                    case "Admin":
                        Administrative newAdmin = (Administrative)staff;
                        UDT_JobField.Rows.Add(1, "Section", newAdmin.Section);
                        break;
                }

                UpdateStaffCommand.Parameters.AddWithValue("UDT_JobField", UDT_JobField);
                UpdateStaffCommand.ExecuteNonQuery();
                Console.WriteLine("Staff updated to DB successfully");
            }
        }
        public List<IStaffOperation> ViewAllStaff()
        {
            List<IStaffOperation> staffList = new();
            using (SqlConnection SQLConnection = new SqlConnection(sQL_ConnectionString))
            {
                SQLConnection.Open();
                SqlCommand ViewAllStaffCommand = SQLConnection.CreateCommand();
                ViewAllStaffCommand.CommandType = CommandType.StoredProcedure;
                ViewAllStaffCommand.CommandText = "ViewAllStaff";
                try
                {
                    SqlDataReader reader = ViewAllStaffCommand.ExecuteReader();
                    IStaffOperation staff = null;
                    while (reader.Read())
                    {
                        
                        Object[] QueryResult = new Object[10];
                        reader.GetValues(QueryResult);
                        switch (QueryResult[5])
                        {
                            case "Teacher":
                                staff = new Teaching();
                                ((Teaching)staff).Subject = Convert.ToString(reader.GetValue(reader.GetOrdinal("Subject")));
                                ((Teaching)staff).ClassTeacher = Convert.ToString(reader.GetValue(reader.GetOrdinal("Class Teacher")));

                                break;
                            case "Support":
                                staff = new Support();
                                ((Support)staff).Lab = Convert.ToString(reader.GetValue(reader.GetOrdinal("Support")));

                                break;
                            case "Admin":
                                staff = new Administrative();
                                ((Administrative)staff).Section = Convert.ToString(reader.GetValue(reader.GetOrdinal("Administrative")));

                                break;
                        }
                        staff.StaffID = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("StaffID")));
                        staff.Name = Convert.ToString(reader.GetValue(reader.GetOrdinal("StaffName")));
                        Enum.TryParse(Convert.ToString(reader.GetValue(reader.GetOrdinal("Gender"))), out GenderType Gender);
                        staff.Gender = Gender;
                        staff.Age = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("age")));
                        staff.DailyWage = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Salary")));
                        staff.JobType = Convert.ToString(reader.GetValue(reader.GetOrdinal("JobType")));
                        staffList.Add(staff);
                    }

                    return staffList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }
    }
}

