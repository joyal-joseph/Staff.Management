using System;
using static StaffManagementConsole.Staff;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace StaffManagementConsole
{
    [XmlInclude(typeof(Teaching))]
    [XmlInclude(typeof(Administrative))]
    [XmlInclude(typeof(Support))]
    [KnownType(typeof(Teaching))]
    [KnownType(typeof(Administrative))]
    [KnownType(typeof(Support))]

    public abstract class Staff : IStaffOperation
    {
        public string JobType { get; set; }
        public int StaffID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public int DailyWage { get; set; }
        public enum GenderType
        {
            M,
            F,
            O
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
            string name = null;
            while (flag)
            {
                Console.WriteLine(string.Format("Enter {0}",x));
                name =Console.ReadLine();
                if (name == "")
                {
                    Console.WriteLine(string.Format("The {0} cannot be empty.",x));
                }
                else
                {
                    flag = false;
                }
            }
            return name;
        }
        public GenderType GenderInput()
        {
            string gender;
            GenderType Gender= new();
            while (true)
            {
                Console.WriteLine("Enter gender: [M/F/O]");
                gender = Console.ReadLine();
                 if (Enum.TryParse<GenderType>(gender, out Gender) )
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Enter correct gender type.");
                }
            }
            return Gender;
        }
        public virtual void AddOrUpdateStaff(int ID)
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
            }
        }
        public abstract void Salary();       
    }
    public interface IStaffOperation
    {
        public string JobType { get; set; }
        public int StaffID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public int DailyWage { get; set; }
        
        public int AgeInput();
        public void AddOrUpdateStaff(int x);
        public void ViewStaff();
        public void UpdateStaff(int choice);
        public GenderType GenderInput();
    }
}
