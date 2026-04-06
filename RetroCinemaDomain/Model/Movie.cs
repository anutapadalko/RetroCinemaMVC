using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetroCinemaDomain.Model
{
    public partial class Movie : Entity
    {
        public Movie()
        {
            Genres = new HashSet<Genre>();
            MovieAuditLogs = new HashSet<MovieAuditLog>();
            Sessions = new HashSet<Session>();
        }

        [Required(ErrorMessage = "Поле 'Назва' не повинно бути порожнім")]
        [Display(Name = "Назва фільму")]
        public string Title { get; set; } = null!;

        [Display(Name = "Опис")]
        public string? Description { get; set; }

        [Display(Name = "Рік випуску")]
        public int? ReleaseYear { get; set; }

        [Display(Name = "Режисер")]
        public string? Director { get; set; }

        [Display(Name = "Тривалість (хв)")]
        public int? DurationMinutes { get; set; }

        [Display(Name = "Посилання на постер")]
        public string? PosterUrl { get; set; }

        [Display(Name = "Видалено")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "Створено")]
        public DateTime? CreatedAt { get; set; }

        [Display(Name = "Оновлено")]
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<MovieAuditLog> MovieAuditLogs { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}