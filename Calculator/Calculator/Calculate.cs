using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculate
    {
        public static string GetResult(string input)
        {

            double result = 0;
            List<string> operations = new List<string>();
            List<string> number = new List<string>();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    string output = "";
                    while (!IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }
                    number.Add(output);
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    operations.Add(input[i].ToString());
                }

            }


            for (int i = 0; i < operations.Count; i++)
            {
                if (operations[i] == "*")
                {
                    result = float.Parse(number[i]) * float.Parse(number[i + 1]);
                    operations.RemoveAt(i);
                    number[i] = result.ToString();
                    number.RemoveAt(i + 1);
                    i = -1;
                }



            }
            for (int i = 0; i < operations.Count; i++)
            {
                if (operations[i] == "/")
                {

                    result = float.Parse(number[i]) / float.Parse(number[i + 1]);
                    operations.RemoveAt(i);
                    number[i] = result.ToString();
                    number.RemoveAt(i + 1);
                    i = -1;
                }

            }


            for (int i = 0; i < operations.Count; i++)
            {
                if (operations[i] == "+")
                {
                    result = float.Parse(number[i]) + float.Parse(number[i + 1]);
                    operations.RemoveAt(i);
                    number[i] = result.ToString();
                    number.RemoveAt(i + 1);
                    i = -1;

                }

            }

            for (int i = 0; i < operations.Count; i++)
            {
                if (operations[i] == "-")
                {
                    result = float.Parse(number[i]) - float.Parse(number[i + 1]);
                    operations.RemoveAt(i);
                    number[i] = result.ToString();
                    number.RemoveAt(i + 1);
                    i = -1;

                }
            }

            return result.ToString();

        }

        static private bool IsOperator(char с)
        {
            if (("+-/*()".IndexOf(с) != -1))
                return true;
            return false;
        }
    }
}
