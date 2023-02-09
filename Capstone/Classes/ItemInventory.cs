using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public abstract class ItemInventory
    {
        public string Location { get; }
        public decimal Price { get; }
        public string Type { get; }
        public int numOfItems { get; set; }
        public string Sound { get; set; }
        public string ProductName { get; set; }

        public ItemInventory(string location, decimal price, string type) 
        {
            Location = location;
            Price = price;
            Type = type;
            numOfItems = 5;
        }

    }
}
