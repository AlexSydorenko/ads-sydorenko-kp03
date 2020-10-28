using static System.Console;
using static System.Math;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int num = 10; num <= 9999; num++)
            {
                int SourseNum = num;
                int ResNum = num;
                int NumOfDigits = 0;
                int sum = 0;
                int digit;
                do
                {
                    NumOfDigits++;
                    SourseNum /= 10;
                }
                while (SourseNum > 0);

                for (int i = 0; i < NumOfDigits; i++)
                {
                    digit = ResNum % 10;
                    sum += (int)Pow(digit, NumOfDigits);
                    ResNum /= 10;
                }

                if (sum == num)
                {
                    WriteLine(num);
                }
            }
        }
    }
}