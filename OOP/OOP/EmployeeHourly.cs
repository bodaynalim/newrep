using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class EmployeeHourly : EmployeeBase
    {
        private float hour_pay {get; set ;}

        public float HourPay
        {
            get { return hour_pay; }
            set { hour_pay=value; }
        }

       public EmployeeHourly()
        {
            ID = 1;
            Name = "Petro";
            Surname = "Petrov";
            HourPay = 20;

        }

        public EmployeeHourly(int id, string name, string surname, int hour_salary)
            : base(id, name, surname)
        {
            HourPay = hour_salary;

        }

        public override float Salary()
        {
            MonthSalary = (float) (this.HourPay * 20.8 * 8);
            return MonthSalary;
        }

        public static implicit operator EmployeeHourly(EmployeeFixed v)
        {
            throw new NotImplementedException();
        }
    }
}
