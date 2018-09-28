using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adventure.Extension;

namespace Adventure
{
    class Game : World
    {
        bool changedRoom = true;
        bool lockedDoor = false;

        Player player;
        World world = new World();
        List<Item> initPlayerInventory = new List<Item>();

        public void NewGame()
        {
            Console.Write("Welcome to Adventure!\nWhat is your name? ");
            player = new Player(Console.ReadLine(), initPlayerInventory, 1); 
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

                if (changedRoom)
                {
                    Console.Write("\n" + currentRoom[0].roomDescription + "\n\n"); // Skriver ut beskrivning av nuvarande rum
                } else if (lockedDoor == true)
                {
                    
                } else
                {
                    //Console.WriteLine("You can't move that way\n");
                }


                Console.Write("> "); Parse(Console.ReadLine());
                /*
                    Describe current room with item descriptions
                    Wait for user input

                    After input, parse command
                */
            }
        }

        private void Parse(string input)
        {
            List<string> actions = new List<string> { "MOVE", "TAKE", "PICKUP" , "DROP", "LOOK", "OPEN", "CLOSE", "GO", "USE", "INSPECT", "PICK", "COMMANDS", "HELP" };
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
                Console.WriteLine("I don't understand what you mean, please rephrase\n"); //Do thing
            }
            else
            {
                switch(words[0])
                {
                    case "MOVE":
                    case "GO":
                        words.RemoveAt(0);
                        changedRoom = Move(words);
                        break;

                    case "TAKE":
                    case "PICK":
                    case "PICKUP":
                        words.RemoveAt(0);
                        Take(words);
                        break;
                    case "LOOK":
                    case "INSPECT":
                        Look(words);
                        break;
                    case "USE":
                        Use(words, player.currPosition);
                        break;
                    case "DROP":
                    case "LEAVE":
                        words.RemoveAt(0);
                        Drop(words);
                        break;
                    case "COMMANDS":
                    case "HELP":
                    case "C":
                        Commands();
                        break;
                }

                //if (words[0] == "MOVE" || words[0] == "GO")
                //{
                //    words.RemoveAt(0);
                //    changedRoom = Move(words);
                //} else if (words[0] == "TAKE" || input.ToUpper() == "PICKUP" || input.ToUpper() == "PICK")
                //{
                //    words.RemoveAt(0);
                //    Take(words);
                //} else if (words[0] == "LOOK" || words[0] == "INSPECT") {
                //    Look(words);
                //} else if (words[0] == "USE")
                //{
                //    Use(words, player.currPosition);
                //} else if (words[0] == "DROP" || words[0] == "LEAVE")
                //{
                //    words.RemoveAt(0);
                //    Drop(words);
                //}
            }
        }

        private void Commands()
        {
            Console.Write("\nAvailable commands:\n" +
                "[Move/Go] - Moves the player\n" +
                "[Take/Pickup] - Picks up items\n" +
                "[Look/Inspect] - Checks your environment\n" +
                "[Use] - Uses item in inventory\n" +
                "[Drop/Leave] - Drops item from inventory\n" +
                "\n[Commands/Help] - Displays this list");

            changedRoom = false;
        }

        private void Drop(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            // Om ACTION är TAKE eller PICK, matcha keyword med föremål i roomInventory
            // Ta bort föremål från roomInventory och lägg till i användarens inventory (playerInventory.Add(item)) 

            // Om items i currentRoom = 1, kolla endast primär keyword
            // Annars kolla även secondary

            CheckPrimaryKeyword(player.currPosition, words, ref tempItems);
            if (tempItems.Count == 1)
            {
                RoomList[player.currPosition].inventory.Add(tempItems[0]);
                player.inventory.Remove(tempItems[0]);
                Console.WriteLine($"You dropped the {words[0].ToLower()}");
            }
            else if (tempItems.Count > 1)
            {
                CheckSecondaryKeyword(player.currPosition, words, ref tempItems);
                player.inventory.Remove(tempItems[0]);
                RoomList[player.currPosition].inventory.Add(tempItems[0]);
                Console.WriteLine($"You dropped the {words[0].ToLower()}");
            }
            else
            {
                Console.WriteLine($"You don't have a {words[0].ToLower()} to drop");
            }

            changedRoom = false;
        }

        private void Use(List<string> words, int currPosition)
        {
            //for (int i = 0; i < length; i++)
            {
                // Jämför playerInventory med exits useWith
                for (int i = 0; i < words.Count; i++)
                {
                    for (int j = 0; j < player.inventory.Count; j++)
                    {
                        if(words[i] == player.inventory[j].Keyword)
                        {
                            
                        }
                    }
                }
            }

            changedRoom = false;
        }

        public bool Move(List<string> words)
        {
            // Kolla exitlista med currentPosition
            // Utifrån dem kollar man keywords från deras input och jämför med direction på exits.

            var exits = (from exit in exitList // Checks for available directions at their current position
                         where exit.cameFrom == player.currPosition
                         select exit).ToList();

            bool changeRoom = false;
            
            if(words.Count() == 0)
            {
                //Console.WriteLine("You can move in many directions");
                Console.Write("\nYou can move in the following direction(s):");
                foreach (var exit in exits)
                {
                   Console.Write($" [{exit.direction}]");
                }
                Console.Write("\n\n");
            }
            
            for (int i = 0; i < exits.Count(); i++)
            {
                for (int j = 0; j < words.Count(); j++)
                {
                    if (words[j].ToUpper() == exits[i].direction.ToUpper())
                    {
                        if (exits[i].locked == true)
                        {
                            Console.WriteLine("\nThe door is locked, you need a key to open it!\n");
                            lockedDoor = true;
                        } else 
                        {
                            player.currPosition = exits[i].leadsTo;
                            changeRoom = true;
                        }
                    }
                }
            }
            return changeRoom;
        }

        public void InventoryAdd(List<string> words, List<Item> tempItems)
        {
            player.inventory.Add(tempItems[0]);
            Console.WriteLine($"\nYou picked up the {words[0].ToCapital()}\n");
            changedRoom = false;
        }

        public void Take(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            // Om ACTION är TAKE eller PICK, matcha keyword med föremål i roomInventory
            // Ta bort föremål från roomInventory och lägg till i användarens inventory (playerInventory.Add(item)) 

            // Om items i currentRoom = 1, kolla endast primär keyword
            // Annars kolla även secondary
            
            CheckPrimaryKeyword(player.currPosition, words, ref tempItems);
            if (tempItems.Count == 1)
            {
                InventoryAdd(words, tempItems);
                RoomList[player.currPosition].inventory.Remove(tempItems[0]);
            } else if (tempItems.Count > 1)
            {
                CheckSecondaryKeyword(player.currPosition, words, ref tempItems);
                InventoryAdd(words, tempItems);
                RoomList[player.currPosition].inventory.Remove(tempItems[0]);
            } else
            {
                Console.WriteLine("There is no item like that to pick up");

            }
        }

        public void Look(List<string> words)
        {
            Console.Write(RoomList[player.currPosition].roomDescription);
            changedRoom = false;
        }
        
        public void CheckPrimaryKeyword(int roomId, List<string> words, ref List<Item> tempItems)
        {
            // Jämför input med keyword i tillgängligt item
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < RoomList[roomId].inventory.Count; j++)
                {
                    if (words[i].ToUpper() == RoomList[roomId].inventory[j].Keyword.ToUpper()) {
                         tempItems.Add(RoomList[roomId].inventory[j]);
                    } 
                }
            }
        }

        public void CheckSecondaryKeyword(int roomId, List<string> words, ref List<Item> tempItems)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < RoomList[roomId].inventory[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == RoomList[roomId].inventory[j].SecondaryKeyword[k].ToUpper())
                        {
                            tempItems.Clear();
                            tempItems.Add(RoomList[roomId].inventory[j]);
                        }
                    }
                }
            }
        }
    }
}
