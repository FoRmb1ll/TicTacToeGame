// TicTacToeV2.cs
// Вторая версия - счетчик побед и кастомизация
// Автор: Дедюхин Дмитрий

using System;
using System.IO;
using System.Text.Json;

namespace TicTacToeGame
{
    
    public class GameStats
    {
        public int PlayerXWins { get; set; }
        public int PlayerOWins { get; set; }
        public int Draws { get; set; }
        public string Player1Symbol { get; set; }
        public string Player2Symbol { get; set; }

        public GameStats()
        {
            PlayerXWins = 0;
            PlayerOWins = 0;
            Draws = 0;
            Player1Symbol = "X";
            Player2Symbol = "O";
        }
    }

    class TicTacToeV2
    {
        private char[] board = new char[9];
        private char currentPlayer;
        private GameStats stats;
        private char player1Symbol;
        private char player2Symbol;
        private string statsFilePath = "gamestats.json";

        public TicTacToeV2()
        {
            LoadStats();
            player1Symbol = char.Parse(stats.Player1Symbol);
            player2Symbol = char.Parse(stats.Player2Symbol);
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = ' ';
            }
            currentPlayer = player1Symbol;
        }

        private void LoadStats()
        {
            try
            {
                if (File.Exists(statsFilePath))
                {
                    string jsonString = File.ReadAllText(statsFilePath);
                    stats = JsonSerializer.Deserialize<GameStats>(jsonString);
                }
                else
                {
                    stats = new GameStats();
                }
            }
            catch
            {
                stats = new GameStats();
            }
        }

        private void SaveStats()
        {
            stats.Player1Symbol = player1Symbol.ToString();
            stats.Player2Symbol = player2Symbol.ToString();

            string jsonString = JsonSerializer.Serialize(stats, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(statsFilePath, jsonString);
        }

        public void Run()
        {
            while (true)
            {
                ShowMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayGame();
                        break;
                    case "2":
                        CustomizeSymbols();
                        break;
                    case "3":
                        ShowStats();
                        break;
                    case "4":
                        Console.WriteLine("\nСпасибо за игру! До свидания!");
                        return;
                    default:
                        Console.WriteLine("❌ Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("     КРЕСТИКИ-НОЛИКИ (Версия 2.0)   ");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. Начать игру");
            Console.WriteLine("2. Кастомизировать символы");
            Console.WriteLine("3. Показать статистику");
            Console.WriteLine("4. Выход");
            Console.Write("\nВыберите действие: ");
        }

        private void PlayGame()
        {
            InitializeBoard();
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("           НОВАЯ ИГРА");
            Console.WriteLine("=====================================");
            Console.WriteLine($"Игрок 1: {player1Symbol}");
            Console.WriteLine($"Игрок 2: {player2Symbol}");
            Console.WriteLine();

            Console.WriteLine("Нумерация клеток:");
            ShowNumbering();

            bool gameEnded = false;

            while (!gameEnded)
            {
                PrintBoard();

                
                int move = GetPlayerMove();
                board[move] = currentPlayer;

                
                if (CheckWinner())
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine($"\n🎉 Игрок {currentPlayer} победил! 🎉");

                    if (currentPlayer == player1Symbol)
                        stats.PlayerXWins++;
                    else
                        stats.PlayerOWins++;

                    gameEnded = true;
                }
                else if (IsBoardFull())
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine("\n🤝 Ничья! 🤝");
                    stats.Draws++;
                    gameEnded = true;
                }
                else
                {
                    
                    currentPlayer = (currentPlayer == player1Symbol) ? player2Symbol : player1Symbol;
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("           НОВАЯ ИГРА");
                    Console.WriteLine("=====================================");
                    Console.WriteLine($"Игрок 1: {player1Symbol}");
                    Console.WriteLine($"Игрок 2: {player2Symbol}");
                    Console.WriteLine();
                }
            }

            SaveStats();
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        private void ShowNumbering()
        {
            Console.WriteLine(" 1 | 2 | 3 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 4 | 5 | 6 ");
            Console.WriteLine("-----------");
            Console.WriteLine(" 7 | 8 | 9 ");
            Console.WriteLine();
        }

        private void PrintBoard()
        {
            
            Console.WriteLine("\nТекущее поле:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($" {board[i * 3]} | {board[i * 3 + 1]} | {board[i * 3 + 2]} ");
                if (i < 2)
                    Console.WriteLine("-----------");
            }
            Console.WriteLine();
        }

        private string GetCellDisplay(int index)
        {
            return board[index] == ' ' ? (index + 1).ToString() : board[index].ToString();
        }

        private int GetPlayerMove()
        {
            int move;
            while (true)
            {
                Console.Write($"Игрок {currentPlayer}, введите номер клетки (1-9): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out move) && move >= 1 && move <= 9)
                {
                    move--; 
                    if (board[move] == ' ')
                    {
                        return move;
                    }
                    else
                    {
                        Console.WriteLine("❌ Эта клетка уже занята! Выберите другую.");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Пожалуйста, введите число от 1 до 9.");
                }
            }
        }

        private void CustomizeSymbols()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("        КАСТОМИЗАЦИЯ СИМВОЛОВ");
            Console.WriteLine("=====================================");
            Console.WriteLine($"Текущие символы: Игрок 1 = {player1Symbol}, Игрок 2 = {player2Symbol}");
            Console.WriteLine();

            
            while (true)
            {
                Console.Write($"Введите новый символ для Игрока 1 (сейчас {player1Symbol}): ");
                string input = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(input) && input.Length == 1 && input[0] != ' ')
                {
                    player1Symbol = input[0];
                    break;
                }
                Console.WriteLine("❌ Введите один непробельный символ!");
            }

            
            while (true)
            {
                Console.Write($"Введите новый символ для Игрока 2 (сейчас {player2Symbol}): ");
                string input = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(input) && input.Length == 1 && input[0] != ' ' && input[0] != player1Symbol)
                {
                    player2Symbol = input[0];
                    break;
                }
                Console.WriteLine($"❌ Введите один непробельный символ, отличный от '{player1Symbol}'!");
            }

            SaveStats();
            Console.WriteLine($"\n✅ Символы успешно изменены!");
            Console.WriteLine($"Игрок 1: {player1Symbol}, Игрок 2: {player2Symbol}");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void ShowStats()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("           СТАТИСТИКА ИГР");
            Console.WriteLine("=====================================");
            Console.WriteLine($"Игрок 1 ({player1Symbol}): {stats.PlayerXWins} побед");
            Console.WriteLine($"Игрок 2 ({player2Symbol}): {stats.PlayerOWins} побед");
            Console.WriteLine($"Ничьих: {stats.Draws}");
            Console.WriteLine($"Всего игр: {stats.PlayerXWins + stats.PlayerOWins + stats.Draws}");
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }

        private bool CheckWinner()
        {
            
            for (int i = 0; i < 9; i += 3)
            {
                if (board[i] != ' ' && board[i] == board[i + 1] && board[i + 1] == board[i + 2])
                    return true;
            }

            
            for (int i = 0; i < 3; i++)
            {
                if (board[i] != ' ' && board[i] == board[i + 3] && board[i + 3] == board[i + 6])
                    return true;
            }

            
            if (board[0] != ' ' && board[0] == board[4] && board[4] == board[8])
                return true;

            if (board[2] != ' ' && board[2] == board[4] && board[4] == board[6])
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            foreach (char cell in board)
            {
                if (cell == ' ')
                    return false;
            }
            return true;
        }
    }
}