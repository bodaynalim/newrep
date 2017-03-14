using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calcul : INotifyPropertyChanged
    {
        private string _textbutton;

        public string TextButton
        {
            get { return _textbutton; }
            set
            {
                _textbutton = value;
                OnPropertyChanged("TextButton");
            }
        }


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
