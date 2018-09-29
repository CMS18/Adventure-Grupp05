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
        List<string> actions = new List<string> { "INVENTORY", "MOVE", "TAKE", "PICKUP", "DROP", "LOOK", "OPEN", "CLOSE", "GO", "USE", "INSPECT", "PICK", "COMMANDS", "HELP" };
        List<string> items = new List<string> { "KEY" };
        List<string> directions = new List<string> { "NORTH", "EAST", "SOUTH", "WEST" };
        List<string> exits = new List<string> { "DOOR", "PAINTING", "WINDOW", "TUNNEL", "HOLE" };

        bool changedRoom = true;
        bool lockedDoor = false;

        Player player;
        World world = new World();
        List<Item> initPlayerInventory = new List<Item>();

        public void NewGame()
        {
            Console.Write("Welcome to Adventure!\nWhat is your name? ");
            player = new Player(Console.ReadLine(), initPlayerInventory, 1);
            Console.Write($"\nWelcome {player.playerName}!\n");

            Console.Write("\nYou wake up in a small abandoned house. A sudden blinding light followed by a rumbling thunder startles you.\n" +
                "The lightning illuminates the walls around you, revealing the decaying paint and rotting wood lining the walls.\n\n");
        }

        public void GameLoop()
        {
            bool alive = true;

            NewGame();
            while (alive && RoomList[player.currPosition].Win == false)
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
            }
            Console.Clear();
            Console.WriteLine("You escaped and have won the game!");
        }

        private void Parse(string input)
        {
            


            List<string> words = new List<string>();
            foreach (string word in input.Split(' '))
            {
                words.Add(word.ToUpper());
            }
            //Console.Write(words[0]);

            if (!actions.Contains(words[0])) 
            {
                Console.WriteLine("\nI don't understand what you mean, please rephrase\n"); //Do thing
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
                        words.RemoveAt(0);
                        Use(words);
                        break;
                    case "DROP":
                    case "LEAVE":
                        words.RemoveAt(0);
                        Drop(words);
                        break;
                    case "COMMANDS":
                    case "HELP":
                        Commands();
                        break;
                    case "INVENTORY":
                        Inventory();
                        break;
                }
            }
        }

        private void Commands()
        {
            Console.Write("\nAvailable commands:\n\n" +
                "[Move/Go] - Moves the player\n" +
                "[Take/Pickup] - Picks up items\n" +
                "[Look/Inspect] - Checks your environment\n" +
                "[Use] - Uses item in inventory\n" +
                "[Drop/Leave] - Drops item from inventory\n" +
                "\n[Commands/Help] - Displays this list\n\n");

            changedRoom = false;
        }

        private void Drop(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            // Om ACTION är TAKE eller PICK, matcha keyword med föremål i roomInventory
            // Ta bort föremål från roomInventory och lägg till i användarens inventory (playerInventory.Add(item)) 

            // Om items i currentRoom = 1, kolla endast primär keyword
            // Annars kolla även secondary

            CheckPrimaryKeywordDrop(words, ref tempItems);
            if (player.inventory.Count == 1)
            {
                RoomList[player.currPosition].inventory.Add(player.inventory[0]);
                player.inventory.Remove(player.inventory[0]);
                Console.WriteLine($"You dropped the {words[0].ToLower()}");
            }
            else if (player.inventory.Count > 1)
            {
                CheckSecondaryKeywordDrop(player.currPosition, words, ref tempItems);
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

        private void Use(List<string> words)
        {
            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].ToUpper().Equals("WITH"))
                {
                    words.RemoveAt(i);
                    UseWith(words);
                }
            }

            changedRoom = false;
        }

        private void UseWith(List<string> words)
        {
            bool valid_1 = false;
            bool valid_2 = false;
            for (int i = 0; i < words.Count; i++)
            {
                if(exits.Contains(words[i].ToUpper()))
                {
                    valid_1 = true;
                }

                if (items.Contains(words[i].ToUpper()))
                {
                    valid_2 = true;
                    
                }
               
            }

            if(valid_1 && valid_2)
            {
                UnlockDoor();
            }

            var usable = (from item in player.inventory
                          where item.UsableWith >= 1
                          select item).ToList();

            for (int i = 0; i < usable.Count; i++)
            {
                for (int j = i+1; j < usable.Count; j++)
                {
                    if(usable[i].UsableWith == usable[j].ID) {
                        FuseItems(usable[j], usable[i]);
                    }
                }
            }


        }

        private void UnlockDoor()
        {
            var exits = (from exit in exitList // Checks for available directions at their current position
                         where exit.locked == true && exit.cameFrom == player.currPosition
                         select exit).ToList();

            List<Item> invent;

            //var test = (from item in player.inventory
            //            where item.ID.Equals(exits[0].unlockItem[0].ID)
            //            select item).ToList();
                        
            if(player.inventory.Count > 0)
            {
                invent = /*(from item in player.inventory
                          where item.Equals(exits[0].unlockItem[0])
                          select item).ToList();*/
                        (from item in player.inventory
                         where item.ID.Equals(exits[0].unlockItem[0].ID)
                         select item).ToList();

                if (exits.Count == 1)
                {
                    if (exits[0].unlockItem[0].Equals(invent[0]))
                    {
                        exits[0].locked = false;
                        //for (int i = 0; i < exitList.Count; i++)
                        //{
                        //    if (exitList[i].unlockItem[0] == exits[0].unlockItem[0])
                        //    {
                        //        exitList[i].locked = false;
                        //    }
                        //}
                    } else
                    {
                        Console.WriteLine("No items like that");
                    }
                }
            } else
            {
                Console.WriteLine("TEST");
            }
        }

        private void FuseItems(Item partA, Item partB)
        {
            var toAdd = (from item in FuseResults
                            where item.ID == partA.FuseResult && item.ID == partB.FuseResult
                            select item).ToList();

            if (partA.FuseResult == partB.FuseResult)
            {
                player.inventory.Add(toAdd[0]);
                player.inventory.Remove(partA);
                player.inventory.Remove(partB);
            }
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
        public void Inventory()
        {
            var query = (from item in player.inventory
                         select item).ToList();
            Console.Write("\n\n[INVENTORY]:\n");
            for (int i=0; i < query.Count(); i++)
            {
                Console.WriteLine($"[{i + 1}] {query[i].Name}");
            }
            Console.WriteLine();
        }

        public void Take(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            // Om ACTION är TAKE eller PICK, matcha keyword med föremål i roomInventory
            // Ta bort föremål från roomInventory och lägg till i användarens inventory (playerInventory.Add(item)) 

            // Om items i currentRoom = 1, kolla endast primär keyword
            // Annars kolla även secondary
            
            CheckPrimaryKeywordTake(player.currPosition, words, ref tempItems);
            if (tempItems.Count == 1)
            {
                InventoryAdd(words, tempItems);
                RoomList[player.currPosition].inventory.Remove(tempItems[0]);
            } else if (tempItems.Count > 1)
            {
                CheckSecondaryKeywordTake(player.currPosition, words, ref tempItems);
                InventoryAdd(words, tempItems);
                RoomList[player.currPosition].inventory.Remove(tempItems[0]);
            } else
            {
                Console.WriteLine("There is no item like that to pick up");

            }
        }

        public void Look(List<string> words)
        {
            if(words.Contains("AT")) 
            {
                for (int i = 0; i < words.Count(); i++)
                {
                    for (int j = 0; j < exitList[player.currPosition].direction.Count(); j++)
                    {
                        if (words[i] == exitList[player.currPosition].direction)
                        {
                            Console.WriteLine("TEST TEST TEST");
                        }
                    }
                }
            }
            Console.Write($"\n{RoomList[player.currPosition].roomDescription} ");
            for (int i = 0; i < RoomList[player.currPosition].inventory.Count(); i++)
            {
                Console.Write($"{ RoomList[player.currPosition].inventory[i].Description } ");
            }
            Console.Write("\n\n");
            changedRoom = false;
        }
        
        public void CheckPrimaryKeywordTake(int roomId, List<string> words, ref List<Item> tempItems)
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
        public void CheckPrimaryKeywordDrop(List<string> words, ref List<Item> tempItems)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < player.inventory.Count; j++)
                {
                    if (words[i].ToUpper() == player.inventory[j].Keyword.ToUpper())
                    {
                        tempItems.Add(player.inventory[j]);
                    }
                }
            }
        }

        public void CheckSecondaryKeywordTake(int roomId, List<string> words, ref List<Item> tempItems)
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

        public void CheckSecondaryKeywordDrop(int roomId, List<string> words, ref List<Item> tempItems)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < player.inventory[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == player.inventory[j].SecondaryKeyword[k].ToUpper())
                        {
                            tempItems.RemoveAt(k);
                            tempItems.Add(RoomList[roomId].inventory[j]);
                        }
                    }
                }
            }
        }
    }
}
