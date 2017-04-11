using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FindRegValue
{
    class Program
    {
        private static readonly List<string> _excludedValues = new List<string>()
        {
            "ImagePath",
            "Type",
            "Start",
            "ErrorControl",
            "DisplayName",
            "Owners",
            "Group",
            "Tag",
            "DependOnService",
            "Description",
            "SupportedFeatures",
            "BootFlags",
            "ObjectName",
            "ServiceSidType",
            "FailureActions",
            "RequiredPrivileges",
            "LaunchProtected",
            "DelayedAutostart",
            "DelayedAutoStart",
            "FailureActionsOnNonCrashFailures",
            "PreshutdownTimeout",
            "ServiceDll",
            "NdisMajorVersion",
            "NdisMinorVersion",
            "DriverMajorVersion",
            "DriverMinorVersion"

        };
        static void Main(string[] args)
        {
            RegistryKey reg = Registry.LocalMachine;

            List<string> values = new List<string>();

            var regServices = reg.OpenSubKey(@"SYSTEM\CurrentControlSet\Services");

            foreach (var v in regServices.GetSubKeyNames())
            {
               

                if (regServices.OpenSubKey(v).ValueCount != null)
                {
                    values.AddRange(regServices.OpenSubKey(v).GetValueNames());

                    Console.WriteLine(regServices.OpenSubKey(v)
                         .GetValueNames()
                               .Any(x => _excludedValues.Any(y => x.Equals(y))));
                    

                }

            }



            var result = values
                .Select(str => new {Name = str, Count = values.Count(s => s == str)})
                .Where(obj => obj.Count > 3)
                .Distinct()
                .ToDictionary(obj => obj.Name, obj => obj.Count);

            //foreach (var v in result)
            //{
            //    Console.WriteLine(v.Key + " (" + v.Value + ")");
            //}
                
            Console.ReadLine();


        }
    }
}
