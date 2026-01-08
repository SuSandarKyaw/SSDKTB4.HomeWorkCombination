using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDKTB4.ConsoleApp3
{
    internal class EFCoreSample
    {
		private readonly AppDbModelFirstContext _db;

		public EFCoreSample()
		{
			_db = new AppDbModelFirstContext();
		}

		public void Read()
        {
			List<ProductEntity> lst=_db.Products.ToList(); // select * from Products
			foreach (ProductEntity item in lst)
			{
				Console.WriteLine($"{item.ProductId}\t{item.ProductName}\t{item.Price}\t{item.Quantity}");
			}
			}
		public void Create()
		{
			ProductEntity entity = new ProductEntity() { 
				ProductName="Strawberry",
				Price=3000,
				Quantity=30,
				CreatedDateTime=DateTime.Now,
				IsDelete= false
			};
			_db.Products.Add(entity);
		    int result = _db.SaveChanges();

			string message = result > 0 ? "Insert successful" : "Insert failed";
			Console.WriteLine(message);


		}
		public void Edit()
		{
			var product = _db.Products.Where(x=> x.ProductId == 7).FirstOrDefault();
			if (product is null) return;
			Console.WriteLine($"{product.ProductId}\t{product.ProductName}\t{product.Price}\t{product.Quantity}");

		}
		public void Update()
		{
			var product = _db.Products.Where(x => x.ProductId == 7).FirstOrDefault();
			if (product is null) return;
			product.ProductName = "Banana";
			product.Price = 5000;
			int result = _db.SaveChanges();
			string message = result > 0 ? "Update successful" : "Update failed";
			Console.WriteLine(message);
		}
		public void Delete()
		{
			var product = _db.Products.Where(x => x.ProductId == 6).FirstOrDefault();
			if (product is null) return;
			product.IsDelete = true;//soft delete
			//_db.Products.Remove(product); // hard delete
			int result = _db.SaveChanges();
			string message = result > 0 ? "Delete successful" : "Delete failed";
			Console.WriteLine(message);

		}
	}
}
