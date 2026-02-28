// TicTacToeV1.cs
// Первая версия - обычная игра в крестики-нолики
// Автор: Иванов Иван Иванович

using System;

namespace TicTacToeGame
{
    class TicTacToeV1
    {
        private char[] board = new char[9];
        private char currentPlayer;

        public TicTacToeV1()
        {
            InitializeBoard();
            currentPlayer = 'X';
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                board[i] = ' ';
            }
        }

        public void Run()
        {
            Console.Clear(); 
            Console.WriteLine("=====================================");
            Console.WriteLine("     КРЕСТИКИ-НОЛИКИ (Версия 1.0)   ");
            Console.WriteLine("=====================================");
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
                    gameEnded = true;
                }
                else if (IsBoardFull())
                {
                    Console.Clear(); 
                    PrintBoard();
                    Console.WriteLine("\n🤝 Ничья! 🤝");
                    gameEnded = true;
                }
                else
                {
                    // Смена игрока
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                    Console.Clear(); 
                    Console.WriteLine("=====================================");
                    Console.WriteLine("     КРЕСТИКИ-НОЛИКИ (Версия 1.0)   ");
                    Console.WriteLine("=====================================");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nИгра окончена. Нажмите любую клавишу...");
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