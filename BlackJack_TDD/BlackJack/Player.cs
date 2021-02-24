// Classen är skriven av Jesper P
namespace BlackJack_TDD
{
    using BlackJack_TDD.BlackJack;
    using System;
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
        public List<(string, string)> Cards { get; set; }

        /// <summary>
        /// value of cards
        /// </summary>
        public List<int> CardValue { get; set; }
        /// <summary>
        /// the second set of cards if a player did split
        /// </summary>
        public List<(string, string)> CardsSplit { get; set; }

        /// <summary>
        /// amout player is betting in current round
        /// </summary>
        public double Bet { get; set; }

        /// <summary>
        /// player
        /// </summary>
        /// <param name="saldo">The amount player brings to table</param>
        public Player(double saldo = 5000)
        {
            Saldo = saldo;
        }

        /// <summary>
        /// clears cards and set bet for next round
        /// </summary>
        /// <param name="bet">about player is betting</param>
        public Return setBet(double bet)
        {
            if(bet < Saldo)
            {
                if(bet > Core.MinBet && bet < Core.MaxBet)
                {
                    Bet = bet;
                    Saldo -= bet;
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
                    Cards.Add(CardsHandler.DrawCard());
                    return new Return { Succses = true };

                case "stand":
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
            if (Cards.Count == 2)
            {
                if (Bet < Saldo)
                {
                    Bet += Bet;
                    Cards.Add(CardsHandler.DrawCard());
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
            if (Cards.Count == 2)
            {
                if (Cards[0] == Cards[1])
                {
                    CardsSplit.Add(Cards[1]);
                    CardsSplit.Add(CardsHandler.DrawCard());
                    Cards.RemoveAt(1);
                    Cards.Add(Cards[1]);
                    return new Return { Succses = true };
                }
                return new Return { Succses = false, Exception = "card aint equal value" };
            }
            return new Return { Succses = false, Exception = "too many cards" };
        }
    }


    /// <summary>
    /// Retrun obj
    /// </summary>
    public class Return
    {
        public bool Succses { get; set; }
        public string Exception { get; set; } = null;
    }
}