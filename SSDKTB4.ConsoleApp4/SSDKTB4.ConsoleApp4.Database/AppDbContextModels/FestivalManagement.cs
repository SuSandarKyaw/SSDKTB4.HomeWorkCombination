using System;
using System.Collections.Generic;

namespace SSDKTB4.ConsoleApp4.Database.AppDbContextModels;

public partial class FestivalManagement
{
    public int FestivalId { get; set; }

    public string FestivalName { get; set; } = null!;

    public DateTime FestivalDateTime { get; set; }

    public string Location { get; set; } = null!;

    public decimal TicketPrice { get; set; }

    public int TicketQuantity { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? ModifiedDateTime { get; set; }
}
