using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection.PortableExecutable;
using Capstone;
using Capstone.Classes;
using System.Collections.Generic;
using Capstone.Classes.Inventory;

namespace CapstoneTests 
{
    [TestClass]
    public class UnitTest1
    {
        private static string mainProjectDirectory;
        private static string testSourceFile;
        private static string testDestFile;
        private static VendingMachine TestMachine;

        [TestInitialize]
        public void Init()
        {
            string currentDirectory = Environment.CurrentDirectory;
            testSourceFile = @"C:\Users\Student\workspace\Partner Projects\c-sharp-minicapstonemodule1-team1\CapstoneTests\bin\Debug\netcoreapp3.1\TestIn.txt";
            string testOutFile = Path.GetFullPath(@"TestOut.txt");
            TestMachine = new VendingMachine(testSourceFile);
        }
        [TestMethod]
        public void Make_Machine_with_inventory_test()
        {
            
            Dictionary<string, ItemInventory> testDict = new Dictionary<string, ItemInventory>();
            testDict["G1"] = new Duck("G1", 5.05m, "Duck", "Quacky Duck");
            Assert.ReferenceEquals(testDict["G1"], TestMachine.Inventory["G1"]);
        }
        [TestMethod]
        public void Check_Display_Method()
        {
            string actual = TestMachine.Display();
            string correct = "\n*Current Inventory*\nG1 | Quacky Duck | $5.05 | x 5\n";

            Assert.AreEqual(correct, actual);
        }

        private void RunProgram()
        {
            using (var reader = new StringReader(testSourceFile))
            {
                Console.SetIn(reader);
                Capstone.Program.Main(null);
            }
        }

            
    }
}
