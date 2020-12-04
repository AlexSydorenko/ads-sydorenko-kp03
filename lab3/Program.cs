using System;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть розмір матриці!");
            int n = 0;
            int m = 0;
            Console.Write("N = ");
            bool result1 = int.TryParse(Console.ReadLine(), out n);
            Console.Write("M = ");
            bool result2 = int.TryParse(Console.ReadLine(), out m);
            if (result1 == false || result2 == false)
            {
                Console.WriteLine("Помилка! Переконайтесь, що ввели правильні дані!");
            }
            else
            { 
                int[,] originalMatrix = new int[n, m];
                GetRandomMatrix(originalMatrix);
                int[,] sortedMatrix = new int[n, m];
                System.Array.Copy(originalMatrix, sortedMatrix, originalMatrix.Length);

                int[] array = GetOneDimensionalArray(originalMatrix);
                CombSort(array);
                GetSortedMatrix(array, originalMatrix);

                Console.WriteLine("Початкова (невідсортована) матриця:");
                PrintOriginalMatrix(originalMatrix, sortedMatrix);
                Console.WriteLine();
                Console.WriteLine("Відсортована матриця:");
                PrintSortedMatrix(originalMatrix, sortedMatrix);
            }
        }

        static int[,] GetRandomMatrix(int[,] matrix)
        {
            Random random = new Random();
            // i - рядок
            // j - стовпець
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            bool uniqueness = false;
            do 
            {
                int numberOfUniqueElements = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        for (int x = 0; x < n; x++)
                        {
                            for (int y = 0; y < m; y++)
                            {
                                if (matrix[i,j] == matrix[x,y])
                                {
                                    numberOfUniqueElements++;
                                }
                            }
                        }
                    }
                }
                // перевіряємо, чи кількість унікальних елементів дорівнює кількості елементів матриці
                // якщо так, то матриця заповнена елементами, що не повторюються;
                // якщо ні, заповнюємо матрицю новими випадковими числами
                if (numberOfUniqueElements == n * m)
                {
                    break;
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            matrix[i,j] = random.Next(10, 99);
                        }
                    }
                    uniqueness = false;
                }
            }
            while(uniqueness == false);

            Console.WriteLine();
            return matrix;
        }

        static int[] GetOneDimensionalArray(int[,] matrix)
        {
            int size = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++) 
                {   
                    if (i == j && i != matrix.GetLength(0) - 1)
                    {
                        for (int k = i + 1; k < matrix.GetLength(0); k++) 
                        {                           
                            size++;
                        }
                    }
                }
            }
            int[] array = new int[size];
            int arrayIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j && i != matrix.GetLength(0) - 1)
                    {
                        for (int k = i + 1; k < matrix.GetLength(0); k++)
                        {
                            array[arrayIndex] = matrix[k, j];
                            arrayIndex++;
                        }
                    }
                }
            }
            return array;
        }

        static int[] CombSort(int[] array)
        {
            bool sorted = false;
            int gap = array.Length;
            while (gap != 1 || sorted)
            {
                sorted = true;
                gap = gap * 10 / 13;
                if (gap < 1)
                {
                    gap = 1;
                }
                for (int i = 0; i < array.Length - gap; i++)
                {
                    int temp = array[i];
                    if (array[i] > array[i + gap])
                    {
                        array[i] = array[i + gap];
                        array [i + gap] = temp;
                        sorted = false;
                    }
                }  
            }
            return array;
        }

        static int[,] GetSortedMatrix(int[] array, int[,] matrix)
        {
            int arrayIndex = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == j && i != matrix.GetLength(0) - 1)
                    {
                        for (int k = i + 1; k < matrix.GetLength(0); k++) 
                        {
                            matrix[k, j] = array[arrayIndex];
                            arrayIndex++;
                        }
                    }
                }
            }
            return matrix;
        }

        static void PrintOriginalMatrix(int[,] matrix1, int[,] matrix2)
        {
            // i - рядок
            // j - стовпець
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.Write(matrix2[i, j] + " ");
                    }
                    else
                    {
                        Console.BackgroundColor = 0;
                        Console.Write(matrix2[i, j] + " ");
                    }
                }
                Console.BackgroundColor = 0;
                Console.WriteLine();
            }
            // Console.BackgroundColor = 0;
        }

        static void PrintSortedMatrix(int[,] matrix1, int[,] matrix2)
        {
            // i - рядок
            // j - стовпець
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    if (matrix1[i, j] != matrix2[i, j])
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(matrix1[i, j] + " ");
                    }
                    else
                    {
                        Console.BackgroundColor = 0;
                        Console.Write(matrix1[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = 0;
        }
    }
}
