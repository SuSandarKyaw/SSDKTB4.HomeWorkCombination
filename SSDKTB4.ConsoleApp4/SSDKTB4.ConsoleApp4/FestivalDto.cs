using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDKTB4.ConsoleApp4
{
	internal class FestivalDto
	{
		public int FestivalId { get; set; }
		public string FestivalName { get; set; }
		public DateTime FestivalDateTime { get; set; }
		public string Location { get; set; }
		public decimal TicketPrice { get; set; }
		public int TicketQuantity { get; set; }
	}
}
