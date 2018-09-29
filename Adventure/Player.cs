using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Player
    {
        public int currPosition { get; set; }
        public string playerName { get; set; }
        public List<Item> inventory;

        public Player(string name, List<Item> playerInventory, int currentPosition)
        {
            playerName = name;
            this.inventory = playerInventory;
            currPosition = currentPosition;
        }        
    }
}
