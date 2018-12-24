using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public decimal Balance { get; set; }

        public string BankName { get; set; }

        public string SWIFTCode { get; set; }        

        public int PaymentMethodId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal Deposit(decimal amount)
        {
            var res = Balance + amount;
            Balance = res;
            return Balance;
        }

        public decimal Withdraw(decimal amount)
        {
            var res = Balance - amount;
            if (res<0)
            {
                Console.WriteLine("Insufficient funds!");
                return Balance;
            }
            else
            {
                Balance = Balance - amount;
                return Balance;
            }
        }
    }
}
