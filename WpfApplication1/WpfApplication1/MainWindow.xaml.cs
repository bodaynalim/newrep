using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Threading;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    class Download
    {
        public string Time { get; set; }
        public string Site { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static string[] http = {"http://speedtest.newark.linode.com/100MB-newark.bin","http://speedtest.atlanta.linode.com/100MB-atlanta.bin",
            "http://speedtest.frankfurt.linode.com/100MB-frankfurt.bin", "http://speedtest.singapore.linode.com/100MB-singapore.bin" };

        static readonly string result = @"C:\Users\pervolo\AppData\Local\Temp\";
        static Download down;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            Download1();

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < http.Length; i++)
            {
                
                new Thread(new ParameterizedThreadStart(func1)).Start(http[i]);
              
            }
            

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            DisplayResultAsync();
           

        }


       public  void Download1()
        {
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
            for (int i = 0; i < http.Length; i++)
            {
                st.Start();

                client.DownloadFile((string)http[i], result + "myBook" + i + ".bin");

                st.Stop();
                listView.Items.Add(new Download() { Time = st.Elapsed.ToString(), Site = (string)http[i] });
                st.Reset();
            }
            for (int i = 0; i < 4; i++)
                File.Delete(result + "myBook" + i + ".bin");

            button.IsEnabled = true;
            button1.IsEnabled = true;
            button2.IsEnabled = true;




        }

        static int number = 0;

        void func1(object http)
        {
            button.Dispatcher.Invoke(() =>
            {
                button.IsEnabled = false;
});
            button2.Dispatcher.Invoke(() =>
            {
                button2.IsEnabled = false;
            });
            button1.Dispatcher.Invoke(() =>
            {
                button1.IsEnabled = false;
            });
            int numb = number;
            number++;
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
            st.Start();
            client.DownloadFile((string)http, result + "myBook" + numb + ".bin");
            st.Stop();
            Console.WriteLine(http + ": {0} ", st.Elapsed);
            listView.Dispatcher.Invoke(() =>
            {
                listView.Items.Add(new Download() { Time = st.Elapsed.ToString(), Site = (string)http });

            });
               
           
            File.Delete(result + "myBook" + numb + ".bin");

            button.Dispatcher.Invoke(() =>
            {
                button.IsEnabled = true;
            });
            button2.Dispatcher.Invoke(() =>
            {
                button2.IsEnabled = true;
            });
            button1.Dispatcher.Invoke(() =>
            {
                button1.IsEnabled = true;
            });


        }


        async Task DisplayResultAsync()
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
