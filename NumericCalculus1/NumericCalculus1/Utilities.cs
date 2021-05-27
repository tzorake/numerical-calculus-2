using System;

namespace NumericCalculus1
{
    class Utilities
    {
        private static Tuple<double, double> interval = new Tuple<double, double>(1.0, 2.0);

        // U(x) = 15/2*x + 1/x
        public static double U(double x)
        {
            return 7.5 * x + 1.0 / x;
        }

        // U(1) = 17/2
        public static double MU1(double x)
        {
            return 8.5;
        }

        // (15*x - y)/x = f(x,y)
        public static double F(double x, double y)
        {
            return (15 * x - y) / x;
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
                z[i] = Math.Abs(y[i] - U(interval.Item1 + step * i));
            }
        }
    }
}
