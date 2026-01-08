namespace SSDKTB4.ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
			//FestivalAdoDotNet festivalAdoDotNet = new FestivalAdoDotNet();
			//FestivalDapper festivalDapper = new FestivalDapper();
			//FestivalEFCore festivalEfCore = new FestivalEFCore();
			FestivalEFCoreDatabaseFirst festival2Efcore = new FestivalEFCoreDatabaseFirst();
		 Start:
			Console.WriteLine("Festival Ticket Management System");
			Console.WriteLine("1. Add Festival");
			Console.WriteLine("2. View Festivals");
			Console.WriteLine("3. Edit Festival");
			Console.WriteLine("4. Delete Festival");
			Console.WriteLine("5. Exit");
			Console.Write("Select an option: ");

			int option = Convert.ToInt32(Console.ReadLine());
			switch (option)
			{
				case 1:
					//festivalAdoDotNet.Create();
					//festivalEfCore.Create();
					festival2Efcore.Create();
					goto Start;
				case 2:
					festival2Efcore.Read();

					goto Start;
				case 3:
					//festivalDapper.Update();
					festival2Efcore.Update();
					goto Start;
				case 4:
					//festivalDapper.Delete();
					festival2Efcore.Delete();
					goto Start;
				case 5:
					default:
					break;

			}

		}

    }
}
