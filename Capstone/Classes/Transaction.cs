using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Transactions;
using System.Media;

namespace Capstone.Classes
{
    public class Transaction : IDisplayable
    {
        
        public decimal Balance { get; set; }
        VendingMachine CurrentMachine { get; }
        public Transaction(VendingMachine machine)
        {
            CurrentMachine = machine;
        }
        public Transaction(VendingMachine machine, string logFile)
        {
            CurrentMachine = machine;
            LogFile = logFile;
        }
        private string LogFile { get; } = "Log.txt";

        public void FeedMoney()
        {                       
            bool isFeeding = true;
            decimal feedAmount = 0;
            while (isFeeding)
            {
                Console.WriteLine($"Current balance is {Balance:C2}");
                Console.WriteLine($"Please insert money in whole dollar amounts\n");
                isFeeding = decimal.TryParse(Console.ReadLine(), out feedAmount);
                if (isFeeding && feedAmount > 0)
                {
                    Balance += feedAmount;
                    Console.WriteLine($"\nCurrent money provided: {Balance:C2}\n");

                    LogTransaction($"{DateTime.Now} FEED MONEY: {feedAmount:C2} | BALANCE: {Balance:C2}", LogFile);

                    Console.WriteLine($"\nEnter more money? Y/N?\n");

                    if(Console.ReadLine().ToUpper() != "Y")
                    {
                        isFeeding = false;
                    }

                } else { Console.WriteLine("\n <Vendo-Matic 800> : If you don't want to give us your money, then pick something else!"); return; }

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
            string purchaseSelection = Console.ReadLine().ToUpper();
            while (!CurrentMachine.Inventory.ContainsKey(purchaseSelection) || CurrentMachine.Inventory[purchaseSelection].numOfItems <= 0)
            {
                Console.WriteLine("Invalid selection. Please try again.");
                purchaseSelection = Console.ReadLine().ToUpper();
            }
            if (Balance < CurrentMachine.Inventory[purchaseSelection].Price)
            {
                Console.WriteLine("Insufficient Funds!");
                return;
            }
            Dispense(purchaseSelection);

        }
        public void Dispense(string selection)
        {
            string soundPath = Path.GetFullPath("tada.wav");
            SoundPlayer dispenseSound = new SoundPlayer(soundPath);


            Console.WriteLine($"\n{CurrentMachine.Inventory[selection].ProductName}, {CurrentMachine.Inventory[selection].Price}, {CurrentMachine.Inventory[selection].Sound}");
            dispenseSound.PlaySync();
            Balance -= CurrentMachine.Inventory[selection].Price;
            CurrentMachine.Inventory[selection].numOfItems -= 1;
            Console.WriteLine($"Your balance is {Balance}");

            LogTransaction($"{DateTime.Now} {CurrentMachine.Inventory[selection].ProductName} {selection}: {CurrentMachine.Inventory[selection].Price:C2} | BALANCE: {Balance:C2}", LogFile);
        }
        public void Display()
        {
        transactionMenu:
            Console.WriteLine($"\n****< Purchase Menu >****");
            Console.WriteLine($"Current money provided: {Balance:C2}");
            Console.WriteLine();
            Console.WriteLine($" (1) Feed Money \n (2) Select Product\n (3) Finish Transaction");
            Console.WriteLine("Please enter 1, 2, or 3");
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
                Console.WriteLine(CurrentMachine.Display());
                SelectProduct();
                goto transactionMenu;
            }
            else //option 3, Exit Transaction
            {

                Console.WriteLine($"Your remaining balance is {Balance:C2}.");
                Console.WriteLine(GiveChange());

            }
        }

        public string GiveChange()
        {
            int numQuarters = 0;
            int numDimes = 0;
            int numNickels = 0;
            decimal changeBalance = Balance;

            numQuarters = (int)(Balance / 0.25M);
            decimal remainder = Balance % 0.25M;
            numDimes = (int)(remainder / 0.10M);
            remainder = remainder % 0.10M;
            numNickels = (int)(remainder / 0.05M);
            remainder = remainder % 0.05M;

            Balance = 0;
            LogTransaction($"{DateTime.Now} GIVE CHANGE: {changeBalance:C2} | BALANCE: {Balance:C2}", LogFile);

           return ($"Your change is {numQuarters} quarters, {numDimes} dimes, {numNickels} nickels.");
           
        }


        public void LogTransaction(string logLine, string file)
        {
            string logFilename = Path.GetFullPath($@"{file}");
            try
            {
                using(StreamWriter sw = new StreamWriter(logFilename, true))
                {
                    sw.WriteLine(logLine);
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine("File write error occurred. Please contact service 1-800-867-5309");
            }
        }

        

        
    }
    
    
}
