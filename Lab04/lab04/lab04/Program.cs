using System;
using static System.Console;
using System.Collections.Generic;

namespace lab04
{
    class Program
    {
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int firstData;

            while (true)
            {
                Clear();
                Write("first node data: ");
                try
                {
                    firstData = int.Parse(ReadLine());
                    break;

                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
            }

            DLNode tail = new DLNode(firstData);
            string choice;
            while (true)
            {
                Clear();
                Print();
                MainMenu();
                choice = ReadLine();
                int position = 0;
                switch (choice)
                {
                    case "+f":
                        Clear();
                        int number;
                        while (true)
                        {
                            Print();
                            Write("Type the number: ");
                            try
                            {
                                number = int.Parse(ReadLine());
                                break;
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        AddFirst(number);
                        break;
                    case "+l":
                        Clear();
                        while (true)
                        {
                            Print();
                            Write("Type the number: ");
                            try
                            {
                                number = int.Parse(ReadLine());
                                break;
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        AddLast(number);
                        break;
                    case "+p":
                        Clear();
                        if (Count() < 2) break;
                        while (true)
                        {
                            Print();
                            Write("Type the data number: ");
                            try
                            {
                                number = int.Parse(ReadLine());
                                while (true)
                                {
                                    Clear();
                                    Print();
                                    Write("Type the position number: ");
                                    try
                                    {
                                        position = int.Parse(ReadLine());
                                        break;
                                    }
                                    catch ( Exception ex)
                                    {
                                        WriteLine(ex.Message);
                                    }
                                    
                                }
                                break;
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        if (position <= 1 || position > Count()) break;
                        AddAtPosition(number, position);
                        break;
                    case "-f":
                        if (Count() < 2) break;
                        DeleteFirst();
                        break;
                    case "-l":
                        if (Count() < 2) break;
                        DeleteLast();
                        break;
                    case "-p":
                        Clear();
                        if (Count() < 3) break;
                        while (true)
                        {
                            Print();
                            Write("Type the position: ");
                            try
                            {
                                position = int.Parse(ReadLine());
                                break;
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        if (position <=1 || position >=Count()) break;
                        DeleteAtPosition(position);
                        break;
                    case "+c":
                        Clear();
                        if (Count() < 2) break;
                        while (true)
                        {
                            Print();
                            Write("Type the number: ");
                            try
                            {
                                number = int.Parse(ReadLine());
                                break;
                            }
                            catch (Exception ex)
                            {
                                WriteLine(ex.Message);
                            }
                        }
                        AddInCentre(number);
                        break;
                }
            }

            void MainMenu()
            {
                WriteLine();
                WriteLine("Commands:");
                WriteLine("«+f» to add first");
                WriteLine("«+l» to add last");
                WriteLine("«+p» to add at position");
                WriteLine("«-f» to delete first");
                WriteLine("«-l» to delete last");
                WriteLine("«-p» to delete at position");
                WriteLine("«+c» to add in centre");
                Write("command: ");
            }

            int Count()
            {
                int count = 1;

                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;
                    count++;
                }
                return count;
            }

            void AddInCentre(int data)
            {
                int count = 1;

                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;
                    count++;
                }

                int pos = count / 2;
                current = tail;
                if (count % 2 != 0)
                {
                    while (count != pos + 1)
                    {
                        current = current.prev;
                        count--;
                    }
                }
                else
                {
                    while (count != pos)
                    {
                        current = current.prev;
                        count--;
                    }
                }
                    

                DLNode newNode = new DLNode(data);
                newNode.prev = current;
                newNode.next = current.next;
                current.next.prev = newNode;
                current.next = newNode;
            }
            void DeleteAtPosition(int pos)
            {
                int count = 1;

                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;
                    count++;
                }

                current = tail;
                while (count != pos)
                {
                    current = current.prev;
                    count--;
                }

                current.prev.next = current.next;
                current.next.prev = current.prev;
            }
            void AddAtPosition(int data, int pos)
            {
                int count = 1;

                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;
                    count++;
                }

                current = tail;
                while (count != pos -1)
                {
                    current = current.prev;
                    count--;
                }
                DLNode newNode = new DLNode(data);
                newNode.prev = current;
                newNode.next = current.next;
                current.next.prev = newNode;
                current.next = newNode;
            }

            void DeleteFirst()
            {
                DLNode current = tail;
                while(current.prev.prev != null)
                {
                    current = current.prev;
                }
                current.prev = null;
            }

            void DeleteLast()
            {
                tail = tail.prev;
                tail.next = null;
            }

            void AddFirst(int data)
            {
                DLNode newNode = new DLNode(data);
                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;

                }

                current.prev = newNode;
                newNode.next = current;
            }

            void AddLast(int data)
            {
                DLNode newNode = new DLNode(data);
                tail.next = newNode;
                newNode.prev = tail;
                tail = newNode; 
            }

            void Print()
            {
                DLNode current = tail;
                while (current.prev != null)
                {
                    current = current.prev;
                }
                Write("<-> ");
                while(current != null)
                {
                    if (current == tail)
                    {
                        ForegroundColor = ConsoleColor.Red;
                        Write(current.data);
                        ForegroundColor = ConsoleColor.White;
                        Write(" <-> ");
                    }
                    else
                    {
                        Write("{0} <-> ", current.data);
                    }
                    
                    current = current.next;
                }
                WriteLine();
            }
        }

        public class DLNode
        {
            public int data;
            public DLNode prev = null;
            public DLNode next = null;

            public DLNode(int data)
            {
                this.data = data;
                this.prev = null;
                this.next = null;
            }
        }
    }
}
