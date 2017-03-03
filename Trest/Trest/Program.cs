﻿using System;
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
                result.Path.Add(path + "\\" + subKeyName);
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
           
                var addedNewValue =
                newKey1.Values.Where(x => !keyNew.Values.Any(y => x.Name == y.Name)).Select(k => k.Name);

            Console.WriteLine("Added values: " + string.Join(",", addedNewValue));

            var deletedValue =
                keyNew.Values.Where(x => !newKey1.Values.Any(y => x.Name == y.Name)).Select(k => k.Name);

            Console.WriteLine("Deleted values: " + string.Join(",", deletedValue));

            var changedValue = 
                newKey1.Values.Where(x => !keyNew.Values.Any(y => x.Data == y.Data)).Select(x => "Name "+ x.Name+" Data "+x.Data );
            
            Console.WriteLine("Changed Values: " + string.Join(",", changedValue));

            var addedKey =
                newKey1.Path.Where(x => !keyNew.Path.Any(y => x == y)).Select(x => x);

            Console.WriteLine("Added key: " + string.Join(",", addedKey));
            
            var deletedKey = 
                keyNew.Path.Where(x => !newKey1.Path.Any(y => x == y)).Select(x => x);

            Console.WriteLine("Deleted key: " + string.Join(",", deletedKey));

           // var COunt = newKey1.Path.Where(x => keyNew.Path.Any(y => y == x));
            List<RegistryKeyModel> key1 = newKey1.SubKeys.Where(x=>keyNew.Path.Any(y=> newKey1.Path.Any(c=>c==y))).Select(x=>x).ToList();
            List<RegistryKeyModel> key2 = keyNew.SubKeys.Where(x=> newKey1.Path.Any(y => keyNew.Path.Any(c=>c==y))).Select(x=>x).ToList();
          
            for (int i = 0; i < key1.Count(); i++)
            {
              
                    Comparison(key2[i], key1[i]);
            }
 }
  


        static int Main(string[] args)
        {
            args[0] = @"System\CurrentControlSet\Control";

            RegistryKeyModel   keyNew =  GetRegistryKey(args[0]);

            int j = 0;
            Console.ReadLine();
            
            SetNewValue(args[0],j);
            
            RegistryKeyModel newKey1 = GetRegistryKey(args[0]);

            GetValue(keyNew, newKey1);
            
            Comparison(keyNew,newKey1);
            
            Console.ReadLine();

            return 0;

        }

  





     }
}
