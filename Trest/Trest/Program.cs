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
    class Program
    {
      
        static int Main(string[] args)
        {
            RegistryKey currentUserKey = Registry.LocalMachine;
            args[0] = @"SYSTEM\ControlSet001\Services\dbupdatem";
            string[] pathToSubKeys = args[0].Split(new char[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);
            RegistryKey f = currentUserKey.CreateSubKey("SYSTEM");
            RegistryKey[] vmsBaseKey = new RegistryKey[20];
            RegistryKey[] vmsBaseKeyCopy = new RegistryKey[20];
            List<string> savePropertyCopy = new List<string>();
            string path = "";
            int numberOfKey = 0;
            List<string> saveProperty = new List<string>();
            for (int i = 0; i < pathToSubKeys.Length; i++)
            {
                if (i == 0)
                    path += pathToSubKeys[i];
                else
                {
                    path += @"\" + pathToSubKeys[i];

                }
                    vmsBaseKey[i] = currentUserKey.CreateSubKey(path);
                    vmsBaseKeyCopy[i] = currentUserKey.CreateSubKey(path);
                    vmsBaseKey[i].SetValue("car" + i, "lada" + i);
                    saveProperty.Add(vmsBaseKey[i].GetValue("car"+i).ToString());
                    numberOfKey++;

            }
            
            Console.WriteLine("Make changes to the registry and click enter");
            Console.WriteLine("1-Delete SubKey");
            Console.WriteLine("2-Add New Key");
            Console.WriteLine("3-Change Value");
            Console.WriteLine("4-Add new Value");
            Console.WriteLine("5-Delete Value");


            string menuItem = Console.ReadLine();
            vmsBaseKeyCopy = vmsBaseKey;
            switch (menuItem)
            {
                case "1":
                   Console.WriteLine("Key was deleted "+vmsBaseKeyCopy[pathToSubKeys.Length - 1].Name);
                   currentUserKey.DeleteSubKey(path);
                   break;
                case "2":
                    string nameOfKey = path;
                    nameOfKey += "\\"+Console.ReadLine();
                    numberOfKey++;
                    vmsBaseKeyCopy[numberOfKey - 1] = currentUserKey.CreateSubKey(nameOfKey);
                    Console.WriteLine("Key was added " + vmsBaseKeyCopy[numberOfKey - 1].Name);
                    break;
                case "3":
                    for (int i = 0; i < numberOfKey; i++)
                    { Console.WriteLine("Change value " + "car "+i.ToString());
                      string changeValue = Console.ReadLine();
                      vmsBaseKeyCopy[i].SetValue("car" + i, changeValue);
                      savePropertyCopy.Add(vmsBaseKeyCopy[i].GetValue("car" + i).ToString());
                    }

                    for (int i = 0; i < saveProperty.Count; i++)
                    {
                        if (saveProperty[i] != savePropertyCopy[i])
                        {
                            Console.WriteLine("The value car"+i+" was changed from "+saveProperty[i]+" to "+savePropertyCopy[i]);
                        }

                    }
                    break;
                case "4":
                    for (int i = 0; i < numberOfKey; i++)
                    {
                        Console.WriteLine("Name of new value: ");
                        string newValueName = Console.ReadLine();
                        Console.WriteLine("Data of new value: ");
                        string newDataeOfValue = Console.ReadLine();
                        vmsBaseKeyCopy[i].SetValue(newValueName, newDataeOfValue );

                    }
                    break;
                case "5":
                    for (int i = 0; i < numberOfKey; i++)
                    {
                        Console.WriteLine("Value of was deleted : "+vmsBaseKey[i].GetValueNames()[1].ToString());
                        vmsBaseKeyCopy[i].DeleteValue("car" + i);

                    }
                    break;
               default : break;
                
            }
           
     

            Console.ReadLine();
            return 0;
          
        }

  





     }
}
