using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

class Program
{
    public static string GetConenctString()
    {
        var configBuilder = new ConfigurationBuilder().
            SetBasePath("D:\\FPT\\GIT\\DotNet\\ADO_NET_test_1\\ADO_NET_test_1").
            AddJsonFile("config.json");
        var configurationroot = configBuilder.Build();
        string? connectionString = configurationroot["coniguration:connection1"];

        if (connectionString != null)
        {
            return connectionString;
        }
        throw new Exception("Invalid configurations !");
    }


    public static void Main(string[] args)
    {
        string sqlConnectString = GetConenctString();
        var connection = new SqlConnection(sqlConnectString);

        // Activate collecting connection information
        connection.StatisticsEnabled = true;

        // Listen states of connection
        connection.StateChange += (object sender, StateChangeEventArgs e) =>
        {
            Console.WriteLine($"\nCurrent state: {e.CurrentState}\nOriginal state: {e.OriginalState}\n");
        };

        connection.Open();

        // Use SqlCommand
        using (DbCommand command = connection.CreateCommand())
        {
            command.CommandText = "select top 100 product_id, product_name from production.products";
            var reader = command.ExecuteReader();
            Console.WriteLine("\r\nFirst 100 products: ");
            Console.WriteLine($"{"produc_id",10} {"produc_name"}");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["product_id"],10} {reader["product_name"]}");
            }

        }

        connection.Close();
    }
}
