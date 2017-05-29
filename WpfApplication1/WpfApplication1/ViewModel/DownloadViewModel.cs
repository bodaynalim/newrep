using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SNFaceCrop;
using Rectangle = System.Windows.Shapes.Rectangle;


namespace WpfApplication1.ViewModel
{
    class DownloadViewModel : INotifyPropertyChanged
    {
        private bool _showLogForm;
       
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
     
        #endregion

        #region Constructor
        public DownloadViewModel()
        {
           _isButtonEnabled = false;
            
        }
        #endregion
        
        
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Command

        private DelegateCommand _downloadImage;
        private DelegateCommand _threadDownload;
       public DelegateCommand DownloadImage
        {
            get
            {
                return _downloadImage ?? (_downloadImage = new DelegateCommand(ImageDownload));
            }
        }

        private void ImageDownload(object imageFileName)
        {
            
           
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
            
            
        }
      

        private void StartAsyncDownload(object arg)
        {
           
          
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
