// Classen är skriven av Jesper P
namespace BlackJack_TDD
{
    using BlackJack_TDD.BlackJack;
    using System.Collections.Generic;

    /// <summary>
    /// en spelare
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Palyer Saldo
        /// </summary>
        public double Saldo { get; set; }

        /// <summary>
        /// players Cards
        /// </summary>
        public List<Card> Hand = new List<Card>();

        public List<Card> Splithand = new List<Card>();
        private List<Card> TempAce = new List<Card>();

        public int HandValue;

        /// <summary>
        /// amout player is betting in current round
        /// </summary>
        public double Bet { get; set; }

        public bool IsPlaying { get; private set; }
        private CardsHandler CardDeck;

        /// <summary>
        /// player
        /// </summary>
        /// <param name="saldo">The amount player brings to table</param>
        public Player(CardsHandler cardDeck, double saldo = 5000)
        {
            Saldo = saldo;
            CardDeck = cardDeck;
            Hand.Add(cardDeck.DrawCard());
        }

        /// <summary>
        /// clears cards and set bet for next round
        /// </summary>
        /// <param name="bet">about player is betting</param>
        public Return SetBet(double bet)
        {
            if (bet == 0)
            {
                IsPlaying = false;
            }
            else if (bet < Saldo)
            {
                if (bet > Core.MinBet && bet < Core.MaxBet)
                {
                    Bet = bet;
                    Saldo -= bet;
                    IsPlaying = true;
                    return new Return { Succses = true };
                }
                return new Return { Succses = false, Exception = "bet isnt inside betRange" };
            }
            return new Return { Succses = false, Exception = "you dont didnt bring that much to the casino" };
        }

        /// <summary>
        /// logic for what palyer can do
        /// </summary>
        /// <param name="choice">what player wanan do</param>
        /// <returns>object if it was succsesfull and error message</returns>
        public Return Turn(string choice)
        {
            switch (choice.ToLower())
            {
                case "split":
                    return Split();

                case "double":
                    return Double();

                case "hit":
                    Hand.Add(CardDeck.DrawCard());
                    CalculateHand();
                    return new Return { Succses = true };

                case "stand":
                    IsPlaying = false;
                    return new Return { Succses = true };

                default:
                    return new Return { Succses = false, Exception = "no choice was made" };
            }
        }

        /// <summary>
        /// Cheack if Double is possible and do double
        /// </summary>
        /// <returns>object if it was succsesfull and error message</returns>
        private Return Double()
        {
            if (Hand.Count == 2)
            {
                if (Bet < Saldo)
                {
                    Saldo -= Bet;
                    Bet += Bet;
                    Hand.Add(CardDeck.DrawCard());
                    IsPlaying = false;
                    CalculateHand();
                    return new Return { Succses = true };
                }
                return new Return { Succses = false, Exception = "Too little on Saldo" };
            }
            return new Return { Succses = false, Exception = "too many cards" };
        }

        /// <summary>
        /// Cheack if split is possible and then split the cards
        /// </summary>
        /// <returns>object if it was succsesfull and error message</returns>
        private Return Split()
        {
            if (Hand.Count == 2)
            {
                if (Hand[0] == Hand[1])
                {
                    Splithand.Add(Hand[1]);
                    Splithand.Add(CardDeck.DrawCard());
                    Hand.RemoveAt(1);
                    Hand.Add(Hand[1]);
                    CalculateHand();
                    return new Return { Succses = true };
                }
                return new Return { Succses = false, Exception = "card aint equal value" };
            }
            return new Return { Succses = false, Exception = "too many cards" };
        }

        /// <summary>
        /// Calculates the value of the hand and insert it inro player.handValue
        /// </summary>
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
            if (HandValue >= 21)
            {
                IsPlaying = false;
            }
        }
    }
}