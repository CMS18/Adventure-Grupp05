using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class World
    {
        
    }

    class Room
    {
        //string name;
        //int roomId;

        //public Room(string name, int roomId)
        //{
        //    this.name = name;
        //    this.roomId = roomId;
        //    Description();
        //}

        Rooms room1 = new Rooms(1, "Room 1", "This is the first room");

        public void Look()
        {
            Console.WriteLine(Description());
        }

        private string Description()
        {
            return $"This is {room1.RoomName} with Room ID: {room1.RoomID}";
        }
    }

    class Connection
    {
        private Connection(int idFrom, int idTo)
        {
            List<Item> Connections {  }
        }
    }
}
