using System;
using System.Collections.Generic;
using System.Text;
using BlackJack_TDD;

namespace BlackJack_TDD.BlackJack
{
    public static class Core
    {
        public static List<Player> Players = new List<Player>();
        public static double MinBet { get; internal set; } = 20;
        public static double MaxBet { get; internal set; } = 500;
        public enum Gamestage
        {
            starting,
            ongoing,
            end
        }

        public static void Game()
        {
            var gamestate = Gamestage.starting;
            var dealer = new Dealer();
            var cardDeck = new CardsHandler();
            var input = new Main.ConsoleInput();
            Players.Add(new Player(cardDeck));

            while (true)
            {
                if(gamestate == Gamestage.starting)
                {
                    dealer.StartOfRound();
                    foreach (var player in Players)
                    {
                        var vailidbet = false;
                        while (!vailidbet)
                        {
                            var tryparse = double.TryParse(input.GetInput($"Table bet range\n {MinBet} - {MaxBet} \nHow much do you wnat to bet?"), out var bet);
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
                    gamestate = Gamestage.ongoing;
                }
                if (gamestate == Gamestage.ongoing)
                {
                    foreach(var player in Players)
                    {
                        while (player.IsPlaying)
                        {
                            foreach(var card in player.Hand)
                            {
                                Console.WriteLine(@"┌─────┐");
                                Console.WriteLine(@"│{0}   │", card.Value);
                                Console.WriteLine(@"│{0}│",card.Suit);
                                Console.WriteLine(@"│  {0} │", card.Value);
                                Console.WriteLine(@"└─────┘");
                                Console.WriteLine();
                            }
                            player.Turn(input.GetInput("what is your next move?"));

                            Console.Clear();
                        }
                    }
                }
            }
        }

        
    }
}
