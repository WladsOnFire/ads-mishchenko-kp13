﻿using System;

namespace t1
{
    class Program
    {
        static void Main(string[] args)
        {
            double x,y,z,a,b;
            int correct_v;
            correct_v = 0; x = 0; y = 0; z = 0;
            while (true)
            {
                try
                {
                    if (correct_v == 0)
                    {
                        Console.Write("x => ");
                        x = double.Parse(Console.ReadLine());
                        correct_v++;
                    }
                    if (correct_v == 1)
                    {
                        Console.Write("y => ");
                        y = double.Parse(Console.ReadLine());
                        correct_v++;
                    }
                    if (correct_v == 2)
                    {
                        Console.Write("z => ");
                        z = double.Parse(Console.ReadLine());
                        correct_v++;
                    }
                    break;

                }
                catch
                {
                    Console.WriteLine("Error! «variable» must be double");
                    Console.WriteLine("Press «Enter», to try again");
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            a = 0;
            int check = 0;
            if (x == 0 && z == 0)
            {
                Console.WriteLine("Error! 0^0 is a mistake");
                check = 1;
            }
            else
            {
                if (Math.Cbrt(Math.Pow(x, 2) - y * Math.Pow(z, 3)) != Math.Pow(x, z) * y)
                {
                    a = (x + y - z) / (Math.Pow(x, z) * y - Math.Cbrt(Math.Pow(x, 2) - y * Math.Pow(z, 3)));
                    Console.WriteLine("a = {0}", a);
                }
                else
                {
                    Console.WriteLine("Error! denominator can't be 0");
                    a = 0;
                }
            }

            if (a != 0)
            {
                b = Math.Cos((x * y + Math.Pow(y, 2)) / Math.Pow(a, 2));
                Console.WriteLine("b = {0}", b);
            }
            else
            {
                if (check == 0) { Console.WriteLine("Error! denominator can't be 0"); }
                else { Console.WriteLine("Error! No «a»"); }
            }
            



            Console.ReadLine();
        }
    }
}
