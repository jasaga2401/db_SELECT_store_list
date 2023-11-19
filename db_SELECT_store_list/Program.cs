using System;
using System.Collections.Generic;
using System.Data.SqlClient;

class Program
{
    // Define a class to represent your database records
    class YourDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string DTime {  get; set; }
        
    }

    static void Main()
    {
        // Replace with your actual SQL Server connection string
        string connectionString = "Data Source=Amaze\\SQLEXPRESS;Initial Catalog=things_to_do;Integrated Security=True";

        // Create a new SqlConnection using the connection string
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                // Open the database connection
                connection.Open();
                Console.WriteLine("Connected to the database.");

                // Define your SQL query
                string sqlQuery = "SELECT * FROM dbo.tbl_items";

                // Create a SqlCommand with the query and the connection
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Execute the query and get a SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a list to store the retrieved data
                        List<YourDataModel> dataList = new List<YourDataModel>();

                        // Check if there are rows in the result set
                        if (reader.HasRows)
                        {
                            // Iterate through the rows and add data to the list
                            while (reader.Read())
                            {
                                YourDataModel data = new YourDataModel
                                {
                                    Id = Convert.ToInt32(reader["itid"]),
                                    Name = reader["name"].ToString(),
                                    Description = reader["description"].ToString(),
                                    DTime = reader["dateTask"].ToString()

                                    // Set other properties as needed
                                };

                                dataList.Add(data);
                            }
                        }

                        // Now, dataList contains the retrieved data from the database

                        // Example: Output data from the list
                        foreach (var data in dataList)
                        {
                            Console.WriteLine($"Id: {data.Id}, ColumnName: {data.Name}, CoiumnName: {data.Description}, ColumnName: {data.DTime}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Ensure the connection is closed, whether an exception occurs or not
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
            }
        }
    }
}
