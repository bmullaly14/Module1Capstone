using Capstone.Classes.Inventory;
using Capstone.Classes.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Capstone.Classes
{
    public class Machine
    {
        public Dictionary<string, ItemInventory> Inventory { get; set; } = new Dictionary<string, ItemInventory>();

        public Machine (string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] lineArr = line.Split("|");
                        
                        if (lineArr[3] == "Duck")
                        {
                            Inventory[lineArr[0]] = new Duck(lineArr[0], decimal.Parse(lineArr[2]), lineArr[3], lineArr[1]);
                        } else if (lineArr[3] == "Cat")
                        {
                            Inventory[lineArr[0]] = new Cat(lineArr[0], decimal.Parse(lineArr[2]), lineArr[3], lineArr[1]);
                        } else if (lineArr[3] == "Pony")
                        {
                            Inventory[lineArr[0]] = new Pony(lineArr[0], decimal.Parse(lineArr[2]), lineArr[3], lineArr[1]);
                        } else if (lineArr[3] == "Penguin")
                        {
                            Inventory[lineArr[0]] = new Penguin(lineArr[0], decimal.Parse(lineArr[2]), lineArr[3], lineArr[1]);
                        } else { throw new Exception("Not a valid type!"); }

                    }
                }
            } catch (IOException ex)
            {
                Console.WriteLine("Our Machine is down... please come back later!");
            }
            
        }

        public void Display()
        {
            foreach(KeyValuePair <string, ItemInventory > item in Inventory) 
            { 
            Console.WriteLine($"{item.Key} | {item.Value.ProductName} | {item.Value.Price:C2} | x {item.Value.numOfItems}");
            }
        }
    }
}
