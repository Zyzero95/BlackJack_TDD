using System;

namespace BlackJack_TDD
{
    internal static class Program
    {
        private static void Main()
        {
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            BlackJack.Core.Game();
        }
    }
}