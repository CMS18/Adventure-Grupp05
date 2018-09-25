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
            Console.WriteLine("Welcome to Adventure!");
        }

        private void Parse()
        {
            List<string> Actions = new List<string> { "MOVE", "TAKE", "DROP", "LOOK", "OPEN", "CLOSE", "GO", "USE", "INSPECT", "LOOK" };
            List<string> Directions = new List<string> { "NORTH", "EAST", "SOUTH", "WEST" };

            string input = Console.ReadLine();
            string[] words = input.Split(' ');
            //Console.Write(words[0]);

            if (!Actions.Contains(words[0])) 
            {
                Console.WriteLine("I don't understand what you mean, please rephrase"); //Do thing
            }
        }
    }
}
