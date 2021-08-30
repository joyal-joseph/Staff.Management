using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staff_management_2
{
    
    public class Support : Staff
    {
        //public int DailyWage { get => DailyWage; set => DailyWage = 600; }
        public string Lab { get; set; }
        

        public override void AddStaff(int ID)
        {
            base.AddStaff(ID);
            this.Lab = Input("Lab department");
            this.DailyWage = 600;
        }
        public override void ViewStaff()
        {
            base.ViewStaff();
            Console.WriteLine(string.Format("Department: {0}\nDailywage: {1}", this.Lab, this.DailyWage));
        }
        

        public override void UpdateStaff(int X)
        {
            Console.WriteLine("1)Name 2)Age 3)Gender 4) Lab 5)Back");
            int Choice = Convert.ToInt32(Console.ReadLine());
            switch (Choice)
            {
                case 4:
                    this.Lab = Input("lab");
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
            Console.WriteLine(DailyWage*30);
        }

    }
}
