using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Item
    {
        public string Name { get; set; }
        private string Description { get; set; }
        public int ID { get; set; }
        public bool Visible { get; set; }

        public Item(int itemID, string itemName, string itemDescription, bool visible )
        {
            ID = itemID;
            Description = itemDescription;
            Name = itemName;
            Visible = visible;
        }

        // Exit list
    }


    //class Key : Item
    //{

    //}
}
