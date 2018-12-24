using P03_FootballBetting.Data;
using System;
using System.Text;

namespace P03_FootballBetting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var context = new FootballBettingContext();
                
            RestartDb(context);
            
        }

        private static void RestartDb(FootballBettingContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();                      
        }
    }
}
