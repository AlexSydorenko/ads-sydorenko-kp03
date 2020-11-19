using System;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть розмір квадратної матриці:");
            Console.Write("N = ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("M = ");
            int m = int.Parse(Console.ReadLine());

            Console.WriteLine();

            if (n != m)
            {
                Console.WriteLine("Матриця квадратна! Спробуйте ще раз!");
            }
            else
            {
                Console.WriteLine("Натисніть 1, щоб згенерувати псевдовипадкову матрицю, або 2, щоб викорстати контрольний приклад:");
                int ChosenVariant = int.Parse(Console.ReadLine());
                Console.WriteLine();
                if (ChosenVariant != 1 && ChosenVariant != 2)
                {
                    Console.WriteLine("Помилка! Переконайтесь, що ввели правильні дані!");
                }
                else
                {
                    // j - стовпець
                    // i - рядок
                    // D - діагональ
                    // G - горизонталь
                    // V - вертикаль
                    int[,] matrix = new int[n, m];
                    if (ChosenVariant == 1)
                    {
                        Random random = new Random();
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < m; j++)
                            {
                                matrix[i, j] = random.Next(10, 99);
                                Console.Write("{0} ", matrix[i, j]);
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        int ElementOfMatrix = 0;
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < m; j++)
                            {
                                matrix[i, j] = ElementOfMatrix;
                                Console.Write("{0} ", matrix[i, j]);
                                ElementOfMatrix++;
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                    }

                    int MinValue = 99;
                    int MaxValue = 0;
                    int iMin = 0;
                    int jMin = 0;
                    int iMax = 0;
                    int jMax = 0;

                    Console.WriteLine("Частина матриці під головною діагоналлю: ");

                    int NumOfTurns1 = 0;   //змінна, що рахуватиме к-ть так званих поворотів (переходів від одного типу руху до іншого)
                    
                    //змінні, у яких будуть зберігатися дані про початок та кінець відрізку
                    //оскільки при горизонтальному (вертикальному) русі номер рядка (стовпця) не змінюється, у таких випадках немає сенсу створювати дві змінні
                    int Gi_1 = matrix.GetLength(0) - 1;
                    int StartingPoint_Gj_1 = matrix.GetLength(1) - 2;
                    int EndPoint_Gj_1 = 0;

                    int StartingPoint_Vi_1 = matrix.GetLength(0) - 2;
                    int EndPoint_Vi_1 = 2;
                    int Vj_1 = 0;

                    int StartingPoint_Di_1 = 1;
                    int EndPoint_Di_1 = matrix.GetLength(0) - 2;
                    int StartingPoint_Dj_1 = 0;
                    int EndPoint_Dj_1 = matrix.GetLength(1) - 3;

                    while (NumOfTurns1 < n-1)
                    {
                        //горизонаталь
                        for (int j = StartingPoint_Gj_1; j >= EndPoint_Gj_1; j--)
                        {
                            Console.Write("{0} ", matrix[Gi_1, j]);
                            if (matrix[Gi_1, j] <= MinValue)
                            {
                                MinValue = matrix[Gi_1, j];
                                iMin = Gi_1;
                                jMin = j;
                            }
                        }
                        NumOfTurns1++;
                        Gi_1--;
                        StartingPoint_Gj_1 -= 2;
                        EndPoint_Gj_1++;

                        //вертикаль
                        for (int i = StartingPoint_Vi_1; i >= EndPoint_Vi_1; i--)
                        {
                            Console.Write("{0} ", matrix[i, Vj_1]);
                            if (matrix[i, Vj_1] <= MinValue)
                            {
                                MinValue = matrix[i, Vj_1];
                                iMin = i;
                                jMin = Vj_1;
                            }
                        }
                        NumOfTurns1++;
                        StartingPoint_Vi_1--;
                        EndPoint_Vi_1 += 2;
                        Vj_1++;

                        //діагональ
                        int Dj_1 = StartingPoint_Dj_1;
                        for (int i = StartingPoint_Di_1; i <= EndPoint_Di_1; i++)
                        {
                            Console.Write("{0} ", matrix[i, Dj_1]);
                            if (matrix[i, Dj_1] <= MinValue)
                            {
                                MinValue = matrix[i, Dj_1];
                                iMin = i;
                                jMin = Dj_1;
                            }
                            Dj_1++;
                        }
                        NumOfTurns1++;
                        StartingPoint_Di_1 += 2;
                        EndPoint_Di_1--;
                        StartingPoint_Dj_1++;
                        EndPoint_Dj_1 -= 2;
                    }
                    Console.WriteLine();

                    Console.WriteLine("Друга частина матриці: ");

                    int NumOfTurns2 = 0;

                    int StartingPoint_Di_2 = matrix.GetLength(0) - 1;
                    int EndPoint_Di_2 = 0;
                    int StartingPoint_Dj_2 = matrix.GetLength(1) - 1;
                    int EndPoint_Dj_2 = 0;

                    int Gi_2 = 0;
                    int StartingPoint_Gj_2 = 1;
                    int EndPoint_Gj_2 = matrix.GetLength(1) - 2;

                    int StartingPoint_Vi_2 = 0;
                    int EndPoint_Vi_2 = matrix.GetLength(0) - 2;
                    int Vj_2 = matrix.GetLength(1) - 1;

                    while (NumOfTurns2 < n)
                    {
                        // діагональ
                        int Dj_2 = StartingPoint_Dj_2;
                        for (int i = StartingPoint_Di_2; i >= EndPoint_Di_2; i--)
                        {
                            Console.Write("{0} ", matrix[i, Dj_2]);
                            if (matrix[i, Dj_2] >= MaxValue)
                            {
                                MaxValue = matrix[i, Dj_2];
                                iMax = i;
                                jMax = Dj_2;
                            }
                            Dj_2--;
                        }
                        NumOfTurns2++;
                        StartingPoint_Di_2 -= 2;
                        EndPoint_Di_2++;
                        StartingPoint_Dj_2--;
                        EndPoint_Dj_2 += 2;
                        
                        // горизонталь
                        for (int j = StartingPoint_Gj_2; j <= EndPoint_Gj_2; j++)
                        {
                            Console.Write("{0} ", matrix[Gi_2, j]);
                            if (matrix[Gi_2, j] >= MaxValue)
                            {
                                MaxValue = matrix[Gi_2, j];
                                iMax = Gi_2;
                                jMax = j;
                            }
                        }
                        NumOfTurns2++;
                        Gi_2++;
                        StartingPoint_Gj_2 += 2;
                        EndPoint_Gj_2--;

                        // вертикаль
                        for (int i = StartingPoint_Vi_2; i <= EndPoint_Vi_2; i++)
                        {
                            Console.Write("{0} ", matrix[i, Vj_2]);
                            if (matrix[i, Vj_2] >= MaxValue)
                            {
                                MaxValue = matrix[i, Vj_2];
                                iMax = i;
                                jMax = Vj_2;
                            }
                        }
                        NumOfTurns2++;
                        StartingPoint_Vi_2++;
                        EndPoint_Vi_2 -= 2;
                        Vj_2--;
                    }
                    Console.WriteLine();
                    Console.WriteLine("[{0}][{1}] {2} - мінімальне значення з частини матриці під головною діагоналлю", iMin, jMin, MinValue);
                    Console.WriteLine("[{0}][{1}] {2} - максимальне значення з другої частини матриці", iMax, jMax, MaxValue);
                }
            }
        }
    }
}