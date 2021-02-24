using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    public class Dealer
    {
        public List<(string, string)> cards { get; set; }

        public void StartOfRound()
        {
            foreach (var player in Core.Players)
            {
                player.Cards.Clear();
            }
            for (int i = 0; i < 1; i++)
            {
                foreach (var player in Core.Players)
                {
                    player.Cards.Add(CardsHandler.DrawCard());
                }
                cards.Add(CardsHandler.DrawCard());
            }
        }
    }
}
