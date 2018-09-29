using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Exit
    {
        public string exitName { get; set; }
        public int leadsTo { get; set; }
        public int cameFrom { get; set; }
        public string exitDescription { get; set; }
        public bool locked { get; set; }
        public string direction { get; set; }
        public List<Item> unlockItem { get; set; }

        public Exit(string name, int leads, int came, string description, bool locked, string direction, List<Item> unlock)
        {
            exitName = name;
            leadsTo = leads;
            cameFrom = came;
            exitDescription = description;
            this.locked = locked;
            this.direction = direction;
            unlockItem = unlock;
        }
    }
}
