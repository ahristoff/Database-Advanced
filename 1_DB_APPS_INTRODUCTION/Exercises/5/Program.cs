using System;
using System.Collections.Generic;
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
                var country = Console.ReadLine();
                string query = File.ReadAllText(@"./getTowns.sql");
                SqlCommand findTownsByCountryCommand = new SqlCommand(query, conect);
                SqlParameter countryName = new SqlParameter("@Country", country);
                findTownsByCountryCommand.Parameters.Add(countryName);

                SqlDataReader reader = findTownsByCountryCommand.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                List<string> towns = new List<string>();

                while (reader.Read())
                {
                    string currentTown = (string)reader["Name"];
                    towns.Add(currentTown.ToUpper());
                }

                Console.WriteLine($"[{string.Join(",", towns)}]");
            }
        }
    }
}