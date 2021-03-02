using System;

namespace BlackJack_TDD
{
    public class Tutoring
    {
        private Player player;

        public Tutoring(Player player)
        {
            this.player = player;
        }

        public void Cheat()
        {
            if (player.HandValue < 17)
            {
                Console.WriteLine("I tihnk you should hit");
            }
            else if (player.HandValue > 16)
            {
                Console.WriteLine("i think you Should stand");
            }
        }
    }
}