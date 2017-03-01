using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Int32;

namespace Calculator
{
    class CalculatorViewModel : INotifyPropertyChanged
        {
            
            #region Private Variables
            private string _buttonText;
            private string _Input;
            private bool isPlusOrMinusClicked;
            private bool isNewDigitEntered;
            private bool isEqualClicked;

        #endregion

        #region Constructor
        public CalculatorViewModel()
            {
            _buttonText = "";
            _Input="";
            isPlusOrMinusClicked = false;
            isNewDigitEntered = false;
            isEqualClicked = false;

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
        public string Input
        {
            get { return _Input; }
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
        }

        public bool IsPlusOrMinusClicked
        {
            get
            {
                return isPlusOrMinusClicked;
            }

            set
            {
                isPlusOrMinusClicked = value;
            }
        }

        public bool IsNewDigitEntered
        {
            get
            {
                return isNewDigitEntered;
            }

            set
            {
                isNewDigitEntered = value;
            }
        }
        public bool IsEqualClicked
        {
            get
            {
                return isEqualClicked;
            }

            set
            {
                isEqualClicked = value;
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

       

       
        private void Click(object arg)
            {
               
                switch (arg.ToString())
                {
                   
                case "CE":
                    ButtonText="";
                        break;
                case "CLEAR":

                    ButtonText = "";
                        Input = "";
                    break;
                case "9":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "8":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "7":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "6":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "5":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "4":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "3":
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "2":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "1":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "0":
                    if (IsEqualClicked == true)
                    {
                        ButtonText = "";
                    }
                    IsEqualClicked = false;
                    ISPlusOrOtherOperationPressed();
                    ButtonText += arg.ToString();
                    break;
                case "+":
                     
                        IsEqualClicked = false;
                    if (ButtonText != "")
                        {
                            IsPlusOrMinusClicked = true;
                            Input += ButtonText;
                            Input += arg.ToString();
                        }
                       ButtonText = "0";
                    break;
                  
                case "-":
                    IsEqualClicked = false;
                    if (ButtonText != "")
                    {
                        IsPlusOrMinusClicked = true;
                        Input += ButtonText;
                        Input += arg.ToString();
                   
                    }
                    ButtonText = "0";
                    break;
                case "/":
                    IsEqualClicked = false;
                    if (ButtonText != "")
                    {
                        IsPlusOrMinusClicked = true;
                        Input += ButtonText;
                        Input += arg.ToString();
                    }
                    ButtonText = "1";
                    break;
                case "*":
                    IsEqualClicked = false;
                    if (ButtonText != "")
                    {
                        IsPlusOrMinusClicked = true;
                        Input += ButtonText;
                        Input += arg.ToString();
                    }
                    ButtonText = "1";

                    break;
                case "+-":
                    if(ButtonText!="" && !ButtonText.Contains("-"))
                    { ButtonText = "-" + ButtonText;
                    }
                    else if(ButtonText != "")
                    {
                        ButtonText = ButtonText.Remove(0, 1);
                    }
                       
                    break;
                case ".":
                   
                    if (ButtonText != "" && !ButtonText.Contains(".") && IsEqualClicked==false)
                        {
                            ButtonText += arg.ToString();
                        }
                        break;
                case "X":
                    ButtonText= (ButtonText.Length==0)? "": ButtonText.Substring(0, ButtonText.Length-1);
                    break;
                case "=":
                   
                   InputFloat(Input);
                   Input = "";
                 
                    break;
                default:
                    ButtonText = "";
                    break;
                }
                  
                
            }
        
            public void ISPlusOrOtherOperationPressed( )
            {
                if ((IsPlusOrMinusClicked == true))
                {
                    IsPlusOrMinusClicked = false;
                    ButtonText = "";
                }
            }

            public void InputFloat(string input)
            {

            input += ButtonText;
            input = "50+20-30*(40-20)";
            if (input.Length>2 && IsEqualClicked ==false)
                {
                    
                    ButtonText =Calculate.Compute(input).ToString();
                    IsEqualClicked = true;
                
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