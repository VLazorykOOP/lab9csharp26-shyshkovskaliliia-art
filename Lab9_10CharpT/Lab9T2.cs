#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab9
{
    internal class Lab9T2
    {
        public void Run()
        {
            Console.Write("Введіть a= ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введіть b= ");
            int b = int.Parse(Console.ReadLine());

            StreamReader filein = new StreamReader("t2.txt", Encoding.UTF8);

            Queue queueLessA = new Queue();    
            Queue queueGreaterB = new Queue(); 

            string line;

            Console.WriteLine("\nЧисла з інтервалу [" + a + ", " + b + "]:");

            while ((line = filein.ReadLine()) != null)
            {
                string[] temp = line.Split(new char[] { ' ', '\t' },
                    StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < temp.Length; i++)
                {
                    int num = int.Parse(temp[i]);

                    if (num >= a && num <= b)
                    {
                        Console.Write(num + " ");
                    }
                    else if (num < a)
                    {
                        queueLessA.Enqueue(num);
                    }
                    else 
                    {
                        queueGreaterB.Enqueue(num);
                    }
                }
            }
            filein.Close(); 

            Console.WriteLine("\n\nЧисла менші " + a + ":");
            while (queueLessA.Count != 0)
            {
                Console.Write(queueLessA.Dequeue() + " ");
            }

            Console.WriteLine("\n\nЧисла більші " + b + ":");
            while (queueGreaterB.Count != 0)
            {
                Console.Write(queueGreaterB.Dequeue() + " ");
            }

            Console.WriteLine();
        }
    }
}
