using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    public class Dealer
    {
        public List<Card>Hand = new List<Card>();
        public CardsHandler cardsHandler = new CardsHandler();

        public int HandValue;
        private List<Card> TempAce = new List<Card>();
        private CardsHandler CardDeck;

        public Dealer(CardsHandler deck)
        {
            CardDeck = deck;
        }

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
                Hand.Add(cardsHandler.DrawCard());
            }
        }
        public void Turn()
        {
            var draw = true;
            while (draw)
            {
                CalculateHand();
                if (HandValue < 17)
                {
                    Hand.Add(CardDeck.DrawCard());
                }
                else
                {
                    draw = false;
                }
            }
        }
        private void CalculateHand()
        {
            HandValue = 0;
            foreach (var card in Hand)
            {
                if (card.Value == Card.CardValue.Ace)
                {
                    TempAce.Add(card);
                }
                else
                {
                    HandValue += card.Score;
                }
            }
            if (TempAce.Count > 0)
            {
                HandValue = HandValue + (10 + TempAce.Count) < 21 ? HandValue + (10 + TempAce.Count) : HandValue + TempAce.Count;
            }
        }
    }
}
