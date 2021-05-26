using System;

namespace NumericCalculus4
{
    class Utilities
    {
        // U(x,t) = x^2*t^2 + x*t + x + 1
        public static double U(double x, double t)
        {
            return x * x * t * t + x * t + x + 1.0;
        }

        // U(0,t) = 1
        public static double MU1(double t)
        {
            return 1.0;
        }

        // U(1,t) = t^2 + t + 2
        public static double MU2(double t)
        {
            return t * t + t + 2.0;
        }

        // U(x,0) = x + 1
        public static double U0(double x)
        {
            return x + 1.0;
        }

        // d(2*x^2*t + x*)/dt = d^2(x^2*t^2 + x*t + x + 1)/dx^2 + f(x,t)
        // f(x,t) = 2*x^2*t + x - 2*t^2
        public static double F(double x, double t)
        {
            return 2.0 * x * x * t + x - 2.0 * t * t;
        }

        public static double MAX(double[,] y)
        {
            double result = 0.0;
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < y.GetUpperBound(1) + 1; j++)
                {
                    result = y[i, j] > result ? y[i, j] : result;
                }
            }
            return result;
        }

        public static void PRINT(double[,] array)
        {
            for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < array.GetUpperBound(1) + 1; j++)
                {
                    Console.Write(String.Format("{0,-6}", String.Format("{0:0.00}", array[i, j])));
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        public static void COPY(double[,] source, double[,] destination)
        {
            for (int i = 0; i < source.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < source.GetUpperBound(1) + 1; j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
        }

        public static void ERROR(double[,] y, Tuple<double, double> steps, double[,] z)
        {
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < y.GetUpperBound(1) + 1; j++)
                {
                    z[i, j] = Math.Abs(y[i, j] - U(steps.Item1 * i, steps.Item2 * j));
                }
            }
        }
    }
}
