using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Game : World
    {
        Player player;
        World world = new World();
        List<Item> playerInventory = new List<Item>();

        public void NewGame()
        {
            Console.Write("Welcome to Adventure!\nWhat is your name? ");
            player = new Player(Console.ReadLine(), playerInventory, 2); 

        }

        public void GameLoop()
        {
            bool alive = true;

            NewGame();
            Console.Write($"Welcome {player.playerName}!\n");
            while (alive)
            {
                var currentRoom = (from room in RoomList
                             where room.roomId == player.currPosition
                             select room).ToList();

                Console.Write("\n" + currentRoom[0].roomDescription + "\n\n");

                Parse(Console.ReadLine());
                /*
                    Describe current room with item descriptions
                    Wait for user input

                    After input, parse command
                */
            }
        }

        private void Parse(string input)
        {
            List<string> actions = new List<string> { "MOVE", "TAKE", "PICKUP" , "DROP", "LOOK", "OPEN", "CLOSE", "GO", "USE", "INSPECT" };
            List<string> items = new List<string> { "KEY" };
            List<string> directions = new List<string> { "NORTH", "EAST", "SOUTH", "WEST" };
            List<string> exits = new List<string> { "DOOR", "PAINTING", "WINDOW", "TUNNEL", "HOLE" };


            List<string> words = new List<string>();
            foreach (string word in input.Split(' '))
            {
                words.Add(word.ToUpper());
            }
            //Console.Write(words[0]);

            if (!actions.Contains(words[0])) 
            {
                Console.WriteLine("I don't understand what you mean, please rephrase"); //Do thing
            }
            else
            {
                if(words[0] == "MOVE" || words[0] == "GO")
                {
                    words.RemoveAt(0);
                    Move(words);
                } else if (words[0] == "TAKE" || input.ToUpper() == "PICKUP")
                {
                    Console.WriteLine("TEST 2");
                } 
            }
        }
        
        public void Move(List<string> words)
        {
            Console.WriteLine("MOVED");
            // Kolla exitlista med currentPosition
            // Utifrån dem kollar man keywords från deras input och jämför med direction på exits.

            var exits = (from exit in exitList // Checks for available directions at their current position
                         where exit.cameFrom == player.currPosition
                         select exit).ToList();

            foreach (Exit e in exits)
            {
                foreach (string direction in words)
                {
                    if(direction.ToUpper() == e.direction.ToUpper())
                    {
                        player.currPosition = e.leadsTo;
                        return;
                    }
                }
            }
        }
    }
}
