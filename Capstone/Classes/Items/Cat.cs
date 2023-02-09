using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Cat : ItemInventory
    {
     public Cat (string location, decimal price, string type, string prodName) :base(location, price, type)
        {
            ProductName = prodName;
            Sound = "Meow Meow Meow!";

        }
    }
}
