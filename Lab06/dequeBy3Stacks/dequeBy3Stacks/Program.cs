using System;
using static System.Console;

namespace Lab06
{
    public class Program
    {
        static void Main(string[] args)
        {
            Deque myDeque = new Deque();
            Write("your text -> ");
            string text = ReadLine();
            foreach (char c in text)
                myDeque.InsertHead(c + "");

            if (CheckIfPalindrome(myDeque) == true)
            {
                WriteLine("writen text is palindrome:");
                myDeque.Print();
            }
            else
            {
                convertToPalindrome(myDeque);
            }
            ReadLine();
        }
        static void convertToPalindrome(Deque inputDeque)
        {
            Deque invertDeque = new Deque();
            Deque bufferDeque = new Deque();

            while (inputDeque.CheckIfEmpty() != true)
            {
                string element = inputDeque.PopTail();
                invertDeque.InsertHead(element);
                bufferDeque.InsertHead(element);
            }

            while (bufferDeque.CheckIfEmpty() != true)
                inputDeque.InsertHead(bufferDeque.PopTail());

            invertDeque.Print();
            while (CheckIfPalindrome(inputDeque) != true)
            {
                if (inputDeque.peekHead() != invertDeque.peekHead())
                {
                    inputDeque.InsertHead(invertDeque.PopHead());
                    inputDeque.Print();
                }
                else invertDeque.PopHead();
            }

        }
        static bool CheckIfPalindrome(Deque inputDeque)
        {
            Deque checkDeque = new Deque();
            Deque bufferDeque = new Deque();

            while (inputDeque.CheckIfEmpty() != true)
            {
                string element = inputDeque.PopTail();
                checkDeque.InsertHead(element);
                bufferDeque.InsertHead(element);
            }

            while (bufferDeque.CheckIfEmpty() != true)
                inputDeque.InsertHead(bufferDeque.PopTail());

            while (checkDeque.Length() > 1)
            {
                if (checkDeque.PopTail() != checkDeque.PopHead())
                    return false;
            }
            return true;
        }
    }
    public class Deque
    {
        public Stack head;
        public Stack tail;
        private Stack buffer;

        public Deque()
        {
            head = new Stack();
            tail = new Stack();
            buffer = new Stack();
        }
        public void InsertHead(string value)
        {
            this.head.Push(value);
        }
        public void InsertTail(string value)
        {
            this.tail.Push(value);
        }

        public bool CheckIfEmpty()
        {
            if (head.isEmpty == true && tail.isEmpty == true)
                return true;
            else return false;
        }
        public string PopTail()
        {
            if (this.CheckIfEmpty() == false)
                if (tail.isEmpty == true)
                {
                    while (head.isEmpty == false)               // buffer -> head -> tail
                        buffer.Push(head.Pop());              // buffer -> head
                    for (int i = 0; i < buffer.Length(); i++)
                        head.Push(buffer.Pop());
                    while (head.isEmpty == false)
                        tail.Push(head.Pop());
                    while (buffer.isEmpty == false)
                        head.Push(buffer.Pop());
                    return tail.Pop();
                }
            return tail.Pop();
        }
        public string PopHead()
        {
            if (this.CheckIfEmpty() == false)
                if (head.isEmpty == true)
                {
                    while (tail.isEmpty == false)               // buffer -> tail -> head
                        buffer.Push(tail.Pop());               // buffer -> tail
                    for (int i = 0; i < buffer.Length(); i++)
                        tail.Push(buffer.Pop());
                    while (tail.isEmpty == false)
                        head.Push(tail.Pop());
                    while (buffer.isEmpty == false)
                        tail.Push(buffer.Pop());
                    return head.Pop();
                }
            return head.Pop();
        }
        public string peekHead()
        {
            string value = head.Pop();
            head.Push(value);
            return value;
        }
        public void Print()
        {
            while (head.isEmpty == false)
                buffer.Push(head.Pop());
            while (tail.isEmpty == false)
                head.Push(tail.Pop());
            while (head.isEmpty == false)
                buffer.Push(head.Pop());

            buffer.Print();

            for (int i = 0; i < buffer.Length(); i++)
                head.Push(buffer.Pop());
            while (tail.isEmpty == false)
                tail.Push(buffer.Pop());
            while (buffer.isEmpty == false)
                head.Push(buffer.Pop());
        }

        public int Length()
        {
            return (buffer.Length() + tail.Length() + head.Length());
        }
    }
    public class Stack
    {
        private StackElement tail;
        public bool isEmpty;
        public Stack()
        {
            this.tail = null;
            this.isEmpty = true;
        }
        public void Push(string value)
        {
            if (isEmpty == true)
            {
                StackElement newTail = new StackElement(value, null);
                tail = newTail;
                isEmpty = false;
            }
            else
            {
                StackElement newTail = new StackElement(value, tail);
                tail = newTail;
            }
        }
        public int Length()
        {
            int length = 0;
            if (isEmpty == true) return length;
            else if (tail.GetOrigin() == null) return 1;
            else
            {
                length++;
                StackElement current = this.tail;
                while (current.GetOrigin() != null)
                {
                    length++;
                    current = current.GetOrigin();
                }
                return length;
            }
        }
        public void Clear()
        {
            Stack tail = null;
            isEmpty = true;
        }
        public string Pop()
        {
            if (isEmpty == false)
            {
                string tailValue = this.tail.GetValue();
                if (tail.GetOrigin() == null)
                {
                    this.Clear();
                }
                else
                {
                    this.tail = tail.GetOrigin();
                }
                return tailValue;
            }
            else return null;
        }
        public void Print()
        {
            if (isEmpty == true) WriteLine("Stack is empty");
            else
            {
                StackElement current = tail;
                Write("[{0}]", current.GetValue());
                while (current.GetOrigin() != null)
                {
                    current = current.GetOrigin();
                    Write("[{0}]", current.GetValue());
                }
                WriteLine();
            }
        }
    }

    public class StackElement
    {
        private string value;
        private StackElement origin;
        public StackElement(string value, StackElement origin)
        {
            this.value = value;
            this.origin = origin;
        }

        public string GetValue()
        {
            return value;
        }
        public StackElement GetOrigin()
        {
            return origin;
        }
    }
}