using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Trest
{
    class Program
    {
      static string[] http = {"http://speedtest.newark.linode.com/100MB-newark.bin","http://speedtest.atlanta.linode.com/100MB-atlanta.bin",
            "http://speedtest.frankfurt.linode.com/100MB-frankfurt.bin", "http://speedtest.singapore.linode.com/100MB-singapore.bin" };

        static void Main(string[] args)
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            string k = Console.ReadLine();
            switch (k)
            {
                case "1": Download1();
                    break;
                case "2":
                        new Thread(func0).Start();
                        new Thread(func1).Start();
                        new Thread(func2).Start();
                        new Thread(func3).Start();
                    break;
                case "3":
                    DisplayResultAsync();
                    break;
            }

            
            Console.ReadLine();
        }

        static void Download1()
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
           for (int i = 0; i < http.Length; i++)
            { st.Start();
              client.DownloadFile(http[i], result + "myBook"+i+".bin");
              st.Stop();
              Console.WriteLine(http[i] +": {0} ", st.Elapsed);
              st.Reset();
            }
            for (int i = 0; i < 4; i++)
                File.Delete(result + "myBook" + i + ".bin");

            Console.WriteLine("Файлы удалены");



        }

        static void func0()
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
                st.Start();
                client.DownloadFile(http[0], result + "myBook" + 0 + ".bin");
                st.Stop();
                Console.WriteLine(http[0] + ": {0} ", st.Elapsed);
            File.Delete(result + "myBook" + 0 + ".bin");

        }

    

        static void func1()
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
                st.Start();
                client.DownloadFile(http[1], result + "myBook" + 1 + ".bin");
                st.Stop();
                Console.WriteLine(http[1] + ": {0} ", st.Elapsed);
            File.Delete(result + "myBook" + 1 + ".bin");



        }
        static void func2()
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
            st.Start();
            client.DownloadFile(http[2], result + "myBook" + 2 + ".bin");
            st.Stop();
            Console.WriteLine(http[2] + ": {0} ", st.Elapsed);
            File.Delete(result + "myBook" + 2 + ".bin");
        }

        static void func3()
        {
            string result = Path.GetTempPath();
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
            st.Start();
            client.DownloadFile(http[3], result + "myBook" + 3 + ".bin");
            st.Stop();
            Console.WriteLine(http[3] + ": {0} ", st.Elapsed);
            File.Delete(result + "myBook" + 3 + ".bin");

        }

        static async Task DisplayResultAsync()
        {

            Task t1 = Task.Run(() =>
                {
                    func0();
                });
            Task t2 = Task.Run(() =>
            {
                func1();
            }); ;
            Task t3 = Task.Run(() =>
            {
                func2();

            });
            Task t4 = Task.Run(() =>
            {
                func3();

            });


            await Task.WhenAll(new[] { t1, t2, t3, t4 });

           
        }
    }
=======
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

            List<RegistryKeyModel> key1 = newKey1.SubKeys.Where(x=> keyNew.SubKeys.Any(y=> y.Path.Any(c=> x.Path.Any(v=>c==v)))).ToList();
            List<RegistryKeyModel> key2 = keyNew.SubKeys.Where(x => newKey1.SubKeys.Any(y => y.Path.Any(c => x.Path.Any(v => c == v)))).ToList();

            
            for (int i = 0; i < key1.Count; i++)
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
>>>>>>> new
}
