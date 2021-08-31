using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaffManagementConsole
{
    class Program
    {
        
        public static int ID { get; set; }
        private static List<Staff> StaffList = new();
        private static Staff GetStaff()
        {
            int id = ValidateStaffID();
            Staff staff = StaffList.FirstOrDefault(_staff => _staff.StaffID == id);
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
            bool FlagAttribute = true;
            do
            {
                Console.WriteLine("\tSTAFF MANAGEMENT\nSelect an operarion :");
                Console.WriteLine("1)Add a staff\n2)View a staff\n3)Update a staff\n4)Delete a staff\n5)View all staff\n6)Exit");
                Console.Write("\nSelect the operation: ");
                int Choice = ChoiceInput(6);
                switch (Choice)
                {
                    case 1:
                        Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\t4)Back\nChoose one.");
                        int Choice2 = ChoiceInput(4);
                        ID++;
                        switch (Choice2)
                        {
                            case 1:
                                Teaching Teacher = new Teaching();
                                Teacher.AddStaff(ID);
                                StaffList.Add(Teacher);
                                break;
                            case 2:
                                Support SupportStaff = new Support();
                                SupportStaff.AddStaff(ID);
                                StaffList.Add(SupportStaff);
                                break;
                            case 3:
                                Administrative Admininstrator = new Administrative();
                                Admininstrator.AddStaff(ID);
                                StaffList.Add(Admininstrator);
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
                            StaffList.Remove(staffToDelete);
                            Console.WriteLine("Staff deleted.");
                        }
                        break;
                    case 5:
                        bool IsNull = true;
                        foreach (var SingleStaff in StaffList)
                        {
                            IsNull = false;
                            StaffChildDetails(SingleStaff);
                            Console.WriteLine("");
                        }
                        if (IsNull)
                        {
                            Console.WriteLine("No staffs found!!!");
                        }
                        break;
                    case 6:
                        FlagAttribute = false;
                        break;
                    default:
                        Console.WriteLine("Wrong choice."); //no need of default
                        break;
                }                
            } while (FlagAttribute == true);
            Console.ReadLine();
        }
    }
}
