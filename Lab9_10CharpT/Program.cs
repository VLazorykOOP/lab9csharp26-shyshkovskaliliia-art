using System;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Лабораторна робота №9 — Колекції C# \n");

            bool running = true;
            while (running)
            {
                Console.WriteLine("=== ГОЛОВНЕ МЕНЮ ===");
                Console.WriteLine("  1 – Завдання 1.2 (Stack)");
                Console.WriteLine("  2 – Завдання 2.2 (Queue)");
                Console.WriteLine("  3 – Завдання 3   (ArrayList)");
                Console.WriteLine("  4 – Завдання 4   (Hashtable)");
                Console.WriteLine("  0 – Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Lab9T1 lab9task1 = new Lab9T1();
                        lab9task1.Run();
                        Console.WriteLine("\nНатисніть Enter для повернення до головного меню...");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;

                    case "2":
                        Lab9T2 lab9task2 = new Lab9T2();
                        lab9task2.Run();
                        Console.WriteLine("\nНатисніть Enter для повернення до головного меню...");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;

                    case "3":
                        Lab9T3 lab9task3 = new Lab9T3();
                        lab9task3.Run();
                        Console.WriteLine("\nНатисніть Enter для повернення до головного меню...");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;

                    case "4":
                        Lab9T4 lab9task4 = new Lab9T4();
                        lab9task4.Run();
                        // T4 має власне меню з виходом, тому одразу повертаємось
                        Console.WriteLine("\nНатисніть Enter для повернення до головного меню...");
                        Console.ReadLine();
                        Console.WriteLine();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("До побачення!");
                        break;

                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.\n");
                        break;
                }
            }
        }
    }
}
