using System;
using System.Collections.Generic;
using System.Text;

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
