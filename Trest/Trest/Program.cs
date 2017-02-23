using System;
using System.Collections.Generic;
using System.Linq;
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
}
