using System;

namespace BlackJack_TDD
{
    internal static class Program
    {
        private static void Main()
        {
            CardsHandler.CreateDeck();
            Console.WriteLine(CardsHandler.DrawCard());
            CardsHandler.ShuffleDeck();
        }
    }
}
