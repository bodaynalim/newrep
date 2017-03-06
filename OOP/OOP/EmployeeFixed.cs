using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class EmployeeFixed : EmployeeBase
    {
        public EmployeeFixed()
           : base()
        {

        }
        public EmployeeFixed(int id, string name, string surname, int salary)
            :base(id, name, surname, salary)
        {
            
        }

        }
}
