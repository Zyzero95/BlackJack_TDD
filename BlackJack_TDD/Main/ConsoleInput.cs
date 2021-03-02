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
    }
}