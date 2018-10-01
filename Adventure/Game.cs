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
        KeywordCheck keywordCheck = new KeywordCheck();
        List<Item> initPlayerInventory = new List<Item>();

        public void NewGame()
        {
            Console.Write("Welcome to Adventure!\nWhat is your name? ");
            player = new Player(Console.ReadLine(), initPlayerInventory, 1);
            Console.Write($"\nWelcome {player.playerName}!\n");

            //string str = "\nYou wake up in a small abandoned house. A sudden blinding light followed by a rumbling thunder startles you.\n" +
            //    "The lightning illuminates the walls around you, revealing the decaying paint and rotting wood lining the walls.";

            //StringOutputFormatter(str, 1, 2);
            Console.Write("\nYou find yourself in a small abandoned house. A sudden blinding light followed by a rumbling thunder startles\n" +
                "you from your slumber. The lightning illuminates the walls around you, revealing the decaying paint and rotting\n" +
                "wood lining the walls.\n");
        }
        #region Core
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
                    //StringOutputFormatter(RoomList[player.currPosition].roomDescription, 1, 2);

                } else if (lockedDoor == true)
                {
                    
                }
                
                Console.Write("> "); Parse(Console.ReadLine());
            }
            Console.WriteLine("\nCongratulations! You escaped and have won the game!\n");
        }

        public void StringFormatter(string str)
        {
            string lines = string.Join(Environment.NewLine, str.Split()
            .Select((word, index) => new { word, index })
            .GroupBy(x => x.index / 9)
            .Select(grp => string.Join(" ", grp.Select(x => x.word))));
        }

        public void StringOutputFormatter(string str, int nrOfNewLinesBefore, int nrOfNewLinesAfter)
        {
            const int textWidth = 200;

            string result = "";
            string lineBuild = "";

            for (int i = 0; i < nrOfNewLinesBefore; i++)
            {
                result += "\n";
            }

            string[] words = str.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                int prevLen = lineBuild.Length;
                lineBuild += words[i];
                if (lineBuild.Length > textWidth)
                {
                    lineBuild = lineBuild.Substring(0, prevLen);
                    i--;
                    result += lineBuild + "\n";
                    lineBuild = "";
                }
                else
                {
                    lineBuild += " ";
                }
            }

            for (int i = 0; i < nrOfNewLinesAfter; i++)
            {
                result += "\n";
            }

            // Output
            Console.WriteLine(result);
        }

        private void Parse(string input)
        {
            List<string> words = new List<string>();
            foreach (string word in input.Split(' '))
            {
                words.Add(word.ToUpper());
            }

            if (!actions.Contains(words[0])) 
            {
                Console.WriteLine("\nI don't understand what you mean, please rephrase\n"); //Do thing
                changedRoom = false;
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
                        Look(words);
                        break;
                    case "INSPECT":
                        words.RemoveAt(0);
                        Inspect(words);
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
        #endregion

        #region Actions
        private void Commands()
        {
            Console.Write("\nAvailable commands:\n\n" +
                "[Move/Go]\t Moves the player\n" +
                "[Take/Pickup]\t Picks up items\n" +
                "[Look/Inspect]\t Checks your environment\n" +
                "[Use]\t\t Uses item in inventory\n" +
                "[Drop/Leave]\t Drops item from inventory\n" +
                "\n[Commands/Help]\t Displays this list\n\n");

            changedRoom = false;
        }

        public bool Move(List<string> words)
        {
            bool changeRoom = false;
            List<Exit> availDir = new List<Exit>();
            // Kolla exitlista med currentPosition
            // Utifrån dem kollar man keywords från deras input och jämför med direction på exits.

            var exits = (from exit in exitList // Checks for available directions at their current position
                         where exit.cameFrom == player.currPosition
                         select exit).ToList();

            for (int i = 0; i < words.Count; i++) // Checks if the desired direction is an option
            {
                for (int j = 0; j < exits.Count; j++)
                {
                    if(words[i].ToUpper().Equals(exits[j].direction.ToUpper()))
                    {
                        availDir.Add(exits[j]);
                    }
                }
            }

            if(availDir.Count() == 0)
            {
                Console.WriteLine("\nYou can't go that way!\n");
                return false;
            }

            if (words.Count() == 0)
            {
                Console.Write("\nYou can move in the following direction(s):");
                foreach (var exit in exits)
                {
                    Console.Write($" [{exit.direction}]");
                }
                Console.Write("\n\n");
                return false;
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
                            return false;
                        }
                        else
                        {
                            player.currPosition = exits[i].leadsTo;
                            changeRoom = true;
                        }
                    }
                }
            }
            return changeRoom;
        }

        public void InventoryDrop(List<string> words)
        {
            player.inventory.Remove(player.inventory[0]);
            Console.WriteLine($"\nYou dropped the {words[0].ToLower()}\n");
        }

        private void Drop(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            // Om ACTION är TAKE eller PICK, matcha keyword med föremål i roomInventory
            // Ta bort föremål från roomInventory och lägg till i användarens inventory (playerInventory.Add(item)) 

            // Om items i currentRoom = 1, kolla endast primär keyword
            // Annars kolla även secondary

            CheckPrimaryKeyword(words, ref tempItems);
            if (player.inventory.Count == 1)
            {
                RoomList[player.currPosition].inventory.Add(player.inventory[0]);
                InventoryDrop(words);
            }
            else if (player.inventory.Count > 1)
            {
                CheckSecondaryKeywordDrop(player.currPosition, words, ref tempItems);
                InventoryDrop(words);
                RoomList[player.currPosition].inventory.Add(tempItems[0]);
            }
            else
            {
                Console.WriteLine($"You don't have a {words[0].ToLower()} to drop");
            }

            changedRoom = false;
        }

        private void Use(List<string> words)
        {
            if(words.Count() == 0)
            {
                Console.WriteLine("\nPlease specify what you want to use\n");
            }
            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].ToUpper().Equals("WITH") || words[i].ToUpper().Equals("ON"))
                {
                    words.RemoveAt(i);
                    UseWith(words);
                }
            }

            changedRoom = false;
        }

        public void Take(List<string> words)
        {
            changedRoom = false;
            if (words.Count == 0)
            {
                Console.WriteLine("\nWhat do you want to pick up?\n");
                return;
            }

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
            }
            else if (tempItems.Count > 1)
            {
                CheckSecondaryKeywordTake(player.currPosition, words, ref tempItems);
                InventoryAdd(words, tempItems);
                RoomList[player.currPosition].inventory.Remove(tempItems[0]);
            }
            else
            {
                Console.WriteLine("\nThere is no item like that to pick up\n");
            }
        }

        public void Look(List<string> words)
        {
            changedRoom = false;
            Console.Write($"\n{RoomList[player.currPosition].roomDescription} ");
            //StringOutputFormatter(RoomList[player.currPosition].roomDescription, 1, 2);
            if (RoomList[player.currPosition].inventory.Count() > 0)
            {
                Console.Write("\n\nItems in your vicinity\n\n");
                for (int i = 0; i < RoomList[player.currPosition].inventory.Count(); i++)
                {
                    Console.WriteLine($"[{i + 1}] { RoomList[player.currPosition].inventory[i].Name } ");
                }
            }
            Console.Write("\n");
        }

        private void Inspect(List<string> words)
        {
            List<Item> tempItems = new List<Item>();
            List<Item> secondaryList = new List<Item>();
            List<string> usedKeyword = new List<string>();
            changedRoom = false;


            if (words.Count > 0)
            {
                CheckPrimaryKeyword(words, ref tempItems);
                if (tempItems.Count == 0)
                {
                    Console.WriteLine("\nYou don't have an item like that\n");
                }
                else if (tempItems.Count() == 1)
                {
                    Console.WriteLine($"\n{ tempItems[0].Description }\n");
                }
                else if (tempItems.Count() > 1)
                {
                    CheckSecondaryKeyword(words, ref tempItems, ref secondaryList, ref usedKeyword);

                    if (secondaryList.Count() == 0)
                    {
                        Console.WriteLine($"\nYou have { tempItems.Count() } { tempItems[0].Keyword }s, which one do you want to look at?\n");
                        return;
                    }
                    else if (secondaryList.Count() == 1)
                    {
                        Console.WriteLine($"\n{ secondaryList[0].Description }\n");
                    }
                    else
                    {
                        Console.WriteLine($"\nYou have { secondaryList.Count() } { usedKeyword[0].ToString() } { tempItems[0].Keyword }s, which one do you want to look at?\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nWhat do you want to inspect?\n");
            }
        }
        #endregion

        #region Ancilliary
        private void UseWith(List<string> words)
        {
            bool valid_1 = false; // Checks what the player wants to do
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
                return;
            }

            if(valid_2)
            {
                var usable = (from item in player.inventory
                              where item.UsableWith >= 1
                              select item).ToList();

                for (int i = 0; i < usable.Count; i++)
                {
                    for (int j = i + 1; j < usable.Count; j++)
                    {
                        if (usable[i].UsableWith == usable[j].ID)
                        {
                            FuseItems(usable[j], usable[i]);
                        }
                    }
                }
            }

            
        }

        private void UnlockDoor()
        {
            var exits = (from exit in exitList // Checks for available directions at their current position
                         where exit.locked == true && exit.cameFrom == player.currPosition
                         select exit).ToList();

            bool foundExit = false;

            List<Item> invent;

            if(exits.Count() > 0)
            {
                foundExit = true;
            }
            
            if(player.inventory.Count > 0 && foundExit)
            {
                bool foundItem = false;
                invent = (from item in player.inventory
                         where item.ID.Equals(exits[0].unlockItem[0].ID)
                         select item).ToList();

                if (invent.Count() > 0)
                {
                    foundItem = true;
                } else
                {
                    Console.Write("\nYou don't have the right item to unlock this door\n\n");
                }

                
                if (exits.Count == 1 && foundItem)
                {
                    if (exits[0].unlockItem[0].Equals(invent[0]))
                    {
                        exits[0].locked = false;
                        Console.Write($"\nYou unlocked the { exits[0].exitName }!\n\n");
                    } else
                    {
                        Console.WriteLine("No items like that");
                    }
                }
            } else
            {
                Console.WriteLine("\nThere is nothing to unlock in here\n");
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
                Console.Write($"\nYou created a new item! You made a {toAdd[0].Name}!\n\n");
            }
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
        #endregion

        #region Keywords
        public void CheckPrimaryKeywordTake(int roomId, List<string> words, ref List<Item> tempItems)
        {
            // Jämför input med keyword i tillgängligt item
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < RoomList[roomId].inventory.Count; j++)
                {
                    if (words[i].ToUpper() == RoomList[roomId].inventory[j].Keyword.ToUpper())
                    {
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
        public void CheckPrimaryKeyword(List<string> words, ref List<Item> tempItems)
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
        public void CheckSecondaryKeyword(List<string> words, ref List<Item> tempItems, ref List<Item> secondaryList, ref List<string> usedKeyword)
        {
            for (int i = 0; i < words.Count; i++)
            {
                for (int j = 0; j < tempItems.Count; j++)
                {
                    for (int k = 0; k < tempItems[j].SecondaryKeyword.Length; k++)
                    {
                        if (words[i].ToUpper() == tempItems[j].SecondaryKeyword[k].ToUpper())
                        {
                            
                            secondaryList.Add(tempItems[j]);
                            usedKeyword.Add(words[i].ToLower());
                        }
                    }
                }
            }
        }
        #endregion
    }
}
