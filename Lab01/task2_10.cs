using System;

namespace t2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, m, sum_n, sum_m;
            int correct = 0;
            n = 0; m = 0; sum_n = 0; sum_m = 0;
            while (true)
            {
                try
                {
                    if (correct == 0)
                    {
                        Console.Write("n => ");
                        n = int.Parse(Console.ReadLine());
                        correct++;
                    }
                    else if (correct == 1)
                    {
                        Console.Write("m => ");
                        m = int.Parse(Console.ReadLine());
                        correct++;
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Error! Incorrect value. Variable must be double.");
                    Console.WriteLine("Press «Enter» to try again.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("[n]{0}\t [m]{1}", n, m);
            Console.WriteLine("-------------------");

            for (int i=1; i<n; i++)
            {
                if (n % i == 0)
                {
                    sum_n += i;
                    Console.Write("[i_n]{0}\t", i);
                }
            }

            Console.WriteLine();

            for (int i = 1; i < m; i++)
            {
                if (m % i == 0)
                {
                    sum_m += i;
                    Console.Write("[i_m]{0}\t", i);
                }
            }
            Console.WriteLine("\n-------------------");
            Console.WriteLine("[sum_n]{0}", sum_n);
            Console.WriteLine("[sum_m]{0}", sum_m);
            Console.WriteLine("-------------------");
            if (sum_n == m && sum_m == n)
            {
                Console.WriteLine("True. Numbers are amicable.");
            }
            else
            {
                Console.WriteLine("False. Numbers are not amicable.");
            }

            Console.ReadLine();
        }
    }
}
