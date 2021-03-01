using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.Main
{
    class ConsoleInput
    {
        public string GetInput(string question = null)
        {
            if (question != null)
            {
                Console.WriteLine(question);
            }
            return Console.ReadLine();
        }
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }

    }
}
