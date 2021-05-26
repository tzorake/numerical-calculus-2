using System;

namespace NumericCulculus6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s = new Solver(10, 10);
            s.Solve();
            s.Show();
        }
    }
}
