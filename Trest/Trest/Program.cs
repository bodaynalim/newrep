using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Data;
using System.Diagnostics;
using Microsoft.Win32;

namespace Trest
{
    public class RegistryValue

    {

        public string Name { get; set; }

        public string Data { get; set; }

    }



    public class RegistryKeyModel

    {

        public string Path { get; set; }

        public List<RegistryKeyModel> SubKeys { get; set; } = new List<RegistryKeyModel>();

        public List<RegistryValue> Values { get; set; } = new List<RegistryValue>();

    }

    class Program
    {

      static  private RegistryKeyModel GetRegistryKey(string path)

        {
            var result = new RegistryKeyModel

            {
                Path = path

            };
            
            var key = Registry.CurrentUser.OpenSubKey(path);

            foreach (var valueName in key.GetValueNames())

            {
                result.Values.Add(new RegistryValue

                {
                    Name = valueName,
                    Data = key.GetValue(valueName).ToString()

                });
                
            }
            foreach (var subKeyName in key.GetSubKeyNames())
            {
                
                result.SubKeys.Add(GetRegistryKey(path + "\\" + subKeyName));
            }
            
            return result;

        }

        static private void SetNewValue(string path, int i)

        {
            var result = new RegistryKeyModel();

          
            var key = Registry.CurrentUser.OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
            i++;
            
            key.SetValue("sfdfs" + i, "sdfsdf" + i);
            foreach (var subKeyName in key.GetSubKeyNames())
            {
               SetNewValue(path + "\\" + subKeyName,i);
            }

          

        }





        static int Main(string[] args)
        {
            args[0] = @"System\CurrentControlSet\Control";
            RegistryKeyModel keyNew;
            keyNew =  GetRegistryKey(args[0]);

            int j = 0;

            SetNewValue(args[0],j);

            RegistryKeyModel newKey1 = GetRegistryKey(args[0]);

            if (keyNew.Values.Count == 0)
            {
                for (int i = 0; i < newKey1.Values.Count; i++)
                {
                    Console.WriteLine("New Value " + newKey1.Values[i].Name + newKey1.Values[i].Data);
                }
            }
            else if (keyNew.Values.Count == newKey1.Values.Count)
            {
                for (int i = 0; i < keyNew.Values.Count; i++)
                
                    if (keyNew.Values[i].Data != newKey1.Values[i].Data && keyNew.Values[i].Name != newKey1.Values[i].Name)
                    {
                        Console.WriteLine("Data an Name were changed" + newKey1.Values[i].Name + newKey1.Values[i].Data);
                    }
                    else if (keyNew.Values[i].Data == newKey1.Values[i].Data &&
                             keyNew.Values[i].Name == newKey1.Values[i].Name)
                    {
                        Console.WriteLine("Nothing was changed");
                    }
                    else if (keyNew.Values[i].Data != newKey1.Values[i].Data &&
                                 keyNew.Values[i].Name == newKey1.Values[i].Name)
                    {
                        Console.WriteLine("Data was changed" + newKey1.Values[i].Name + newKey1.Values[i].Data);
                    }
                    else if (keyNew.Values[i].Data == newKey1.Values[i].Data &&
                                     keyNew.Values[i].Name != newKey1.Values[i].Name)
                    {
                        Console.WriteLine("Name of Value was changed" + newKey1.Values[i].Name);
                    }
            }
                
            

            Console.ReadLine();



            return 0;

        }

  





     }
}
