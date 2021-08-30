using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace staff_management_2
{
    class Program
    {
        
         public static int ID { get; set; }
        public static List<Staff> StaffList = new();
        public static bool IsAStaff(int id)
        {
            foreach(var staff in StaffList)
            {
                if (staff.StaffID == id)
                {
                    return true;
                }
            }
            return false;
        }
        public static int StaffExistance()
        {
            bool IDFlag = true;
            int id=0;
            while (IDFlag)
            {
                Console.WriteLine("Enter ID : ");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    if (!IsAStaff(id))
                    {
                        Console.WriteLine("No staff with ID{0}", id);
                        return 0;
                    }
                    IDFlag = false;
                }
                catch
                {
                    Console.WriteLine("Wrong input type.");
                }
            }
            return id;
        }
        public static void StaffChildDetails(Staff staff)
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
        public static void ViewStaff(int id)
        {
            Staff staff =null;
            foreach (var SingleStaff in StaffList)
            {
                if(SingleStaff.StaffID == id)
                {
                    staff = SingleStaff;
                }
            }
            StaffChildDetails(staff);
        }
        public static int ChoiceInput(int NumberOfChoices)
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
                        Console.WriteLine("1)Teaching staff\t2)Support staff\t3)Administrative staff\t4)Exit\nChoose one.");
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
                        //view staff      
                        ViewStaff(StaffExistance());
                        break;
                    case 3:
                        Console.WriteLine("Update");                       
                        int ViewID = StaffExistance();
                        foreach (var staff in StaffList)
                        {
                            if(staff.StaffID == ViewID)
                            {
                                staff.UpdateStaff(0);
                                break;
                            }
                        }
                        break;
                    case 4:
                        Console.WriteLine("Delete.");
                        int DeleteID = StaffExistance();
                        foreach (var staff in StaffList)
                        {
                            if (staff.StaffID == DeleteID)
                            {
                                StaffList.Remove(staff);
                                break;
                            }
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
