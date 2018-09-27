using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Item
    {
        public string Name { get; private set; }
        private string Description { get; set; }
        public int ID { get; private set; }
        public bool Visible { get; set; }
        public string Keyword { get; private set; }
        public string[] SecondaryKeyword { get; set; }

        public Item(int itemID, string itemName, string itemDescription, string keyword, string[] secondaryKeyword, bool visible )
        {
            ID = itemID;
            Description = itemDescription;
            Name = itemName;
            Keyword = keyword;
            SecondaryKeyword = secondaryKeyword;
            Visible = visible;
        }

        public Item()
        {

        }

        // Exit list
    }


    //class Key : Item
    //{

    //}
}
