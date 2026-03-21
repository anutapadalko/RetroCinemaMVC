using System;
using System.Collections.Generic;

namespace RetroCinemaDomain.Model;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Director { get; set; }

    public int? DurationMinutes { get; set; }

    public string? PosterUrl { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<MovieAuditLog> MovieAuditLogs { get; set; } = new List<MovieAuditLog>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
