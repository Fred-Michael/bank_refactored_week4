using BankClassLibraries;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankClassLibraries
{
    public class Accounts
    {
        private int accountNumberGenerator = 100001;
        public AccountType AccountType { get; private set; }
        public string AccountNumber { get; private set; }
        public DateTime AccountDateCreation { get; set; }
        public string AccountOwnerID { get; set; }

        //getting the balance of this particular account from the BankData store
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in BankData.Transactions)
                {
                    if (item.AccountNumber == AccountNumber)
                    {
                        balance += item.Amount;
                    }
                }
                return balance;
            }
        }

        //List of transactions done on the instance of this account
        public List<Transactions> AccountTransactions
        {
            get
            {
                var accTransaction = new List<Transactions>();
                foreach (var item in BankData.Transactions)
                {
                    if (item.AccountNumber == AccountNumber)
                    {
                        accTransaction.Add(item);
                    }
                }
                return accTransaction;
            }
        }

        //constructor of the account object
        public Accounts(string ownerID, int typeOfAccount, decimal firstDeposit)
        {
            AccountNumber = (accountNumberGenerator + BankData.Accounts.Count + 1).ToString();
            AccountOwnerID = ownerID;
            AccountType = (AccountType)typeOfAccount;
            AccountDateCreation = DateTime.Now;
            var newAccount = new Accounts(AccountNumber, AccountOwnerID, AccountType, AccountDateCreation);

            BankData.Accounts.Add(newAccount);
            MakeDeposit(AccountNumber, firstDeposit, "Initial balance", AccountType);
        }

        //overloaded account class that takes ALL necessary parameters and adds it to the BankData store for accounts
        private Accounts(string accountNumber, string accountOwnerID, AccountType accountType, DateTime accountDateCreation)
        {
            AccountNumber = accountNumber;
            AccountOwnerID = accountOwnerID;
            AccountType = accountType;
            AccountDateCreation = accountDateCreation;
        }

        //making deposits on this account
        public void MakeDeposit(string accNum, decimal amountToDeposit, string description, AccountType typeOfAccount)
        {
            //verify that the minimum deposit amount is from #100 above
            if (amountToDeposit < 100)
            {
                throw new InvalidOperationException("Deposit must be from #100 upwards");
            }

            //add a deposit to the account in the Bank store
            var deposit = new Transactions(amountToDeposit, accNum, description, typeOfAccount.ToString(), DateTime.Now);
            BankData.Transactions.Add(deposit);
        }

        //making withdrawals on this account
        public void MakeWithdrawal(string accNum, decimal amountToWithdraw, string description, AccountType typeOfAccount)
        {
            if (amountToWithdraw <= 0)
            {
                throw new InvalidOperationException("Invalid operation: You cannot withdraw such an amount. Enter a valid amount next time");
            }
            if (Balance - amountToWithdraw <= 0)
            {
                throw new InvalidOperationException("Invalid Operation: The amount you want to withdraw exceeds the minimum amount that should be left in your account");
            }

            //make a withdrawal from the account in the Bank store
            var withdrawalChanges = new Transactions(-amountToWithdraw, accNum, description, typeOfAccount.ToString(), DateTime.Now);
            BankData.Transactions.Add(withdrawalChanges);
        }

        //making transfer from/to this account
        public void MakeTransfer(string senderAccNum, string receiverAccNum, decimal amountToTransfer, string description, AccountType typeOfAccount)
        {
            foreach (var account in BankData.Accounts)
            {
                if (account.AccountNumber == senderAccNum && account.Balance > 0)
                {
                    account.MakeWithdrawal(senderAccNum, amountToTransfer, description, typeOfAccount);
                    account.MakeDeposit(receiverAccNum, amountToTransfer, description, typeOfAccount);
                    break;
                }
            }
        }
    }
}
