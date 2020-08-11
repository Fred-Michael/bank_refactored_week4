using System;
using System.Collections.Generic;
using System.Text;

namespace BankClassLibraries
{
    public class Transactions
    {
        public string AccountNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }

        //Transaction constructor
        public Transactions(decimal amount, string accNum, string note, string type, DateTime date)
        {
            AccountNumber = accNum;
            Amount = amount;
            Note = note;
            TransactionDate = date;
            Type = type;
        }
    }
}
