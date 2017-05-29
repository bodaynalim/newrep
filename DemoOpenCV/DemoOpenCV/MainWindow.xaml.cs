using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

using Microsoft.Win32;
using SNFaceCrop;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace DemoOpenCV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private bool _showLogForm;
        private FaceDetector _faceDetector;

        private bool _imageIsOpened;
        private bool _detectionIsDone;

        public string ImageFileName { get; private set; }
        public string ClassifierFileName { get; private set; }
        public TimeSpan DetectionTime { get; private set; }

        private Bitmap _bitmapDisplayed;
        private Bitmap _bitmapOriginal;
        private Bitmap _bitmapOneFace;
        private List<Bitmap> _faceImages;
        private List<Bitmap> _faceImagesExtended;
        int _extendXPercent;
        int _extendYPercent;

        private List<Rectangle> _detectedFaces;
        private List<Rectangle> _extendedFaces;
        private Rectangle _selectedFace = null;
        private int _selectedFaceIndex = -1;

        private string _mainFormTitlePrefix = "SNFaceCrop";
        Dictionary<string, object> _properties = new Dictionary<string, object>();
       
        private bool _fitImageSizeToWindow;
        private int _highLightedFace = -1;
        double _scaleRatio;  // ratio between the original image size and the displayed image in picture box
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenImageFile(string imageFileName)
        {
           
            ImageFileName = imageFileName;

            _faceDetector.SetImage(ImageFileName);

            this._detectedFaces.Clear();
            this._extendedFaces.Clear();

            _imageIsOpened = true;
            _detectionIsDone = false;

            if (_bitmapOriginal != null)
                _bitmapOriginal.Dispose();
            if (_bitmapDisplayed != null)
                _bitmapDisplayed.Dispose();

            _bitmapOriginal = new Bitmap(ImageFileName);

            Image1.Source = null;
           

            _properties["Image file"] = ImageFileName;
            _properties["Image width"] = _bitmapOriginal.Width.ToString();
            _properties["Image height"] = _bitmapOriginal.Height.ToString();
            _properties["Image format"] = _bitmapOriginal.PixelFormat.ToString();
          

            ListView1.Items.Clear();
           

        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Select image file";
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == true)
            {
                OpenImageFile(openFileDialog1.FileName);
            }
            return;
        }
    }
}
