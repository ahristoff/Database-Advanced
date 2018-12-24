using System;
using System.Data.SqlClient;
using System.IO;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {           
            string query = File.ReadAllText("./VillainNames.sql");

            var conect = new SqlConnection
            (
            "Server = ALEN\\SQLEXPRESS01;Database = MinionsDB ;Integrated Security = True"
            );

            conect.Open();
            using (conect)
            {
                //var cmd = new SqlCommand($"SELECT v.Name as Name, Count(mv.MinionId)as Number FROM "+
                //    "Villains as v join MinionsVillains as mv on mv.VillainId = v.Id "+
                //    "group by v.Name "+
                //    "Having Count(mv.MinionId) > 3 "+
                //    "order by Number DESC", conect);

                var cmd = new SqlCommand(query, conect);
                var read = cmd.ExecuteReader();

                while (read.Read())
                {
                    var name = (string)read["Name"];
                    var number = (int)read["Number"];

                    Console.WriteLine($"{name} - {number}");
                }
            }
        }      
    }
}