using System;

namespace BlackJack_TDD.Main
{
    internal class ConsoleInput
    {
        public string GetInput(string question = null)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }
            return Console.ReadLine();
        }
        public string GetOption(Player player)
        {
            var Activeoption = 0;
            ConsoleKey KeyInput;

            var numberOfOPtions = 2;
            if (player.Hand.Count == 2)
            {
                if (player.Saldo > player.Bet)
                {
                    numberOfOPtions++;
                    if (player.Hand[0].Value == player.Hand[1].Value)
                    {
                        numberOfOPtions++;
                    }
                }
            }
            while (true)
            {
                DrawTable.DrawOptions(numberOfOPtions, Activeoption);
                KeyInput = Console.ReadKey().Key;
                switch (KeyInput)
                {
                    case ConsoleKey.Enter:
                        if (Activeoption == 0)
                        {
                            return "hit";
                        }
                        else if (Activeoption == 1)
                        {
                            return "stand";
                        }
                        else if (Activeoption == 2)
                        {
                            return "double";
                        }
                        else if (Activeoption == 3)
                        {
                            return "split";
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Activeoption > 0)
                        {
                            Activeoption--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (Activeoption < numberOfOPtions-1)
                        {
                            Activeoption++;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}