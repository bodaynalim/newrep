using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class EmployeeFixed : EmployeeBase
    {
        public EmployeeFixed(int id, string name, string surname, int salary)
            :base(id, name, surname, salary)
        {
            
        }


        public override float Salary()
        {
            return this.MonthSalary;
        }
    }
}
