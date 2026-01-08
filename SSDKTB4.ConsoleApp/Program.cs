namespace SSDKTB4.ConsoleApp
{
    internal class Program
    {
		static List<Product> Products = new List<Product>()
		{
			new Product(1, "Apple", 2000, 10),
			new Product(2, "Banana", 3000, 20),
			new Product(3, "Orange", 4000, 30),

		};
		static void Main(string[] args)
        {
		Start:
			Console.WriteLine("---Mini-POS---");

			Console.WriteLine("1. Add a Product");
			Console.WriteLine("2. Product Lists");
			Console.WriteLine("3. Edit Product");
			Console.WriteLine("4. Delete Product");
			Console.WriteLine("5. Exit");
			Console.Write("Please select an option: ");
			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					AddProduct();
					goto Start;
				case 2:
					Console.WriteLine("Product Lists selected.");
					GetProducts();
					goto Start;
				case 3:
					EditProduct();
					goto Start;
				case 4:
					DeleteProduct();
					goto Start;
				case 5:
				default:
					
					break;
			}


		}

        private static void DeleteProduct()
        {
           Console.Write("Please enter product id to delete:");
			int id = Convert.ToInt32(Console.ReadLine());
			Console.Write("Are you sure to delete this product? (y/n):");
			string confirm = Console.ReadLine();
			if (confirm.ToUpper() != "Y")
			{
				return;
			}
			else
			{
				var product = Products.Where(p => p.Id == id).FirstOrDefault();
				if (product is null)
				{
					Console.WriteLine("Product not found.");
					return;
				}
				else
				{
					Products.Remove(product);
					Console.WriteLine("Product deleted successfully.");
				}
			}
		}

        private static void EditProduct()
        {
			Console.Write("Please enter product id to edit:");
			int id = Convert.ToInt32(Console.ReadLine());
			var product = Products.Where(p => p.Id == id).FirstOrDefault();
			if (product is null)
			{
				Console.WriteLine("Product not found.");
				return;
			}
			else
			{
				Console.WriteLine($"id -{product.Id}  / N - {product.Name} / P - {product.Price} / Q - {product.Quantity}");
				Console.WriteLine("----------------");
				Console.Write("Please enter new product name:");
				string name = Console.ReadLine();
				if (string.IsNullOrEmpty(name))
				{
					name = product.Name;
				}

				Console.Write("Please enter new product price:");
				string strPrice = Console.ReadLine();
				decimal price=0;
				if (string.IsNullOrEmpty(strPrice)) {
					price = product.Price;
				}else
				{
					price = Convert.ToDecimal(strPrice);
				}


				Console.Write("Please enter new product quantity:");
				string strQuantity = Console.ReadLine();
				int quantity=0;
				if(string.IsNullOrEmpty(strQuantity))
				{
					quantity = product.Quantity;
				}
				else
				{
					quantity = Convert.ToInt32(strQuantity);
				}

				int index = Products.FindIndex(p => p.Id == id);
				Products[index].Name = name;
				Products[index].Price = price;
				Products[index].Quantity = quantity;
				Console.WriteLine("Product updated successfully.");
			}

		}

        private static void GetProducts()
        {
			Console.WriteLine("Product Lists");
			Console.WriteLine($"Product Count : {Products.Count}");
            foreach (var product in Products)
            {
				Console.WriteLine($"id -{product.Id}.  / N - {product.Name} / P - {product.Price} / Q - {product.Quantity}");
				Console.WriteLine("----------------");
            }
        }

        private static void AddProduct()
        {
			Console.Write("Please enter product name:");
			string name = Console.ReadLine();

			Console.Write("Please enter product price:");
			decimal price = Convert.ToDecimal(Console.ReadLine());

			Console.Write("Please enter product quantity:");

			int quantity = Convert.ToInt32(Console.ReadLine());
			int no = Products.Max(x => x.Id)+1;
			Product product = new Product(no, name, price, quantity);
			Products.Add(product);
			Console.WriteLine("Product saves successfully.");
		}
	}

	public class Product
	{
		public Product(int id, string name, decimal price, int quantity)
		{
			Id = id;
			Name = name;
			Price = price;
			Quantity = quantity;
		}
		public int Id { get; set; }

		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
