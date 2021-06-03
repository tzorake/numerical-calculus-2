using System;

namespace NumericCalculus7
{
    class Utilities
    {
        // U(x,t) = U(x1,x2,t) = (x1^3 + x2^3)*t^2 + x1 + x2
        public static double U(double x1, double x2, double t)
        {
            return (x1 * x1 * x1 + x2 * x2 * x2) * t * t + x1 + x2;
        }

        // U(0,x2,t) = x2^3*t^2 + x2
        public static double MU1(double x2, double t)
        {
            return x2 * x2 * x2 * t * t + x2;
        }

        // U(1,x2,t) = (x2^3 + 1)*t^2 + x2 + 1
        public static double MU2(double x2, double t)
        {
            return (x2 * x2 * x2 + 1.0) * t * t + x2 + 1.0;
        }

        // U(x1,0,t) = x1^3*t^2 + x1
        public static double MU3(double x1, double t)
        {
            return x1 * x1 * x1 * t * t + x1;
        }

        // U(x1,1,t) = (x1^3 + 1)*t^2 + x1 + 1
        public static double MU4(double x1, double t)
        {
            return (x1 * x1 * x1 + 1.0) * t * t + x1 + 1.0;
        }

        // U(x1,x2,0) = x1 + x2
        public static double U0(double x1, double x2)
        {
            return x1 + x2;
        }
        
        // f(x1,x2,t) = 2*t*(x1^3 + x2^3) - 6*t^2*(x1 + x2)
        public static double F(double x1, double x2,double t)
        {
            //return 2.0 * t * (x1 * x1 * x1 + x2 * x2 * x2) - 6 * t * t * (x1 + x2);
            return 2.0 * t * (x1 * x1 * x1 + x2 * x2 * x2) - 6.0 * t * t * x2 - 6.0 * t * t * x1;
        }

        public static double MAX(double[,,] y)
        {
            double result = 0.0;
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < y.GetUpperBound(1) + 1; j++)
                {
                    for (int n = 0; n < y.GetUpperBound(2) + 1; n++)
                    {
                        result = y[i, j, n] > result ? y[i, j, n] : result;
                    }
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

        public static void PRINT(double[,,] array)
        {
            for (int n = 0; n < array.GetUpperBound(2) + 1; n++)
            {
                // double tau = (double)n / (double)array.GetUpperBound(2);
                double[,] output = ARR3DTO2D(array, 2, n);
                // Console.WriteLine($"tau={tau:0.000}");
                PRINT(output);
            }
        }

        public static void COPY(double[,,] source, double[,,] destination)
        {
            for (int i = 0; i < source.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < source.GetUpperBound(1) + 1; j++)
                {
                    for (int n = 0; n < source.GetUpperBound(2) + 1; n++)
                    {
                        destination[i, j, n] = source[i, j, n];
                    }
                }
            }
        }

        public static void ERROR(double[,,] y, Tuple<double, double, double> steps, double[,,] z)
        {
            for (int i = 0; i < y.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < y.GetUpperBound(1) + 1; j++)
                {
                    for (int n = 0; n < y.GetUpperBound(2) + 1; n++)
                    {
                        z[i, j, n] = Math.Abs(y[i, j, n] - U(steps.Item1 * i, steps.Item2 * j, steps.Item3 * n));
                    }
                }
            }
        }

        public static double[,] ARR3DTO2D(double[,,] array, int axis, int index)
        {
            double[,] result = new double[0, 0];
            
            switch (axis)
            {
                case 0:
                    result = new double[array.GetUpperBound(1) + 1, array.GetUpperBound(2) + 1];

                    for (int j = 0; j < array.GetUpperBound(1) + 1; j++)
                    {
                        for (int n = 0; n < array.GetUpperBound(2) + 1; n++)
                        {
                            result[j, n] = array[index, j, n];
                        }
                    }

                    break;
                case 1:
                    result = new double[array.GetUpperBound(0) + 1, array.GetUpperBound(2) + 1];

                    for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
                    {
                        for (int n = 0; n < array.GetUpperBound(2) + 1; n++)
                        {
                            result[i, n] = array[i, index, n];
                        }
                    }

                    break;
                case 2:
                    result = new double[array.GetUpperBound(0) + 1, array.GetUpperBound(1) + 1];

                    for (int i = 0; i < array.GetUpperBound(0) + 1; i++)
                    {
                        for (int j = 0; j < array.GetUpperBound(1) + 1; j++)
                        {
                            result[i, j] = array[i, j, index];
                        }
                    }

                    break;
            }

            return result;
        }
    }
}
