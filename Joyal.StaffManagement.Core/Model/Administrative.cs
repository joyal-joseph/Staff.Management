using System;



namespace StaffManagementConsole
{
    public class Administrative : Staff
    {
        public string Section { get; set; }
        public override void AddStaff(int ID)
        {

            base.AddStaff(ID);
            this.Section = Input("section");
            this.DailyWage = 800;
        }
        public override void ViewStaff()
        {
            base.ViewStaff();
            Console.WriteLine(string.Format("Section: {0}\nDailywage: {1}", this.Section, this.DailyWage));
        }


        public override void UpdateStaff(int X)
        {
            Console.WriteLine("1)Name 2)Age 3)Gender 4) Section 5)Back");
            int Choice = Convert.ToInt32(Console.ReadLine());
            switch (Choice)
            {
                case 4:
                    this.Section = Input("section");
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
