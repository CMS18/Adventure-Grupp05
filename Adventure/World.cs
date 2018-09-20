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
        string name;
        int roomId;

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
            return $"This is {name} with Room ID: {roomId}";
        }
    }

    class Connection
    {
        private Connection(int idFrom, int idTo)
        {
            List<> 
        }
    }
}
