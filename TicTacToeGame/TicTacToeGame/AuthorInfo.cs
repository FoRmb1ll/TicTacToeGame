// AuthorInfo.cs
// Файл с информацией об авторе
// Created: [текущая дата]

using System;

namespace TicTacToeGame
{
    public static class AuthorInfo
    {
        public const string FullName = "Дедюхин Дмитрий"; 
        public const string Group = "ИСП(9)-23-1"; 
        public const string Date = "28.02.2026";

        public static void DisplayInfo()
        {
            Console.WriteLine($"Автор: {FullName}");
            Console.WriteLine($"Группа: {Group}");
            Console.WriteLine($"Дата: {Date}");
        }
    }
}