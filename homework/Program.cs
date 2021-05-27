using System;
using System.Collections.Generic;

namespace homework
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] matrix = new double[,] {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            };

            HashSet<double> numbers = GetNumsWhichAreEqualToAvareges(matrix);
            foreach (double num in numbers)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }

        static HashSet<double> GetNumsWhichAreEqualToAvareges(double[,] matrix)
        {
            HashSet<double> numbers = new HashSet<double>();
            double[] avgNumbers = new double[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                double[] row = new double[matrix.GetLength(1)];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                avgNumbers[i] = GetAverageOfTheRow(row);
            }

            foreach (double number in matrix)
            {
                foreach (double avgNum in avgNumbers)
                {
                    if (number == avgNum)
                    {
                        numbers.Add(number);
                        break;
                    }
                }
            }

            return numbers;
        }

        static double GetAverageOfTheRow(double[] row)
        {
            double sum = 0;
            foreach (double num in row)
            {
                sum += num;
            }
            return sum / row.Length;
        }
    }
}
