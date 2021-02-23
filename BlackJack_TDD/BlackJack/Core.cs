using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.BlackJack
{
    public static class Core
    {
        public static List<Player> Players { get; set; }
        public static double MinBet { get; internal set; }
        public static double MaxBet { get; internal set; }
        public enum Gamestage
        {
            starting,
            ongoing,
            end
        }

        public static void game()
        {
            var gamestate = new Gamestage();
            var dealer = new Dealer();
            Players.Add(new Player());
            
            while (true)
            {
                if(gamestate == Gamestage.starting)
                {
                    dealer.startOfRound();
                    foreach (var player in Players)
                    {
                        var tryparse = double.TryParse(Console.ReadLine(), out var bet);
                        if (tryparse)
                        {
                            var result = player.setBet(bet);
                            if (!result.Succses)
                            {
                                Console.WriteLine(result.Exception);
                            }
                        }
                        Console.WriteLine("bet wasn't an number");
                    }
                }
            }
        }
    }
}
