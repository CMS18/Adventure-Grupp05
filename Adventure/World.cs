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
            new Room(0, "Sitting Room", "This is the sitting room", false, new List<Item>()),
            new Room(1, "Kitchen", "This is the kitchen", false, new List<Item>()),
            new Room(2, "Outside", "The outside", true, new List<Item>())
            //new Room(4, "Basement", "A spooky basement", false),
        };

        public List<Exit> exitList = new List<Exit>()
        {
            new Exit("Wooden Door", 0, 1, "A thick wooden door", false, "West", new List<Item>()),
            new Exit("Wooden Door", 1, 0, "A thick wooden door", false, "East", new List<Item>()),
            new Exit("Metal Door", 2, 1, "A heavy metal door with a grating", true, "East", new List<Item>()),
            new Exit("Metal Door", 1, 2, "A heavy metal door with a grating", true, "West", new List<Item>())
        };

        public List<Item> room_1_Inventory = new List<Item>()
        {
            new Item(1, "Golden Key", "A heavy gold key inlaid with small jewels", "key", new string[] {"golden", "gold"}, true),
            new Item(2, "Shortsword", "A worn shortsword", "sword", new string[] { "short" }, false)
        };

        public List<Item> room_2_Inventory = new List<Item>()
        {

        };

        public List<Item> room_3_Inventory = new List<Item>()
        {

        };


        public World()
        {
            RoomList.ElementAt(0).inventory = room_1_Inventory;
            RoomList.ElementAt(1).inventory = room_2_Inventory;
            RoomList.ElementAt(2).inventory = room_3_Inventory;

            exitList[2].unlockItem.Add(room_1_Inventory[0]);
        }
        

        //Rooms SittingRoom = new Rooms(1, "Sitting Room", "The living room is old and worn, with tattered drapes hanging on the walls. " +
        //        "To the north is a fireplace that looks like it hasn't been lit for a long time.");
        //Rooms Kitchen = new Rooms(2, "Kitchen", "This is a kitchen");
        //Rooms Basement = new Rooms(3, "Basement", "This is a basement");
    }
}
