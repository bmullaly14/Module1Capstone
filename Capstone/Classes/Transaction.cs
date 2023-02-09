using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Transactions;

namespace Capstone.Classes
{
    public class Transaction : IDisplayable
    {
       
        public decimal Balance { get; private set; }
        Machine CurrentMachine { get; }
        public Transaction(Machine machine)
        {
            CurrentMachine= machine;
        }

        public void FeedMoney()
        {
            Console.WriteLine($"Current balance is {Balance:C2}");
            Console.WriteLine($"Please insert money in whole dollar amounts\n Enter \"stop\" when finished");
            bool isFeeding = true;
            decimal feedAmount = 0;
            while (isFeeding)
            { 
                    isFeeding = decimal.TryParse(Console.ReadLine(), out feedAmount);
                    if (isFeeding)
                    {
                        Balance += feedAmount;
                        Console.WriteLine($"\nCurrent money provided: {Balance:C2}\n");
                    }
              
            }
        }
        public void SelectProduct()
        {
            //show list avialable products (machine.Display?)
            //allow entering of product code - if doesn't exist, tell customer and return to purchase menu
            //if sold out, tell customer and return to purchase menu
            //if they select a valid product, dispense the item (print product name, cost, money remaining, animal sound
            //after dispensing, machine updates its inventory and returns customer to purchase menu
            Console.WriteLine("Please enter the code for the item you want.");
            string purchaseSelection = Console.ReadLine();
            while (!CurrentMachine.Inventory.ContainsKey(purchaseSelection) || CurrentMachine.Inventory[purchaseSelection].numOfItems <= 0)
            {
                Console.WriteLine("Invalid selection. Please try again.");
                purchaseSelection = Console.ReadLine();
            }
            if(Balance < CurrentMachine.Inventory[purchaseSelection].Price)
            {
                Console.WriteLine("Insufficient Funds!");
                return;
            }
          Dispense(purchaseSelection);
          
        }
        public void Dispense(string selection)
        {
            Console.WriteLine($"{CurrentMachine.Inventory[selection].ProductName}, {CurrentMachine.Inventory[selection].Price}, {CurrentMachine.Inventory[selection].Sound}");
            Balance -= CurrentMachine.Inventory[selection].Price;
            CurrentMachine.Inventory[selection].numOfItems -= 1;
            Console.WriteLine($"Your balance is {Balance}");
        }
        public void Display()
        {
transactionMenu:
            Console.WriteLine($"****< Purchase Menu >****");
            Console.WriteLine($"Current money provided: {Balance:C2}");
            Console.WriteLine();
            Console.WriteLine($" (1) Feed Money \n (2) Select Product\n (3) Finish Transaction");
            Console.WriteLine("Please make your selection");
            Console.WriteLine();


            int userInput = -1;
            while (userInput <= 0 || userInput > 3)
            {
                bool parseWorked = int.TryParse(Console.ReadLine(), out userInput);
                if (userInput <= 0 || userInput > 3 || !parseWorked)
                {
                    Console.WriteLine("Please try again!");
                }
            }
            if (userInput == 1)
            {
                FeedMoney();
                goto transactionMenu;
            }
            else if (userInput == 2)
            {
                CurrentMachine.Display();
                SelectProduct();
                goto transactionMenu;
            }
            else
            { //this will print receipt and finish transaction
            }
            }
    
    }
}
