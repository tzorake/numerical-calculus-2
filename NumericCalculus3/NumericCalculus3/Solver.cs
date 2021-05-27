using System;

namespace NumericCalculus3
{
    class Solver
    {
        public readonly static int maxIter = 800;
        public readonly static double epsilon = 0.001;

        private int N;
        private double h;

        private double[,] y;
        private double[,] y_next;
        private double[,] z;

        private double sigma = 1.0;
        public bool showMatricies = false;

        private int iters = 0;
        private bool solutionFound = false;

        public Solver(int N)
        {
            this.N = N;

            h = 1.0 / (double)N;
        }

        private void Init()
        {
            y = new double[N + 1, N + 1];
            y_next = new double[N + 1, N + 1];
            z = new double[N + 1, N + 1];

            iters = 0;
            solutionFound = false;

            for (int i = 0; i <= N; i++)
            {
                y[0, i] = Utilities.MU1(h * i);
                y[N, i] = Utilities.MU2(h * i);
                y[i, 0] = Utilities.MU3(h * i);
                y[i, N] = Utilities.MU4(h * i);
            }

            Utilities.COPY(y, y_next);

            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y_next);
            }
        }

        public void Solve(double sigmaValue)
        {
            Init();

            sigma = sigmaValue;

            for (int iter = 0; iter < maxIter; iter++, iters++)
            {
                for (int i = 1; i < N; i++)
                {
                    for (int j = 1; j < N; j++)
                    {
                        y_next[i, j] = sigma / 4.0 * (y_next[i - 1, j] + y_next[i, j - 1] + y[i + 1, j] + y[i, j + 1] + h * h * Utilities.F(h * i, h * j)) + (1.0 - sigma) * y[i, j];
                    }
                }

                if (Utilities.NORM(y, y_next) < epsilon)
                {
                    solutionFound = true;
                    break;
                }

                Utilities.COPY(y_next, y);

            }

            Utilities.ERROR(y_next, new Tuple<double, double>(h, h), z);
        }

        public void Show()
        {
            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y_next);

                Console.WriteLine("z:");
                Utilities.PRINT(z);
            }

            if (solutionFound)
            {
                Console.WriteLine($"Solver(N={N}, simga={sigma:#.##})\t :: Max. error is {Utilities.MAX(z)}\n\t\t\t\t :: Iteration count is {iters}");
            }
            else
            {
                Console.WriteLine("Solution wasn't found");
            }
        }

        public double GetMaxError()
        {
            return Utilities.MAX(z);
        }
    }
}
