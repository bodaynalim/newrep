using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator
{
    class CalculatorViewModel : INotifyPropertyChanged
        {
            
            #region Private Variables
            private string _buttonText;
            private string _answer;

        #endregion

        #region Constructor
        public CalculatorViewModel()
            {
            _buttonText = "";
                _answer="";

            }
            #endregion

            #region Public Properties
            public string ButtonText
            {
                get { return _buttonText; }
                set
                {
                _buttonText = value;
                    OnPropertyChanged("ButtonText");
                }
            }
        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
            #endregion

            #region Command

            private DelegateCommand _onClick;
        
        

           public DelegateCommand OnClick
            {
                get
                {
                    return _onClick ?? (_onClick = new DelegateCommand(Click));
                }
            }

            public string _CEText;
            private void Click(object arg)
            {
               
                switch (arg.ToString())
                {
                   
                case "CE":
                    ButtonText=_CEText;
                        break;
                case "CLEAR":
                    ButtonText = "";
                    break;
                case "9":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "8":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "7":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "6":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "5":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "4":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "3":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "2":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "1":
                    _CEText = ButtonText;
                    ButtonText += arg.ToString();
                    break;
                case "0":
                    ButtonText += arg.ToString();
                    break;
                case "+":
                    ButtonText += arg.ToString();
                    break;
                case "-":
                    ButtonText += arg.ToString();
                    break;
                case "/":
                    ButtonText += arg.ToString();
                    break;
                case "*":
                    ButtonText += arg.ToString();
                    break;
                case "+-":
                    ButtonText += arg.ToString();
                    break;
                case ".":
                    ButtonText += arg.ToString();
                    break;
                case "X":
                    break;
                case "=":
                    ButtonText += arg.ToString();
                    Answer = ButtonText;
                    ButtonText = "";
                    break;
                default:
                        ButtonText = "";
                        break;
                }
                  
                
            }
           



            #endregion
            #region public methods
           
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

