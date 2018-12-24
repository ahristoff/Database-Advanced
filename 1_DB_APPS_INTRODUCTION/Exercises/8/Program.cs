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
            string query = File.ReadAllText(@"./MinionAge.sql");

            var conect = new SqlConnection
            (
            "Server = ALEN\\SQLEXPRESS01;Database = MinionsDB;Integrated Security = True"
            );

            conect.Open();
            using (conect)
            {
                var idNum = Console.ReadLine().Split(' ').Select(int.Parse).ToList();

                var cmd = new SqlCommand(query, conect);
                var read = cmd.ExecuteReader();

                while (read.Read())
                {
                    var name = (string)read["Name"];
                    var age = (int)read["Age"];
                    var number = (int)read["Id"];

                    foreach (var x in idNum)
                    {
                        if (x == number)
                        {
                            age++;
                        }

                        Console.WriteLine($"{name} - {age}");
                    }                
                }
            }
        }
    }
}