using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class World
    {
        List<Room> RoomList = new List<Room>()
        {
            new Room(0, "Sitting Room", "This is the sitting room", false),
            new Room(0, "Kitchen", "This is the kitchen", false),
            new Room(1, "Basement", "A spooky basement", false),
            new Room(1, "Outside", "The outside", true)
        };
        


        //Rooms SittingRoom = new Rooms(1, "Sitting Room", "The living room is old and worn, with tattered drapes hanging on the walls. " +
        //        "To the north is a fireplace that looks like it hasn't been lit for a long time.");
        //Rooms Kitchen = new Rooms(2, "Kitchen", "This is a kitchen");
        //Rooms Basement = new Rooms(3, "Basement", "This is a basement");

        List<Item> PlayerInventory = new List<Item>()
        {
            
        };
    }
}
