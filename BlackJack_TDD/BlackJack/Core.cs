using System;
using System.Collections.Generic;

namespace BlackJack_TDD.BlackJack
{
    public static class Core
    {
        public static List<Player> Players = new List<Player>();

        public static CardDesign design = new CardDesign();
        public static double MinBet { get; internal set; } = 20;
        public static double MaxBet { get; internal set; } = 500;
        public static CardsHandler CardDeck { get; set; }
        public static Dealer Dealer { get; set; }

        public static void Game()
        {
            var input = new Main.ConsoleInput();
            CardDeck = new CardsHandler();
            Dealer = new Dealer(CardDeck);
            AddPlayers(input);
            while (true)
            {
                //starting Phase
                Dealer.StartOfRound();
                Tutoring.StartOfRound = true;
                var i = 0;
                foreach (var player in Players)
                {
                    i++;
                    Console.WriteLine($"plyer {i}:\n");
                    var vailidbet = false;
                    while (!vailidbet)
                    {
                        var tryparse = double.TryParse(input.GetInput($"Saldo: {player.Saldo} \nTable bet range\n {MinBet} - {MaxBet} \nHow much do you wnat to bet?"), out var bet);
                        if (tryparse)
                        {
                            var result = player.SetBet(bet);
                            if (!result.Succses)
                            {
                                Console.WriteLine(result.Exception);
                            }
                            vailidbet = result.Succses;
                        }
                        else
                        {
                            Console.WriteLine("bet wasn't an number");
                        }
                    }
                }
                //GamePhase
                Console.Clear();
                foreach (var player in Players)
                {
                    if (player.IsPlaying)
                    {
                        PlayerHand(input, player);
                        if (player.Splithand.Count > 0)
                        {
                            PlayerSlipHand(input, player);
                        }
                    }
                    Console.WriteLine("Dealers hand");
                    foreach (var card in Dealer.Hand)
                    {
                        design.Design(card);
                    }
                    Console.WriteLine("Your card");
                    foreach (var card in player.Hand)
                    {
                        design.Design(card);
                    }
                }

                //EndPhase
                Tutoring.RemoveOneCard(Dealer.Hand[1]);
                Dealer.Turn();

                Calculatewin();
            }
        }

        private static void AddPlayers(Main.ConsoleInput input)
        {
            while (true)
            {
                if (int.TryParse(input.GetInput("how many player between 1 and 7"), out int amount))
                {
                    if (amount > 0 && amount < 8)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            Players.Add(new Player(CardDeck));
                        }
                        return;
                    }
                    Console.WriteLine("wrong value");
                }
            }
        }

        private static void PlayerSlipHand(Main.ConsoleInput input, Player player)
        {
            player.SplithandIsplaying = true;
            player.IsPlaying = true;
            while (player.IsPlaying)
            {
                foreach (var card in Dealer.Hand)
                {
                    design.Design(card);
                }
                Console.WriteLine("Your card");
                foreach (var card in player.Splithand)
                {
                    design.Design(card);
                }
                if (player.CheatOn)
                {
                    Console.WriteLine(player.Tutoring.Cheat());
                }
                player.Turn(input.GetInput("what is your next move?"));

                Console.Clear();
            }
        }

        private static void PlayerHand(Main.ConsoleInput input, Player player)
        {
            while (player.IsPlaying)
            {
                foreach (var card in Dealer.Hand)
                {
                    design.Design(card);
                }
                Console.WriteLine("Your card");
                foreach (var card in player.Hand)
                {
                    design.Design(card);
                }
                if (player.CheatOn)
                {
                    Console.WriteLine(player.Tutoring.Cheat());
                }
                player.Turn(input.GetInput("what is your next move?"));

                Console.Clear();
            }
        }

        /// <summary>
        /// Calculte if player wins agienst the House
        /// </summary>
        public static void Calculatewin()
        {
            foreach (var player in Players)
            {
                if (player.Bet > 0)
                {
                    if (player.HandValue == 21)
                    {
                        Console.WriteLine("blackjack");
                        player.Saldo += player.Bet + (player.Bet * 1.5);
                    }
                    else if ((player.HandValue < 21 && player.HandValue > Dealer.HandValue) || (player.HandValue < 21 && Dealer.HandValue > 21))
                    {
                        Console.WriteLine("won");
                        player.Saldo += player.Bet * 2;
                    }
                    else if (player.HandValue < 21 && player.HandValue == Dealer.HandValue)
                    {
                        Console.WriteLine("Push");
                        player.Saldo += player.Bet;
                    }
                    else
                    {
                        Console.WriteLine("you lost");
                    }
                    if (player.Splithand.Count > 0)
                    {
                        if (player.SplitHandValue == 21)
                        {
                            Console.WriteLine("blackjack");
                            player.Saldo += player.Bet + (player.Bet * 1.5);
                        }
                        else if ((player.SplitHandValue < 21 && player.SplitHandValue > Dealer.HandValue) || (player.HandValue < 21 && Dealer.HandValue > 21))
                        {
                            Console.WriteLine("won");
                            player.Saldo += player.Bet * 2;
                        }
                        else if (player.SplitHandValue < 21 && player.SplitHandValue == Dealer.HandValue)
                        {
                            Console.WriteLine("Push");
                            player.Saldo += player.Bet;
                        }
                        else
                        {
                            Console.WriteLine("you lost");
                        }
                    }
                }
            }
        }
    }
}