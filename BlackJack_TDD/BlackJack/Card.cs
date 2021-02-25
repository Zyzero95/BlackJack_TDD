﻿using System;
using System.Collections.Generic;
using System.Text;

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
        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
    }
}
