using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SSDKTB4.ConsoleApp4
{
    internal class FestivalAdoDotNet
    {
		private SqlConnectionStringBuilder sb;
		public FestivalAdoDotNet()
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

			SqlCommand cmd = new SqlCommand("select * from festival_management",connection);
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			connection.Close();

			Console.WriteLine("All Festivals:");
			Console.WriteLine("-----------------------------------");

			if (dt.Rows.Count == 0)
			{
				Console.WriteLine("No festivals found.");
			}

			foreach (DataRow dr in dt.Rows)
			{
				int id = Convert.ToInt32(dr["FestivalId"]);
				string name = dr["FestivalName"].ToString();
				string datetime = Convert.ToDateTime(dr["FestivalDateTime"]).ToString("yyyy-MM-dd HH:mm");
				string location = dr["Location"].ToString();
				decimal price = Convert.ToDecimal(dr["TicketPrice"]);
				int quantity = Convert.ToInt32(dr["TicketQuantity"]);

				Console.WriteLine($"No: {id} / Name: {name} / Date: {datetime} / Location: {location} / Price: {price:n0} / Quantity: {quantity}");
			}
			Console.WriteLine("-----------------------------------");
		}
		public void Create()
		{
			Console.Write("Please enter festival name: ");
			string name = Console.ReadLine();

			Console.Write("Please enter festival date & time (yyyy-MM-dd HH:mm): ");
			DateTime dateTime = Convert.ToDateTime(Console.ReadLine());

			Console.Write("Please enter festival location: ");
			string location = Console.ReadLine();

			Console.Write("Please enter ticket price: ");
			decimal price = Convert.ToDecimal(Console.ReadLine());

			Console.Write("Please enter ticket quantity: ");
			int quantity = Convert.ToInt32(Console.ReadLine());

		    string connectionString = sb.ConnectionString;
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();
			string query = @"INSERT INTO [dbo].[festival_management]
                           (
                            [FestivalName]
                           ,[FestivalDateTime]
                           ,[Location]
                           ,[TicketPrice]
                           ,[TicketQuantity]
                           ,[CreatedDateTime]
                           )
                     VALUES
                           (@Name,
                           @DateTime,
                           @Location,
                           @Price,
                           @Quantity,
                           GETDATE())";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@Name", name);
			command.Parameters.AddWithValue("@DateTime", dateTime);
			command.Parameters.AddWithValue("@Location", location);
			command.Parameters.AddWithValue("@Price", price);
			command.Parameters.AddWithValue("@Quantity", quantity);
			int result = command.ExecuteNonQuery();
			connection.Close();
			string message = result > 0 ? "Festival created successfully." : "Festival Addition Failed";
			Console.WriteLine(message);

		}
		public void Update()
		{
			Console.Write("Please enter festival id to update: ");
			int id = Convert.ToInt32(Console.ReadLine());

			SqlConnection connection = new SqlConnection(sb.ConnectionString);
			connection.Open();
			SqlCommand selectCmd = new SqlCommand("SELECT * FROM festival_management WHERE FestivalId = @Id", connection);
			selectCmd.Parameters.AddWithValue("@Id", id);
			SqlDataAdapter adapter = new SqlDataAdapter(selectCmd);
			DataTable dt = new DataTable();
			adapter.Fill(dt);
			connection.Close();
			if (dt.Rows.Count == 0)
			{
				Console.WriteLine("Festival not found.");
				return;
			}
			DataRow dr = dt.Rows[0];
			Console.WriteLine($"Current Name: {dr["FestivalName"]} / Price: {dr["TicketPrice"]}");
			Console.WriteLine("-----------");
			Console.Write("Please enter new festival name (leave blank to keep current): ");
			string name = Console.ReadLine();
			if(string.IsNullOrEmpty(name))
			{
				name = dr["FestivalName"].ToString();
			}
			Console.Write("Please enter new ticket price (leave blank to keep current): ");
			string priceInput = Console.ReadLine();
			decimal price = 0;
			if(string.IsNullOrEmpty(priceInput))
			{
				price = Convert.ToDecimal(dr["TicketPrice"]);
			}
			else
			{
				price = Convert.ToDecimal(priceInput);
			}

			Console.Write("Enter new Date (leave empty to keep current): ");
			string dateStr = Console.ReadLine();
			DateTime dateTime;
			if (string.IsNullOrEmpty(dateStr))
			{
				dateTime = Convert.ToDateTime(dr["FestivalDateTime"]);
			}
			else
			{
				dateTime = Convert.ToDateTime(dateStr);
			}

			Console.Write("Enter new Location (leave empty to keep current): ");
			string location = Console.ReadLine();
			if(string.IsNullOrEmpty(location))
			{
				location = dr["Location"].ToString();
			}

			Console.Write("Enter new Quantity (leave empty to keep current): ");
			string quantityInput = Console.ReadLine();
			int quantity = 0;
			if(string.IsNullOrEmpty(quantityInput))
			{
				quantity = Convert.ToInt32(dr["TicketQuantity"]);
			}
			else
			{
				quantity = Convert.ToInt32(quantityInput);
			}


			SqlConnection connectionString = new SqlConnection(sb.ConnectionString);
			connection.Open();
			string updateQuery = @"UPDATE [dbo].[festival_management]
								   SET [FestivalName] = @Name,
									   [FestivalDateTime] = @DateTime,
									   [Location] = @Location,
									   [TicketPrice] = @Price,
									   [TicketQuantity] = @Quantity
								 WHERE FestivalId = @Id";
			SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
			updateCmd.Parameters.AddWithValue("@Name", name);
			updateCmd.Parameters.AddWithValue("@DateTime", dateTime);
			updateCmd.Parameters.AddWithValue("@Location", location);
			updateCmd.Parameters.AddWithValue("@Price", price);
			updateCmd.Parameters.AddWithValue("@Quantity", quantity);
			updateCmd.Parameters.AddWithValue("@Id", id);
			int result = updateCmd.ExecuteNonQuery();
			connection.Close();
			string message = result > 0 ? "Festival updated successfully." : "Festival Update Failed";
			Console.WriteLine(message);
		}
		public void Delete()
		{
			Console.Write("Please Enter Festival Id to Delete:");
			int id = Convert.ToInt32((Console.ReadLine()));

			Console.Write("Are you sure to delete this festival?");
			string confirm = Console.ReadLine();
			if (confirm.ToUpper() != "Y") return;
			
			SqlConnection connection = new SqlConnection(sb.ConnectionString);
			connection.Open();
			SqlCommand command = new SqlCommand("delete from festival_management WHERE FestivalId = @Id", connection);
			command.Parameters.AddWithValue("@Id", id);
			
			int deleteResult = command.ExecuteNonQuery();
			connection.Close();
			string message = deleteResult > 0 ? "Festival Delete Successfully" : "Festival Deletation failed";
			Console.WriteLine(message);

		}
	}
}
