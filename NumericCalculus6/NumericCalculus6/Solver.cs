using System;

namespace NumericCulculus6_2
{
    class Solver
    {
        public readonly static int maxIter = 800;
        public readonly static double epsilon = 0.001;

        private int N, M;
        private double h, tau;

        private double[,] y;
        private double[,] y_next;
        private double[,] z;

        private bool solutionFound;

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
            y_next = new double[N + 1, M + 1];
            z = new double[N + 1, M + 1];

            solutionFound = false;

            for (int i = 0; i <= N; i++)
            {
                y[i, 0] = Utilities.U0(h * i);
            }

            for (int j = 0; j <= M; j++)
            {
                y[0, j] = Utilities.MU1(tau * j);
                y[N, j] = Utilities.MU2(tau * j);
            }

            // y_next = y.Clone() as double[,];
            Utilities.COPY(y, y_next);

            Console.WriteLine("y:");
            Utilities.PRINT(y_next);
        }

        // pseudo code of the newton's method
        //
        //for i = 1:maxIterations
        //  y = f(x0)
        //  yprime = fprime(x0)
        //
        //  if abs(yprime) < epsilon            # Stop if the denominator is too small
        //    break
        //  end
        //
        //  global x1 = x0 - y/yprime           # Do Newton's computation
        //
        //  if abs(x1 - x0) <= tolerance        # Stop when the result is within the desired tolerance
        //    global solutionFound = true
        //    break
        //  end
        //
        //  global x0 = x1                      # Update x0 to start the process again
        //end

        public void Solve()
        {
            Init();

            double[] alpha = new double[N + 1];
            double[] beta = new double[N + 1];

            for (int j = 0; j < M; j++)
            {
                alpha[1] = 0.0;
                beta[1] = Utilities.MU1((j + 1) * tau);
                for (int iter = 0; iter < maxIter; iter++)
                {
                    for (int i = 1; i < N; i++)
                    {
                        double A = tau / h / h;
                        double C = 2.0 * tau / h / h + Utilities.DPHI(y[i, j + 1]);
                        double B = tau / h / h;
                        double F = Utilities.PHI(y_next[i, j]) - Utilities.PHI(y[i, j + 1]) + Utilities.DPHI(y[i, j + 1]) * y[i, j + 1] + tau * Utilities.F(h * i, tau * j, y[i, j]);

                        alpha[i + 1] = B / (C - A * alpha[i]);
                        beta[i + 1] = (A * beta[i] + F) / (C - A * alpha[i]);
                    }

                    for (int i = N - 1; i > 0; i--)
                    {
                        y_next[i, j + 1] = alpha[i + 1] * y_next[i + 1, j + 1] + beta[i + 1];
                    }

                    if (Utilities.NORM(y, y_next, j + 1) < epsilon)
                    {
                        solutionFound = true;
                        break;
                    }

                    Utilities.COPY(y_next, y, j + 1);
                }
            }

            Utilities.ERROR(y_next, new Tuple<double, double>(h, tau), z);
        }

        public void Show()
        {
            if (solutionFound)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y_next);

                Console.WriteLine("z:");
                Utilities.PRINT(z);

                Console.WriteLine($"Max. error is {Utilities.MAX(z)}");
            } 
            else
            {
                Console.WriteLine("Solution wasn't found");
            }
        }
    }
}
