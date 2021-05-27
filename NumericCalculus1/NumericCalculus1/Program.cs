using System;

namespace NumericCalculus1
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver s = new Solver(10);

            Console.WriteLine("\nEuler's Difference Scheme: ");
            s.SolveByEulerMethod();
            s.Show();

            Console.WriteLine("\nRunge-Kutta Method 2nd Order: ");
            s.SolveByRK2Method(0.5, 1.0);
            s.Show();

            Console.WriteLine("\nRunge-Kutta Method 4nd Order: ");
            s.SolveByRK4Method();
            s.Show();
        }
    }
}
