using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class World
    {
        List<Rooms> RoomList = new List<Rooms>()
        {
            new Rooms(new Coord(0,0), "Sitting Room", "This is the sitting room"),
            new Rooms(new Coord(0,0), "Kitchen", "This is the kitchen"),
            new Rooms(3, "Basement", "A spooky basement")
        };
        
        //Rooms SittingRoom = new Rooms(1, "Sitting Room", "The living room is old and worn, with tattered drapes hanging on the walls. " +
        //        "To the north is a fireplace that looks like it hasn't been lit for a long time.");
        //Rooms Kitchen = new Rooms(2, "Kitchen", "This is a kitchen");
        //Rooms Basement = new Rooms(3, "Basement", "This is a basement");
    }

    //class Coord (int row, col)
    class Connection
    {
        int[] exitA = new int[2];

        int[] exitB = new int[2];

        public Connection()
        {
            
        }
    }

    class Room
    {
        string name;
        int roomId;
        Connection[] 

        public Room(string name, int roomId)
        {
            this.name = name;
            this.roomId = roomId;
            Description();
        }

        public void Look()
        {
            Console.WriteLine(Description());
        }

        private string Description()
        {
            throw new NotImplementedException();
        }
    }

    class Connection
    {
        private Connection(int idFrom, int idTo)
        {
            List<Item> connections = new List<Item>();
        }
    }
}
