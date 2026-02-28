
// Главный файл программы
// Автор: Дедюхин Дмитрий

using System;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("     КРЕСТИКИ-НОЛИКИ");
                Console.WriteLine("=====================================");
                Console.WriteLine("Выберите версию игры:");
                Console.WriteLine("1. Версия 1.0 (Классическая игра)");
                Console.WriteLine("2. Версия 2.0 (Со статистикой и кастомизацией)");
                Console.WriteLine("3. Информация об авторе");
                Console.WriteLine("4. Выход");
                Console.Write("\nВаш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TicTacToeV1 gameV1 = new TicTacToeV1();
                        gameV1.Run();
                        break;
                    case "2":
                        TicTacToeV2 gameV2 = new TicTacToeV2();
                        gameV2.Run();
                        break;
                    case "3":
                        ShowAuthorInfo();
                        break;
                    case "4":
                        Console.WriteLine("\nДо свидания!");
                        return;
                    default:
                        Console.WriteLine("❌ Неверный выбор! Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowAuthorInfo()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("        ИНФОРМАЦИЯ ОБ АВТОРЕ");
            Console.WriteLine("=====================================");
            AuthorInfo.DisplayInfo();
            Console.WriteLine("\n=====================================");
            Console.WriteLine("Этапы разработки:");
            Console.WriteLine("1. Создание репозитория");
            Console.WriteLine("2. Версия 1.0 - базовая игра");
            Console.WriteLine("3. Версия 2.0 - добавлены статистика и кастомизация");
            Console.WriteLine("\nРепозиторий: https://github.com/FoRmb1ll/TicTacToeGame...");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}