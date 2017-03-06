using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class EmployeeHourly : EmployeeBase
    {
        private float hourpay {get; set ;}

        

       public EmployeeHourly()
        {
            id = 1;
            name = "Petro";
            surname = "Petrov";
           hourpay = 20;

        }

        public EmployeeHourly(int id, string name, string surname, int hourSalary)
            : base(id, name, surname)
        {
           hourpay = hourSalary;
           month_salary = (float)(this.hourpay * 20.8 * 8);

        }

        public override float Salary()
        {
           
            return month_salary;
        }

        public static implicit operator EmployeeHourly(EmployeeFixed v)
        {
            throw new NotImplementedException();
        }
    }
}
