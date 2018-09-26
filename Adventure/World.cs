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
            new Room(1, "Sitting Room", "This is the sitting room", false),
            new Room(2, "Kitchen", "This is the kitchen", false),
            new Room(3, "Outside", "The outside", true)
            //new Room(4, "Basement", "A spooky basement", false),
        };

        public List<Exit> exitList = new List<Exit>()
        {
            new Exit("Wooden Door", 1, 2, "A thick wooden door", false, "West"),
            new Exit("Wooden Door", 2, 1, "A thick wooden door", false, "East"),
            new Exit("Metal Door", 3, 2, "A heavy metal door with a grating", true, "East"),
            new Exit("Metal Door", 2, 3, "A heavy metal door with a grating", true, "West")
        };
        

        List<Item> room1Inventory = new List<Item>()
        {
            new Item(1, "Golden Key", "A heavy gold key inlaid with small jewels", true),
            new Item(2, "Shortsword", "A worn shortsword", false)
        };
        

        

        //Rooms SittingRoom = new Rooms(1, "Sitting Room", "The living room is old and worn, with tattered drapes hanging on the walls. " +
        //        "To the north is a fireplace that looks like it hasn't been lit for a long time.");
        //Rooms Kitchen = new Rooms(2, "Kitchen", "This is a kitchen");
        //Rooms Basement = new Rooms(3, "Basement", "This is a basement");

        public List<Item> PlayerInventory = new List<Item>()
        {
            
        };
    }
}
