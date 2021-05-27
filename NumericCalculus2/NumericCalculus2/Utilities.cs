using System;

namespace NumericCalculus2
{
    class Utilities
    {
        // U(x) = x^3 + x/15 + 15
        public static double U(double x)
        {
            return x * x * x + x / 15.0 + 15.0;
        }

        // U(0) = 15
        public static double MU1(double x2)
        {
            return 15.0;
        }

        // U(1) = 1/15 + 16 = 241/15
        public static double MU2(double x2)
        {
            return 241.0/15.0;
        }

        // Q(x) = 15*x
        public static double Q(double x)
        {
            return 15.0 * x;
        }

        // d^2(x^3 + x/15 + 15)/dx^2 - q(x)*d(x^3 + x/15 + 15)/dx = -f(x)
        // -6*x + q(x)*(x^3 + x/15 + 15) = f(x)
        public static double F(double x)
        {
            return -6.0 * x + Q(x) * (x * x * x + x / 15.0 + 15.0);
        }

        public static double MAX(double[] y)
        {
            double result = 0.0;
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                result = y[i] > result ? y[i] : result;
            }
            return result;
        }

        public static void PRINT(double[] array)
        {
            for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
            {
                Console.Write(String.Format("{0,-6}", String.Format("{0:0.00}", array[i])));
            }
            Console.Write("\n");
        }

        public static void ERROR(double[] y, double step, double[] z)
        {
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                z[i] = Math.Abs(y[i] - U(step * i));
            }
        }
    }
}
