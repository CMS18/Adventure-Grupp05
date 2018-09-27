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

        //List<items> inventory = new List<items>;
        //var currPosition = 0;

        public void PickupItem()
        {
            
        }

        public void Inventory()
        {
            // Show inventory
            //List<string> PlayerInventory = new List<string> { "Key", "Sword" };
        }

        public void Use()
        {
            // Use item (e.g. use key on door)
        }

        
    }
}
