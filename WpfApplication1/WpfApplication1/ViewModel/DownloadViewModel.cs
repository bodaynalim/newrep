using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApplication1.ViewModel
{
    class DownloadViewModel : INotifyPropertyChanged
    {
        #region Button Enables
      private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
                OnPropertyChanged(nameof(IsButtonEnabled));
            }
        }

        #endregion
        #region Private Variables
        private ObservableCollection<Download> _files;
        static readonly string result = @"C:\Users\pervolo\AppData\Local\Temp\";
        static string[] http = {"http://speedtest.newark.linode.com/100MB-newark.bin","http://speedtest.atlanta.linode.com/100MB-atlanta.bin",
            "http://speedtest.frankfurt.linode.com/100MB-frankfurt.bin", "http://speedtest.singapore.linode.com/100MB-singapore.bin" };
        #endregion

        #region Constructor
        public DownloadViewModel()
        {
            _files = new ObservableCollection<Download>();
            _isButtonEnabled = true;



        }
        #endregion

        #region Public Properties
        public ObservableCollection<Download> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                OnPropertyChanged("Files");
            }
        }

        #endregion
        
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Command

        private DelegateCommand _syncDownload;
        private DelegateCommand _threadDownload;
        private DelegateCommand _asyncDownload;
        public DelegateCommand SyncDownload
        {
            get
            {
                return _syncDownload ?? (_syncDownload = new DelegateCommand(StartSyncDownload));
            }
        }

        private void StartSyncDownload(object arg)
        {
            _files.Clear();
            for (int i = 0; i < http.Length; i++)
            {
                DownLoad(http[i]);

            }
        }
        public DelegateCommand ThreadDownload
        {
            get
            {
                return _threadDownload ?? (_threadDownload = new DelegateCommand(StartThreadDownload));
            }
        }

        private void StartThreadDownload(object arg)
        {
            _files.Clear();
             _isButtonEnabled = false; 

            for (int i = 0; i < http.Length; i++)
            {

                new Thread(new ParameterizedThreadStart(DownLoad)).Start(http[i]);

            }
        }
        public DelegateCommand AsyncDownload
        {
            get
            {
                return _asyncDownload ?? (_asyncDownload = new DelegateCommand(StartAsyncDownload));
            }
        }

        private void StartAsyncDownload(object arg)
        {
            _files.Clear();
            _isButtonEnabled = false;
            DisplayResultAsync();
        }



        #endregion
        #region public methods
        int number = 0;

        void DownLoad(object http)
        {
            

            int numb = number;
            number++;
            WebClient client = new WebClient();
            Stopwatch st = new Stopwatch();
            st.Start();
            client.DownloadFile((string)http, result + "myBook" + numb + ".bin");
            st.Stop();
            App.Current.Dispatcher.Invoke((Action)delegate 
            {
                 _files.Add(new Download() { Time = st.Elapsed.ToString(), Site = (string)http });
            });
           
             File.Delete(result + "myBook" + numb + ".bin");
        
             

        }

        #endregion

        #region Task
        async Task DisplayResultAsync()
        {

            Task t1 = Task.Run(() =>
            {
                DownLoad(http[0]);
            });
            Task t2 = Task.Run(() =>
            {
                DownLoad(http[1]);
            }); ;
            Task t3 = Task.Run(() =>
            {
                DownLoad(http[2]);

            });
            Task t4 = Task.Run(() =>
            {
                DownLoad(http[3]);

            });


            await Task.WhenAll(new[] { t1, t2, t3, t4 });

        }

        #endregion
        #region Private Methods
        
        private void OnPropertyChanged(string propertyChanged)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyChanged));
        }
        #endregion
    }
}
