using System;

namespace NumericCalculus4
{
    class Solver
    {
        private int N, M;
        private double h, tau;

        private double[,] y;
        private double[,] z;

        private double sigma = 1.0;
        public bool showMatricies = false;

        public Solver(int N, int M)
        {
            this.M = M;
            this.N = N;

            h = 1.0 / (double)N;
            tau = 1.0 / (double)M;
        }

        private void Init()
        {
            y = new double[N + 1, M + 1];
            z = new double[N + 1, M + 1];

            for (int i = 0; i <= N; i++)
            {
                y[i, 0] = Utilities.U0(h * i);
            }

            for (int j = 0; j <= M; j++)
            {
                y[0, j] = Utilities.MU1(tau * j);
                y[N, j] = Utilities.MU2(tau * j);
            }

            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y);
            }

        }

        public void Solve(double sigmaValue)
        {
            Init();

            sigma = sigmaValue;

            double[] alpha = new double[N + 1];
            double[] beta = new double[N + 1];

            double gamma = tau / h / h;

            for (int j = 0; j < M; j++)
            {
                alpha[1] = 0.0;
                beta[1] = Utilities.MU1(tau * j);

                for (int i = 1; i < N; i++)
                {
                    double A = sigma * gamma;
                    double C = 1.0 + 2.0 * sigma * gamma;
                    double B = sigma * gamma;
                    double F = (1.0 - 2.0 * (1.0 - sigma) * gamma) * y[i,j] + (1.0 - sigma) * gamma * (y[i - 1,j] + y[i + 1,j]) + tau * Utilities.F(h * i, tau * (j + 0.5));

                    alpha[i + 1] = B / (C - A * alpha[i]);
                    beta[i + 1] = (A * beta[i] + F) / (C - A * alpha[i]);
                }

                for (int i = N - 1; i > 0; i--)
                {
                    y[i, j + 1] = alpha[i + 1] * y[i + 1, j + 1] + beta[i + 1];
                }
            }

            Utilities.ERROR(y, new Tuple<double, double>(h, tau), z);
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

            Console.WriteLine($"Solver(N={N}, M={M}, sigma={sigma})\t :: Max. error is {Utilities.MAX(z)}");
        }

        public double GetMaxError()
        {
            return Utilities.MAX(z);
        }
    }
}
