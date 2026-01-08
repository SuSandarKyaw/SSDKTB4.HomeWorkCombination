using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using static System.Net.Mime.MediaTypeNames;

namespace SSDKTB4.WebApi.ConsoleApp
{
    internal class ProductApiService
    {
	 private readonly string domainUrl;
		private readonly string productEndpoint ;

        public ProductApiService()
        {
            domainUrl = "https://localhost:7087";

			productEndpoint = $"{domainUrl}/api/product";

		}
		public async Task ReadAsync()
        {
            Console.Write("Enter page number:");
            string? pageNoInput = Console.ReadLine() ?? "1";
            Console.Write("Enter page size:");
            string? pageSizeInput = Console.ReadLine() ?? "10";

			//HttpClient httpClient = new HttpClient();
			//HttpResponseMessage respone = await httpClient.GetAsync($"{productEndpoint}/{pageNoInput}/{pageSizeInput}");

			//if (respone.IsSuccessStatusCode)
			//{
			//    string jsonData = await respone.Content.ReadAsStringAsync();
			//    Console.WriteLine(jsonData);
			//}
			string endpoint = $"{productEndpoint}/{pageNoInput}/{pageSizeInput}";
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(endpoint);
            RestResponse response = await client.ExecuteAsync(request);
            //RestResponse response = await client.GetAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonData = response.Content!;
				Console.WriteLine(jsonData);
			}

        }

		public async Task CreateAsync()
        {
			Console.Write("Enter Product Name : ");
			string name = Console.ReadLine();
			Console.Write("Enter Price : ");
			decimal price = Convert.ToDecimal(Console.ReadLine());
			Console.Write("Enter Quantity :");
			int quantity = Convert.ToInt32(Console.ReadLine());

			ProductCreateRequestModel requestModel = new ProductCreateRequestModel
			{
				ProductName = name,
				Price = price,
				Quantity = quantity,
			};

			//string json = JsonConvert.SerializeObject(requestModel);
			//var content = new StringContent(json, Encoding.UTF8, Application.Json);

			//HttpClient httpClient = new HttpClient();
			//HttpResponseMessage respone = await httpClient.PostAsync(productEndpoint, content);

			//if (respone.IsSuccessStatusCode)
			//{
			//	string jsonData = await respone.Content.ReadAsStringAsync();
			//	Console.WriteLine(jsonData);

			//}
			string endpoint = productEndpoint;
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(endpoint,Method.Post);
			request.AddJsonBody(requestModel);
			//RestResponse response = await client.PostAsync(request);
			RestResponse response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonData = response.Content!;
				Console.WriteLine(jsonData);

			}
		}

		public async Task UpdateAsync() {
			Console.Write("Enter product id to edit: ");
			int id = Convert.ToInt32(Console.ReadLine());

			Console.Write("Enter new product name: ");
			string newName = Console.ReadLine();

			Console.Write("Enter new product price: ");
			decimal newPrice = Convert.ToDecimal(Console.ReadLine());

			Console.Write("Enter new product quantity:");
			int newQuantity = Convert.ToInt32(Console.ReadLine());

			ProductUpdateRequestModel requestModel = new ProductUpdateRequestModel
			{
				ProductName = newName,
				Price = newPrice,
				Quantity = newQuantity,

			};
			//string json = JsonConvert.SerializeObject(requestModel);
			//var content = new StringContent(json, Encoding.UTF8, Application.Json);

			//HttpClient httpClient = new HttpClient();
			//HttpResponseMessage respone = await httpClient.PutAsync($"{productEndpoint}/{id}", content);

			//if (respone.IsSuccessStatusCode)
			//{
			//	string data = await respone.Content.ReadAsStringAsync();
			//	Console.WriteLine("Update Success: " + data);
			//}
			//else
			//{
			//	Console.WriteLine("Update Failed: " + respone.StatusCode);
			//}
			string endpoint = $"{productEndpoint}/{id}";
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(endpoint, Method.Put);
			request.AddJsonBody(requestModel);
			RestResponse response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content!;
				Console.WriteLine("Update Success: " + data);
			}
			else
			{
				Console.WriteLine("Update Failed: " + response.StatusCode);
			}
		}

		public async Task PatchAsync()
        {
			Console.Write("Enter id of product to patch: ");
			int patchId = Convert.ToInt32(Console.ReadLine());

			Console.Write("Enter new product name (leave empty to skip): ");
			string? patchName = Console.ReadLine();

			Console.Write("Enter new price (leave empty to skip): ");
			string? patchPriceInput = Console.ReadLine();
			decimal? patchPrice = string.IsNullOrEmpty(patchPriceInput) ? null : Convert.ToDecimal(patchPriceInput);

			Console.Write("Enter new quantity (leave empty to skip): ");
			string? patchQtyInput = Console.ReadLine();
			int? patchQuantity = string.IsNullOrEmpty(patchQtyInput) ? null : Convert.ToInt32(patchQtyInput);

			ProductPatchRequestModel patchRequestModel = new ProductPatchRequestModel
			{
				ProductName = string.IsNullOrEmpty(patchName) ? null : patchName,
				Price = patchPrice,
				Quantity = patchQuantity
			};

			//string jsonPatch = JsonConvert.SerializeObject(patchRequestModel);
			//var contentPatch = new StringContent(jsonPatch, Encoding.UTF8, "application/json");

			//HttpClient clientPatch = new HttpClient();
			//HttpResponseMessage responsePatch = await clientPatch.PatchAsync($"{productEndpoint}/{patchId}", contentPatch);

			//if (responsePatch.IsSuccessStatusCode)
			//{
			//	string data = await responsePatch.Content.ReadAsStringAsync();
			//	Console.WriteLine("Patch Success: " + data);
			//}
			//else
			//{
			//	Console.WriteLine("Patch Failed: " + responsePatch.StatusCode);
			//}
			string endpoint = $"{productEndpoint}/{patchId}";
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(endpoint, Method.Patch);
			request.AddJsonBody(patchRequestModel);
			RestResponse response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content!;
				Console.WriteLine("Patch Success: " + data);
			}
			else
			{
				Console.WriteLine("Patch Failed: " + response.StatusCode);
			}
		}

		public async Task DeleteAsync()
        {
			Console.Write("Enter id of product to delete: ");
			int deleteId = Convert.ToInt32(Console.ReadLine());

			//HttpClient httpClient = new HttpClient();
			//HttpResponseMessage respone = await httpClient.DeleteAsync($"{productEndpoint}/{deleteId}");

			//if (respone.IsSuccessStatusCode)
			//{
			//	string jsonData = await respone.Content.ReadAsStringAsync();
			//	Console.WriteLine(jsonData);
			//}


		    string endpoint = $"{productEndpoint}/{deleteId}";
			RestClient client = new RestClient();
			RestRequest request = new RestRequest(endpoint, Method.Delete);
			RestResponse response = await client.ExecuteAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string jsonData = response.Content!;
				Console.WriteLine(jsonData);
			}
		}
	}
}
