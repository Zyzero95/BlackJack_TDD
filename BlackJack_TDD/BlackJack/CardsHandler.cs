//Viktor Löfgren
using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;

namespace BlackJack_TDD
{
    public class CardsHandler : Card
    {
        public static int NumberOfdecks = 2;
        public Stack<Card> SetOfCards = new Stack<Card>();
        public List<Card> cards = new List<Card>();

        public CardsHandler()
        {
            CreateDeck();
            ShuffleDeck(SetOfCards);
        }

        //Creates a deck of cards.
        private void CreateDeck()
        {
            for (int i = 0; i < NumberOfdecks; i++)
            {
                foreach (Card.CardSuit suit in (Card.CardSuit[])Enum.GetValues(typeof(Card.CardSuit)))
                {
                    foreach (Card.CardValue value in (Card.CardValue[])Enum.GetValues(typeof(Card.CardValue)))
                    {
                        if (value != CardValue.YellowCard)
                        {
                            var newCard = new Card
                            {
                                Suit = suit,
                                Value = value
                            };
                            cards.Add(newCard);
                        }
                    }
                }
            }
        }

        //Enables the dealer to draw a card for either a player or the table.
        public Card DrawCard()
        {
            if (SetOfCards.Peek().Value == CardValue.YellowCard)
            {
                SetOfCards.Clear();
                ShuffleDeck(SetOfCards);
                Tutoring.ClearCard();
            }
            var card = SetOfCards.Pop();
            Tutoring.RemoveOneCard(card);
            return card;
        }

        private void ShuffleDeck(Stack<Card> cardDeck)
        {
            var arrayOfCards = cards.ToArray();
            var r = new Random();
            var yellow = r.Next(10, 26);

            for (var i = arrayOfCards.Length - 1; i > 0; i--)
            {
                var k = r.Next(i + 1);

                var tempCard = arrayOfCards[i];
                arrayOfCards[i] = arrayOfCards[k];
                arrayOfCards[k] = tempCard;
            }
            for (var j = 0; j < arrayOfCards.Length; j++)
            {
                if (j == yellow)
                {
                    cardDeck.Push(new Card { Value = CardValue.YellowCard });
                }
                cardDeck.Push(arrayOfCards[j]);
            }
        }
    }
}