#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab9
{
    internal class Lab9T4
    {
        private static void PrintTab(string s, Hashtable a)
        {
            Console.WriteLine(s);
            ICollection keys = a.Keys;
            foreach (string key in keys)
            {
                Console.WriteLine(key + "\t" + a[key]);
            }
            Console.WriteLine();
        }

        static void PrintDiskSongs(string diskId, Hashtable catalog, Hashtable songs)
        {
            if (!catalog.ContainsKey(diskId))
            {
                Console.WriteLine("Диск [" + diskId + "] не знайдено.");
                return;
            }
            Console.WriteLine("\n=== Диск [" + diskId + "]: " + catalog[diskId] + " ===");
            ArrayList diskSongs = (ArrayList)songs[diskId];
            if (diskSongs.Count == 0)
                Console.WriteLine("  (пісень немає)");
            else
                foreach (string song in diskSongs)
                    Console.WriteLine("  • " + song);
        }

        // Допоміжний метод для виводу всього каталогу
        static void PrintAllCatalog(Hashtable catalog, Hashtable songs)
        {
            Console.WriteLine("\n===== КАТАЛОГ ДИСКІВ =====");
            if (catalog.Count == 0)
            {
                Console.WriteLine("  (каталог порожній)");
                return;
            }
            ICollection keys = catalog.Keys;
            foreach (string diskId in keys)
            {
                Console.WriteLine("[" + diskId + "] " + catalog[diskId]);
                ArrayList diskSongs = (ArrayList)songs[diskId];
                if (diskSongs.Count == 0)
                    Console.WriteLine("        (пісень немає)");
                else
                    foreach (string song in diskSongs)
                        Console.WriteLine("        • " + song);
            }
            Console.WriteLine();
        }

        // Створення демо-файлу з даними
        static void CreateDemoFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                StreamWriter fileout = new StreamWriter(fileName, false, Encoding.UTF8);
                fileout.WriteLine("D001|Queen – Greatest Hits");
                fileout.WriteLine("D002|Pink Floyd – The Dark Side of the Moon");
                fileout.WriteLine("D003|Nirvana – Nevermind");
                fileout.WriteLine("SONGS|D001|Queen – Bohemian Rhapsody");
                fileout.WriteLine("SONGS|D001|Queen – We Will Rock You");
                fileout.WriteLine("SONGS|D001|Queen – Somebody to Love");
                fileout.WriteLine("SONGS|D002|Pink Floyd – Money");
                fileout.WriteLine("SONGS|D002|Pink Floyd – Time");
                fileout.WriteLine("SONGS|D003|Nirvana – Smells Like Teen Spirit");
                fileout.Close();
            }
        }

        // Завантаження каталогу з файлу
        static void LoadCatalog(string fileName, Hashtable catalog, Hashtable songs)
        {
            StreamReader filein = new StreamReader(fileName, Encoding.UTF8);
            string line;
            while ((line = filein.ReadLine()) != null)
            {
                string[] parts = line.Split('|');
                if (parts[0] != "SONGS" && parts.Length >= 2)
                {
                    // Завантаження диска
                    catalog.Add(parts[0], parts[1]);
                    songs.Add(parts[0], new ArrayList());
                }
                else if (parts[0] == "SONGS" && parts.Length >= 3)
                {
                    // Завантаження пісні
                    string diskId = parts[1];
                    string songInfo = parts[2];
                    if (songs.ContainsKey(diskId))
                        ((ArrayList)songs[diskId]).Add(songInfo);
                }
            }
            filein.Close();
        }

        // Збереження каталогу у файл
        static void SaveCatalog(string fileName, Hashtable catalog, Hashtable songs)
        {
            StreamWriter fileout = new StreamWriter(fileName, false, Encoding.UTF8);
            ICollection keys = catalog.Keys;
            foreach (string diskId in keys)
            {
                fileout.WriteLine(diskId + "|" + catalog[diskId]);
                ArrayList diskSongs = (ArrayList)songs[diskId];
                foreach (string song in diskSongs)
                    fileout.WriteLine("SONGS|" + diskId + "|" + song);
            }
            fileout.Close();
        }

        // Пошук записів виконавця по всьому каталогу
        static void SearchByArtist(string artist, Hashtable catalog, Hashtable songs)
        {
            Console.WriteLine("\nРезультати пошуку виконавця '" + artist + "':");
            bool found = false;
            ICollection keys = catalog.Keys;
            foreach (string diskId in keys)
            {
                ArrayList diskSongs = (ArrayList)songs[diskId];
                foreach (string song in diskSongs)
                {
                    // Перевірка: чи містить рядок ім'я виконавця
                    if (song.IndexOf(artist, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine("  [" + diskId + "] " + catalog[diskId] + "  →  " + song);
                        found = true;
                    }
                }
            }
            if (!found)
                Console.WriteLine("  Записів не знайдено.");
            Console.WriteLine();
        }

        public void Run()
        {
            Console.WriteLine("=== Завдання 4: Каталог музичних CD (Hashtable) ===\n");

            // Створення та ініціалізація хеш-таблиць
            Hashtable catalog = new Hashtable(); // diskId -> diskTitle
            Hashtable songs = new Hashtable();   // diskId -> ArrayList of songs

            // Завантаження демо-даних
            CreateDemoFile("cd_catalog.txt");
            LoadCatalog("cd_catalog.txt", catalog, songs);
            Console.WriteLine("Каталог завантажено. Дисків: " + catalog.Count + "\n");

            bool running = true;
            while (running)
            {
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("  1 – Переглянути весь каталог");
                Console.WriteLine("  2 – Переглянути окремий диск");
                Console.WriteLine("  3 – Додати диск");
                Console.WriteLine("  4 – Видалити диск");
                Console.WriteLine("  5 – Додати пісню на диск");
                Console.WriteLine("  6 – Видалити пісню з диска");
                Console.WriteLine("  7 – Пошук за виконавцем");
                Console.WriteLine("  8 – Зберегти каталог у файл");
                Console.WriteLine("  0 – Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        PrintAllCatalog(catalog, songs);
                        break;

                    case "2":
                        Console.Write("ID диска для перегляду: ");
                        string viewId = Console.ReadLine();
                        PrintDiskSongs(viewId, catalog, songs);
                        Console.WriteLine();
                        break;

                    case "3":
                        Console.Write("ID нового диска (напр. D004): ");
                        string newId = Console.ReadLine();
                        if (catalog.ContainsKey(newId))
                        {
                            Console.WriteLine("Диск [" + newId + "] вже існує!\n");
                            break;
                        }
                        Console.Write("Назва диска: ");
                        string newTitle = Console.ReadLine();
                        catalog.Add(newId, newTitle);
                        songs.Add(newId, new ArrayList());
                        Console.WriteLine("Диск [" + newId + "] '" + newTitle + "' додано.\n");
                        break;

                    case "4":
                        Console.Write("ID диска для видалення: ");
                        string delId = Console.ReadLine();
                        if (!catalog.ContainsKey(delId))
                        {
                            Console.WriteLine("Диск [" + delId + "] не знайдено.\n");
                            break;
                        }
                        Console.WriteLine("Видаляємо диск [" + delId + "]: " + catalog[delId]);
                        catalog.Remove(delId);
                        songs.Remove(delId);
                        Console.WriteLine("Диск видалено.\n");
                        break;

                    case "5":
                        Console.Write("ID диска: ");
                        string songDiskId = Console.ReadLine();
                        if (!catalog.ContainsKey(songDiskId))
                        {
                            Console.WriteLine("Диск [" + songDiskId + "] не знайдено.\n");
                            break;
                        }
                        Console.Write("Виконавець: ");
                        string artist = Console.ReadLine();
                        Console.Write("Назва пісні: ");
                        string title = Console.ReadLine();
                        string songEntry = artist + " – " + title;
                        ((ArrayList)songs[songDiskId]).Add(songEntry);
                        Console.WriteLine("Пісню '" + songEntry + "' додано до диска [" + songDiskId + "].\n");
                        break;

                    case "6":
                        Console.Write("ID диска: ");
                        string remDiskId = Console.ReadLine();
                        if (!catalog.ContainsKey(remDiskId))
                        {
                            Console.WriteLine("Диск [" + remDiskId + "] не знайдено.\n");
                            break;
                        }
                        ArrayList diskSongs = (ArrayList)songs[remDiskId];
                        if (diskSongs.Count == 0)
                        {
                            Console.WriteLine("На цьому диску немає пісень.\n");
                            break;
                        }
                        Console.WriteLine("Пісні на диску:");
                        for (int i = 0; i < diskSongs.Count; i++)
                            Console.WriteLine("  " + (i + 1) + ". " + diskSongs[i]);
                        Console.Write("Номер пісні для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= diskSongs.Count)
                        {
                            Console.WriteLine("Видалено: " + diskSongs[idx - 1] + "\n");
                            diskSongs.RemoveAt(idx - 1);
                        }
                        else
                            Console.WriteLine("Невірний номер.\n");
                        break;

                    case "7":
                        Console.Write("Введіть ім'я виконавця для пошуку: ");
                        string searchArtist = Console.ReadLine();
                        SearchByArtist(searchArtist, catalog, songs);
                        break;

                    case "8":
                        SaveCatalog("cd_catalog.txt", catalog, songs);
                        Console.WriteLine("Каталог збережено у файл 'cd_catalog.txt'.\n");
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("=== Кінець роботи програми ===\n");
                        break;

                    default:
                        Console.WriteLine("Невідома команда. Спробуйте ще раз.\n");
                        break;
                }
            }
        }
    }
}
