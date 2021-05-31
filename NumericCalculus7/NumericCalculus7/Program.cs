using System;

namespace NumericCalculus7
{
    class Program
    {
        static void Main(string[] args)
        {

            int N1 = 5, N2 = 5, M = 50;

            //for (int i = 0; i < 10; i++)
            //{
            //    Solver s = new Solver(N1, N2, M);
            //    s.Solve();
            //    s.Show();

            //    N1 += 5;
            //    N2 += 5;
            //    M = N1 * N1 + N2 * N2;
            //}


            Solver s = new Solver(100, 100, 150);
            s.showMatricies = false;
            s.Solve();
            s.Show();
        }
    }
}
