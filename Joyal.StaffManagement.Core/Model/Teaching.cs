using System;


namespace StaffManagementConsole
{
    public class Teaching: Staff
    {
        public string ClassTeacher { get; set; }
        public string Subject { get; set; }
        public override void AddOrUpdateStaff(int ID)
        { 
            base.AddOrUpdateStaff(ID);
            this.Subject = Input("subject");
            Console.WriteLine("class of duty as Class Teacher(if not hit enter )");
            //this.ClassTeacher = Input("class of duty as Class Teacher(if not  enter NA)");
            this.ClassTeacher = Console.ReadLine();
            this.DailyWage = 1000;
            this.JobType = "Teacher";

        }
        
        public override void ViewStaff()
        {
            base.ViewStaff();
            Console.WriteLine(string.Format("Subject: {0}\nDailywage: {1}\nClass teacher role: {2}", this.Subject, this.DailyWage,this.ClassTeacher));
            
        }
        

        public override void UpdateStaff(int X)
        {
            Console.WriteLine("1)Name 2)Age 3)Gender 4) Subject 5)Back");
            int Choice = Convert.ToInt32(Console.ReadLine());
            switch(Choice)
            {
                
                case 4:
                    this.Subject = Input("subject");
                    break;
                case 5:
                    break;
                default:
                    if (Choice == 1 || Choice == 2 || Choice == 3)
                    {
                        base.UpdateStaff(Choice);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Choice");
                        break;
                    }
            }
        }
        public override void Salary()
        {
            Console.WriteLine(DailyWage * 30);
        }


    }
}
