using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSDKTB4.ConsoleApp3.Database.AppDbContextModels;
namespace SSDKTB4.ConsoleApp3
{
    internal class EFCore2Sample
    {
		private readonly AppDbContext _db;

		public EFCore2Sample()
		{
			_db = new AppDbContext();
		}

		public void Read()
        {
			List<Tbl3Product> lst=_db.Tbl3Products.ToList(); // select * from Products
			foreach (Tbl3Product item in lst)
			{
				Console.WriteLine($"{item.ProductId}\t{item.ProductName}\t{item.Price}\t{item.Quantity}");
			}
			}
		public void Create()
		{
			Tbl3Product entity = new Tbl3Product() { 
				ProductName="Strawberry",
				Price=3000,
				Quantity=30,
				CreatedDateTime=DateTime.Now,
				IsDelete= false
			};
			_db.Tbl3Products.Add(entity);
		    int result = _db.SaveChanges();

			string message = result > 0 ? "Insert successful" : "Insert failed";
			Console.WriteLine(message);


		}
		public void Edit()
		{
			var product = _db.Tbl3Products.Where(x=> x.ProductId == 7).FirstOrDefault();
			if (product is null) return;
			Console.WriteLine($"{product.ProductId}\t{product.ProductName}\t{product.Price}\t{product.Quantity}");

		}
		public void Update()
		{
			var product = _db.Tbl3Products.Where(x => x.ProductId == 7).FirstOrDefault();
			if (product is null) return;
			product.ProductName = "Banana";
			product.Price = 5000;
			int result = _db.SaveChanges();
			string message = result > 0 ? "Update successful" : "Update failed";
			Console.WriteLine(message);
		}
		public void Delete()
		{
			var product = _db.Tbl3Products.Where(x => x.ProductId == 6).FirstOrDefault();
			if (product is null) return;
			product.IsDelete = true;//soft delete
			//_db.Products.Remove(product); // hard delete
			int result = _db.SaveChanges();
			string message = result > 0 ? "Delete successful" : "Delete failed";
			Console.WriteLine(message);

		}
	}
}
