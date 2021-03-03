using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack_TDD
{
    public class Tutoring
    {
        private static bool startOfRound;
        public static bool StartOfRound
        {
            get { return startOfRound; }
            set {
                startOfRound = value;
                if (value)
                {
                    RemoveCards();
                }
            }
        }
        private Player player;

        private static List<Card> DeckOfCard = Core.CardDeck.cards;

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

        public void Cheat()
        {
            int untilBlackJack;
            if (player.HandValue < 10)
            {
                Console.WriteLine("You should hit");
            }
            else
            {
                untilBlackJack = 21 - player.HandValue;
                if (Core.Dealer.HandValue > 16 && player.HandValue <Core.Dealer.HandValue)
                {
                    Console.WriteLine("You should hit");
                }
                var listOfVaildDraws = DeckOfCard.Where(x => x.Score <= untilBlackJack).ToList();
                if (listOfVaildDraws.Count >= DeckOfCard.Count/2)
                {
                    Console.WriteLine("You should hit");
                }
                else
                {
                    Console.WriteLine("You should stand");
                }
            }
        }
    }
}