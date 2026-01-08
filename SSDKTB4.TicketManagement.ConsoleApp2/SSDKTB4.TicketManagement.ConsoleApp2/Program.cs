namespace SSDKTB4.TicketManagement.ConsoleApp2
{
    internal class Program
    {
        static List<Festival> Festivals = new List<Festival>()
        {
            new Festival(1, "Huntrix", "2026-07-15 18:00", "Central Park", 50000, 100),
            new Festival(2, "EXO", "2026-08-20 20:00", "Madison Square Garden", 70000, 300),
            new Festival(3, "Saja Boy", "2026-08-22 20:00", "Madison Square Garden", 60000, 200)
		};
		static void Main(string[] args)
        {
        Start:
            Console.WriteLine("Festival Ticket Management System");
            Console.WriteLine("1.Add Festival");
            Console.WriteLine("2.View Festivals");
            Console.WriteLine("3.Edit Festival");
            Console.WriteLine("4.Delete Festival");
            Console.WriteLine("5.Exit");
            Console.Write("Select an option: ");
            int option =Convert.ToInt32( Console.ReadLine());
            switch (option)
            {
                case 1:
                    AddFestival();
                    goto Start;
                      case 2:
                    ViewFestivals();
					goto Start;
				case 3:
                    EditFestival();
					goto Start;
				case 4:
                    DeleteFestival();
					goto Start;
				case 5:
                default:
                    break;
			}
		}

        private static void DeleteFestival()
        {
            Console.Write("Pleae enter festival id to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Are you sure you want to delete this festival? (Y/N)");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() != "Y")
            {
                return;
            }
            else
            {
                var festival = Festivals.Where(f => f.Id == id).FirstOrDefault();
                if(festival is null)
                {
                    Console.WriteLine("Festival Not Found!");
                    return;
                }
                else
                {
                    Festivals.Remove(festival);
                    Console.WriteLine("Festival deleted successfully!");
				}
			}
        }
        private static void EditFestival()
        {
			Console.Write("Please enter festival id to edit: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var festival = Festivals.Where(f => f.Id == id).FirstOrDefault();
            if(festival is null)
            {
                Console.WriteLine("Festival Not Found!");
                return;
            }
            else
            {
				Console.WriteLine($"No: {festival.Id} / N: {festival.Name} / D: {festival.DateTime} / L: {festival.Location} / P: {festival.TicketPrice} / Q: {festival.TicketQuantity}");
                Console.WriteLine("-----------");
				Console.Write("Please enter new festival name: ");
				string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    name = festival.Name;
                }

				Console.Write("Please enter new festival date & time: ");
				string dateTime = Console.ReadLine();
                if (string.IsNullOrEmpty(dateTime))
                {
                    dateTime = festival.DateTime;
				}

                Console.Write("Please enter new festival location: ");
				string location = Console.ReadLine();
                if(string.IsNullOrEmpty(location))
                {
                    location = festival.Location;
				}

				Console.Write("Please enter new ticket price: ");
                string priceStr = Console.ReadLine();
                decimal price=0;
				if (string.IsNullOrEmpty(priceStr))
                {
                   price = festival.TicketPrice;
                }
                else
                {
                    price = Convert.ToDecimal(priceStr);
                }


                 Console.Write("Please enter new ticket quantity: ");
                string quantityStr = Console.ReadLine();
                int quantity=0;
                if (string.IsNullOrEmpty(quantityStr)) {
                    quantity = festival.TicketQuantity;
                }
                else
                {
                    quantity = Convert.ToInt32(quantityStr);
				}

                int index = Festivals.FindIndex(f=> f.Id == id);
                Festivals[index].Name = name;
                Festivals[index].DateTime = dateTime;
                Festivals[index].Location = location;
                Festivals[index].TicketPrice = price;
                Festivals[index].TicketQuantity = quantity;
                Console.WriteLine("Festival updated successfully!");
			}
            
		}

        private static void ViewFestivals()
        {
            Console.WriteLine("All Festivals:");
            Console.WriteLine($"Festivals Count: {Festivals.Count}");
            Console.WriteLine("-----------------------------------");
            foreach(var festival in Festivals)
            {
                Console.WriteLine($"No: {festival.Id} / N: {festival.Name} / D: {festival.DateTime} / L: {festival.Location} / P: {festival.TicketPrice} / Q: {festival.TicketQuantity}");
			}
        }

        private static void AddFestival()
        {
			Console.Write("Please enter festival name: ");
			string name = Console.ReadLine();

			Console.Write("Please enter festival date & time: ");
			string dateTime = Console.ReadLine();

			Console.Write("Please enter festival location: ");
			string location = Console.ReadLine();

			Console.Write("Please enter ticket price: ");
			decimal price = Convert.ToDecimal(Console.ReadLine());

			Console.Write("Please enter ticket quantity: ");
			int quantity = Convert.ToInt32(Console.ReadLine());

            int newId = Festivals.Max(f => f.Id) + 1;
            Festival festival = new Festival (newId,name, dateTime, location, price, quantity);
            Festivals.Add(festival);
            Console.WriteLine("Festival added successfully!");
		}
    }

    public class Festival
    {
        public Festival(int id, string name, string dateTime, string location, decimal ticketPrice, int ticketQuantity)
        {
            Id = id;
            Name = name;
            DateTime = dateTime;
            Location = location;
            TicketPrice = ticketPrice;
            TicketQuantity = ticketQuantity;
		}
		public int Id { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string Location { get; set; }
        public decimal TicketPrice { get; set; }
        public int TicketQuantity { get; set; }
	}
}
