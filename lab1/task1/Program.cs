using static System.Console;
using static System.Math;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter x: ");
            double x = double.Parse(ReadLine());
            Write("Enter y: ");
            double y = double.Parse(ReadLine());
            Write("Enter z: ");
            double z = double.Parse(ReadLine());

            double sqrt = Pow(x, 2) + x * Pow(y, 2) + Pow(x, 2) * z;
            double a = Sin(x) / Sqrt(sqrt) + 2 * y;

            double denom_b = Cos(a);
            double b = Pow(x, 3) / denom_b;

            if (sqrt <= 0 || denom_b == 0)
            {
                WriteLine("Mistake!");
            }
            else
            {
                WriteLine("a = {0}", a);
                WriteLine("b = {0}", b);
            }
        }
    }
}