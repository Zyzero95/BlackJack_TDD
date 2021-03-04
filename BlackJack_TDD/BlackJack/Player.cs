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
        /// tutoring Activted
        /// </summary>
        public bool CheatOn;

        /// <summary>
        /// players Cards
        /// </summary>
        public List<Card> Hand = new List<Card>();

        /// <summary>
        /// vlue of the card in teh hand
        /// </summary>
        public int HandValue;

        public int SplitHandValue;

        /// <summary>
        /// second hand ov card if it was a slit
        /// </summary>
        public List<Card> Splithand = new List<Card>();

        public bool SplithandIsplaying { get; set; }

        private CardsHandler cardDeck;

        /// <summary>
        /// player
        /// </summary>
        /// <param name="saldo">The amount player brings to table</param>
        public Player(CardsHandler cardDeck, double saldo = 5000)
        {
            Saldo = saldo;
            this.cardDeck = cardDeck;
            Hand.Add(cardDeck.DrawCard());
        }

        /// <summary>
        /// amout player is betting in current round
        /// </summary>
        public double Bet { get; set; }

        /// <summary>
        /// if pleyer is coint to make more moves this turn.
        /// </summary>
        public bool IsPlaying { get; set; }

        /// <summary>
        /// Palyer Saldo
        /// </summary>
        public double Saldo { get; set; }

        /// <summary>
        /// The TutoringBot
        /// </summary>
        public Tutoring Tutoring { get; private set; }

        /// <summary>
        /// clears cards and set bet for next round
        /// 0 = skip round
        /// </summary>
        /// <param name="bet">pleyer Bet amount</param>
        public Return SetBet(double bet)
        {
            if (bet == -1337)
            {
                CheatOn = true;
                Tutoring = new Tutoring(this);
                return new Return { Succses = false, Exception = "Activaded Cheat" };
            }
            else if (bet == -88224646 && CheatOn)
            {
                Tutoring.helpterSwitch = true;
                return new Return { Succses = false, Exception = "Ulitmate Cheat Activated" };
            }
            else if (bet == 0)
            {
                IsPlaying = false;
                return new Return { Succses = true, Exception = "palyer is skiping this round" };
            }
            else if (bet <= Saldo)
            {
                if (bet >= Core.MinBet && bet <= Core.MaxBet)
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
                    if (!SplithandIsplaying)
                    {
                        Hand.Add(cardDeck.DrawCard());
                    }
                    else
                    {
                        Splithand.Add(cardDeck.DrawCard());
                    }
                    CalculateHand();
                    return new Return { Succses = true };

                case "stand":
                    IsPlaying = false;
                    CalculateHand();
                    return new Return { Succses = true };

                default:
                    return new Return { Succses = false, Exception = "no choice was made" };
            }
        }

        /// <summary>
        /// Calculates the value of the hand and insert it inro player.handValue
        /// </summary>
        internal void CalculateHand()
        {
            var tempValue = 0;
            var TempAce = new List<Card>();

            foreach (var card in !SplithandIsplaying ? Hand : Splithand)
            {
                if (card.Value == Card.CardValue.Ace)
                {
                    TempAce.Add(card);
                }
                else
                {
                    tempValue += card.Score;
                }
            }
            if (TempAce.Count > 0)
            {
                tempValue = tempValue + (10 + TempAce.Count) <= 21 ? tempValue + (10 + TempAce.Count) : tempValue + TempAce.Count;
            }
            if (tempValue >= 21)
            {
                IsPlaying = false;
            }

            if (SplithandIsplaying)
            {
                SplitHandValue = tempValue;
            }
            else
            {
                HandValue = tempValue;
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
                    if (!SplithandIsplaying) { Hand.Add(cardDeck.DrawCard()); }
                    else { Splithand.Add(cardDeck.DrawCard()); }

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
        /// <returns>object if it was succsesfull and error message</returns>jagkom
        private Return Split()
        {
            if (Hand.Count == 2)
            {
                if (Saldo > Bet)
                {
                    if (Hand[0].Value == Hand[1].Value)
                    {
                        Saldo -= Bet;
                        Splithand.Add(Hand[1]);
                        Splithand.Add(cardDeck.DrawCard());
                        Hand.RemoveAt(1);
                        Hand.Add(cardDeck.DrawCard());
                        CalculateHand();
                        return new Return { Succses = true };
                    }
                    return new Return { Succses = false, Exception = "card aint equal value" };
                }
                return new Return { Succses = false, Exception = "you didn't bring enougth to the casino" };
            }
            return new Return { Succses = false, Exception = "too many cards" };
        }
    }
}