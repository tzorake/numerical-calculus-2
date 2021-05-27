using System;

namespace NumericCalculus6
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s = new Solver(100, 10);
            s.Solve();
            s.Show();
        }
    }
}
