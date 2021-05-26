using System;

namespace NumericCalculus4
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s1 = new Solver(10, 10);

            s1.Solve(0.5);
            s1.Show();

            Solver s2 = new Solver(10, 100);

            s2.Solve(1.0);
            s2.Show();
        }
    }
}
