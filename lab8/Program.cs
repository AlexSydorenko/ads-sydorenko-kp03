using System;

namespace lab8
{
    class Program
    {
        static void Main()
        {
            while(true)
            {
                Console.WriteLine("Оберіть команду:");
                Console.WriteLine("1 - виконати контрольний приклад");
                Console.WriteLine("2 - ввести вираз з клавіатури");
                Console.WriteLine("exit - завершити виконання програми");
                string command = Console.ReadLine();

                if (command == "1")
                {
                    ProcessControlExpression();
                }
                else if (command == "2")
                {
                    ProcessUserExpression();
                }
                else if (command == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Bye!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Такої команди не існує! Спробуйте ще раз!");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void ProcessControlExpression()
        {
            string controlExpression = "x+y*(5*x-(y+x)/(10-y/x))";
            BinaryTree<string> binaryTree = CreateTree(controlExpression.ToCharArray());
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Дерево виразу: ");
            Console.ResetColor();
            binaryTree.PrintTree();
            Console.WriteLine();

            string[] elementsOfExpression = binaryTree.PreorderTraversal();
            Console.WriteLine("Вираз в інфіксній формі: " + controlExpression);
            Console.Write("Вираз у польській нотації: ");
            foreach (string item in elementsOfExpression)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static void ProcessUserExpression()
        {
            Console.Write("Введіть фираз: ");
            string userExpression = Console.ReadLine();
            if (!InputChecker.IsCorrectInput(userExpression))
            {
                return;
            }

            BinaryTree<string> binaryTree = CreateTree(userExpression.ToCharArray());
            
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Дерево виразу: ");
            Console.ResetColor();
            binaryTree.PrintTree();
            Console.WriteLine();

            string[] elementsOfExpression = binaryTree.PreorderTraversal();
            Console.WriteLine("Вираз в інфіксній формі: " + userExpression);
            Console.Write("Вираз у польській нотації: ");
            foreach (string item in elementsOfExpression)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static BinaryTree<string> CreateTree(char[] expression)
        {
            BinaryTree<string> tree = ProcessSubtree(new BinaryTree<string>(expression.Length), expression, 0);
            return tree;
        }

        static BinaryTree<string> ProcessSubtree(BinaryTree<string> binaryTree, char[] subexpression, int nodeIndex)
        {
            bool containsBrackets = false;
            int insideBrackets = 0;
            for (int i = subexpression.Length - 1; i > -1; i--)
            {
                if (subexpression[i] == '(')
                {
                    insideBrackets -= 1;
                    containsBrackets = true;
                }
                else if (subexpression[i] == ')')
                {
                    insideBrackets += 1;
                }
                if (insideBrackets > 0)
                {
                    continue;
                }
                else if (subexpression[i] == '+' || subexpression[i] == '-')
                {
                    binaryTree.AddNode(nodeIndex, subexpression[i].ToString());

                    char[] rightSubtreeExpression = new char[i];
                    Array.Copy(subexpression, rightSubtreeExpression, i);

                    char[] leftSubtreeExpression = new char[subexpression.Length - 1 -i];
                    Array.Copy(subexpression, i+1, leftSubtreeExpression, 0, subexpression.Length - 1 - i);

                    binaryTree = ProcessSubtree(binaryTree, rightSubtreeExpression, nodeIndex*2 + 1);
                    binaryTree = ProcessSubtree(binaryTree, leftSubtreeExpression, nodeIndex*2 + 2);
                    return binaryTree;
                }
            }
            
            for (int i = subexpression.Length - 1; i > -1; i--)
            {
                if (subexpression[i] == '(')
                {
                    insideBrackets -= 1;
                }
                else if (subexpression[i] == ')')
                {
                    insideBrackets += 1;
                }

                if (insideBrackets > 0)
                {
                    continue;
                }
                else if (subexpression[i] == '*' || subexpression[i] == '/')
                {
                    binaryTree.AddNode(nodeIndex, subexpression[i].ToString());

                    char[] rightSubtreeExpression = new char[i];
                    Array.Copy(subexpression, rightSubtreeExpression, i);

                    char[] leftSubtreeExpression = new char[subexpression.Length - 1 - i];
                    Array.Copy(subexpression, i + 1, leftSubtreeExpression, 0, subexpression.Length - 1 - i);

                    binaryTree = ProcessSubtree(binaryTree, rightSubtreeExpression, nodeIndex*2 + 1);
                    binaryTree = ProcessSubtree(binaryTree, leftSubtreeExpression, nodeIndex*2 + 2);
                    return binaryTree;
                }
            }

            if (containsBrackets)
            {
                char[] newSubtree = new char[subexpression.Length - 2];
                Array.Copy(subexpression, 1, newSubtree, 0, subexpression.Length - 2);
                binaryTree = ProcessSubtree(binaryTree, newSubtree, nodeIndex);
                return binaryTree;
            }

            if (subexpression.Length == 0)
            {
                binaryTree.AddNode(nodeIndex, "0");
                return binaryTree;
            }
            binaryTree.AddNode(nodeIndex, new string(subexpression));

            return binaryTree;
        }
    }
}
