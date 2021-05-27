using System;

namespace NumericCalculus2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s = new Solver(10);
            s.Solve();
            s.Show();
        }
    }
}
