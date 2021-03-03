using System;

namespace BlackJack_TDD.BlackJack
{
    public class CardDesign
    {
        public Card cards = new Card();
        public string suit;

        public void Design(Card card)
        {
            if (cards.Suit == Card.CardSuit.Clubs)
            {
                suit = "♣";
            }
            else if (cards.Suit == Card.CardSuit.Diamonds)
            {
                suit = "♦";
            }
            else if (cards.Suit == Card.CardSuit.Hearts)
            {
                suit = "♥";
            }
            else if (cards.Suit == Card.CardSuit.Spades)
            {
                suit = "♠";
            }
            Console.WriteLine(@"┌─────┐");
            Console.WriteLine(@"│{0}│", card.Value.ToString().PadLeft(5));
            Console.WriteLine(@"│{0}    │", suit);
            Console.WriteLine(@"│{0}│", card.Value.ToString().PadLeft(5));
            Console.WriteLine(@"└─────┘");
            Console.WriteLine();
        }
        public void FlipCard(Card card)
        {
            var flipCard = new Card();
            flipCard = card;
        }
    }
}