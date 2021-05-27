using System;

namespace NumericCalculus2
{
    class Solver
    {
        private int N;
        private double h;

        private double[] y;
        private double[] z;

        public bool showMatricies = false;

        public Solver(int N)
        {
            this.N = N;

            h = 1.0 / (double)N;
        }

        private void Init()
        {
            y = new double[N + 1];
            z = new double[N + 1];

            y[0] = Utilities.MU1(h * 0);
            y[N] = Utilities.MU2(h * N);

            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y);
            }
        }

        public void Solve()
        {
            Init();

            double[] alpha = new double[N + 1];
            double[] beta = new double[N + 1];

            alpha[1] = 0;
            beta[1] = Utilities.MU1(0.0);

            for (int i = 1; i < N; i++)
            {
                double A = 1.0;
                double C = 2.0 + h * h * Utilities.Q(h * i);
                double B = 1.0;
                double F = h * h * Utilities.F(h*i);

                alpha[i + 1] = B / (C - A * alpha[i]);
                beta[i + 1] = (A * beta[i] + F) / (C - A * alpha[i]);
            }

            for (int i = N - 1; i >= 0; i--)
            {
                y[i] = alpha[i + 1] * y[i + 1] + beta[i + 1];
            }

            Utilities.ERROR(y, h, z);
        }

        public void Show()
        {
            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y);

                Console.WriteLine("z:");
                Utilities.PRINT(z);
            }

            Console.WriteLine($"Solver(N={N})\t :: Max. error is {Utilities.MAX(z)}");
        }
    }
}
