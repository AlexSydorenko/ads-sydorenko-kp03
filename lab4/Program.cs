using System;

namespace lab4
{
    class SLList
    {
        public Node head;
        public Node tail;
        public static int lengthOfList = 1;

        public class Node
        {
            public int data;
            public Node next;

            public Node(int data)
            {
                this.data = data;
            }

            public Node(int data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }

        public SLList(int data)
        {
            head = new Node(data);
            tail = head;
        }

        /* ОПЕРАЦІЇ НАД ОДНОЗВ'ЯЗНИМ СПИСКОМ */

        public void AddFirst(int data)
        {
            Node newHead = new Node(data);

            if (head == null) // перевіряємо, чи не є список порожнім
            {
                head = newHead;
            }
            else
            {
                newHead.next = head;
                head = newHead;
            }
            lengthOfList++;
        }

        public void AddAtPosition(int data, int position)
        {
            if (position > 0 && position <= lengthOfList + 1) 
            {
                if (position == 1)
                {
                    AddFirst(data);
                }
                else if (position == lengthOfList + 1)
                {
                    AddLast(data);
                }
                else
                {
                    Node newNode = new Node(data);
                    Node current = head;
                    for (int i = 1; i < position - 1; i++)
                    {
                        current = current.next;
                    }
                    newNode.next = current.next;
                    current.next = newNode;
                    lengthOfList++;
                }
            }
            else
            {
                Console.WriteLine($"Такої позиції не існує. Спробуйте в діапазоні від 1 до {lengthOfList}");
            }
        }

        public void AddLast(int data)
        {
            if (head == null)
            {
                head = new Node(data);
            }
            else
            {
                Node current = head;

                while (current.next != null)
                {
                    current = current.next;
                }

                current.next = new Node(data);
            }
            lengthOfList++;
        }

        public void DeleteFirst()
        {
            if (head != null && head.next != null)
            {
                head = head.next;
            }
            else
            {
                head = null;
                tail = null;
            }
            lengthOfList--;
        }

        public void DeleteFromPosition(int position)
        {
            if (head == null)
            {
                Console.WriteLine("Список порожній. Елементів для видалення немає.");
                return;
            }
            if (position <= 0 || position > lengthOfList)
            {
                Console.WriteLine($"Такої позиції не існує. Спробуйте в діапазоні від 1 до {lengthOfList}");
            }
            else if (position == 1)
            {
                DeleteFirst();
                lengthOfList--;
            }
            else if (position == lengthOfList)
            {
                DeleteLast();
                lengthOfList--;
            }
            else
            {
                Node current = head;
                for (int i = 1; i < position - 1; i++)
                {
                    current = current.next;
                }
                current.next = current.next.next;
                lengthOfList--;
            }
        }

        public void DeleteLast()
        {
            if (head != null && head.next != null) // перевіряємо, чи містить список вузли та чи не є голова останнім вузлом
            {
                Node current = head;

                while (current.next.next != null)
                {
                    current = current.next;
                }

                current.next = null;
            }
            else head = null;
            lengthOfList--;
        }

        public void AddAfterLastNegative(int data)
        {
            if (head == null)
            {
                head = new Node(data);
            }
            else
            {
                Node newNode = new Node(data);
                Node current = head;
                int negativeValue = 0;

                for (int i = 0; i < lengthOfList; i++)
                {
                    if (current.data < 0)
                    {
                        negativeValue = current.data;
                    }
                    current = current.next;
                }

                if (negativeValue == 0)
                {
                    AddLast(data);
                }

                current = head;
                for (int i = 0; i < lengthOfList; i++)
                {
                    if (current.data == negativeValue)
                    {
                        newNode.next = current.next;
                        current.next = newNode;
                        break;
                    }
                    current = current.next;
                }
            }
            lengthOfList++;
        }

        public void DeleteAllUnpaired()
        {
            if (head == null)
            {
                Console.WriteLine("Список порожній. Немає елементів для видалення.");
            }
            else
            {
                Node current = head;
                if (head.data % 2 == 1)
                {
                    head = head.next;
                    lengthOfList--;
                }
                for (int i = 0; i < lengthOfList; i++)
                {
                    if (current.next.data % 2 == 1)
                    {
                        current.next = current.next.next;
                        lengthOfList--;
                    }
                    current = current.next;
                }
            }
        }

        public void AddAfterTheSame(int data)
        {
            if (head == null)
            {
                head = new Node(data);
            }
            else
            {
                Node newNode = new Node(data);
                Node current = head;

                for (int i = 0; i < lengthOfList; i++)
                {
                    if (current.data == data)
                    {
                        newNode.next = current.next;
                        current.next = newNode;
                        return;
                    }
                    current = current.next;
                }
                newNode.next = current.next;
                current.next = newNode;
            }
        }

