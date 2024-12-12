using System;
using System.Threading;

namespace BankAccountApp
{
    public class BankAccount
    {
        private decimal _balance;

        // TODO: Declare a lock object for thread synchronization
        private readonly object _lock = new object();

        // Constructor
        public BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        // TODO: Implement the Deposit method
        // This method should:
        // 1. Use a synchronization mechanism to protect the shared balance.
        // 2. Add the amount to the balance.
        // 3. Print a message showing the deposit amount and new balance.
        public void Deposit(decimal amount)
        {
            lock (_lock)
            {
                _balance += amount;
                Console.WriteLine($"Deposited: {amount}, New Balance: {_balance}");
            }
        }

        // TODO: Implement the Withdraw method
        // This method should:
        // 1. Use a synchronization mechanism to protect the shared balance.
        // 2. Check if there are sufficient funds before withdrawing.
        // 3. Subtract the amount from the balance if possible.
        // 4. Print a message showing the withdrawal amount and remaining balance.
        // 5. If insufficient funds, print an appropriate message.
        public void Withdraw(decimal amount)
        {
            lock (_lock)
            {
                if (amount <= _balance)
                {
                    _balance -= amount;
                    Console.WriteLine($"Withdrawn: {amount}, Remaining Balance: {_balance}");
                }
                else
                {
                    Console.WriteLine($"Insufficient funds for withdrawal of {amount}. Available: {_balance}");
                }
            }
        }

        // Method to get the current balance
        // Modify this method to use a synchronization mechanism to protect the shared balance.
        public decimal GetBalance()
        {
            lock (_lock)
            {
                return _balance;
            }
        }

        static void Main(string[] args)
        {
            BankAccount account = new BankAccount(1000);

            Thread depositThread = new Thread(() => account.Deposit(500));
            Thread withdrawThread = new Thread(() => account.Withdraw(300));
            Thread balanceThread = new Thread(() => Console.WriteLine($"Balance: {account.GetBalance()}"));

            depositThread.Start();
            withdrawThread.Start();
            balanceThread.Start();

            depositThread.Join();
            withdrawThread.Join();
            balanceThread.Join();

            Console.WriteLine("Final Balance: " + account.GetBalance());
        }
    }
}
