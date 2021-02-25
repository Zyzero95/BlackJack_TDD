//Viktor Löfgren
using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD
{
    public class CardsHandler
    {
        public static Stack<Card> SetOfCards = new Stack<Card>();
        public static List<Card> cards = new List<Card>();

        //Creates a deck of cards.
        public static void CreateDeck()
        {
            foreach (Card.CardSuit suit in (Card.CardSuit[])Enum.GetValues(typeof(Card.CardSuit)))
            {
                foreach (Card.CardValue value in (Card.CardValue[])Enum.GetValues(typeof(Card.CardValue)))
                {
                    Card newCard = new Card()
                    {
                        Suit = suit,
                        Value = value
                    };
                    cards.Add(newCard);
                }
            }
        }

        //Enables the dealer to draw a card for either a player or the table.
        public static Card DrawCard()
        {
            return SetOfCards.Pop();
        }

        public static void ShuffleDeck()
        {
            var arrayOfCards = cards.ToArray();
            Random r = new Random();

            for(int i = arrayOfCards.Length - 1; i > 0; i--)
            {
                int k = r.Next(i + 1);

                Card tempCard = arrayOfCards[i];
                arrayOfCards[i] = arrayOfCards[k];
                arrayOfCards[k] = tempCard;
            }
            for(int j = 0; j < arrayOfCards.Length; j++)
            {
                SetOfCards.Push(arrayOfCards[j]);
            }
        }
    }
}
