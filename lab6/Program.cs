using System;
using System.Collections.Generic;

namespace lab6
{
    public class Operator
    {
        public int _priority;
        public string _symbol;
        public Operator(string symbol, int priority)
        {
            this._symbol = symbol;
            this._priority = priority;
        }
    }

    public class ContainerOperator
    {
        public static List<Operator> _operators = new List<Operator>();

        public static void AddOperator(Operator op)
        {
            _operators.Add(op);
        }

        public static Operator FindOperator(string op)
        {
            foreach (var item in _operators)
            {
                if (item._symbol == op)
                {
                    return item;
                }
            }
            return null;
        }
    }

    class Stack
    {
        private string[] _items;
        private int _size;
        // private string _top;

        public Stack()
        {
            _items = new string[16];
            _size = 0;
            // _top = _items[_size];
        }

        public void Push(string item)
        {
            _items[_size] = item;
            _size++;
            if (_items.Length == _size)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }
        }

        public string Pop()
        {
            if (IsEmpty())
            {
                throw new Exception("ERROR! Stack is empty!");
            }
            string buff = _items[_size-1];
            // _top = _items[_size - 1];
            _items[_size-1] = null;
            _size--;
            return buff;
        }

        public string Peek()
        {
            if (IsEmpty())
            {
                return null;
            }
            return _items[_size-1];
        }

        public bool IsEmpty()
        {
            if (_size == 0)
            {
                return true;
            }
            return false;
        }

        public int GetCount()
        {
            return _size;
        }

