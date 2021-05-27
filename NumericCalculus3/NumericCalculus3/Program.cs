using System;

namespace NumericCalculus3
{
    class Program
    {
        static double FindBest(int N, Tuple<double, double> interval, double step)
        {
            double omegaValue = 0.0, maxValue = Double.PositiveInfinity;

            Solver s = new Solver(N);

            for (double omega = interval.Item1 + step; omega < interval.Item2; omega += step)
            {
                s.Solve(omega);
                if (maxValue > s.GetMaxError())
                {
                    maxValue = s.GetMaxError();
                    omegaValue = omega;
                }
            }
            return omegaValue;
        }

        static void Main(string[] args)
        {
            int N = 10;
            double omega = FindBest(N, new Tuple<double, double>(0.0, 2.0), 0.01);

            Solver s = new Solver(N);
            s.Solve(omega);
            s.Show();
        }
    }
}
