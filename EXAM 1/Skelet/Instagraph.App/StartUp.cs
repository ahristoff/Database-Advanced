using System;
using AutoMapper;
using Instagraph.Data;

namespace Instagraph.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Mapper.Initialize(options => options.AddProfile<InstagraphProfile>());

            Console.WriteLine(ResetDatabase());

            Console.WriteLine(ImportData());

            ExportData();
        }

        private static string ImportData()
        {
            return null;
        }

        private static void ExportData()
        {
            
        }
        
        private static string ResetDatabase()
        {
            using (var context = new InstagraphContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return $"Database reset succsessfully.";
        }
    }
}
