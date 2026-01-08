using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SSDKTB4.ConsoleApp3;

internal class DapperSample
{
	private SqlConnectionStringBuilder sb;
	public DapperSample()
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
			string query = "SELECT * FROM [dbo].[Tbl3_product]";
			List<ProductDto> lists = db.Query<ProductDto>(query).ToList(); // execute

			foreach (ProductDto item in lists)
			{
				Console.WriteLine($"ProductId - {item.ProductId}, Product Name - {item.ProductName}, Price - {item.Price}, Quantity - {item.Quantity}  ");

				//Console.WriteLine(item.ProductId);
				//Console.WriteLine(item.ProductName);
				//Console.WriteLine(item.Price);
				//Console.WriteLine(item.Quantity);

			}
		}
		
		
	}

	public void Edit()
	{
		using (IDbConnection db = new SqlConnection(sb.ConnectionString))
		{
			db.Open();
			string query = "SELECT * FROM [dbo].[Tbl3_product] where productId = 5";
			ProductDto item = db.Query<ProductDto>(query).FirstOrDefault()!; // execute

			if (item is null) return;
			
				Console.WriteLine(item.ProductId);
				Console.WriteLine(item.ProductName);
				Console.WriteLine(item.Price);
				Console.WriteLine(item.Quantity);

			
		}
	}
	public void Create()
	{
		using (IDbConnection db = new SqlConnection(sb.ConnectionString))
		{
			db.Open();
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
			int result = db.Execute(query); // execute

			string message = result > 0 ? "Insert successful." : "Insert failed.";
			Console.WriteLine(message);


		}
	}

	public void Update()
	{
		using (IDbConnection db = new SqlConnection(sb.ConnectionString))
		{
			db.Open();
			string query = @"UPDATE [dbo].[Tbl3_product]
			   SET [Price] = 1200
			 WHERE ProductId = 5";
			int result = db.Execute(query); // execute
			string message = result > 0 ? "Update successful." : "Update failed.";
			Console.WriteLine(message);
		}
	}
	public void Delete()
	{
		using (IDbConnection db = new SqlConnection(sb.ConnectionString))
		{
			db.Open();
			string query = @"DELETE FROM [dbo].[Tbl3_product]
			 WHERE ProductId = 5";
			int result = db.Execute(query); // execute
			string message = result > 0 ? "Delete successful." : "Delete failed.";
			Console.WriteLine(message);
		}
	}
	
}
