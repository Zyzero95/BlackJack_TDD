using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    public class Dealer
    {
        public List<Card> Cards { get; set; }

        public void StartOfRound()
        {
            foreach (var player in Core.Players)
            {
                player.Hand.Clear();
            }
            for (int i = 0; i < 1; i++)
            {
                foreach (var player in Core.Players)
                {
                    player.Hand.Add(CardsHandler.DrawCard());
                }
                Cards.Add(CardsHandler.DrawCard());
            }
        }
    }
}
