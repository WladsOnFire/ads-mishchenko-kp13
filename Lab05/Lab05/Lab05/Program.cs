using System;
using static System.Console;

namespace Lab05
{
    public class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            int n = 0, m = 0;

        x1:
            try
            {
                Write("M -> ");
                m = int.Parse(ReadLine());
                if (m <= 0) throw new Exception("'M' must be positive");

            x2:
                try
                {
                    Write("N -> ");
                    n = int.Parse(ReadLine());
                    if (n <= 0) throw new Exception("'N' must be positive");

                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                    ReadLine();
                    Clear();
                    goto x2;
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
                ReadLine();
                Clear();
                goto x1;
            }

        x3:
            Clear();
            WriteLine("type 'r' to get random values");
            WriteLine("type 'h' to set values by hand");
            Write("-> ");

            string decision = ReadLine().ToLower();

            int[,] array = new int[m, n];

            switch (decision)
            {
                case "r":
                    for (int i = 0; i < m; i++)
                        for (int j = 0; j < n; j++)
                            array[i, j] = rnd.Next(50);
                    Clear();
                    break;
                case "h":
                    for (int i = 0; i < m; i++)
                        for (int j = 0; j < n; j++)
                        {
                        x4:
                            Write("array[{0},{1}] -> ", i, j);
                            try
                            {
                                array[i, j] = int.Parse(ReadLine());
                            }
                            catch (Exception e)
                            {
                                WriteLine(e.Message);
                                Clear();
                                goto x4;
                            }
                            Clear();
                        }
                    break;
                default:
                    goto x3;
            }

            WriteLine("{0}x{1} array: \n", m, n);

            int elements = 0;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((j < i && i < n - 1 - j))
                    {
                        ForegroundColor = ConsoleColor.Red;
                        elements++;
                    }
                    Write(array[i, j] + "\t");
                    ForegroundColor = ConsoleColor.White;
                }
                WriteLine("\n");
            }

            int[] sortPart = new int[elements];

            int counter = 0;

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    if ((j < i && i < n - 1 - j))
                    {
                        sortPart[counter] = array[i, j];
                        counter++;
                    }
                }

            WriteLine("\n elements to sort:");
            foreach (int i in sortPart)
                Write(i + " ");

            sortPart = Sort(sortPart, m, 0, sortPart.Length - 1);


            WriteLine("\n\n sorted elements:");
            foreach (int i in sortPart)
                Write(i + " ");
            WriteLine("\n");

            WriteLine("\n");

            int J = 0;
            int I = 0;
            int count = 0;
            bool moveUp = false;
            bool put = false;
            while (count != sortPart.Length)
            {
                put = false;


                if (J < I && I < n - 1 - J)
                {

                    array[I, J] = sortPart[count];
                    count++;
                    
                }



                if (I == 0 && moveUp == true)
                {
                    J++;
                    moveUp = false;
                    put = true;
                }
                else if (I == m - 1 && moveUp == false)
                {
                    J++;
                    moveUp = true;
                    put = true;
                }

                if(put == false)
                {
                    if (moveUp == false) I++;
                    else I--;
                }
                
            }




            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((j < i && i < n - 1 - j))
                    {
                        ForegroundColor = ConsoleColor.Green;
                    }
                    Write(array[i, j] + "\t");
                    ForegroundColor = ConsoleColor.White;
                }
                WriteLine("\n");
            }

            ReadLine();
        }

        static int[] Sort(int[] array, int M, int start, int end)
        {


            if (end - start + 1 < M)
            {

                for (int i = start; i < end + 1; i++)
                {
                    int key = array[i];
                    int j = i - 1;
                    while (j >= 0 && array[j] < key)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
                return array;
            }
            else
            {
                int left = array[start];
                int right = array[end];
                int middle = array[(end - start + 1) / 2];

                if ((left > middle && middle > right) || (left < middle && middle < right))
                {
                    int buffer = middle;
                    middle = right;
                    right = buffer;
                }
                if ((left > middle && left < right) || (left < middle && left > right))
                {
                    int buffer = left;
                    left = right;
                    right = buffer;
                }

                int i = start;
                int j = end - 1;
                bool i_r = false;
                bool j_r = false;
                while (!(i >= j))
                {
                    if (array[i] > right)
                    {
                        i_r = true;
                    }
                    else i++;
                    if (array[j] < right)
                    {
                        j_r = true;
                    }
                    else j--;

                    if (j_r == i_r == true)
                    {
                        int buffer = array[i];
                        array[i] = array[j];
                        array[j] = buffer;
                        j_r = false;
                        i_r = false;
                        i++;
                        j--;
                    }
                }


                if (array[i] < array[end])
                {
                    int buffer = array[i];
                    array[i] = array[end];
                    array[end] = buffer;
                }

                int pivot_i = i;

                array = Sort(array, M, start, pivot_i);
                array = Sort(array, M, pivot_i + 1, end);

                return array;
            }
        }
    }
}