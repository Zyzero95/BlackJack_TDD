using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    class Dealer
    {
        public List<string> cards { get; set; }

        public void DealCards()
        {
            for (int i = 0; i < 1; i++)
            {
                foreach (var player in Core.Players)
                {
                    player.Cards.Add(CardsHandeler.draw());
                }
                cards.Add(CardsHandeler.draw());
            }
        }
    }
}
