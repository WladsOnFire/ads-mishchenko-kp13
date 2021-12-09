using System;
using static System.Console;

namespace var3_ads
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            int n = 0;
            Write("N -> ");
            try
            {
                n = int.Parse(ReadLine());
                if (n < 1) throw new Exception("«N» can not be < 1");
            }
            catch(Exception ex)
            {
                WriteLine(ex.Message.ToString());
                return;
            }

            WriteLine("\n");

            double[] array = new double[n];
            for (int i = 0; i<n; i++)
                array[i] = Math.Round(rnd.Next(1, 10) + rnd.NextDouble(), 2);

            double mid = array[0];
            int lessCount = 0;
            int moreCount = 0;
            for (int i = 0; i<n; i++)
            {
                if (array[i] <= mid)
                {
                    ForegroundColor = ConsoleColor.Red;
                    if (i!=0) lessCount++;
                }
                else if (array[i] > mid)
                {
                    ForegroundColor = ConsoleColor.Green;
                    moreCount++;
                }

                    if (i == 0) ForegroundColor = ConsoleColor.Cyan;
                Write(array[i] + " ");

                ForegroundColor = ConsoleColor.White;
            }
            WriteLine();

            //creating less and more arrays
            double[] more = new double[moreCount];
            double[] less = new double[lessCount];

            moreCount = 0;
            lessCount = 0;
            for (int i = 1; i < n; i++)
            {
                if (array[i] > mid)
                {
                    more[moreCount] = array[i];
                    moreCount++;
                }
                if (array[i] <= mid)
                {
                    less[lessCount] = array[i];
                    lessCount++;
                }
                    
            }

            //sorting less (left part)
            bool sorted = false;
            while (sorted == false)
            {
                sorted = true;
                for (int i = 0; i < less.Length-1; i += 2)
                {
                    if (less[i] < less[i + 1])
                    {
                        double buffer = less[i];
                        less[i] = less[i + 1];
                        less[i + 1] = buffer;
                        sorted = false;
                    }
                }
                for (int i = 1; i < less.Length-1; i += 2)
                {
                    if (less[i] < less[i + 1])
                    {
                        double buffer = less[i];
                        less[i] = less[i + 1];
                        less[i + 1] = buffer;
                        sorted = false;
                    }
                }
            }

            //sorting more (right part)
            sorted = false;
            while (sorted == false)
            {
                sorted = true;
                for (int i = 0; i < more.Length - 1; i += 2)
                {
                    if (more[i] > more[i + 1])
                    {
                        double buffer = more[i];
                        more[i] = more[i + 1];
                        more[i + 1] = buffer;
                        sorted = false;
                    }
                }
                for (int i = 1; i < more.Length - 1; i += 2)
                {
                    if (more[i] > more[i + 1])
                    {
                        double buffer = more[i];
                        more[i] = more[i + 1];
                        more[i + 1] = buffer;
                        sorted = false;
                    }
                }
            }

            double[] result = new double[n];
            for(int i=0; i<n; i++)
            {
                if (i < less.Length) result[i] = less[i];
                if (i == less.Length) result[i] = mid;
                if (i > less.Length) result[i] = more[i - less.Length-1];
            }

            WriteLine("\n");

            for (int i = 0; i < n; i++)
            {
                if (result[i] <= mid)
                {
                    ForegroundColor = ConsoleColor.Red;
                }
                else if (result[i] > mid)
                {
                    ForegroundColor = ConsoleColor.Green;
                }

                if (i == less.Length) ForegroundColor = ConsoleColor.Cyan;
                Write(result[i] + " ");
                ForegroundColor = ConsoleColor.White;
            }
            WriteLine();

            ReadLine();
        }
    }
}
