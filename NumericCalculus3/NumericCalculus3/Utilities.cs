using System;

namespace NumericCalculus3
{
    class Utilities
    {
        // U(x1,x2) = x1^3 + x2^3 + x1 + x2 + 1
        public static double U(double x1, double x2)
        {
            return x1 * x1 * x1 + x2 * x2 * x2 + x1 + x2 + 1.0;
        }

        // U(0,x2) = x2^3 + x2 + 1
        public static double MU1(double x2)
        {
            return x2 * x2 * x2 + x2 + 1.0;
        }

        // U(1,x2) = x2^3 + x2 + 3
        public static double MU2(double x2)
        {
            return x2 * x2 * x2 + x2 + 3.0;
        }

        // U(x1,0) = x1^3 + x1 + 1
        public static double MU3(double x1)
        {
            return x1 * x1 * x1 + x1 + 1.0;
        }

        // U(x1,1) = x1^3 + x1 + 3
        public static double MU4(double x1)
        {
            return x1 * x1 * x1 + x1 + 3.0;
        }

        // f(x1,x2) = -6*x1 - 6*x2
        public static double F(double x1, double x2)
        {
            return -6.0 * x1 - 6.0 * x2;
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

        public static double NORM(double[,] y0, double[,] y1)
        {
            double result = 0.0;
            for (int i = 0; i < y0.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < y0.GetUpperBound(1) + 1; j++)
                {
                    double pm = Math.Abs(y0[i, j] - y1[i, j]);
                    result = pm > result ? pm : result;
                }
            }
            return result;
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
