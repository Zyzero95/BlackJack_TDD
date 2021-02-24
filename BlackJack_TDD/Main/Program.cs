using System;

namespace BlackJack_TDD
{
    class Program
    {
        static void Main()
        {
            CardsHandler.CreateDeck();
            Console.WriteLine(CardsHandler.DrawCard());
            CardsHandler.ShuffleDeck();
            for(int i = 0; i < CardsHandler.CardDeck[0].Count; i++)
            {
                Console.WriteLine(CardsHandler.CardDeck[0][i]);
            }
        }
    }
}
