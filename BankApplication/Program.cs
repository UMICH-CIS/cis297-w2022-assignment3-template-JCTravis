using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    //Jacob Travis
    //CIS297
    //2-10-22


    ///<summary>
    ///This parent account class instaniates the properties used by the savings and checking account
    ///classes; it returns the account balance and adds/deducts money through debit and credit functions
    ///</summary>
    public class Account
    {
        private decimal balance;

        public Account(decimal bal)
        {
            Balance = bal;
        }

        public decimal Balance
        {
            get
            {
                return balance;
            }

            set
            {
                if(value > 0)
                {
                    balance = value;
                }

                else
                {
                    throw new ArgumentOutOfRangeException("Balance", value, "Balance should be greater than 0");
                }
            }
        }

        public virtual bool Debit(decimal amount)
        {
            if((Balance - amount) > 0)
            {
                Balance -= amount;
                return true;
            }

            else
            {
                Console.WriteLine("Debit amount requested is more than the balance");
                return false;
            }
        }

        public virtual void Credit(decimal amount)
        {
            Balance += amount;
        }
    }
    ///Jacob Travis
    ///CIS 297
    ///2-10-22
    ///<summary>
    ///Savingsaccount class inherits all functionality from account class, and also adds interest rate
    ///to the balance in your savings account
    public class SavingsAccount : Account
    {
        private decimal interestRate;

        public SavingsAccount(decimal bal, decimal intrstRte) : base(bal)
        {
            interestRate = intrstRte;
        }

        public decimal InterestRate
        {
            get
            {
                return interestRate;
            }

        }

        public decimal CalculateInterest()
        {
            return Balance * interestRate;
        }
    }
    ///Jacob Travis
    ///CIS 297
    ///2-10-22
    ///<summary>
    ///Checking account inherits all functionality from account class, and allots for a fee to be
    ///taken out of the account when the user withdraws money out
    public class CheckingAccount : Account
    {
        private decimal bankFee;

        public CheckingAccount(decimal bal, decimal fee) : base(bal)
        {
            bankFee = fee;
        }

        public decimal BankFee
        {
            get
            {
                return bankFee;
            }
        }

        public override bool Debit(decimal amount)
        {
            if(base.Debit(amount))
            {
                base.Debit(bankFee);
                return true;
            }

            else
            {
                return false;
            }

        }

        public override void Credit(decimal amount)
        {
            base.Credit(amount);
            base.Debit(bankFee);
        }
    }

   public class AccountTest
   {
        static void Main(string[] args)
        {
               // create array of accounts
          Account[] accounts = new Account[4];

      // initialize array with Accounts
          accounts[0] = new SavingsAccount(25M, .03M);
          accounts[1] = new CheckingAccount(80M, 1M);
          accounts[2] = new SavingsAccount(200M, .015M);
          accounts[3] = new CheckingAccount(400M, .5M);

      // loop through array, prompting user for debit and credit amounts
         for (int i = 0; i < accounts.Length; i++)
         {
            Console.WriteLine($"Account {i + 1} balance: {accounts[i].Balance:C}");

            Console.Write($"\nEnter an amount to withdraw from Account {i + 1}: ");
            decimal withdrawalAmount = decimal.Parse(Console.ReadLine());

            accounts[i].Debit(withdrawalAmount); // attempt to debit

            Console.Write($"\nEnter an amount to deposit into Account {i + 1}: ");
            decimal depositAmount = decimal.Parse(Console.ReadLine());

         // credit amount to Account
            accounts[i].Credit(depositAmount);

         // if Account is a SavingsAccount, calculate and add interest
            if (accounts[i] is SavingsAccount)
            {
            // downcast
               SavingsAccount currentAccount = (SavingsAccount) accounts[i];

               decimal interestEarned = currentAccount.CalculateInterest();
               Console.WriteLine($"Adding {interestEarned:C} interest to Account {i + 1} (a SavingsAccount)");
               currentAccount.Credit(interestEarned);
            } 
         }
            Console.WriteLine($"\nUpdated Account {i + 1} balance: {accounts[i].Balance:C}\n\n");
        }
   }
}
