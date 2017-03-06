using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    abstract class EmployeeBase
    {
        private int id {get; set; }
        private string name { get; set; }
        private string surname { get; set; }
       private float _monthSalary;

        protected string Name
        {
            get { return name; }
            set { name = value; }

        }

       public EmployeeBase()
       {
           ID = 1;
           Name = "Petro";
           Surname = "Petrov";
           MonthSalary = 3200;
       }
        public EmployeeBase( int id, string name, string surname, int salary)
        {
            ID = id;
            Name = name;
            Surname = surname;
            MonthSalary = salary;
        }
        public EmployeeBase(int id, string name, string surname)
        {
            ID = id;
            Name = name;
            Surname = surname;
      
        }
        protected string Surname
        {
            get { return surname; }
            set { surname = value; }

        }

        protected int ID
        {
            get { return id; }
            set { id = value; }

        }

        protected float MonthSalary
        {
            get { return _monthSalary; }

            set { _monthSalary = value; }

        }

        public abstract float Salary();

        static void Write_to(List<EmployeeHourly> emp, List<EmployeeFixed> empf, string path)
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    for (int i = 0; i < emp.Count; i++)
                    {
                        sw.WriteLine(emp[i].ID + " " + emp[i].Name + " " + emp[i].Surname + " " + emp[i].Salary());
                    }
                    for (int i = 0; i < empf.Count; i++)
                    {
                        sw.WriteLine(emp[i].ID + " " + emp[i].Name + " " + emp[i].Surname + " " + emp[i].Salary());
                    }

                }

            }
            catch (Exception e)
            { 
            }
        }

        static string[] read_from_file(string path)
        {
            string[] empl = new string[] { };
            try
            {
                empl = File.ReadAllLines(path);
                return empl;
            }
            catch (Exception e)
            { 
            }


            return null;
        }

        static void Main(string[] args)
        {
            string path = @"employee.txt";

        
        
            EmployeeFixed[] emp1 = new EmployeeFixed[8];
            EmployeeHourly[] emp2 = new EmployeeHourly[8];
            Random pay = new Random();
            

            for (int i = 0; i < emp1.Length; i++)
            {
                emp1[i] = new EmployeeFixed(i,"Petro"+i,"Petrov"+i,pay.Next(3200,400));
                emp2[i] = new EmployeeHourly(i, "Ivanov" + i, "Ivan" + i, pay.Next(20, 30));
            }

            List<EmployeeFixed> empf = emp1.OrderBy(x =>x.Salary()).ThenBy(x=> x.Name).Select(x=>x).ToList();
            List<EmployeeHourly> emph = emp2.OrderBy(x => x.Salary()).ThenBy(x => x.Name).Select(x => x).ToList();

            for (int i=0; i < empf.Count; i++)
            {
                  Console.WriteLine("Salary Fixed:" + empf[i].ID + " "+ empf[i].Name+" "+ empf[i].Surname+" "+ empf[i].Salary());
                  Console.WriteLine("Salary Hourly:" + empf[i].ID + " " + empf[i].Name + " " + empf[i].Surname + " " + empf[i].Salary());
            }

            var firstfive = empf.Take(5);
           
            foreach (var employee in firstfive)
            {
                Console.WriteLine("FirstFive:" + employee.ID + " " + employee.Name);
            }

            var lasthree = empf.Skip(empf.Count-3);

            foreach (var employee in lasthree)
            {
                Console.WriteLine("Last Three:" + employee.ID);
            }

            Write_to(emph,empf,path);

            string[] empl = read_from_file(path);

            Console.WriteLine(string.Join(",", empl));

            Console.ReadLine();




        }
    }
}
