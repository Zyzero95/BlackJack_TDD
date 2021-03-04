using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack_TDD
{
    public class Tutoring
    {

        public static bool StartOfRound
        {
            get { return startOfRound; }
            set
            {
                startOfRound = value;
                if (value)
                {
                    RemoveCards();
                }
            }
        }

        private static bool startOfRound;
        private static List<Card> DeckOfCard = Core.CardDeck.cards;
        public bool helpterSwitch;

        private Player player;

        public Tutoring(Player player)
        {
            this.player = player;
        }

        internal static void RemoveOneCard(Card card)
        {
            DeckOfCard.Remove(card);
        }

        internal static void ClearCard()
        {
            DeckOfCard = Core.CardDeck.cards;
        }

        private static void RemoveCards()
        {
            foreach (var player in Core.Players)
            {
                foreach (var card in player.Hand)
                {
                    DeckOfCard.Remove(card);
                }
            }
            foreach (var card in Core.Dealer.Hand)
            {
                DeckOfCard.Remove(card);
            }
            foreach (var card in DeckOfCard.Where(c => c.Value == Card.CardValue.YellowCard).ToList())
            {
                DeckOfCard.Remove(card);
            }
        }


        public string Cheat()
        {
            int untilBlackJack;
            if (player.HandValue < 15)
            {
                return "You should hit";
            }
            else
            {
                untilBlackJack = 21 - player.HandValue;
                var listOfVaildDraws = DeckOfCard.Where(x => x.Score <= untilBlackJack).ToList();
                if (Core.Dealer.HandValue > 16 && player.HandValue < Core.Dealer.HandValue)
                {
                    return "You should hit";
                }
                else if (player.HandValue >= 20)
                {
                    return "You should stand";
                }
                else if (listOfVaildDraws.Count >= DeckOfCard.Count / 2)
                {
                    return helpterSwitch ? "You should hit" : "You should stand";
                }
                else
                {
                    return !helpterSwitch ? "You should hit" : "You should stand";
                }

            }
        }
    }
}