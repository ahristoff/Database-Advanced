using System;
using System.Data.SqlClient;
using System.IO;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {           
            var conect = new SqlConnection
            (
            "Server = ALEN\\SQLEXPRESS01;Database = MinionsDB;Integrated Security = True"
            );

            conect.Open();
            using (conect)
            {
                int villainId = int.Parse(Console.ReadLine());

                string findNamequery = File.ReadAllText("./VillianNameById.sql");
                var findVillainNameCommand = new SqlCommand(findNamequery, conect);

                var villaiIdParam = new SqlParameter("@villainId", villainId);

                findVillainNameCommand.Parameters.Add(villaiIdParam);

                var read = findVillainNameCommand.ExecuteReader();

                if (read.Read())
                {
                    string villName = (string)read["Name"];
                    Console.WriteLine($"Villain: {villName}");

                    string findMinionsQuery = File.ReadAllText(@"./FindMinions.sql");

                    var findMinionsCommand = new SqlCommand(findMinionsQuery, conect);
                    var param = new SqlParameter("@villainId", villainId);

                    findMinionsCommand.Parameters.Add(param);

                    read.Dispose();

                    var minionsread = findMinionsCommand.ExecuteReader();

                    int n = 1;
                    while (minionsread.Read())
                    {
                        string minionName = (string)minionsread["Name"];
                        int minionAge = (int)minionsread["Age"];

                        Console.WriteLine($"{n}. {minionName} {minionAge}");
                        n++;
                    }
                }
                else
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                }
            }
        }
    }
}