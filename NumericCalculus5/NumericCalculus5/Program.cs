using System;
using System.Collections.Generic;

namespace NumericCalculus5 {
    class Program {
        static int  N1 = 100, M1 = 100, 
                    N2 = 10,  M2 = 100;

        static double FindBest(int N, int M, Tuple<double, double> interval, double step)
        {
            double omegaValue = 0.0, maxValue = Double.PositiveInfinity;

            Solver s = new Solver(N, M);

            for (double omega = interval.Item1 + step; omega < interval.Item2; omega+=step)
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

        static void Main(string[] args) {
            double step = 0.1;
            Tuple<double, double> interval = new Tuple<double, double>(0, 10);

            double omega = FindBest(N1, M1, interval, step);

            Solver s1 = new Solver(N1, M1);
            s1.Solve(omega);
            s1.Show();

            omega = FindBest(N2, M2, interval, step);

            Solver s2 = new Solver(N2, M2);
            s2.Solve(omega);
            s2.Show();

        }
    }
}
