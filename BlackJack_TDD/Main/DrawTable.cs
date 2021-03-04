namespace BlackJack_TDD.Main
{
    using BlackJack_TDD.BlackJack;
    using System;
    using System.Collections.Generic;
    using System.Text;

    class DrawTable
    {
        public static void DrawGameTabel()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 3, 2); Console.Write("Dealer");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, 5); Console.Write("┌─────┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, 6); Console.Write("│Face │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, 7); Console.Write("│Down │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, 8); Console.Write("│     │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 10, 9); Console.Write("└─────┘");

            Console.SetCursorPosition((Console.WindowWidth / 2) + 3, 5); Console.Write("┌─────┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) + 3, 6); Console.Write("│{0}│", Core.Dealer.Hand[0] .Value.ToString().PadLeft(5));
            Console.SetCursorPosition((Console.WindowWidth / 2) + 3, 7); Console.Write("│     │");
            Console.SetCursorPosition((Console.WindowWidth / 2) + 3, 8); Console.Write("│{0}│", Core.Dealer.Hand[0].Value.ToString().PadLeft(5));
            Console.SetCursorPosition((Console.WindowWidth / 2) + 3, 9); Console.Write("└─────┘");

            for (int i = 0; i < Core.Players.Count; i++)
            {
                var YShift = 5;
                var XShift = 0;
                if (Core.Players[i].Splithand.Count > 0)
                {
                    foreach (var card in Core.Players[i].Hand)
                    {
                        DrawCard(i, YShift,-5, card);
                        YShift += 5;
                    }
                    YShift = 5;
                    foreach (var card in Core.Players[i].Splithand)
                    {
                        DrawCard(i, YShift, 5, card);
                        YShift += 5;
                    }
                    YShift += 2;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 -5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].HandValue}");
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + 5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].SplitHandValue}");
                }
                else
                {
                    foreach (var card in Core.Players[i].Hand)
                    {
                        DrawCard(i, YShift, 0, card);
                        YShift += 5;
                    }
                    YShift += 2;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].HandValue}");
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        private static void DrawCard(int i, int YShift, int XShift, Card card)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + XShift, Console.WindowHeight - YShift - 4); Console.Write("┌─────┐");
            Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + XShift, Console.WindowHeight - YShift - 3); Console.Write("│{0}│", card.Value.ToString().PadLeft(5));
            Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + XShift, Console.WindowHeight - YShift - 2); Console.Write("│     │");
            Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + XShift, Console.WindowHeight - YShift - 1); Console.Write("│{0}│", card.Value.ToString().PadLeft(5));
            Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + XShift, Console.WindowHeight - YShift); Console.Write("└─────┘");
        }

        public static void DrawScoreTable()
        {

        }
    }
}
