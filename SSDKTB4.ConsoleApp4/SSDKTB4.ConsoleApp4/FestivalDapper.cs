using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace SSDKTB4.ConsoleApp4
{
    internal class FestivalDapper
    {
		private SqlConnectionStringBuilder sb;
		public FestivalDapper()
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
			using (IDbConnection db = new SqlConnection(sb.ConnectionString))
			{
				db.Open();
				string query = "SELECT * FROM festival_management";

				List<FestivalDto> festivals = db.Query<FestivalDto>(query).ToList();

				Console.WriteLine("All Festivals:");
				Console.WriteLine("-----------------------------------");
				foreach (FestivalDto item in festivals)
				{
					string dateStr = item.FestivalDateTime.ToString("yyyy-MM-dd HH:mm");
					Console.WriteLine($"No: {item.FestivalId} / N: {item.FestivalName} / D: {dateStr} / L: {item.Location} / P: {item.TicketPrice:n0} / Q: {item.TicketQuantity}");
				}
				Console.WriteLine("-----------------------------------");
			}
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

			using (IDbConnection db = new SqlConnection(sb.ConnectionString))
			{
				db.Open();
				string query = @"INSERT INTO festival_management
                               (
                                [FestivalName],
                                [FestivalDateTime],
                                [Location],
                                [TicketPrice],
                                [TicketQuantity],
                                [CreatedDateTime]
                               )
                               VALUES
                               (
                                @Name,
                                @DateTime,
                                @Location,
                                @Price,
                                @Quantity,
                                GETDATE()
                               )";

				var parameters = new
				{
					Name = name,
					DateTime = dateTime,
					Location = location,
					Price = price,
					Quantity = quantity
				};

				int result = db.Execute(query,parameters);

				string message = result > 0 ? "Festival Created Successfully." : "Festival Insertation failed.";
				Console.WriteLine(message);
			}
		}
		public void Update()
		{
			Console.Write("Please enter festival id to edit: ");
			int id = Convert.ToInt32(Console.ReadLine());

			using (IDbConnection db = new SqlConnection(sb.ConnectionString))
			{
				db.Open();
				string query = "SELECT * FROM festival_management WHERE FestivalId = @Id";
				var parameter = new { id = id };

				FestivalDto item = db.Query<FestivalDto>(query, parameter).FirstOrDefault();

				if (item is null)
				{
					Console.WriteLine("Festival Not Found!");
					return;
				}

				Console.WriteLine($"Current Name: {item.FestivalName} / Price: {item.TicketPrice}");
				Console.WriteLine("-----------");

				Console.Write("Enter new Name (leave empty to keep current): ");
				string name = Console.ReadLine();
				if (string.IsNullOrEmpty(name)) name = item.FestivalName;

				Console.Write("Enter new Date (leave empty to keep current): ");
				string dateStr = Console.ReadLine();
				DateTime dateTime = string.IsNullOrEmpty(dateStr) ? item.FestivalDateTime : Convert.ToDateTime(dateStr);
				

				Console.Write("Enter new Location (leave empty to keep current): ");
				string location = Console.ReadLine();
				if (string.IsNullOrEmpty(location)) location = item.Location;

				Console.Write("Enter new Price (leave empty to keep current): ");
				string priceStr = Console.ReadLine();
				decimal price = string.IsNullOrEmpty(priceStr) ? item.TicketPrice : Convert.ToDecimal(priceStr);

				Console.Write("Enter new Quantity (leave empty to keep current): ");
				string qtyStr = Console.ReadLine();
				int quantity = string.IsNullOrEmpty(qtyStr) ? item.TicketQuantity : Convert.ToInt32(qtyStr);

				string updateQuery = @"UPDATE festival_management
                                       SET [FestivalName] = @Name,
                                           [FestivalDateTime] = @DateTime,
                                           [Location] = @Location,
                                           [TicketPrice] = @Price,
                                           [TicketQuantity] = @Quantity
                                       WHERE FestivalId = @Id";

				var parameters = new
				{
					Id = id,
					Name = name,
					DateTime = dateTime,
					Location = location,
					Price = price,
					Quantity = quantity
				};

				int result = db.Execute(updateQuery, parameters);
				string message = result > 0 ? "Update successful." : "Update failed.";
				Console.WriteLine(message);
			}
		}
		public void Delete()
		{
			Console.Write("Please enter festival id to delete: ");
			int id = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("Are you sure? (Y/N)");
			if (Console.ReadLine()?.ToUpper() != "Y") return;

			using (IDbConnection db = new SqlConnection(sb.ConnectionString))
			{
				db.Open();
				string query = "DELETE FROM festival_management WHERE FestivalId = @Id";

				int result = db.Execute(query, new { Id = id });

				string message = result > 0 ? "Delete successful." : "Delete failed (ID not found).";
				Console.WriteLine(message);
			}
		}
	}
}
