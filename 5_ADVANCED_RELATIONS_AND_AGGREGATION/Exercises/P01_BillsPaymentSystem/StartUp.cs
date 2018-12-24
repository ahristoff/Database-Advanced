using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Linq;

namespace P01_BillsPaymentSystem.App
{
    class StartUp
    {
        public static void Main(string[] args)
        {
            var userId = int.Parse(Console.ReadLine());
            var amount = decimal.Parse(Console.ReadLine());
            string command = Console.ReadLine();

            using (var db = new BillsPaymentSystemContext())
            {
                RestartDb(db);
                try
                {
                    var user = db.Users
                    .Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = $"{u.FirstName} {u.LastName}",
                        CreditCards = u.PaymentMethods
                           .Where(pm => pm.Type == PaymentMethodType.CreditCard)
                           .Select(pm => pm.CreditCard).ToList(),
                        BankAccounts = u.PaymentMethods
                           .Where(pm => pm.Type == PaymentMethodType.BankAccount)
                           .Select(pm => pm.BankAccount).ToList(),
                    })
                    .FirstOrDefault();

                    Console.WriteLine($"User: {user.Name}");

                    var bankAccounts = user.BankAccounts;
                    if (bankAccounts.Any())
                    {
                        Console.WriteLine($"BankAccounts:");

                        foreach (var x in bankAccounts)
                        {
                            x.Balance = PayBills(amount, command);
                            db.SaveChanges();
                            Console.WriteLine($@"--ID: {x.BankAccountId}
---Balance: {x.Balance}
---Bank: {x.BankName}
---SWIFT: {x.SWIFTCode}");
                        }
                    }
                    

                    var creditCards = user.CreditCards;
                    if (creditCards.Any())
                    {
                        Console.WriteLine($"Credit Cards:");

                        foreach (var y in creditCards)
                        {
                            Console.WriteLine($@"--ID: {y.CreditCardId}
---Limit: {y.Limit:f2}
---Money Owed: {y.MoneyOwed:f2}
---Limit Left: {y.LimitLeft:f2}
---Expiration Date: {y.ExpirationDate.ToString("yyyy/MM")}");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"User with id {userId} not found!");
                }
            }
        }
        private static void RestartDb(BillsPaymentSystemContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Seed(context);
        }
        private static decimal PayBills(decimal amount, string comand)
        {
            var db = new BillsPaymentSystemContext();
            var ba = db.BankAccounts
                .FirstOrDefault();

            if (comand == "Deposit".ToLower())
            {
                ba.Deposit(amount);
                db.SaveChanges();
                return ba.Balance;
            }
            else if (comand == "Withdraw".ToLower())
            {
                ba.Withdraw(amount);
                db.SaveChanges();
                return ba.Balance;
            }
            else
            {
                Console.WriteLine("Unknow command!");
                return ba.Balance;
            }      
        }

        private static void Seed(BillsPaymentSystemContext db)
        {
            var users = new[]
                {
                    new User{FirstName = "Gay", LastName = "Gilbert", Email ="Gay1@abv.bg", Password= "1a2s3d"},
                    new User{FirstName = "Pesho", LastName = "Gilbert", Email ="Pesho@abv.bg", Password= "4a5s3d"}
                    };
            db.Users.AddRange(users);

            var creditCards = new[]
                {
                        new CreditCard { Limit = 800m, MoneyOwed = 100m, ExpirationDate = new DateTime(2020,03,01)},
                        new CreditCard { Limit = 400m, MoneyOwed = 200m, ExpirationDate = new DateTime(2020,03,01)}
                    };
            db.CreditCards.AddRange(creditCards);

            var bankaccounts = new[]
                {
                        new BankAccount{ Balance = 2000.00m, BankName = "Unicredit Bulbank", SWIFTCode = "UNCRBGSF"},
                        new BankAccount{ Balance = 1000.00m, BankName = "First Investment Bank", SWIFTCode = "FINVBGSF"}
                    };
            db.BankAccounts.AddRange(bankaccounts);

            var paymethods = new[]
                {
                        new PaymentMethod{ CreditCard = creditCards[0], User = users[0], Type = PaymentMethodType.CreditCard},
                        new PaymentMethod{ CreditCard = creditCards[1], User = users[0], Type =PaymentMethodType.CreditCard},
                        new PaymentMethod{ BankAccount = bankaccounts[0], User = users[0], Type =PaymentMethodType.BankAccount},
                    };
            db.PaymentMethods.AddRange(paymethods);

            db.SaveChanges();
        }
    }
}
