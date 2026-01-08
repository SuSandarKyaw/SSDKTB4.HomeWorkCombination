using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSDKTB4.ConsoleApp4
{
    [Table("festival_management")]
	public class FestivalEntity
	{
		[Key]
		public int FestivalId { get; set; }
		public string FestivalName { get; set; }
		public DateTime FestivalDateTime { get; set; }
		public string Location { get; set; }
		public decimal TicketPrice { get; set; }
		public int TicketQuantity { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public DateTime? ModifiedDateTime { get; set; }
	}
}
