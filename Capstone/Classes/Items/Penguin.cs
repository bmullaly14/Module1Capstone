using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Penguin : ItemInventory
    {
        public Penguin(string location, decimal price, string type, string prodName) : base(location, price, type)
        {
            ProductName = prodName;
            Sound = "Squawk Squawk Whee!";

        }
    }
}