        public void Print()
        {
            for (int i = 0; i < _size; i++)
            {
                Console.Write(_items[i] + " ");
            }
            Console.WriteLine();
        }
    }

    class InfixToPostfix
    {
        private string _inputString;
        private string _outputString = "";
        private string _tempResult;
        private double _ro;
        private double _lo;
        static Stack stackOfOperators = new Stack();
        static Stack stackOfNums = new Stack();
        
        public InfixToPostfix(string inputString)
        {
            this._inputString = inputString;
            ContainerOperator.AddOperator(new Operator("(", 1));
            ContainerOperator.AddOperator(new Operator("+", 2));
            ContainerOperator.AddOperator(new Operator("-", 2));
            ContainerOperator.AddOperator(new Operator("/", 3));
            ContainerOperator.AddOperator(new Operator("*", 3));
        }

        public string ConvertToPostfix()
        {
            bool isEndOfNum = false;
            for (int i = 0; i < _inputString.Length; i++)
            {
                if (char.IsDigit(_inputString[i]))
                {
                    if (i != _inputString.Length - 1)
                    {
                        _outputString += _inputString[i];
                        isEndOfNum = true;
                    }
                    else
                    {
                        _outputString += _inputString[i] + " ";
                    }
                    continue;
                }
                if (isEndOfNum)
                {
                    if (_inputString[i] != '.')
                    {
                        _outputString += " ";
                        isEndOfNum = false;
                    }
                }
                if (_inputString[i] == '.')
                {
                    _outputString += _inputString[i];
                    continue;
                }
                if (_inputString[i] == '+' || _inputString[i] == '-' || _inputString[i] == '*' || _inputString[i] == '/')
                {
                    if (stackOfOperators.IsEmpty())
                    {
                        stackOfOperators.Push(_inputString[i].ToString());
                        stackOfOperators.Print();
                        continue;
                    }
                    else
                    {
                        if (ContainerOperator.FindOperator(stackOfOperators.Peek())._priority < ContainerOperator.FindOperator(_inputString[i].ToString())._priority)
                        {
                            stackOfOperators.Push(_inputString[i].ToString());
                            stackOfOperators.Print();
                            continue;
                        }
                    }
                    if (!stackOfOperators.IsEmpty())
                    {
                        try
                        {
                            while (ContainerOperator.FindOperator(stackOfOperators.Peek())._priority >= ContainerOperator.FindOperator(_inputString[i].ToString())._priority)
                            {
                                _outputString += stackOfOperators.Pop() + " ";
                                stackOfOperators.Print();
                            }
                        }
                        catch
                        {

                        }
                        if (stackOfOperators.IsEmpty())
                        {
                            stackOfOperators.Push(_inputString[i].ToString());
                            stackOfOperators.Print();
                            continue;
                        }
                        else
                        {
                            if (ContainerOperator.FindOperator(stackOfOperators.Peek())._priority < ContainerOperator.FindOperator(_inputString[i].ToString())._priority)
                            {
                                stackOfOperators.Push(_inputString[i].ToString());
                                stackOfOperators.Print();
                                continue;
                            }
                        }
                    }
                }
                if (_inputString[i] == '(')
                {
                    if (_inputString[i+1] == '-')
                    {
                        _outputString += "0 ";
                    }
                    stackOfOperators.Push(_inputString[i].ToString());
                    stackOfOperators.Print();
                    continue;
                }
                if (_inputString[i] == ')')
                {
                    while (stackOfOperators.Peek() != "(")
                    {
                        _outputString += stackOfOperators.Pop() + " ";
                        stackOfOperators.Print();
                    }
                    stackOfOperators.Pop();
                    stackOfOperators.Print();
                    continue;
                }
            }
            while (!stackOfOperators.IsEmpty())
            {
                _outputString += stackOfOperators.Pop() + " ";
                stackOfOperators.Print();
            }
            return _outputString;
        }

        public double CalculateExpression()
        {
            string[] elementsOfExpression = _outputString.Split(" ");
            for (int i = 0; i < elementsOfExpression.Length - 1; i++)
            {
                if (elementsOfExpression[i] == "+" || elementsOfExpression[i] == "-" || elementsOfExpression[i] == "*" || elementsOfExpression[i] == "/")
                {
                    if (elementsOfExpression[i] == "+")
                    {
                        _tempResult = (double.Parse(stackOfNums.Pop()) + double.Parse(stackOfNums.Pop())).ToString();
                        stackOfNums.Print();
                    }
                    else if (elementsOfExpression[i] == "-")
                    {
                        _ro = double.Parse(stackOfNums.Pop());
                        if (stackOfNums.IsEmpty())
                        {
                            _lo = 0;
                        }
                        else
                        {
                            _lo = double.Parse(stackOfNums.Pop());
                        }
                        _tempResult = (_lo - _ro).ToString();
                        stackOfNums.Print();
                    }
                    else if (elementsOfExpression[i] == "*")
                    {
                        _tempResult = (double.Parse(stackOfNums.Pop()) * double.Parse(stackOfNums.Pop())).ToString();
                        stackOfNums.Print();
                    }
                    else if (elementsOfExpression[i] == "/")
                    {
                        _ro = double.Parse(stackOfNums.Pop());
                        _lo = double.Parse(stackOfNums.Pop());
                        _tempResult = (_lo / _ro).ToString();
                        stackOfNums.Print();
                    }
                    stackOfNums.Push(_tempResult);
                    stackOfNums.Print();
                }
                else
                {
                    stackOfNums.Push(elementsOfExpression[i].ToString());
                    stackOfNums.Print();
                    continue;
                }
            }
            double result = double.Parse(stackOfNums.Pop());
            stackOfNums.Print();
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string command;
            do
            {
                Console.WriteLine("Натисніть:");
                Console.WriteLine("1 - виконати контрольний приклад");
                Console.WriteLine("2 - ввести приклад з клавіатури");
                Console.WriteLine("exit - завершити виконання програми");
                command = Console.ReadLine();
                if (command == "1")
                {
                    string inputString = "1-(2+3+(4-5*6)*7)+8*9";
                    if (!IsCorrectInput(inputString))
                    {
                        Console.WriteLine();
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"Інфіксна форма: {inputString}");
                    Console.ResetColor();
                    Console.WriteLine();
                    ProcessInput(inputString);
                }
                else if (command == "2")
                {
                    Console.Write("Введіть приклад для обчислення: ");
                    string inputString = Console.ReadLine();
                    Console.WriteLine();
                    if (!IsCorrectInput(inputString))
                    {
                        Console.WriteLine();
                        continue;
                    }
                    ProcessInput(inputString);
                }
                else if (command == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Bye!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Помилка! Команди `{command}` не існує!");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            while (command != "exit");
        }

        static void ProcessInput(string inputString)
        {
            InfixToPostfix str = new InfixToPostfix(inputString);
            Console.WriteLine("Як змінювався вміст стеку під час конвертації виразу в постфіксну форму:");
            string postfixString = str.ConvertToPostfix();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Постфіксна форма: {postfixString}");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("Як змінювався вміст стеку під час обчислення виразу:");
            double result = str.CalculateExpression();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Результат: {result}");
            Console.ResetColor();
            Console.WriteLine();
        }

        static bool IsCorrectInput(string input)
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
                if (!char.IsDigit(input[i]) && !IsOperator(input[i]) && input[i] != '(' && input[i] != ')' && input[i] != '.')
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Вираз має містити лише цифри, круглі дужки та знаки `+`, `-`, `*`, `/`");
                    Console.ResetColor();
                    return false;
                }
                if (i != input.Length - 1)
                {
                    if (char.IsDigit(input[i]) && input[i+1] == '(')
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
                    Console.WriteLine("Знаки `+`, `*` та `/` не можуть стояти після дужки!");
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
            if (!char.IsDigit(input[input.Length-1]) && input[input.Length-1] != ')' && input[input.Length-1] != '.')
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Вираз не може починатися та закінчуватися на знак оператора!");
                Console.ResetColor();
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

        static bool IsCorrectBrackets(string input)
        {
            Stack stackOfBrackets = new Stack();
            foreach (char c in input)
            {
                if (c == '(')
                {
                    stackOfBrackets.Push(c.ToString());
                    continue;
                }
                if (c == ')')
                {
                    if (stackOfBrackets.IsEmpty())
                    {
                        return false;
                    }
                    stackOfBrackets.Pop();
                }
            }
            if (!stackOfBrackets.IsEmpty())
            {
                return false;
            }
            return true;
        }
    }
}
