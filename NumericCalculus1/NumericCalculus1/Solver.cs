using System;

namespace NumericCalculus1
{
    class Solver
    {
        private int N;
        private double h;

        private static Tuple<double, double> interval = new Tuple<double, double>(1.0, 2.0);

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

            if (showMatricies)
            {
                Console.WriteLine("y:");
                Utilities.PRINT(y);
            }
        }

        public void SolveByEulerMethod()
        {
            Init();

            for (int i = 0; i < N; i++)
            {
                y[i + 1] = y[i] + h * Utilities.F(interval.Item1 + h*i, y[i]);
            }

            Utilities.ERROR(y, h, z);
        }

        public void SolveByRK2Method(double alpha, double omega)
        {
            Init();

            for (int i = 0; i < N; i++)
            {
                double x = interval.Item1 + h * i;
                y[i + 1] = (1 - omega) * Utilities.F(x, y[i]) + omega * Utilities.F(x + alpha * h, y[i] + alpha * h * Utilities.F(x, y[i])) * h + y[i];
            }

            Utilities.ERROR(y, h, z);
        }

        public void SolveByRK4Method()
        {
            Init();

            for (int i = 0; i < N; i++)
            {
                double x = interval.Item1 + h * i;

                double K1 = Utilities.F(x, y[i]);
                double K2 = Utilities.F(x + h / 2.0, y[i] + h * K1 / 2.0);
                double K3 = Utilities.F(x + h / 2.0, y[i] + h * K2 / 2.0);
                double K4 = Utilities.F(x + h, y[i] + h * K3);

                y[i + 1] = y[i] + 1.0 / 6.0 * (K1 + 2.0 * K2 + 2.0 * K3 + K4) * h;
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
