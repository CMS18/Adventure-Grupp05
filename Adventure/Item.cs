using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    abstract class Item
    {
        public string Name { get; set; }
        private string Description { get; set; }
        public int ID { get; set; }

        public Item(int roomID, string itemDescription,
            string itemName )
        {
            ID = roomID;
            Description = itemDescription;
            Name = itemName;
        }

        // Exit list
    }


    //class Key : Item
    //{

    //}
}
