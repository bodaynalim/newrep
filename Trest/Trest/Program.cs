using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Trest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] NewFile = File.ReadAllLines("sample.txt");
           IEnumerable<string> numbers = ( NewFile.Where(x => new Regex(@"^\d+$").IsMatch(x) )).ToList();


            IEnumerable<int> number = numbers.Select(x=>Int32.Parse(x)).ToList();

            foreach (string k in numbers)
            {
                Console.WriteLine(k);
            }

            Console.WriteLine("Sum: " + number.Sum());
            Console.WriteLine("Average: " + number.Average());
            Console.WriteLine("MIn: "+number.Min());
            Console.WriteLine("Max: " + number.Max());
            Console.ReadLine();
        }
    }
}
