using System;

namespace NumericCalculus7
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s = new Solver(10, 10, 10);
            s.showMatricies = false;
            s.Solve();
            s.Show();

            s = new Solver(100, 100, 100);
            s.showMatricies = false;
            s.Solve();
            s.Show();
        }
    }
}
