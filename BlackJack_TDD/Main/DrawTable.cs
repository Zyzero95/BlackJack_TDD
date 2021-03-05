namespace BlackJack_TDD.Main
{
    using BlackJack_TDD.BlackJack;
    using System;

    internal class DrawTable
    {
        public static void DrawGameTabel()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 3, 2); Console.Write("{0}", PadBoth("Dealer", 6));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 5); Console.Write("┌─────┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 6); Console.Write("│Face │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 7); Console.Write("│Down │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 8); Console.Write("│     │");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 8, 9); Console.Write("└─────┘");

            Console.SetCursorPosition((Console.WindowWidth / 2) + 1, 5); Console.Write("┌─────┐");
            Console.SetCursorPosition((Console.WindowWidth / 2) + 1, 6); Console.Write("│{0}│", Core.Dealer.Hand[0].Value.ToString().PadLeft(5));
            Console.SetCursorPosition((Console.WindowWidth / 2) + 1, 7); Console.Write("│     │");
            Console.SetCursorPosition((Console.WindowWidth / 2) + 1, 8); Console.Write("│{0}│", Core.Dealer.Hand[0].Value.ToString().PadLeft(5));
            Console.SetCursorPosition((Console.WindowWidth / 2) + 1, 9); Console.Write("└─────┘");

            for (int i = 0; i < Core.Players.Count; i++)
            {
                var YShift = 5;
                var XShift = 0;
                if (Core.Players[i].Splithand.Count > 0)
                {
                    XShift = -5;
                    foreach (var card in Core.Players[i].Hand)
                    {
                        DrawCard(i, YShift, XShift, card);
                        YShift += 5;
                    }
                    if (Core.Players[i].Isturn && !Core.Players[i].SplithandIsplaying)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + -5, Console.WindowHeight - YShift - 2); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + -5, Console.WindowHeight - YShift - 1); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + -5, Console.WindowHeight - YShift); Console.WriteLine("v");
                    }
                    YShift = 5;
                    foreach (var card in Core.Players[i].Splithand)
                    {
                        DrawCard(i, YShift, XShift + 10, card);
                        YShift += 5;
                    }
                    YShift += 2;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 - 5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].HandValue}");
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + 5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].SplitHandValue}");
                    if (Core.Players[i].Isturn && Core.Players[i].SplithandIsplaying)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + 5, Console.WindowHeight - YShift - 5); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + 5, Console.WindowHeight - YShift - 4); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + 5, Console.WindowHeight - YShift - 3); Console.WriteLine("v");
                    }
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
                    if (Core.Players[i].Isturn)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + XShift, Console.WindowHeight - YShift - 5); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + XShift, Console.WindowHeight - YShift - 4); Console.WriteLine("|");
                        Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + XShift, Console.WindowHeight - YShift - 3); Console.WriteLine("v");
                    }
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].HandValue}");
                }

                Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + XShift - 3, Console.WindowHeight - YShift); Console.WriteLine($"Player {i + 1}");
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

        public static void DrawOptions(int numberOfQuestion, int ActiveOption)
        {
            Console.SetCursorPosition(0, 19); Console.Write(new String(' ', Console.BufferWidth));

            var xShit = numberOfQuestion == 2 ? -16 : numberOfQuestion == 3 ? -24 : numberOfQuestion == 4 ? -31 : 0;
            for (int i = 0; i < numberOfQuestion; i++)
            {
                var stinrgOutput = i == 0 ? "hit" : i == 1 ? "stand" : i == 2 ? "double" : i == 3 ? "split" : "Error";
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShit, 15); Console.Write("┌─────────────┐");
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShit, 16); Console.Write("│{0}│", PadBoth(stinrgOutput, 13));
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShit, 17); Console.Write("└─────────────┘");
                if (ActiveOption == i)
                {
                    Console.SetCursorPosition((Console.WindowWidth / 2) + xShit + 7, 19); Console.Write("/\\");
                }
                xShit += 17;
            }
            Console.SetCursorPosition(0, 0);
        }

        public static void DrawScoreTable()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth / 2) - 3, 2); Console.Write("Dealer");
            Console.SetCursorPosition((Console.WindowWidth / 2) - 5, 3); Console.Write($"Total: {Core.Dealer.HandValue.ToString()}");
            var xShift = 0;
            var cardCount = Core.Dealer.Hand.Count;
            xShift = -((Core.Dealer.Hand.Count * 7) / 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var card in Core.Dealer.Hand)
            {
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShift, 5); Console.Write("┌─────┐");
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShift, 6); Console.Write("│{0}│", card.Value.ToString().PadRight(5));
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShift, 7); Console.Write("│     │");
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShift, 8); Console.Write("│{0}│", card.Value.ToString().PadLeft(5));
                Console.SetCursorPosition((Console.WindowWidth / 2) + xShift, 9); Console.Write("└─────┘");
                xShift += 7;
            }
            for (int i = 0; i < Core.Players.Count; i++)
            {
                var YShift = 5;
                var XShift = 0;
                if (Core.Players[i].Splithand.Count > 0)
                {
                    foreach (var card in Core.Players[i].Hand)
                    {
                        DrawCard(i, YShift, -5, card);
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
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 - 5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].HandValue}");
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + 5, Console.WindowHeight - 2); Console.Write($"Total {Core.Players[i].SplitHandValue}");
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 - 5, Console.WindowHeight - 1); Console.Write($"{Core.Players[i].FinishStatusHand}");
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3 + 5, Console.WindowHeight - 1); Console.Write($"{Core.Players[i].FinishStatusSplit}");
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
                    Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) - 3, Console.WindowHeight - 1); Console.Write($"{Core.Players[i].FinishStatusHand}");
                }
                Console.SetCursorPosition(Console.WindowWidth / 8 * (i + 1) + XShift - 3, Console.WindowHeight - YShift); Console.WriteLine($"Player {i + 1}");
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// Make stirng in center
        /// code by "David Colwell"
        /// https://stackoverflow.com/questions/17590528/pad-left-pad-right-pad-center-string
        /// </summary>
        /// <param name="source">sring</param>
        /// <param name="length">length of Gap</param>
        /// <returns></returns>
        public static string PadBoth(string source, int length)
        {
            var spaces = length - source.Length;
            var padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);
        }
    }
}