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

namespace Trest
{
    class Program
    {
      static string[] http = {"http://speedtest.newark.linode.com/100MB-newark.bin","http://speedtest.atlanta.linode.com/100MB-atlanta.bin",
            "http://speedtest.frankfurt.linode.com/100MB-frankfurt.bin", "http://speedtest.singapore.linode.com/100MB-singapore.bin" };

       static readonly string result = Path.GetTempPath();

        static int Main(string[] args)
        {
            if (args.Length == 0)
            { return 1; }

            string nuber = Console.ReadLine();
                switch (args[Int32.Parse(nuber)-1])
            {
                case "1": Download1();
                    break;
                case "2":
                    for (int i = 0; i < http.Length; i++)
                    {                       
                        new Thread(new ParameterizedThreadStart(func1)).Start(http[i]);

                    }
                    break;
                case "3":
                    DisplayResultAsync();
                    break;
            }

            
            Console.ReadLine();
            return 0;
          
        }

        static void Download1()
        {
           
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
           for (int i = 0; i < http.Length; i++)
            { st.Start();
              client.DownloadFile((string)http[i], result + "myBook"+i+".bin");
              st.Stop();
              Console.WriteLine((string)http[i] +": {0} ", st.Elapsed);
              st.Reset();
            }
            for (int i = 0; i < 4; i++)
                File.Delete(result + "myBook" + i + ".bin");

            Console.WriteLine("Файлы удалены");

        }

       static int number = 0;
        static void  func1 (object http)
        {
           int numb = number;
            number++;
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
                st.Start();
                client.DownloadFile((string)http, result + "myBook" +numb+  ".bin");
                st.Stop();
                Console.WriteLine(http + ": {0} ", st.Elapsed);
            File.Delete(result + "myBook" +numb+ ".bin");
            
        }
        

        static async Task DisplayResultAsync()
        {

            Task t1 = Task.Run(() =>
                {
                    func1(http[0]);
                });
            Task t2 = Task.Run(() =>
            {
                func1(http[1]);
            }); ;
            Task t3 = Task.Run(() =>
            {
                func1(http[2]);

            });
            Task t4 = Task.Run(() =>
            {
                func1(http[3]);

            });


            await Task.WhenAll(new[] { t1, t2, t3, t4 });

           
        }
    }
}
