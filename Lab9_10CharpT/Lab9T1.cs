#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab9
{
    internal class Lab9T1
    {
        public void Run()
        {
            StreamWriter fileout = new StreamWriter("t1.txt", false, Encoding.UTF8);
            fileout.WriteLine("Привіт, це тестовий файл для лабораторної роботи.");
            fileout.WriteLine("Колекції в мові програмування C# дуже зручні.");
            fileout.WriteLine("Стек реалізує принцип LIFO.");
            fileout.Close();

            StreamReader filein = new StreamReader("t1.txt", Encoding.UTF8);
            string line = filein.ReadToEnd();
            filein.Close();

            string vowels = "аеєиіїоуюяАЕЄИІЇОУЮЯaeiouAEIOU";
            Stack vowelStack = new Stack();

            for (int i = 0; i < line.Length; i++)
            {
                if (vowels.IndexOf(line[i]) >= 0)
                    vowelStack.Push(line[i]);
            }

            Console.WriteLine("Кількість голосних: " + vowelStack.Count);
            Console.Write("Голосні у зворотному порядку: ");

            while (vowelStack.Count != 0)
                Console.Write("{0} ", vowelStack.Pop());

            Console.WriteLine();
        }
    }
}

