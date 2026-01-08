// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SSDKTB4.WebApi.ConsoleApp;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Hello, World!");

Console.WriteLine("Consuming Web API from Console App...");
Console.ReadLine();

ProductApiService productApiService = new ProductApiService();
Start:
Console.WriteLine("");
Console.WriteLine("---------------------------------------------");
Console.WriteLine("Please select an option:");
Console.WriteLine("1. Get All Products (Read)");
Console.WriteLine("2. Create Product");
Console.WriteLine("3. Update Product (Put)");
Console.WriteLine("4. Patch Product");
Console.WriteLine("5. Delete Product");
Console.WriteLine("6. Exit");
Console.WriteLine("---------------------------------------------");
Console.Write("Enter your choice: ");

string? choice = Console.ReadLine();

switch (choice)
{
	case "1":
		await productApiService.ReadAsync();
		Console.ReadLine();
		goto Start;
	case "2":
		await productApiService.CreateAsync();
		Console.ReadLine();
		goto Start;
	case "3":
		await productApiService.UpdateAsync();
		Console.ReadLine();
		goto Start;
	case "4":
		await productApiService.PatchAsync();
		Console.ReadLine();
		goto Start;
	case "5":
		await productApiService.DeleteAsync();
		Console.ReadLine();
		goto Start;
	case "6":
		Console.WriteLine("Exiting application...");
		break;
	default:
		Console.WriteLine("Invalid option. Please try again.");
		break;

}


public class ProductCreateRequestModel
{
	public string ProductName { get; set; } = null!;

	public decimal Price { get; set; }

	public int Quantity { get; set; }
}
public class ProductUpdateRequestModel
{
	public string ProductName { get; set; } = null!;

	public decimal Price { get; set; }

	public int Quantity { get; set; }
}

public class ProductPatchRequestModel
{
	public string? ProductName { get; set; } = null!;

	public decimal? Price { get; set; }

	public int? Quantity { get; set; }
}

