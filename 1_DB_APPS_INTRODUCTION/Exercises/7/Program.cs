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
            var list = new List<string>();

            string query = File.ReadAllText(@"./MinionNames.sql");

            var conect = new SqlConnection
            (
            "Server = ALEN\\SQLEXPRESS01;Database = MinionsDB;Integrated Security = True"
            );

            conect.Open();
            using (conect)
            {

                var cmd = new SqlCommand(query, conect);
                var read = cmd.ExecuteReader();

                while (read.Read())
                {
                    var names = (string)read["name"];
                    
                    list.Add(names);                                     
                }

                for (int i = 0; i < (list.Count/2)+1; i++)
                {
                    Console.WriteLine(list[i]);

                    if (i == list.Count / 2)
                    {
                        return;
                    }

                    for (int j = list.Count - 1 - i; j >= list.Count - 1 - i; j--)
                    {
                        Console.WriteLine($"{list[j]}");
                    }
                }
            }
        }
    }
}