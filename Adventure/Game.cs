using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Game
    {
        public void NewGame()
        {

        }

        private void Parse()
        {
            string input = Console.ReadLine();
            string[] words = input.Split(' ');
            Console.Write(words[0]);
        }
    }
}
