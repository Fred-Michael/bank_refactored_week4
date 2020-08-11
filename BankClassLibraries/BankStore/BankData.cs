using System;
using System.Collections.Generic;
using System.Text;

namespace BankClassLibraries
{
    public class BankData
    {
        //This BankData class holds the lists of Accounts, Customers and Transactions
        public static List<Accounts> Accounts { get; set; } = new List<Accounts>();
        public static List<Customers> Customers { get; set; } = new List<Customers>();
        public static List<Transactions> Transactions { get; set; } = new List<Transactions>();
    }
}