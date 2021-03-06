﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OOP
{
    abstract public  class EmployeeBase
    {
        

        protected string name { get; set; }
        protected string surname { get; set; }
        protected int id { get; set; }
        protected float month_salary { get; set; }

        public EmployeeBase()
       {
           id = 1;
           name = "Petro";
           surname = "Petrov";
            month_salary = 3200;
       }
        public EmployeeBase( int id, string name, string surname, int salary=0)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.month_salary = salary;
        }



        public abstract float Salary { get; set; }



        static void WriteToFileEmpHourly(EmployeeHourly[] emp, string path)
        {
           
                XmlSerializer formatter = new XmlSerializer(typeof(EmployeeHourly[]));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                   formatter.Serialize(fs,emp);
                    
             }

            
        }
        static void WriteToFileEmpFixed(EmployeeFixed[] emp, string path)
        {
            
                XmlSerializer formatter = new XmlSerializer(typeof(EmployeeFixed[]));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    
                        formatter.Serialize(fs, emp);
                    
                }

          
        }

        static EmployeeHourly[] ReadFromFileEmpHourly(string path)
        {
            EmployeeHourly[] emp = null;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(EmployeeHourly[]));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    emp = (EmployeeHourly[]) formatter.Deserialize(fs);
                }

            }
            catch (Exception e)
            {
            }

            return emp;
        }

        static EmployeeFixed[] ReadFromFileEmpFixed(string path)
        {
            EmployeeFixed[] emp = null;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(EmployeeFixed[]));
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    emp = (EmployeeFixed[])formatter.Deserialize(fs);
                }

            }
            catch (Exception e)
            {
            }

            return emp;
        }




        static void Main(string[] args)
        {
           var pathEmpFixed = @"employeef.xml";
           var pathEmpHourly = @"employeeh.xml";

            var emp1 = new EmployeeFixed[8];
            var emp2 = new EmployeeHourly[8];
            Random pay = new Random();
            
            for (int i = 0; i < emp1.Length; i++)
            {
                emp1[i] = new EmployeeFixed(i,"Petro"+i,"Petrov"+i,pay.Next(3200,4000));
                emp2[i] = new EmployeeHourly(i, "Ivanov" + i, "Ivan" + i, pay.Next(20, 30));
            }

           EmployeeFixed[] empf = emp1.OrderBy(x =>x.Salary).ThenBy(x=> x.name).Select(x=>x).ToList().ToArray();
            EmployeeHourly[] emph = emp2.OrderBy(x => x.Salary).ThenBy(x => x.name).Select(x => x).ToList().ToArray();

            for (int i=0; i < empf.Length; i++)
            {
                  Console.WriteLine("Salary Fixed:" + empf[i].id + " "+ empf[i].name+" "+ empf[i].surname+" "+ empf[i].Salary);
                  Console.WriteLine("Salary Hourly:" + emph[i].id + " " + emph[i].name + " " + emph[i].surname + " " + emph[i].Salary);
            }

            foreach (var employee in empf.Take(5))
            {
                Console.WriteLine("FirstFive:" + employee.id + " " + employee.name);
            }
        
            foreach (var employee in empf.Skip(empf.Length - 3))
            {
                Console.WriteLine("Last Three:" + employee.id);
            }

            WriteToFileEmpHourly(emph,pathEmpHourly);
            WriteToFileEmpFixed(empf, pathEmpFixed);



         
            
            Console.ReadLine();




        }
    }
}
