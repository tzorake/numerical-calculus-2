using System;

namespace NumericCulculus6_2
{
    class Utilities
    {
        public static double U(double x, double t)
        {
            return x * x * t * t + x + 1.0;
        }

        public static double PHI(double u)
        {
            return u * u * u;
        }

        public static double DPHI(double u)
        {
            return 3.0 * u * u;
        }

        public static double MU1(double t)
        {
            return 1.0;
        }

        public static double MU2(double t)
        {
            return t * t + 2.0;
        }

        public static double U0(double x)
        {
            return x + 1.0;
        }

        public static double F(double x, double t, double y)
        {
            return 6.0 * y * y * x * x * t - 2.0 * t * t;
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

        public static double NORM(double[,] y0, double[,] y1, int j)
        {
            double result = 0.0;
            for (int i = 0; i < y0.GetUpperBound(0) + 1; i++)
            {
                double pm = Math.Abs(y0[i, j] - y1[i, j]);
                result = pm > result ? pm : result;
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

        public static void COPY(double[,] source, double[,] destination, int j)
        {
            for (int i = 0; i < source.GetUpperBound(0) + 1; i++)
            {
                destination[i, j] = source[i, j];
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
