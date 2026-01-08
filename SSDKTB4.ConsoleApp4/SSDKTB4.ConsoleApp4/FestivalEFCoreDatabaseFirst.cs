using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSDKTB4.ConsoleApp4.Database.AppDbContextModels;

namespace SSDKTB4.ConsoleApp4
{
    internal class FestivalEFCoreDatabaseFirst
	{
        private readonly  AppDbContext _dbcontext;

        public FestivalEFCoreDatabaseFirst()
        {
            _dbcontext = new AppDbContext();
        }

        public void Read()
        {
          List<FestivalManagement> festivals= _dbcontext.FestivalManagements.ToList();
			Console.WriteLine("All Festivals (EF Core- Database First):");
			Console.WriteLine("-----------------------------------");

			if (festivals.Count == 0)
			{
				Console.WriteLine("No festivals found.");
			}

			foreach (FestivalManagement item in festivals)
			{
				string dateStr = item.FestivalDateTime.ToString("yyyy-MM-dd HH:mm");
				Console.WriteLine($"No: {item.FestivalId} / N: {item.FestivalName} / D: {dateStr} / L: {item.Location} / P: {item.TicketPrice:n0} / Q: {item.TicketQuantity}");
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

			FestivalManagement entity = new FestivalManagement()
			{
				FestivalName = name,
				FestivalDateTime = dateTime,
				Location = location,
				TicketPrice = price,
				TicketQuantity = quantity,
				CreatedDateTime = DateTime.Now
			};

			_dbcontext.FestivalManagements.Add(entity);
			int result = _dbcontext.SaveChanges();
			string message = result > 0 ? "Festival created successfully." : "Insert failed.";
			Console.WriteLine(message);

		}
		public void Update()
		{
			Console.Write("Please enter festival id to update: ");
			int id = Convert.ToInt32(Console.ReadLine());

			var item = _dbcontext.FestivalManagements.Where(x=> x.FestivalId == id).FirstOrDefault();
			if (item is null)
			{
				Console.WriteLine("Festival Not Found!");
				return;
			}

			Console.WriteLine("-----------------------------------------");
			Console.WriteLine($"Current Name    : {item.FestivalName}");
			Console.WriteLine($"Current Date    : {item.FestivalDateTime:yyyy-MM-dd HH:mm}");
			Console.WriteLine($"Current Location: {item.Location}");
			Console.WriteLine($"Current Price   : {item.TicketPrice:n0}");
			Console.WriteLine($"Current Qty     : {item.TicketQuantity}");
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("Enter new values (or press ENTER to keep current value):");
			Console.Write("Enter new Name (leave empty to keep current): ");
			string name = Console.ReadLine();
			if (!string.IsNullOrEmpty(name))
			{
				item.FestivalName = name;
			}

			Console.Write("Enter new Date: ");
			string dateStr = Console.ReadLine();
			DateTime datetime;
			if (string.IsNullOrEmpty(dateStr))
			{
				item.FestivalDateTime = Convert.ToDateTime(item.FestivalDateTime);
			} else
			{
				item.FestivalDateTime = Convert.ToDateTime(dateStr);
			}

			Console.Write("Enter new Location: ");
			string location = Console.ReadLine();
			if (!string.IsNullOrEmpty(location))
			{
				item.Location = location;
			}

			Console.Write("Enter new Price: ");
			string priceStr = Console.ReadLine();
			if (!string.IsNullOrEmpty(priceStr))
			{
				item.TicketPrice = Convert.ToDecimal(priceStr);
			}

			Console.Write("Enter new Quantity: ");
			string qtyStr = Console.ReadLine();
			if (!string.IsNullOrEmpty(qtyStr))
			{
				item.TicketQuantity = Convert.ToInt32(qtyStr);
			}

			item.ModifiedDateTime = DateTime.Now;

			int result = _dbcontext.SaveChanges();
			string message = result > 0 ? "Festival Updated Successfully!" : "No changes saved.";
			Console.WriteLine(message);
		}
		public void Delete()
		{
			Console.Write("Please enter festival id to delete: ");
			int id = Convert.ToInt32(Console.ReadLine());

			var item = _dbcontext.FestivalManagements.FirstOrDefault(x => x.FestivalId == id);

			if (item is null)
			{
				Console.WriteLine("Festival Not Found!");
				return;
			}

			Console.Write("Are you sure? (Y/N)?");
			if (Console.ReadLine()?.ToUpper() != "Y") return;

			
			_dbcontext.FestivalManagements.Remove(item);

			int result = _dbcontext.SaveChanges();
			string message = result > 0 ? "Delete successful." : "Delete failed.";
			Console.WriteLine(message);
		}
		
	}
}
