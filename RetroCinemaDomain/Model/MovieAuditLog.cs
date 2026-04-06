using System;
using System.Collections.Generic;

namespace RetroCinemaDomain.Model;

public partial class MovieAuditLog : Entity
{

    public int? MovieId { get; set; }

    public string? ChangeType { get; set; }

    public string? ColumnName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? ChangedByUser { get; set; }

    public DateTime? ChangedAt { get; set; }

    public virtual Movie? Movie { get; set; }
}
