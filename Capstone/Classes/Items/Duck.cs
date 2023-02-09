using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Inventory
{
    public class Duck : ItemInventory
    {
        public Duck(string location, decimal price, string type, string prodName) : base(location, price, type)
        {
            ProductName = prodName;
            Sound = "Quack Quack Splash!";

        }
    }
}
