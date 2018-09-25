using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Rooms
    {
        // Move genom att kolla på utgångar i rumet
        public int RoomID { get; private set; }
        public string RoomName { get; private set; }
        public string RoomDescription { get; private set; }

        public List<Item> RoomInventory { get; set; }

        public Rooms(int id, string name, string description )
        {
            RoomID = id;
            RoomName = name;
            RoomDescription = description;
        }

        //List<string> roomInventory = new List<string>;
        


    }
}
