using System;
using static System.Console;

class program
{
    static void Main()
    {
        Random rnd = new Random();
        Write("N = ");
        int n = int.Parse(ReadLine());
        Write("M = ");
        int m = int.Parse(ReadLine());
        WriteLine("введiть «1», для вiдображення шляху");
        Write("введiть «2», для рандомного генерування та знаходження вiдповiдi -> ");
        int choice = int.Parse(ReadLine());

        switch (choice)
        {
            case 1:
                showWay(n, m);
                break;
            case 2:
                int [,]array = generateMatrix(n, m);
                findMax(array, n, m);
                break;
            default:
                Console.WriteLine("Error!");
                break;
        }
        ReadLine();
    }
    static void findMax(int[,]array, int n, int m)
    {
        int n1 = n - 1;
        int m1 = 0;
        int max = array[n1, 0];
        while (n1 != n / 2 || m1 != m - 1)
        {
            if (n1 == n - 1 && m1 != m - 1)
            {
                m1++;
                max = Math.Max(max, array[n1, m1]);
                while (m1 != 0 && n1 != n / 2)
                {
                    n1--;
                    m1--;
                    max = Math.Max(max, array[n1, m1]);
                }
            }
            if (m1 == 0 && n1 != n / 2)
            {
                n1--;
                max = Math.Max(max, array[n1, m1]);
                while (n1 != n - 1 && m1 != m - 1)
                {
                    n1++;
                    m1++;
                    max = Math.Max(max, array[n1, m1]);
                }
            }
            if (m1 == m - 1 && n1 != n / 2)
            {
                n1--;
                max = Math.Max(max, array[n1, m1]);
                while (n1 != n / 2)
                {
                    n1--;
                    m1--;
                    max = Math.Max(max, array[n1, m1]);
                }
            }
            if (n1 == n / 2 && m1 != m - 1)
            {
                m1++;
                max = Math.Max(max, array[n1, m1]);
                while (m1 != m - 1 && n1 != n - 1)
                {
                    n1++;
                    m1++;
                    max = Math.Max(max, array[n1, m1]);
                }

            }

        }
        
        

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (i >= n / 2) ForegroundColor = ConsoleColor.Green;
                Write(array[i, j] + "\t");
            }
            WriteLine();
        }
        ForegroundColor = ConsoleColor.Red;
        Write("Max value from zigzag: ");
        ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(max);
        ForegroundColor = ConsoleColor.White;
        n1--;
        if (array[n1, m1] > max)
        {
            ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(array[n1, m1] + " > " + max);
            ForegroundColor = ConsoleColor.White;
        }
        bool check = false;
        for (int i = n1; i >= 0; i--)
        {
            if (!check)
            {
                for (int j = m1; j >= 0; j--)
                {
                    if (array[i, j] > max)
                    {
                        ForegroundColor = ConsoleColor.Green;
                        Console.Write(array[i, j] + " > ");
                        ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(max);
                        ForegroundColor = ConsoleColor.White;
                    }
                    check = true;
                }
            }
            else
            {
                for (int j = 0; j < m; j++)
                {
                    if (array[i, j] > max)
                    {
                        ForegroundColor = ConsoleColor.Green;
                        Console.Write(array[i, j] + " > ");
                        ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(max);
                        ForegroundColor = ConsoleColor.White;
                    }
                    check = false;
                }
            }

        }
    }
    static int[,] generateMatrix(int n, int m)
    {
        Random rnd = new Random();
        int[,] matrix = new int[n, m];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = rnd.Next(0, 100);
            }
        }
        return matrix;
    }
    static void showWay(int n, int m)
    {
        int[,] array = new int[n, m];
        int n1 = n - 1;
        int m1 = 0;
        int step = 1;
        array[n1, 0] = step;
        while (n1 != n / 2 || m1 != m - 1)
        {
            if (n1 == n - 1 && m1 != m - 1)
            {
                m1++;
                step++;
                array[n1, m1] = step;
                while (m1 != 0 && n1 != n / 2)
                {
                    n1--;
                    m1--;
                    step++;
                    array[n1, m1] = step;
                }
            }
            if (m1 == 0 && n1 != n / 2)
            {
                n1--;
                step++;
                array[n1, m1] = step;
                while (n1 != n - 1 && m1 != m - 1)
                {
                    n1++;
                    m1++;
                    step++;
                    array[n1, m1] = step;
                }
            }
            if (m1 == m - 1 && n1 != n / 2)
            {
                n1--;
                step++;
                array[n1, m1] = step;
                while (n1 != n / 2)
                {
                    n1--;
                    m1--;
                    step++;
                    array[n1, m1] = step;
                }
            }
            if (n1 == n / 2 && m1 != m - 1)
            {
                m1++;
                step++;
                array[n1, m1] = step;
                while (m1 != m - 1 && n1 != n - 1)
                {
                    n1++;
                    m1++;
                    step++;
                    array[n1, m1] = step;
                }

            }

        }
        n1--;
        step++;
        array[n1, m1] = step;
        bool check = false;
        for (int i = n1; i >= 0; i--)
        {
            if (!check)
            {
                for (int j = m1; j >= 0; j--)
                {
                        array[i, j] = step;
                        step++;
                        check = true;
                }
            }
            else
            {
                for (int j = 0; j < m; j++)
                {
                    array[i, j] = step;
                    step++;
                    check = false;
                }
            }
            
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (i >= n / 2) ForegroundColor = ConsoleColor.Green;
                Write(array[i, j] + "\t");
            }
            WriteLine();
        }
        ForegroundColor = ConsoleColor.White;
    }
}
