using Lab.Data.Models;
using System;
using System.Linq;

namespace _15
{
    class Program
    {
        public static int? NULL { get; private set; }

        static void Main(string[] args)
        {
            using (var context = new SoftUniDbContext())
            {
                var town = Console.ReadLine();
                var deletedTown = context.Towns.Where(t => t.Name == town);

                var n = 0;
                foreach (var x in deletedTown)
                {
                    var deletedAdresses = context.Addresses.Where(a => a.TownId == x.TownId);
                    
                    foreach (var y in deletedAdresses)
                    {
                        n++;
                        var deletedEmployees = context.Employees.Where(e => e.AddressId == y.AddressId);
                        foreach (var z in deletedEmployees)
                        {
                            z.AddressId = null;                           
                        }

                        context.Addresses.Remove(y);
                        context.SaveChanges();
                    }

                    context.Remove(x);
                    context.SaveChanges();
                }

                Console.WriteLine($"{n} addresses in {town} were deleted");
            }
        }
    }
}
