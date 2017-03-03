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
        public List<string> Path { get; set; } = new List<string>();
        public List<RegistryKeyModel> SubKeys { get; set; } = new List<RegistryKeyModel>();

        public List<RegistryValue> Values { get; set; } = new List<RegistryValue>();

    }

    class Program
    {

      static  private RegistryKeyModel GetRegistryKey(string path)

      {
          var result = new RegistryKeyModel();

            result.Path.Add(path);
            
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
            var key = Registry.CurrentUser.OpenSubKey(path, RegistryKeyPermissionCheck.ReadWriteSubTree);
            i++;
            
            key.SetValue("sfdfs" + i, "sdfsdf" + i);

            foreach (var subKeyName in key.GetSubKeyNames())
            {
               SetNewValue(path + "\\" + subKeyName,i);
            }

          

        }

        static private void GetValue(RegistryKeyModel keyNew, RegistryKeyModel newKey1)

        {
            for (int i = 0; i < keyNew.Path.Count; i++)
            {
                Console.WriteLine(keyNew.Path[i]);
            }

            for (int i = 0; i < keyNew.SubKeys.Count; i++)
            {
                GetValue(keyNew.SubKeys[i], newKey1);
            }



        }

        static private void Comparison(RegistryKeyModel keyNew, RegistryKeyModel newKey1)

        {
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

            if (keyNew.Path.Count == newKey1.Path.Count)
            {
                for (int i = 0; i < keyNew.Path.Count; i++)
                {
                    if (keyNew.Path[i] == newKey1.Path[i])
                    {
                        Console.WriteLine("Key was not changed" + newKey1.Values[i].Name);
                    }
                    if (keyNew.Path[i] != newKey1.Path[i])
                    {
                        Console.WriteLine("Key was changed from " + keyNew.Path[i] + " to " + newKey1.Path[i]);
                    }

                }
            }
            else if (keyNew.Path.Count != newKey1.Path.Count)
            {
                if (keyNew.Path.Count > newKey1.Path.Count)
                {
                }
                else if(keyNew.Path.Count < newKey1.Path.Count)
                {
                }
            }
            

            for (int i = 0; i < keyNew.SubKeys.Count; i++)
            {
                Comparison(keyNew.SubKeys[i], newKey1.SubKeys[i]);
            }
 }
  


        static int Main(string[] args)
        {
            args[0] = @"System\CurrentControlSet\Control";

            RegistryKeyModel   keyNew =  GetRegistryKey(args[0]);

            int j = 0;

            SetNewValue(args[0],j);

            RegistryKeyModel newKey1 = GetRegistryKey(args[0]);

            GetValue(keyNew, newKey1);
            
            Comparison(newKey1, keyNew);
            
            Console.ReadLine();

            return 0;

        }

  





     }
}
