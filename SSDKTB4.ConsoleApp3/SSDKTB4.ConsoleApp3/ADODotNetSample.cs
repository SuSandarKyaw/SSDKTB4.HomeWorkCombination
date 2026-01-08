using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SSDKTB4.ConsoleApp3;

internal class ADODotNetSample
{

	private SqlConnectionStringBuilder sb;

	public ADODotNetSample()
	{
		sb = new SqlConnectionStringBuilder();
		sb.DataSource = ".";
		sb.InitialCatalog = "SSDKMiniPOS";
		sb.UserID = "sa";
		sb.Password = "sasa@123";
		sb.TrustServerCertificate = true;
	}
	public void Read()
	{

		string connectionString = sb.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);

		connection.Open();

		SqlCommand cmd = new SqlCommand("SELECT TOP 10 * FROM [dbo].[Tbl3_product]", connection);
		SqlDataAdapter adapter = new SqlDataAdapter(cmd);
		DataTable dataTable = new DataTable();
		adapter.Fill(dataTable);

		connection.Close();
		foreach (DataRow dr in dataTable.Rows)
		{
			string productName = dr["ProductName"].ToString();
			decimal price = Convert.ToDecimal(dr["Price"]);
			int quantity = Convert.ToInt32(dr["Quantity"]);
			Console.WriteLine($"Product Name: {productName}, Price: {price.ToString("n0")}, Quantity: {quantity}");
		}
	}
	public void Edit()
	{

		string connectionString = sb.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);

		connection.Open();
		SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Tbl3_product] where ProductId=2", connection);
		SqlDataAdapter adapter = new SqlDataAdapter(cmd);
		DataTable dataTable = new DataTable();
		adapter.Fill(dataTable);
		connection.Close();

		if(dataTable.Rows.Count == 0)
		{
			Console.WriteLine("No data found to edit.");
			return;
		}

		DataRow dr = dataTable.Rows[0];
		string productName = dr["ProductName"].ToString();
		decimal price = Convert.ToDecimal(dr["Price"]);
		int quantity = Convert.ToInt32(dr["Quantity"]);
		Console.WriteLine($"Product Name: {productName}, Price: {price.ToString("n0")}, Quantity: {quantity}");
		
	}
	public void Create()
	{
		string connectionString = sb.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);

		connection.Open();
		string query = @"INSERT INTO [dbo].[Tbl3_product]
		           (
		           [ProductName]
		           ,[Price]
		           ,[Quantity]
		           ,[CreatedDateTime]
		           )
		     VALUES
		           ('Mango',
				   1000,
				   50,
				   GETDATE())";
		SqlCommand command = new SqlCommand(query, connection);
		int result = command.ExecuteNonQuery();
		connection.Close();
		string message = result > 0 ? "Data Inserted Successfully" : "Data Insertion Failed";
		Console.WriteLine(message);

	}
	public void Update()
	{
		string connectionString = sb.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);

		connection.Open();
		SqlCommand updateCmd = new SqlCommand(@"UPDATE [dbo].[Tbl3_product]
		   SET
			  [ProductName] = 'Pineapple',
			  [Price] = 2500,
			  [Quantity] = 30
		 WHERE ProductId = 6", connection);
		int updateResult = updateCmd.ExecuteNonQuery();
		connection.Close();
		string updateMessage = updateResult > 0 ? "Data Updated Successfully" : "Data Update Failed";
		Console.WriteLine(updateMessage);
	}
	public void Delete()
	{
		
		string connectionString = sb.ConnectionString;
		SqlConnection connection = new SqlConnection(connectionString);
		connection.Open();
		SqlCommand deleteCmd = new SqlCommand(@"DELETE FROM [dbo].[Tbl3_product] WHERE ProductId = 3", connection);
		int deleteResult = deleteCmd.ExecuteNonQuery();
		connection.Close();
		string deleteMessage = deleteResult > 0 ? "Data Deleted Successfully" : "Data Deletion Failed";
		Console.WriteLine(deleteMessage);
	}
}
