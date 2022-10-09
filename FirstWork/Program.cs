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
                var calculationResult = ReversePolishNotation.ResultPolishNotation(userInput);
                Console.WriteLine(calculationResult);

            }
        }

        static double ResultPolishNotation(string userInput)
        {
            string polishExpression = ConvertInPolishExpression(userInput);
            Console.WriteLine("Polish: " + polishExpression);
            double result = CalculationResult(polishExpression);
            return result;
        }

        static string ConvertInPolishExpression(string userInput)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < userInput.Length; i++)
            {
                if (Char.IsDigit(userInput[i]))
                {
                     while (!(IsDelimeter(userInput[i]) || IsOperator(userInput[i])))
                     {
                         output += userInput[i];
                         i++;
                         if (i == userInput.Length) break;
                     }

                    output += " ";
                    i--;
                }

                else if (IsOperator(userInput[i]))
                {
                    switch (userInput[i])
                    {
                        case '(':
                            operStack.Push(userInput[i]);
                            break;
                        case ')':
                            {
                                char symbol = operStack.Pop();

                                while (symbol != '(')
                                {
                                    output += symbol.ToString() + ' ';
                                    symbol = operStack.Pop();
                                }
                                break;
                            }
                        default:
                            if (operStack.Count > 0)
                                if (CheckOperatorPrecedence(userInput[i]) <= CheckOperatorPrecedence(operStack.Peek()))
                                    output += operStack.Pop().ToString() + " ";
                            operStack.Push(char.Parse(userInput[i].ToString()));
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

        static bool IsDelimeter(char symbol)
        {
            switch (symbol)
            {
                case ' ':
                case '=':
                    return true;
                default:
                    return false;
            }
        }

        static bool IsOperator(char symbol)
        {
            return CheckOperatorPrecedence(symbol) < MAX_PRIORITY;
        }

        const int MAX_PRIORITY = 10;
        static byte CheckOperatorPrecedence(char symbol) => symbol switch
        {
            '(' => 0,
            ')' => 1,
            '+' => 2,
            '-' => 2,
            '*' => 3,
            '/' => 3,
            _ => MAX_PRIORITY,
        };
        static private double CalculationResult(string input)
        {
            double result = 0; 
            Stack<double> stack = new Stack<double>(); 

            for (int symbol = 0; symbol < input.Length; symbol++)
            {
                if (IsOperator(input[symbol])) 
                {  
                    double a = stack.Pop();
                    double b = stack.Pop();

                    switch (input[symbol]) 
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                    }
                    stack.Push(result); 
                }
                
                else if (Char.IsDigit(input[symbol]))
                {
                    string output = string.Empty;

                    while (!(IsDelimeter(input[symbol]) || IsOperator(input[symbol])))
                    {
                        output += input[symbol];
                        symbol++;
                        if (symbol == input.Length) break;
                    }
                    stack.Push(double.Parse(output)); 
                    symbol--;
                }
            }
            return stack.Peek(); 
        }
       
    }
}