        public void Print()
        {
            Console.WriteLine();
            Console.Write("Поточний список: ");

            Node current = head;

            while (current != null)
            {
                Console.Write(current.data);
                if (current.next != null) Console.Write(" -> ");
                current = current.next;
            }

            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GetCommandList();
            Console.Write("Введiть початковий вузол: ");
            int firstNode;
            bool result1 = int.TryParse(Console.ReadLine(), out firstNode);
            if(result1 == false)
            {
                Console.WriteLine("Неправильно введенi данi! Ви маєте ввести ціле число!");
                return;
            }      

            SLList node = new SLList(firstNode);

            while(true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Введiть команду: ");
                Console.ResetColor();
                string command = Console.ReadLine();
                Console.WriteLine();

                if(command == "AddFirst")
                {
                    Console.Write("Введіть ціле число: ");
                    Console.WriteLine();
                    int newNode;
                    bool result = int.TryParse(Console.ReadLine(), out newNode);
                    if(result == false)
                    {
                        Console.Write("Неправильно введенi данi! Спробуйте ще раз.");
                    }   
                    else if(result == true)   
                    {
                        node.AddFirst(newNode);
                    }
                }
                else if(command == "AddLast")
                {
                    Console.Write("Введіть ціле число: ");
                    int newNode;
                    bool result = int.TryParse(Console.ReadLine(), out newNode);
                    if(result == false)
                    {
                        Console.Write("Неправильно введенi данi! Спробуйте ще раз.");
                    }   
                    else if(result == true)   
                    {
                        node.AddLast(newNode);
                    }
                }
                else if(command == "AddAtPosition")
                {
                    Console.Write("Введіть ціле число: ");
                    int newNode;
                    bool result = int.TryParse(Console.ReadLine(), out newNode);
                    if(result == false)
                    {
                        Console.Write("Неправильно введенi данi! Спробуйте ще раз.");
                    }   
                    else if(result == true)   
                    {
                        Console.Write("Введіть номер позиції, на яку хочете вставити елемент: ");
                        int index;
                        bool resultIndex = int.TryParse(Console.ReadLine(), out index);
                        if(resultIndex == false)
                        {
                            Console.WriteLine("Неправильно введенi данi! Спробуйте ще раз.");
                        }
                        else
                        {
                            node.AddAtPosition(newNode, index);
                        }
                    }
                }
                else if(command == "DeleteFirst")
                {
                    node.DeleteFirst();
                }
                else if(command == "DeleteLast")
                {
                    node.DeleteLast();
                }
                else if(command == "DeleteFromPosition")
                {
                    Console.Write("Введіть позицію, з якої хочете видалити елемент: ");
                    int index;
                    bool result = int.TryParse(Console.ReadLine(), out index);
                    if(result == false)
                    {
                        Console.WriteLine("Неправильно введенi данi! Спробуйте ще раз.");
                    }
                    else
                    {
                        node.DeleteFromPosition(index);
                    }
                }
                else if(command == "AddAfterLastNegative")
                {
                    Console.Write("Введіть ціле число: ");
                    int newNode;
                    bool result = int.TryParse(Console.ReadLine(), out newNode);
                    if(result == false)
                    {
                        Console.Write("Неправильно введенi данi! Спробуйте ще раз.");
                    }   
                    else  
                    {
                        node.AddAfterLastNegative(newNode);
                    }
                }
                else if(command == "DeleteAllUnpaired")
                {
                    node.DeleteAllUnpaired();
                }
                else if(command == "AddAfterTheSame")
                {
                    Console.Write("Введіть ціле число: ");
                    int newNode;
                    bool result = int.TryParse(Console.ReadLine(), out newNode);
                    if(result == false)
                    {
                        Console.Write("Неправильно введенi данi! Спробуйте ще раз.");
                    }   
                    else  
                    {
                        node.AddAfterTheSame(newNode);
                    }
                }
                else if(command == "Exit")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Bye!");
                    Console.ResetColor();
                    break;
                }
                else
                {
                    Console.WriteLine("Такої команди не існує! Спробуйте ввести іншу!");
                }
                node.Print();
            }
        }

        static void GetCommandList()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.Write("СПИСОК ДОСТУПНИХ КОМАНД:");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("AddFirst");
            Console.ResetColor();
                Console.WriteLine(" - додавання нового вузла у голову списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("AddLast");
            Console.ResetColor();
                Console.WriteLine(" - додавання нового вузла у хвiст списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("DeleteFirst");
            Console.ResetColor();
                Console.WriteLine(" - видалення голови списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("DeleteLast");
            Console.ResetColor();
                Console.WriteLine(" - видалення хвоста списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("AddAtPosition");
            Console.ResetColor();
                Console.WriteLine(" - додавання нового вузла на визначену позицiю");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("DeleteFromPosition");
            Console.ResetColor();
                Console.WriteLine(" - видалення вузла з визначеної позиції");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("AddAfterLastNegative");
            Console.ResetColor();
                Console.WriteLine(" - додати новий вузол після останнього вузла, який містить від’ємне значення, інакше – після хвоста списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("DeleteAllUnpaired");
            Console.ResetColor();
                Console.WriteLine(" - видалити вузли з непарними значеннями");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("AddAfterTheSame");
            Console.ResetColor();
                Console.WriteLine(" - додати новий вузол після вузла з рівним значенням або на початок списку, якщо вузла з рівним значенням немає у списку");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("Print");
            Console.ResetColor();
                Console.WriteLine(" - виведення вмiсту списку");
            
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("Exit");
            Console.ResetColor();
                Console.WriteLine(" - завершення програми");

            Console.WriteLine();
        }
    }
}
