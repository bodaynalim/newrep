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
        



        private RegistryKeyModel GetRegistryKey(string path)

        {

            var result = new RegistryKeyModel

            {
                Path = path

            };

          
            var key = Registry.LocalMachine.OpenSubKey(path, true);
            
            foreach (var subKeyName in key.GetSubKeyNames())
            {

                result.SubKeys.Add(GetRegistryKey(path + "\\" + subKeyName));

            }



            return result;

        }
        static int Main(string[] args)
        {
            args[0] = @"SYSTEM\ControlSet001\Services";
            string[] pathToSubKeys = args[0].Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string path = "";
            for (int i = 0; i < pathToSubKeys.Length; i++)
            {
                if (i == 0)
                    path += pathToSubKeys[i];
                else
                {
                    path += @"\" + pathToSubKeys[i];

                }
                new Program().GetRegistryKey(path);

            }

            return 0;

        }

  





     }
}
