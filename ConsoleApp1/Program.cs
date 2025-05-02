
using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=localhost;Port=3306;Database=mydatabase;User=mark;Password=mark;";
        MySqlConnection connection = new(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Connected to MySQL!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}