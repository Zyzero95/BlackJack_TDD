//Viktor Löfgren
using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD
{
    public class CardsHandler
    {
        public Stack<Card> SetOfCards = new Stack<Card>();
        public List<Card> cards = new List<Card>();

        //Creates a deck of cards.
        public void CreateDeck()
        {
            foreach (Card.CardSuit suit in (Card.CardSuit[])Enum.GetValues(typeof(Card.CardSuit)))
            {
                foreach (Card.CardValue value in (Card.CardValue[])Enum.GetValues(typeof(Card.CardValue)))
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

        //Enables the dealer to draw a card for either a player or the table.
        public Card DrawCard()
        {
            return SetOfCards.Pop();
        }

        public void ShuffleDeck()
        {
            var arrayOfCards = cards.ToArray();
            var r = new Random();

            for (var i = arrayOfCards.Length - 1; i > 0; i--)
            {
                var k = r.Next(i + 1);

                var tempCard = arrayOfCards[i];
                arrayOfCards[i] = arrayOfCards[k];
                arrayOfCards[k] = tempCard;
            }
            for(var j = 0; j < arrayOfCards.Length; j++)
            {
                SetOfCards.Push(arrayOfCards[j]);
            }
        }
    }
}
