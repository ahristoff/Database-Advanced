using System;
using System.Data.SqlClient;
using System.IO;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = File.ReadAllText("./20.sql");

            var conect = new SqlConnection
              (
              "Server = ALEN\\SQLEXPRESS01;Integrated Security = True"
              );

            conect.Open();

            string sqlCreateDB = "CREATE DATABASE MinionsDB";

            SqlCommand createDBCommand = new SqlCommand(sqlCreateDB, conect);

            SqlCommand createTables = new SqlCommand(query, conect);

            using (conect)
            {
                createDBCommand.ExecuteNonQuery();

                Console.WriteLine(createTables.ExecuteNonQuery());
            }              
        }
    }
}