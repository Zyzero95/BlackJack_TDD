using System;

namespace BlackJack_TDD
{
    class Program
    {
        static void Main()
        {
            CardsHandler.CreateDeck();
            Console.WriteLine(CardsHandler.DrawCard());
        }
    }
}
