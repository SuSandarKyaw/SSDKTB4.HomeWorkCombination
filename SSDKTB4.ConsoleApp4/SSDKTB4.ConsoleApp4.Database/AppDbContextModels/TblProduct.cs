using System;
using System.Collections.Generic;

namespace SSDKTB4.ConsoleApp4.Database.AppDbContextModels;

public partial class TblProduct
{
    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedDateTime { get; set; }
}
