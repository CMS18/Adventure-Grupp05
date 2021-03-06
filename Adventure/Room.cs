﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Room
    {
        public bool endPoint = false;

        // Move genom att kolla på utgångar i rumet
        public int roomRow { get; set; }
        public int roomCol { get; set; }
        public int roomId { get; set; }
        public string roomName { get; set; }
        public string roomDescription { get; set; }
        public List<Item> inventory { get; set; }
        public bool Win { get; set; }

        public Room(int id, string name, string description, bool endPoint, List<Item> roomInventory, bool win)
        {
            roomId = id;
            roomName = name;
            roomDescription = description;
            this.endPoint = endPoint;
            this.inventory = roomInventory;
            Win = win;
        }
    }
}
