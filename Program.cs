using System;
using System.Collections.Generic;


namespace Calculator
{
    class ReversePolishNotation
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Введите ваше выражение: ");
                var userInput = Console.ReadLine();
                var calculationResult = ReversePolishNotation.Calculate(userInput);
                Console.WriteLine(calculationResult);

            }
        }

        static double Calculate(string input)
        {
            string output = GetExpression(input);
            Console.WriteLine("Polish: " + output);
            double result = Counting(output);
            return result;
        }

        static string GetExpression(string input) //TODO change
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
               
                    if (Char.IsDigit(input[i]) && !IsDelimeter(input[i]))
                    {
                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            output += input[i];
                            i++;

                            if (i == input.Length) break;
                        }

                        output += " ";
                        i--;
                    }

                    else if (IsOperator(input[i]))
                    {
                        switch (input[i])
                        {
                            case '(':
                                operStack.Push(input[i]);
                                break;
                            case ')':
                                {
                                    char s = operStack.Pop();

                                    while (s != '(')
                                    {
                                        output += s.ToString() + ' ';
                                        s = operStack.Pop();
                                    }

                                    break;
                                }

                            default:
                                if (operStack.Count > 0)
                                    if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                        output += operStack.Pop().ToString() + " ";

                                operStack.Push(char.Parse(input[i].ToString()));
                                break;
                        }
                    }
                
            }

            while (operStack.Count > 0)
            {
                output += operStack.Pop() + " ";
            }

            return output;
        }

       
        static bool IsDelimeter(char c)
        {
            switch (c)
            {
                case ' ':
                case '=':
                    return true;
                default:
                    return false;
            }
        }

        static bool IsOperator(char c)
        {
            return GetPriority(c) < MAX_PRIORITY;
        }

        const int MAX_PRIORITY = 10;
        static byte GetPriority(char s) => s switch
        {
            '(' => 0,
            ')' => 1,
            '+' => 2,
            '-' => 2,
            '*' => 3,
            '/' => 3,
            _ => MAX_PRIORITY,
        };
        static private double Counting(string input)
        {
            double result = 0; 
            Stack<double> temp = new Stack<double>(); 

            for (int i = 0; i < input.Length; i++)
            {

                if (!Char.IsDigit(input[i]))
                {
                    if (IsOperator(input[i])) 
                    {
                     
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i]) 
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': result = b / a; break;
                        }
                        temp.Push(result); 
                    }
                }
                else
                {
                    string a = string.Empty;

                    while (!(IsDelimeter(input[i]) || IsOperator(input[i])))
                    {
                        a += input[i];
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); 
                    i--;
                }
            }
            return temp.Peek(); 
        }
       
    }
}
 
