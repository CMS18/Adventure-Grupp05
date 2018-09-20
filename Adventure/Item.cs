using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    abstract class Item
    {
        public string ItemName()
        {
            string name = null;
            return name;
        }

        private string Description()
        {
            return "";
        }
        // Exit list
    }


    class Key : Item
    {

    }
}
