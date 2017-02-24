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
   
    public partial class MainWindow : Window
    {
        #region Private Variables
        private readonly ViewModel.DownloadViewModel _downloadViewModel;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            _downloadViewModel = new ViewModel.DownloadViewModel();
            DataContext = _downloadViewModel;
        }
     
    }
}
