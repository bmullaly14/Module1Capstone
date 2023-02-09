using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.Items
{
    public class Pony : ItemInventory
    {
        public Pony(string location, decimal price, string type, string prodName) : base(location, price, type)
        {
            ProductName = prodName;
            Sound = "Neigh Neigh YAY!";

        }
    }
}
