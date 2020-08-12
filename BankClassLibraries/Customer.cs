using BankClassLibraries;
using System;
using System.Collections.Generic;

namespace BankClassLibraries
{
    public class Customers
    {
        private int customerIDGenerator = 0;
        public string customerName { get; set; }
        public string customerID { get; set; }
        public string customerEmail { get; set; }
        public List<Accounts> customersAccounts
        {
            get
            {
                var custAccTransaction = new List<Accounts>();
                foreach (var item in BankData.Accounts)
                {
                    if (item.AccountOwnerID == customerID)
                    {
                        custAccTransaction.Add(item);
                    }
                }
                return custAccTransaction;
            }
        }

        //customer constructor class
        public Customers(string name, string email)
        {
            customerID = (customerIDGenerator + BankData.Customers.Count + 1).ToString();
            if (name.Length > 0 && email.Contains(".com"))
            {
                customerName = name;
                customerEmail = email;
            }
            else
            {
                throw new InvalidOperationException("Name and/or Email is not valid. Try again");
            }

            var newCustomer = new Customers(customerName, customerEmail, customerID);

            BankData.Customers.Add(newCustomer);
        }

        //overloaded customer class that takes ALL necessary parameters and adds it to the BankData store for newly added customers
        private Customers(string name, string email, string customerId)
        {
            customerName = name;
            customerEmail = email;
            customerID = customerId;
        }
    }
}
