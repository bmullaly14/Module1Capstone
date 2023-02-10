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
    public class CapstoneUnitTests
    {
        private static string mainProjectDirectory;
        private static string testSourceFile;
        private static string testDestFile;
        private static VendingMachine TestMachine;

        [TestInitialize]
        public void Init()
        {
            string currentDirectory = Environment.CurrentDirectory;
            testSourceFile = Path.GetFullPath("TestIn.txt");
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
        [TestMethod]
        public void GiveChangeTest()
        {

            Transaction testTransaction = new Transaction(TestMachine);
            testTransaction.Balance = 0.65M;

            string correct = "Your change is 2 quarters, 1 dimes, 1 nickels.";
            string actual = testTransaction.GiveChange();
            Assert.AreEqual(correct, actual);
        }

        [TestMethod]
        public void LogTransactionTest()
        {
            ResetTestFile("TestOut.txt");
            Transaction testTransaction = new Transaction(TestMachine, "TestOut.txt");
            testTransaction.Balance = 0.65M;
            testTransaction.GiveChange();

            string testLog = Path.GetFullPath("TestOut.txt");
            string actual = File.ReadAllText(testLog);
            string expected = $"GIVE CHANGE: $0.65 | BALANCE: $0.00";

            Assert.IsTrue(actual.Contains(expected));


        }
        private void ResetTestFile(string testFile)
        {
            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }
        }
    }
}
