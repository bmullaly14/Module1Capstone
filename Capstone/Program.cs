using System;
using System.Collections.Generic;
using System.IO;
using Capstone.Classes;
using Capstone.Classes.Items;
using System.Media;

namespace Capstone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string fileInventory = Path.GetFullPath(@"vendingmachine.csv");
            VendingMachine machine = new VendingMachine(fileInventory);
            Transaction transaction = new Transaction(machine);

            Console.WriteLine("Welcome to the CuteCo, inc. Vendo-Matic 800!");
        mainMenu:
            Console.WriteLine("\nMain Menu");
            Console.WriteLine(" (1) Display Vending Machine Items \n (2) Purchase \n (3) Exit");
            Console.WriteLine("Please make your selection");

            int userInput = -1;
            while (userInput <= 0 || userInput > 3)
            {
                bool parseWorked = int.TryParse(Console.ReadLine(), out userInput);
                if (userInput == 99)
                {
                    RollCredits();
                    userInput = -1;
                    goto mainMenu;
                    
                }
                else if (userInput <= 0 || userInput > 3|| !parseWorked)
                {
                    Console.WriteLine("Please try again!");
                }                
            }
            if(userInput == 1)
            {
                Console.WriteLine(machine.Display());
                goto mainMenu;
            } else if (userInput == 2)
            {
                transaction.Display();
                goto mainMenu;
                
            } else { Console.WriteLine("Thanks for using CuteCo, inc. Vendo-Matic 800! \nPlease come back soon!"); }
        }
        private static void RollCredits()
        {
            Console.WriteLine("*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*\n");
            Console.WriteLine("This project was created as a Mini Capstone Project for Tech Elevator C#/.NET Cohort 22");
            Console.WriteLine("**** Brought to you by ****");
            Console.WriteLine("Dustan Byler  **  Darya Taraban");
            Console.WriteLine("Ben Mullaly  **  Seth Barnett");
            Console.WriteLine("\n*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*");

            
        }
    }
}
