#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab9
{
    internal class Lab9T3
    {
        static void ArrayPrint(string s, ArrayList a)
        {
            Console.WriteLine(s);
            foreach (object item in a)
                Console.Write("{0} ", item);
            Console.WriteLine();
        }

        static bool IsVowel(char c)
        {
            string vowels = "аеєиіїоуюяАЕЄИІЇОУЮЯaeiouAEIOU";
            return vowels.IndexOf(c) >= 0;
        }

        public void Run()
        {
            Console.WriteLine("=== Завдання 3а: голосні у зворотному порядку ===\n");

            StreamWriter fileout = new StreamWriter("t1.txt", false, Encoding.UTF8);
            fileout.WriteLine("Привіт, це тестовий файл для лабораторної роботи.");
            fileout.WriteLine("Колекції в мові програмування C# дуже зручні.");
            fileout.WriteLine("ArrayList реалізує динамічний масив.");
            fileout.Close();

            StreamReader filein = new StreamReader("t1.txt", Encoding.UTF8);
            string text = filein.ReadToEnd();
            filein.Close();

            Console.WriteLine("Вміст файлу:");
            Console.WriteLine(text);

            ArrayList vowels = new ArrayList();

            for (int i = 0; i < text.Length; i++)
            {
                if (IsVowel(text[i]))
                    vowels.Add(text[i]);
            }

            Console.WriteLine("\nКількість голосних: " + vowels.Count);
            Console.WriteLine("Початкова ємність масиву: " + vowels.Capacity);

            ArrayPrint("\nГолосні у прямому порядку:", vowels);

            vowels.Reverse();
            ArrayPrint("Голосні у зворотному порядку:", vowels);

            Console.WriteLine("Ємність після Reverse(): " + vowels.Capacity);
            vowels.TrimToSize(); 
            Console.WriteLine("Ємність після TrimToSize(): " + vowels.Capacity);

            Console.WriteLine("\n\n=== Завдання 3б: групування чисел по інтервалах ===\n");

            fileout = new StreamWriter("t2.txt", false, Encoding.UTF8);
            fileout.WriteLine("15 3 42 7 100 -5 28 1 55 8 33 2 77 19 6");
            fileout.Close();

            Console.Write("Введіть a= ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введіть b= ");
            int b = int.Parse(Console.ReadLine());

            filein = new StreamReader("t2.txt", Encoding.UTF8);
            string line = filein.ReadToEnd();
            filein.Close();

            Console.WriteLine("\nВміст файлу: " + line);
            Console.WriteLine("Інтервал: [" + a + ", " + b + "]\n");

            string[] temp = line.Split(new char[] { ' ', '\n', '\r', '\t' },
                StringSplitOptions.RemoveEmptyEntries);

            ArrayList inRange = new ArrayList();   
            ArrayList lessA = new ArrayList();    
            ArrayList greaterB = new ArrayList(); 

            for (int i = 0; i < temp.Length; i++)
            {
                int num = int.Parse(temp[i]);
                if (num >= a && num <= b)
                    inRange.Add(num);
                else if (num < a)
                    lessA.Add(num);
                else
                    greaterB.Add(num);
            }

            ArrayPrint("Числа з інтервалу [" + a + ", " + b + "]:", inRange);
            ArrayPrint("Числа менші " + a + ":", lessA);
            ArrayPrint("Числа більші " + b + ":", greaterB);

            Console.WriteLine("\n--- Групи після сортування (ArrayList.Sort) ---");
            inRange.Sort();
            lessA.Sort();
            greaterB.Sort();
            ArrayPrint("Sorted [" + a + "," + b + "]:", inRange);
            ArrayPrint("Sorted < " + a + ":", lessA);
            ArrayPrint("Sorted > " + b + ":", greaterB);

            Console.WriteLine("\n--- Властивості колекцій ---");
            Console.WriteLine("inRange: Count=" + inRange.Count + ", Capacity=" + inRange.Capacity);
            Console.WriteLine("lessA:   Count=" + lessA.Count + ", Capacity=" + lessA.Capacity);
            Console.WriteLine("greaterB: Count=" + greaterB.Count + ", Capacity=" + greaterB.Capacity);

            Console.WriteLine("\n=== Кінець виконання завдання 3 ===");
        }
    }
}
