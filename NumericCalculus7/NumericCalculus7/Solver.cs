using System;

namespace NumericCalculus7
{
    class Solver
    {
        private int N1, N2, M;
        private double h1, h2, tau;

        private double[,,] y;
        private double[,,] z;

        public bool showMatricies = true;

        public Solver(int N1, int N2, int M)
        {
            this.N1 = N1;
            this.N2 = N2;
            this.M = M;

            h1 = 1.0 / (double)N1;
            h2 = 1.0 / (double)N2;
            tau = 1.0 / (double)M;
        }

        private void Init()
        {
            y = new double[N1 + 1, N2 + 1, M + 1];
            z = new double[N1 + 1, N2 + 1, M + 1];

            for (int i = 0; i <= N1; i++)
            {
                for (int j = 0; j <= N2; j++)
                {
                    y[i, j, 0] = Utilities.U0(h1 * i, h2 * j);
                }
            }

            for (int j = 0; j <= N2; j++)
            {
                for (int n = 1; n <= M; n++)
                {
                    y[0, j, n] = Utilities.MU1(h2 * j, tau * n);
                    y[N1, j, n] = Utilities.MU2(h2 * j, tau * n);
                }
            }

            for (int i = 0; i <= N1; i++)
            {
                for (int n = 1; n <= M; n++)
                {
                    y[i, 0, n] = Utilities.MU3(h1 * i, tau * n);
                    y[i, N2, n] = Utilities.MU4(h1 * i, tau * n);
                }
            }

            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y);
            }

        }

        public void Solve()
        {
            Init();

            for (int n = 0; n < M; n++)
            {
                double[,] y_half = new double[N1 + 1, N2 + 1];

                for (int j = 0; j <= N2; j++)
                {
                    y_half[0, j] = (Utilities.MU1(h2 * j, tau * (n + 1)) + Utilities.MU1(h2 * j, tau * n)) / 2.0 - tau / 4.0 * ((Utilities.MU1(h2 * (j - 1), tau * (n + 1)) - Utilities.MU1(h2 * (j - 1), tau * n)) - 2.0 * (Utilities.MU1(h2 * j, tau * (n + 1)) - Utilities.MU1(h2 * j, tau * n)) + (Utilities.MU1(h2 * (j + 1), tau * (n + 1)) - Utilities.MU1(h2 * (j + 1), tau * n))) / h2 / h2;
                    y_half[N1, j] = (Utilities.MU2(h2 * j, tau * (n + 1)) + Utilities.MU2(h2 * j, tau * n)) / 2.0 - tau / 4.0 * ((Utilities.MU2(h2 * (j - 1), tau * (n + 1)) - Utilities.MU2(h2 * (j - 1), tau * n)) - 2.0 * (Utilities.MU2(h2 * j, tau * (n + 1)) - Utilities.MU2(h2 * j, tau * n)) + (Utilities.MU2(h2 * (j + 1), tau * (n + 1)) - Utilities.MU2(h2 * (j + 1), tau * n))) / h2 / h2;
                }

                // Thomas algorithm for (n + 1/2)th layer
                for (int j = 1; j < N2; j++)
                {
                    double[] alpha = new double[N1 + 1];
                    double[] beta = new double[N1 + 1];

                    alpha[1] = 0.0;
                    beta[1] = y_half[0, j];
                    //beta[1] = Utilities.MU1(h2 * j, tau * n);

                    for (int i = 1; i < N1; i++)
                    {

                        double A = 1.0 / h1 / h1;
                        double C = 2.0 * (1.0 / h1 / h1 + 1.0 / tau);
                        double B = 1.0 / h1 / h1;
                        double F = 2.0 / tau * y[i, j, n] + 1.0 / h2 / h2 * y[i, j + 1, n] - 2.0 / h2 / h2 * y[i, j, n] + 1.0 / h2 / h2 * y[i, j - 1, n] + Utilities.F(h1 * i, h2 * j, tau * n);

                        alpha[i + 1] = B / (C - A * alpha[i]);
                        beta[i + 1] = (A * beta[i] + F) / (C - A * alpha[i]);
                    }

                    for (int i = N1 - 1; i > 0; i--)
                    {
                        y_half[i, j] = alpha[i + 1] * y_half[i + 1, j] + beta[i + 1];
                    }
                }

                // Thomas algorithm for (n + 1)th layer
                for (int i = 1; i < N1; i++)
                {
                    double[] alpha = new double[N2 + 1];
                    double[] beta = new double[N2 + 1];

                    alpha[1] = 0.0;
                    beta[1] = y[i,0,n+1];
                    //beta[1] = Utilities.MU3(h1 * i, tau * n);

                    for (int j = 1; j < N2; j++)
                    {
                        double A = 1.0 / h2 / h2;
                        double C = 2.0 * (1.0 / h2 / h2 + 1.0 / tau);
                        double B = 1.0 / h2 / h2;
                        double F = 2.0 / tau * y_half[i, j] + 1.0 / h1 / h1 * y_half[i + 1, j] - 2.0 / h1 / h1 * y_half[i, j] + 1.0 / h1 / h1 * y_half[i - 1, j] + Utilities.F(h1 * i, h2 * j, tau * n);

                        alpha[j + 1] = B / (C - A * alpha[j]);
                        beta[j + 1] = (A * beta[j] + F) / (C - A * alpha[j]);
                    }

                    for (int j = N2 - 1; j > 0; j--)
                    {
                        y[i, j, n + 1] = alpha[j + 1] * y[i, j + 1, n + 1] + beta[j + 1];
                    }
                }
            }

            Utilities.ERROR(y, new Tuple<double, double, double>(h1, h2, tau), z);

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

            Console.WriteLine($"Solver(N1={N1}, N2={N2}, M={M})\t :: Max. error is {Utilities.MAX(z)}");
        }
    }
}
