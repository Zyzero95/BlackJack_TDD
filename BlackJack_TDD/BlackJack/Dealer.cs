using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    public class Dealer
    {
        public CardsHandler cardsHandler = new CardsHandler();
        public List<Card> Cards = new List<Card>();

        public void StartOfRound()
        {
            foreach (var player in Core.Players)
            {
                    player.Hand.Clear();
            }
            for (int i = 0; i <= 1; i++)
            {
                foreach (var player in Core.Players)
                {
                    player.Hand.Add(cardsHandler.DrawCard());
                }
                Cards.Add(cardsHandler.DrawCard());
            }
        }
    }
}
