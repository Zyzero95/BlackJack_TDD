using System;

namespace BlackJack_TDD
{
    internal static class Program
    {
        private static void Main()
        {
            CardsHandler cardsHandler = new CardsHandler();
            cardsHandler.CreateDeck();
            cardsHandler.ShuffleDeck();
            Console.WriteLine(cardsHandler.DrawCard());
            
        }
    }
}
