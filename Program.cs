using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        //входной метод класса
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
                if (IsDelimeter(input[i]))
                    continue;

                if (Char.IsDigit(input[i]))
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

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                    {
                        operStack.Push(input[i]);
                    }
                    else if (input[i] == ')')
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }

            }

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output;
        }

        //проверка на ввод разделителя
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

        //проверка на ввод оператора
        static bool IsOperator(char c)
        {
            return GetPriority(c) < MAX_PRIORITY;
        }

        const int MAX_PRIORITY = 10;
        static byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 5;
                default: return MAX_PRIORITY;
            }
        }

        static double Counting(string userInput)
        {
            double result = 0;
            Stack<double> stack = new Stack<double>();

            string digitBuffer = string.Empty;
            foreach (var symbol in userInput)
            {
                if (Char.IsDigit(symbol)) //TODO change
                {                    
                    while (!(IsDelimeter(symbol) || IsOperator(symbol)))
                    {
                        digitBuffer += symbol;
                        i++;
                        if (i == userInput.Length) break;
                    }
                    stack.Push(double.Parse(digitBuffer));
                    digitBuffer = string.Empty;
                    i--;
                }
                else if (IsOperator(symbol))
                {                    
                    if (stack.Count == 1)
                    {
                        if (symbol == '-') stack.Push(-stack.Pop());
                        continue;
                    }
                    stack.Push(ExecuteBinaryOperation(symbol, stack.Pop(), stack.Pop()));
                }
            }
            return stack.Peek();
        }

        static double ExecuteBinaryOperation(char symbol, double a, double b)
        {
            switch (symbol)
            {
                case '+': return b + a;
                case '-': return b - a;
                case '*': return b * a;
                case '/': return b / a;
                default:
                    Console.WriteLine("");
                    Environment.Exit(1);
                    return -1;
            }
        }
    }
}

    