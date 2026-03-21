using System;
using System.Collections.Generic;

namespace RetroCinemaDomain.Model;

public partial class Session
{
    public int Id { get; set; }

    public int? MovieId { get; set; }

    public int? HallId { get; set; }

    public DateTime StartTime { get; set; }

    public decimal BasePrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
