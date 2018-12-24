using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

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
               
                var dataMinions = Console.ReadLine().Split(new [] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var typeM = dataMinions[0];
                var nameMinions = dataMinions[1];
                var ageMinions = int.Parse(dataMinions[2]);
                var town = dataMinions[3];

                var dataVillains = Console.ReadLine().Split(new Char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries).ToString();
                var typeV = dataVillains[0];
                var nameVallains = dataVillains[1];

                string min = File.ReadAllText(@"./MinionsData.sql");
                string vil = File.ReadAllText(@"./VillainsData.sql");

                var minData = new SqlCommand(min, conect);
                var vilData = new SqlCommand(vil, conect);

                var minName = new SqlParameter("@mname", nameMinions);
                var minAge = new SqlParameter("@age", ageMinions);
                var mintown = new SqlParameter("@tname", town);

                minData.Parameters.Add(minName);
                minData.Parameters.Add(minAge);
                minData.Parameters.Add(mintown);

                var read = minData.ExecuteReader();

                if (!read.Read())
                {
                    string minionName = (string)read["Mname"];
                    int minionAge = (int)read["age"];
                    string minionTown = (string)read["Tname"];

                    Console.WriteLine($"{minionName} {minionAge} {minionTown}");
                }
            }
        }
    }
}