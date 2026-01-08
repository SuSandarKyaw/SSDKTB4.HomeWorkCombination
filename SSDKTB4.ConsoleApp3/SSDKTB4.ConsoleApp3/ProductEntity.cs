using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSDKTB4.ConsoleApp3
{
    [Table("Tbl3_product")]
	internal class ProductEntity
	{
		[Key]
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }

		public bool? IsDelete { get; set; }
		public DateTime CreatedDateTime { get; set; }
		public DateTime? ModifiedDateTime { get; set; }
	}
}
