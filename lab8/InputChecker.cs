using System;
using System.Collections.Generic;

namespace lab8
{
    public class InputChecker
    {
        public static bool IsCorrectInput(string input)
        {
            if (input == "")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Вхідний рядок не може бути порожнім!");
                Console.ResetColor();
                return false;
            }
            if (!IsCorrectBrackets(input))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Перевірте, чи правильно розставлені дужки! Якщо використовуєте від'ємне число, візьміть його в дужки!");
                Console.ResetColor();
                return false;
            }
            int amountOfPointsInNum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[0] == '+' || input[0] == '-' || input[0] == '*' || input[0] == '/')
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Вираз не може починатися та закінчуватися на знак оператора!");
                    Console.ResetColor();
                    return false;
                }
                if (!char.IsLetterOrDigit(input[i]) && !IsOperator(input[i]) && input[i] != '(' && input[i] != ')' && input[i] != '.')
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Вираз має містити лише числла, літери, круглі дужки та знаки `+`, `-`, `*`, `/`");
                    Console.ResetColor();
                    return false;
                }
                if (i != input.Length - 1)
                {
                    if (char.IsLetterOrDigit(input[i]) && input[i+1] == '(')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Використовуйте знак `*` між числом та виразом у дужках!");
                        Console.ResetColor();
                        return false;
                    }
                }
                if (input[i] == '.')
                {
                    if (i == 0 || i == input.Length-1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Переконайтесь, що правильно використовуєте роздідювач десяткової частини!");
                        Console.ResetColor();
                        return false;
                    }
                    else if (!char.IsDigit(input[i-1]) || !char.IsDigit(input[i+1]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Переконайтесь, що правильно використовуєте роздідювач десяткової частини!");
                        Console.ResetColor();
                        return false;
                    }
                    else
                    {
                        amountOfPointsInNum++;
                        continue;
                    }
                }
                if ((IsOperator(input[i]) || input[i] == '(' || input[i] == ')') && (amountOfPointsInNum > 1))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Переконайтесь, що правильно використовуєте роздідювач десяткової частини!");
                    Console.ResetColor();
                    return false;
                }
                if (i != input.Length - 1)
                {
                    if (IsOperator(input[i]) && IsOperator(input[i + 1]))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Не можна використовувати 2 оператори поспіль!");
                        Console.ResetColor();
                        return false;
                    }
                }
                if (input[i] == '(' && (input[i+1] == '+' || input[i+1] == '*' || input[i+1] == '/'))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Знаки `+`, `*` та `/` не можуть стояти після дужки `(`!");
                    Console.ResetColor();
                    return false;
                }
                if (input[i] == '(' && input[i+1] == '-')
                {
                    int j = i + 2;
                    while (char.IsDigit(input[j]))
                    {
                        j++;
                    }
                    if (input[j] != ')')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Перевірте, чи правильно розставлені дужки! Якщо використовуєте від'ємне число, візьміть його в дужки!");
                        Console.ResetColor();
                        return false;
                    }
                }
                if (char.IsLetter(input[i]) && char.IsLetter(input[i+1]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Використовуйте оператор між двома літерами!");
                    Console.ResetColor();
                    return false;
                }
                if (input[i] == '/' && input[i+1] == '0')
                {
                    if (i + 1 == input.Length - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("На нуль ділити не можна!");
                        Console.ResetColor();
                        return false;
                    }
                    else if (IsOperator(input[i+2]) || input[i+2] == '(' || input[i+2] == ')')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("На нуль ділити не можна!");
                        Console.ResetColor();
                        return false;
                    }
                }
            }
            if (!char.IsLetterOrDigit(input[input.Length-1]) && input[input.Length-1] != ')' && input[input.Length-1] != '.')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Вираз не може починатися та закінчуватися на знак оператора!");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        static bool IsCorrectBrackets(string input)
        {
            Stack<char> stackOfBrackets = new Stack<char>();
            foreach (char c in input)
            {
                if (c == '(')
                {
                    stackOfBrackets.Push(c);
                    continue;
                }
                if (c == ')')
                {
                    if (stackOfBrackets.Count == 0)
                    {
                        return false;
                    }
                    stackOfBrackets.Pop();
                }
            }
            if (stackOfBrackets.Count != 0)
            {
                return false;
            }
            return true;
        }

        static bool IsOperator(char c)
        {
            if (c != '+' && c != '-' && c != '*' && c != '/')
            {
                return false;
            }
            return true;
        }
    }
}
