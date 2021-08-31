﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagementConsole
{    
    public abstract class Staff
    {
        public int StaffID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public int DailyWage { get; set; }
        public enum GenderType
        {
            Male,
            Female,
            Other
        }
        public int AgeInput()
        {
            bool flag = true;
            int age = 0;
            while (flag)
            {
                Console.Write("Enter age: ");
                try
                {
                    age = Convert.ToInt32(Console.ReadLine());
                    if (age < 18 || age > 75)
                    {
                        Console.WriteLine("Age out of Range. Age should be in between 18 and 75.");
                    }
                    else
                    {
                        flag = false;
                        return age;
                    }
                }
                catch
                {
                    Console.WriteLine("The age should be an integer. You entered wrong type.");
                    flag = true;
                }
                flag = true;
            }
            return 0;
        }
        public string Input(string x)
        {
            bool flag = true;
            string name;
            while (flag)
            {
                Console.WriteLine(string.Format("Enter {0}",x));
                name =Console.ReadLine();
                if (name == "")
                {
                    flag = true;
                    Console.WriteLine(string.Format("The {0} cannot be empty.",x));
                }
                else
                {
                    return name;
                }
            }
            return (null);
        }
        public GenderType GenderInput()
        {
            bool flag = true;
            string gender;
            while (flag)
            {
                Console.WriteLine("Enter gender: [M/F/O]");
                gender = Console.ReadLine();
                if(gender == "")
                {
                    flag = true;
                    Console.WriteLine("Gender cannot be empty");
                }
                else if(gender=="M" || gender =="F" || gender=="O")
                {
                    switch (gender)
                    {
                        case "M":
                            return GenderType.Male;
                        case "F":
                            return GenderType.Female;
                        case "O":
                            return GenderType.Other;
                    }
                }
                else
                {
                    Console.WriteLine("Enter correct gender type.");
                    flag = true;
                }
            }
            return GenderType.Other;
        }
        public virtual void AddStaff(int ID)
        {
            this.Name = Input("name");
            this.Age = AgeInput();
            this.Gender = GenderInput();
            this.StaffID = ID;
        }
        public  virtual void ViewStaff()
        {
            Console.WriteLine(String.Format("ID:{3}\nName: {0}\nAge: {1}\nGender: {2}", this.Name, this.Age, this.Gender,this.StaffID));            
        }
        public virtual void UpdateStaff(int choice) 
        {
            switch (choice)
            {
                case 1:
                    this.Name = Input("name");
                    break;
                case 2:
                    this.Age = AgeInput();
                    break;
                case 3:
                    this.Gender = GenderInput();
                    break;
                default:
                    //no default value required here
                    break;
            }
        }
        public abstract void Salary();       
    }
}
