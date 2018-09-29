using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class World
    {
        public List<Room> RoomList = new List<Room>()
        {
            new Room(0, "Sitting Room", "You are in the sitting room.", false, new List<Item>(), false),
            new Room(1, "Kitchen", "Around you are various pots and pans that look like they haven't been used in years. \nDrawers have been pulled" +
                "out around you and their contents now scatter the floor around you." +
                "\n\nTo your west you see a sturdy wooden door and to the east there's a heavy metal with a large padlock on it", false, new List<Item>(), false),
            new Room(2, "Outside", "The outside", true, new List<Item>(), true)
            //new Room(4, "Basement", "A spooky basement", false),
        };

        public List<Exit> exitList = new List<Exit>()
        {
            new Exit("Wooden Door", 0, 1, "a thick wooden door", false, "West", new List<Item>()),
            new Exit("Wooden Door", 1, 0, "a thick wooden door", false, "East", new List<Item>()),
            new Exit("Metal Door", 2, 1, "a heavy metal door with a grating", true, "East", new List<Item>()),
            new Exit("Metal Door", 1, 2, "a heavy metal door with a grating", true, "West", new List<Item>())
        };

        public List<Item> room_1_Inventory = new List<Item>()
        {
            new Item(4, "Bottom piece of a key", "The bottom half of a broken key", "key", new string[] {"bottom", "half", "broken", "piece" }, true, 3, 5)
            //new Item(1, "Golden Key", "A heavy gold key inlaid with small jewels", "key", new string[] {"golden", "gold"}, true, 0),

        };

        public List<Item> room_2_Inventory = new List<Item>()
        {
            new Item(3, "Top piece of a key", "The top half of a broken key", "key", new string[] {"top", "half", "broken", "piece" }, true, 4, 5),
            new Item(2, "Shortsword", "A worn shortsword", "sword", new string[] { "short", "blade" }, false, 0, 0),
        };

        public List<Item> room_3_Inventory = new List<Item>()
        {

        };

        public List<Item> FuseResults = new List<Item>() // Items created by combining items
        {
            new Item(5, "Golden Key", "A heavy gold key inlaid with small jewels", "key", new string[] {"golden", "gold"}, true, 0, 0)
        };


        public World()
        {
            RoomList.ElementAt(0).inventory = room_1_Inventory;
            RoomList.ElementAt(1).inventory = room_2_Inventory;
            RoomList.ElementAt(2).inventory = room_3_Inventory;

            exitList[2].unlockItem.Add(FuseResults[0]); 
        }
    }
}
