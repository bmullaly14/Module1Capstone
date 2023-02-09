using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Transactions;

namespace Capstone.Classes
{
    public class Transaction
    {
       
        public decimal Balance { get; private set; }
        
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
                SelectProduct();

            }
            else
            { //this will print receipt and finish transaction
            }
            }
    
    }
}
