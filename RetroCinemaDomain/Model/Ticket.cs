using System;
using System.Collections.Generic;

namespace RetroCinemaDomain.Model;

public partial class Ticket
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public string? UserId { get; set; }

    public int? RowNumber { get; set; }

    public int? SeatNumber { get; set; }

    public decimal? PricePaid { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual Session? Session { get; set; }
}
