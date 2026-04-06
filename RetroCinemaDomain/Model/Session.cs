using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetroCinemaDomain.Model;

public partial class Session : Entity
{

    public int? MovieId { get; set; }

    public int? HallId { get; set; }

    [Display(Name = "Час початку")]
    public DateTime StartTime { get; set; }

    [Display(Name = "Базова ціна")]
    public decimal BasePrice { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

}
