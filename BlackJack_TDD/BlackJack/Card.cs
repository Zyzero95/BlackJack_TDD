﻿//Viktor Löfgren
namespace BlackJack_TDD.BlackJack
{
    public class Card
    {
        public enum CardSuit
        {
            Clubs,
            Spades,
            Hearts,
            Diamonds
        }

        public enum CardValue
        {
            YellowCard = 0,
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }

        public bool isVisible { get; set; } = true;

        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }

        public int Score
        {
            get
            {
                if (Value == CardValue.Jack || Value == CardValue.Queen || Value == CardValue.King)
                {
                    return 10;
                }
                if (Value == CardValue.Ace)
                {
                    return 1;
                }
                return Value == CardValue.YellowCard ? 0 : (int)Value;
            }
        }

        public override string ToString() => Suit + " " + Value;
    }
}