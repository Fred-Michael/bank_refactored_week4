using System;
using BankClassLibraries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankTestCases
{
    [TestClass]
    public class TestCases
    {
        [TestMethod]
        public void TestForMakingDeposit()
        {
            //Arrange
            //Create a new account and then make a deposit on the newly created account.
            var firstCurrentAccnt = new Accounts("1", 1, 2000);
            firstCurrentAccnt.MakeDeposit(firstCurrentAccnt.AccountNumber, 500, "shopping POS debit", (AccountType)1);

            decimal Expected = 2500;

            //Act
            decimal Actual = firstCurrentAccnt.Balance;

            //Assert
            Assert.AreEqual(Expected, Actual);
        }

        [TestMethod]
        public void ThrowExceptionForMakingWrongDeposit()
        {
            //Arrange
            string ownerId = "7";
            int acctType = 0;
            decimal initialDeposit = 1000;

            //Act
            var newAccount = new Accounts(ownerId, acctType, initialDeposit);

            //Assert
            //Minimum deposit that can be made on any account should be from 100 upwards.
            //A lesser deposit amounts to an exception error as indicated in the test below
            Assert.ThrowsException<InvalidOperationException>(() => newAccount.MakeDeposit(newAccount.AccountNumber, 50, "small deposit", (AccountType)acctType));
        }

        [TestMethod]
        public void TestForMakingWithdrawals()
        {
            //Arrange
            //Create a new account and make a withdrawal on the account
            var firstSavingsAccnt = new Accounts("2", 1, 8000);
            firstSavingsAccnt.MakeWithdrawal(firstSavingsAccnt.AccountNumber, 1900, "shopping POS debit", (AccountType)1);

            decimal expected = 6100;

            //Act
            decimal actual = firstSavingsAccnt.Balance;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThrowExceptionForMakingWrongWithdrawals()
        {
            //Arrange
            string ownerId = "12";
            int acctType = 1;
            decimal initialDeposit = 3500;

            //Act
            var firstSavingsAccnt = new Accounts(ownerId, acctType, initialDeposit);

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => firstSavingsAccnt.MakeWithdrawal(firstSavingsAccnt.AccountNumber, 5000, "shopping POS debit", (AccountType)acctType));
        }

        [TestMethod]
        public void TestForMakingTransfers()
        {
            //Arrange
            //Create two customer accounts and make a transfer from the first customer to the second
            var firstCustomerAccnt = new Accounts("1", 0, 45000);
            var secondCustomerAccnt = new Accounts("2", 1, 1000);
            firstCustomerAccnt.MakeTransfer(firstCustomerAccnt.AccountNumber, secondCustomerAccnt.AccountNumber, 2000, "refund", (AccountType)0);

            //Expected value that is meant to be on the receiver account after a successful transfer
            decimal ExpectedsecondCustomerBalance = 3000;

            //Act
            decimal ActualSecondCustomerBalance = secondCustomerAccnt.Balance;

            //Assert
            Assert.AreEqual(ExpectedsecondCustomerBalance, ActualSecondCustomerBalance);
        }

        [TestMethod]
        public void ThrowExceptionForWrongTransfers()
        {
            //Arrange
            //Set paramaters for instantiating the sender and receiver accounts
            string senderId = "1";
            int senderAcctType = 1;
            decimal SenderAmount = 5000;

            string receiverId = "1";
            int receiverAcctType = 1;
            decimal receiverAmount = 2000;

            //Act
            var firstCustomerAccnt = new Accounts(senderId, senderAcctType, SenderAmount);
            var secondCustomerAccnt = new Accounts(receiverId, receiverAcctType, receiverAmount);

            //Assert
            //For a transfer to fail, it means that the money to be withdrawn from the sender's account is more than his/her current balance,
            //OR that a zero/negative withdrawal was attempted on the sender's account
            Assert.ThrowsException<InvalidOperationException>(() => firstCustomerAccnt.MakeWithdrawal(firstCustomerAccnt.AccountNumber, 7500, "refund", (AccountType)senderAcctType));
        }

        [TestMethod]
        public void TestForTransactions()
        {
            //Arrange
            string senderId = "4";
            int senderAcctType = 1;
            decimal initialDeposit = 10000;

            //Act
            //Create a new account. The account creation makes an intial deposit of the amount passed
            //which accounts for a transaction. Subsequent transactions on the account are also stored
            var firstCustomerAccnt = new Accounts(senderId, senderAcctType, initialDeposit);
            firstCustomerAccnt.MakeWithdrawal(firstCustomerAccnt.AccountNumber, 9000, "monthly savings", (AccountType)senderAcctType);
            firstCustomerAccnt.MakeDeposit(firstCustomerAccnt.AccountNumber, 3000, "second monthly savings", (AccountType)senderAcctType);

            var actual = firstCustomerAccnt.AccountTransactions.Count;
            //Assert
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void ThrowExceptionForWrongTransactions()
        {
            //Arrange
            string senderId = "4";
            int senderAcctType = 1;
            decimal initialDeposit = 10000;

            //Act
            //When a new account is created, a transaction automatically takes place because the customer
            //has to make an initial deposit (transaction) on his/her account
            var firstCustomerAccnt = new Accounts(senderId, senderAcctType, initialDeposit);

            //Assert
            //For a transaction to fail, it means the customer violated a deposit, transfer or withdrawal rule on his/her bank account.
            //Here, amount to withdraw is greater than initial deposit on the account (20,000 > 10,000)
            Assert.ThrowsException<InvalidOperationException>(() => firstCustomerAccnt.MakeWithdrawal(firstCustomerAccnt.AccountNumber, 20000, "monthly savings", (AccountType)senderAcctType));
        }

        [TestMethod]
        public void ValidLogin()
        {
            //Arrange
            string name = "zord";
            string email = "zord@popmail.com";
            var customer = new Customers(name, email);

            var expectedCustomerName = customer.customerName;

            //Act
            var actualCustomerName = BankData.Customers[0].customerName;

            //Assert
            Assert.AreEqual(expectedCustomerName, actualCustomerName);
        }

        [TestMethod]
        public void ThrowExceptionForWrongLogin()
        {
            //Arrange
            //Create a new account and make a withdrawal on the account
            var firstSavingsAccnt = new Accounts("2", 0, 2000);

            //Act

            //Assert
        }

        [TestMethod]
        public void ValidRegistration()
        {
            //Arrange
            BankData.Customers.Clear();
            string name = "mam";
            string email = "mam@example.com";
            var customer = new Customers(name, email);

            var expectedCustomerName = customer.customerName;

            //Act
            //This will get the actual name that is in the Customer database
            var actualCustomerName = BankData.Customers[0].customerName;

            //Assert
            //Comparison is done between the name that the user enters and the name that was gotten from the database
            Assert.AreEqual(expectedCustomerName, actualCustomerName);
        }

        [TestMethod]
        public void ThrowExceptionForInvalidRegistration()
        {
            //Arrange
            string name = "";
            string email = "";

            //Act and Assert
            //An exception is thrown when a wrong name and / or email parameter is passed during registration
            Assert.ThrowsException<InvalidOperationException>(() => new Customers(name, email));
        }
    }
}
