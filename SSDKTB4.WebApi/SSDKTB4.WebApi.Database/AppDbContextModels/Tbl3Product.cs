using System;
using System.Collections.Generic;

namespace SSDKTB4.WebApi.Database.AppDbContextModels;

public partial class Tbl3Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }
}
